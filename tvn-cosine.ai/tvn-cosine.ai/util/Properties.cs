﻿using tvn.cosine.collections;
using tvn.cosine.collections.api;

namespace tvn.cosine.ai.util
{
    public class Properties : Map<object, object>
    {
        protected Properties defaults;

        public Properties()
            : this(null)
        { }

        public Properties(Properties defaults)
        {
            this.defaults = defaults;
        }

        public object setProperty(string key, string value)
        {
            object obj = null;
            if (ContainsKey(key))
            {
                obj = Get(key);
            }

            Put(key, value);
            return obj;
        }

        public object getProperty(object key)
        {
            if (ContainsKey(key))
            {
               return Get(key);
            }
            return null;
        }
    }
}
