using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using StlAuction.Types;

namespace StlAuction.Data
{
    public class BannedBidderManager
    {
        private readonly IRedisTypedClient<BannedBidder> _redis;
        private const string _bannedbidderKey = "urn:bannedbidders";

        public BannedBidderManager()
        {
            var redisClient = new RedisClient();
            _redis = redisClient.As<BannedBidder>();
        }

        public long Save(BannedBidder bannedBidder)
        {
            var Id = GetMaxId() + 1;
            bannedBidder.Id = Id;
            var key = string.Format("{0}:{1}", _bannedbidderKey, Id);
            _redis.SetValue(key, bannedBidder);
            return Id;
        }

        public long GetMaxId()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_bannedbidderKey)).ToList();
            
            List<long> longKeys = new List<long>();
            foreach (var key in listOfKeys)
            {
                var longKey = long.Parse(key.Replace(_bannedbidderKey + ":", string.Empty));
                longKeys.Add(longKey);
            }

            if (longKeys.Count == 0)
            {
                return 0;
            }

            return longKeys.Max();
        }
    

        public int GetNumberOfBannedBidders()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_bannedbidderKey)).ToList();
            return listOfKeys.Count;
        }

        public List<BannedBidder> GetAllBannedBidders()
        {
            var listOfKeys =  _redis.GetAllKeys().Where(k => k.StartsWith(_bannedbidderKey)).ToList();
            return _redis.GetValues(listOfKeys);
        }

        public void RemoveBannedBidder(BannedBidder bannedBidder)
        {
            var productKey = string.Format("{0}:{1}", _bannedbidderKey,  bannedBidder.Id);
            var redisClient = new RedisClient();
            redisClient.Remove(productKey);
        }

        public void RemoveAllBannedBidders()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_bannedbidderKey)).ToList();
            var redisClient = new RedisClient();
            foreach (var key in listOfKeys)
            {
                redisClient.Remove(key);
            }
            
        }

        public BannedBidder GetById(long id)
        {
            return _redis.GetValue(string.Format("{0}:{1}", _bannedbidderKey, id));
        }

        public void Update(BannedBidder bannedBidder)
        {
            _redis.SetValue(string.Format("{0}:{1}", _bannedbidderKey, bannedBidder.Id), bannedBidder);
        }
    }
}
