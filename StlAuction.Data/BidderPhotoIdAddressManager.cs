using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using StlAuction.Types;

namespace StlAuction.Data
{
    public class BidderPhotoIdAddressManager
    {
        private readonly IRedisTypedClient<BidderPhotoIdAddress> _redis;
        private const string _bidderPhotoIdAddressKey = "urn:bidderphotoidtoaddress";

        public BidderPhotoIdAddressManager()
        {
            var redisClient = new RedisClient();
            _redis = redisClient.As<BidderPhotoIdAddress>();
        }

        public long Save(BidderPhotoIdAddress bidderPhotoIdAddress)
        {
            var Id = GetMaxId() + 1;
            bidderPhotoIdAddress.Id = Id;
            var key = string.Format("{0}:{1}", _bidderPhotoIdAddressKey, Id);
            _redis.SetValue(key, bidderPhotoIdAddress);
            return Id;
        }

        public long GetMaxId()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_bidderPhotoIdAddressKey)).ToList();
            
            List<long> longKeys = new List<long>();
            foreach (var key in listOfKeys)
            {
                var longKey = long.Parse(key.Replace(_bidderPhotoIdAddressKey + ":", string.Empty));
                longKeys.Add(longKey);
            }

            if (longKeys.Count == 0)
            {
                return 0;
            }

            return longKeys.Max();
        }
    

        public int GetNumberOfBidderPhotoIdAddresses()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_bidderPhotoIdAddressKey)).ToList();
            return listOfKeys.Count;
        }

        public List<BidderPhotoIdAddress> GetAllBidderPhotoIdAddresss()
        {
            var listOfKeys =  _redis.GetAllKeys().Where(k => k.StartsWith(_bidderPhotoIdAddressKey)).ToList();
            return _redis.GetValues(listOfKeys);
        }

        public void RemoveBidderPhotoIdAddress(BidderPhotoIdAddress bidderPhotoIdAddress)
        {
            var productKey = string.Format("{0}:{1}", _bidderPhotoIdAddressKey,  bidderPhotoIdAddress.Id);
            var redisClient = new RedisClient();
            redisClient.Remove(productKey);
        }

        public void RemoveAllBidderPhotoIdAddresss()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_bidderPhotoIdAddressKey)).ToList();
            var redisClient = new RedisClient();
            foreach (var key in listOfKeys)
            {
                redisClient.Remove(key);
            }
            
        }

        public BidderPhotoIdAddress GetById(long id)
        {
            return _redis.GetValue(string.Format("{0}:{1}", _bidderPhotoIdAddressKey, id));
        }

        public void Update(BidderPhotoIdAddress bidderPhotoIdAddress)
        {
            _redis.SetValue(string.Format("{0}:{1}", _bidderPhotoIdAddressKey, bidderPhotoIdAddress.Id), bidderPhotoIdAddress);
        }
    }
}
