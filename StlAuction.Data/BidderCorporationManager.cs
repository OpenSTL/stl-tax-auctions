using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using StlAuction.Types;

namespace StlAuction.Data
{
    public class BidderCorporationManager
    {
        private readonly IRedisTypedClient<BidderCorporation> _redis;
        private const string _bidderCorporationKey = "urn:biddercorporation";

        public BidderCorporationManager()
        {
            var redisClient = new RedisClient();
            _redis = redisClient.As<BidderCorporation>();
        }

        public long Save(BidderCorporation bidderCorporation)
        {
            var Id = GetMaxId() + 1;
            bidderCorporation.Id = Id;
            var key = string.Format("{0}:{1}", _bidderCorporationKey, Id);
            _redis.SetValue(key, bidderCorporation);
            return Id;
        }

        public long GetMaxId()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_bidderCorporationKey)).ToList();
            
            List<long> longKeys = new List<long>();
            foreach (var key in listOfKeys)
            {
                var longKey = long.Parse(key.Replace(_bidderCorporationKey + ":", string.Empty));
                longKeys.Add(longKey);
            }

            if (longKeys.Count == 0)
            {
                return 0;
            }

            return longKeys.Max();
        }
    

        public int GetNumberOfBidderCorporations()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_bidderCorporationKey)).ToList();
            return listOfKeys.Count;
        }

        public List<BidderCorporation> GetAllBidderCorporations()
        {
            var listOfKeys =  _redis.GetAllKeys().Where(k => k.StartsWith(_bidderCorporationKey)).ToList();
            return _redis.GetValues(listOfKeys);
        }

        public void RemoveBidderCorporation(BidderCorporation bidderCorporation)
        {
            var bidderCorporationKey = string.Format("{0}:{1}", _bidderCorporationKey,  bidderCorporation.Id);
            var redisClient = new RedisClient();
            redisClient.Remove(bidderCorporationKey);
        }

        public void RemoveAllBidderCorporations()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_bidderCorporationKey)).ToList();
            var redisClient = new RedisClient();
            foreach (var key in listOfKeys)
            {
                redisClient.Remove(key);
            }
            
        }

        public BidderCorporation GetById(long id)
        {
            return _redis.GetValue(string.Format("{0}:{1}", _bidderCorporationKey, id));
        }

        public void Update(BidderCorporation bidderCorporation)
        {
            _redis.SetValue(string.Format("{0}:{1}", _bidderCorporationKey, bidderCorporation.Id), bidderCorporation);
        }
    }
}
