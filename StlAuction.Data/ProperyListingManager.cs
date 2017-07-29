using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using StlAuction.Types;

namespace StlAuction.Data
{
    public class PropertyListingManager
    {
        private readonly IRedisTypedClient<PropertyListing> _redis;
        private const string _propertyListingKey = "urn:propertylisting";

        public PropertyListingManager()
        {
            var redisClient = new RedisClient();
            _redis = redisClient.As<PropertyListing>();
        }

        public long Save(PropertyListing PropertyListing)
        {
            var Id = GetMaxId() + 1;
            PropertyListing.Id = Id;
            var key = string.Format("{0}:{1}", _propertyListingKey, Id);
            _redis.SetValue(key, PropertyListing);
            return Id;
        }

        public long GetMaxId()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_propertyListingKey)).ToList();

            List<long> longKeys = new List<long>();
            foreach (var key in listOfKeys)
            {
                var longKey = long.Parse(key.Replace(_propertyListingKey + ":", string.Empty));
                longKeys.Add(longKey);
            }

            if (longKeys.Count == 0)
            {
                return 0;
            }

            return longKeys.Max();
        }

        public int GetNumberOfPropertyListings()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_propertyListingKey)).ToList();
            return listOfKeys.Count;
        }

        public List<PropertyListing> GetAllPropertyListings()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_propertyListingKey)).ToList();
            return _redis.GetValues(listOfKeys);
        }

        public void RemovePropertyListing(PropertyListing PropertyListing)
        {
            var productKey = string.Format("{0}:{1}", _propertyListingKey, PropertyListing.Id);
            var redisClient = new RedisClient();
            redisClient.Remove(productKey);
        }

        public void RemoveAllPropertyListings()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_propertyListingKey)).ToList();
            var redisClient = new RedisClient();
            foreach (var key in listOfKeys)
            {
                redisClient.Remove(key);
            }
        }

        public PropertyListing GetById(long id)
        {
            return _redis.GetValue(string.Format("{0}:{1}", _propertyListingKey, id));
        }

        public void Update(PropertyListing PropertyListing)
        {
            _redis.SetValue(string.Format("{0}:{1}", _propertyListingKey, PropertyListing.Id), PropertyListing);
        }

    }
}
