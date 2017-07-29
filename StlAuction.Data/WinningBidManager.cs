using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using StlAuction.Types;

namespace StlAuction.Data
{
    public class WinningBidManager
    {
        private readonly IRedisTypedClient<PropertyBid> _redis;
        private const string _winningBidKey = "urn:winningbid";

        public WinningBidManager()
        {
            var redisClient = new RedisClient();
            _redis = redisClient.As<PropertyBid>();
        }

        public long Save(PropertyBid propertyBid)
        {
            var Id = GetMaxId() + 1;
            propertyBid.Id = Id;
            var key = string.Format("{0}:{1}", _winningBidKey, Id);
            _redis.SetValue(key, propertyBid);
            return Id;
        }

        public long GetMaxId()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_winningBidKey)).ToList();

            List<long> longKeys = new List<long>();
            foreach (var key in listOfKeys)
            {
                var longKey = long.Parse(key.Replace(_winningBidKey + ":", string.Empty));
                longKeys.Add(longKey);
            }

            if (longKeys.Count == 0)
            {
                return 0;
            }

            return longKeys.Max();
        }

        public int GetNumberOfWinningBids()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_winningBidKey)).ToList();
            return listOfKeys.Count;
        }

        public List<PropertyBid> GetAllWinningBids()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_winningBidKey)).ToList();
            return _redis.GetValues(listOfKeys);
        }

        public void RemoveWinningBid(PropertyBid propertyBid)
        {
            var productKey = string.Format("{0}:{1}", _winningBidKey, propertyBid.Id);
            var redisClient = new RedisClient();
            redisClient.Remove(productKey);
        }

        public void RemoveAllWinningBids()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_winningBidKey)).ToList();
            var redisClient = new RedisClient();
            foreach (var key in listOfKeys)
            {
                redisClient.Remove(key);
            }
        }

        public PropertyBid GetById(long id)
        {
            return _redis.GetValue(string.Format("{0}:{1}", _winningBidKey, id));
        }

        public void Update(PropertyBid propertyBid)
        {
            _redis.SetValue(string.Format("{0}:{1}", _winningBidKey, propertyBid.Id), propertyBid);
        }

    }
}
