using UnityEngine;
using System.Collections.Generic;

namespace HOA { 

    public class WatchList : ListSet<TokenRecord> {

        public WatchList () 
        {
            list = new List<TokenRecord>(1);
        }

        public void Add(Token token) { list.Add(new TokenRecord(token)); }
        public void Remove(Token token)
        {
            foreach (TokenRecord record in list)
                if (record.Token == token) 
                    list.Remove(record);
        }

        public TokenRecord Record(Token token)
        {
            foreach (TokenRecord record in list)
            {
                if (record.Token == token) return record;
            }
            return default(TokenRecord);
        }


    }
}
