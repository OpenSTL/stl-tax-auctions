using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using StlAuction.Types;

namespace StlAuction.Data
{
    public class BidderIndividualManager
    {
        private readonly IRedisTypedClient<BidderIndividual> _redis;
        private const string _bidderIndividualKey = "urn:bidderindividual";

        public BidderIndividualManager()
        {
            var redisClient = new RedisClient();
            _redis = redisClient.As<BidderIndividual>();
        }

        public long Save(BidderIndividual bidderIndividual)
        {
            var Id = GetMaxId() + 1;
            bidderIndividual.Id = Id;
            var key = string.Format("{0}:{1}", _bidderIndividualKey, Id);
            _redis.SetValue(key, bidderIndividual);
            return Id;
        }

        public long GetMaxId()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_bidderIndividualKey)).ToList();
            
            List<long> longKeys = new List<long>();
            foreach (var key in listOfKeys)
            {
                var longKey = long.Parse(key.Replace(_bidderIndividualKey + ":", string.Empty));
                longKeys.Add(longKey);
            }

            if (longKeys.Count == 0)
            {
                return 0;
            }

            return longKeys.Max();
        }
    

        public int GetNumberOfBidderIndividuals()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_bidderIndividualKey)).ToList();
            return listOfKeys.Count;
        }

        public List<BidderIndividual> GetAllBidderIndividuals()
        {
            var listOfKeys =  _redis.GetAllKeys().Where(k => k.StartsWith(_bidderIndividualKey)).ToList();
            return _redis.GetValues(listOfKeys);
        }

        public void RemoveBidderIndividual(BidderIndividual bidderIndividual)
        {
            var bidderCorporationKey = string.Format("{0}:{1}", _bidderIndividualKey, bidderIndividual.Id);
            var redisClient = new RedisClient();
            redisClient.Remove(bidderCorporationKey);
        }

        public void RemoveAllBidderIndividuals()
        {
            var listOfKeys = _redis.GetAllKeys().Where(k => k.StartsWith(_bidderIndividualKey)).ToList();
            var redisClient = new RedisClient();
            foreach (var key in listOfKeys)
            {
                redisClient.Remove(key);
            }
        }

        public BidderIndividual GetById(long id)
        {
            return _redis.GetValue(string.Format("{0}:{1}", _bidderIndividualKey, id));
        }

        public void Update(BidderIndividual bidderCorporation)
        {
            _redis.SetValue(string.Format("{0}:{1}", _bidderIndividualKey, bidderCorporation.Id), bidderCorporation);
        }
    }
}
