

using Sample.Domains.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Impls.Context
{
    public class InMemoryContext : Domains.Repositories.IContextWithTransaction
    {
        static Dictionary<string, List<TIdentity>> dictionaryGlobal = new Dictionary<string, List<TIdentity>>();
        Dictionary<string, List<TIdentity>> dictionary = new Dictionary<string, List<TIdentity>>();
        public void Add<T>(T model) where T : TIdentity
        {
            var datas = new List<TIdentity>();
            if (!dictionary.TryGetValue(typeof(T).Name, out datas))
            {
                dictionary.Add(typeof(T).Name, new List<TIdentity>());
                datas = new List<TIdentity>();

            }

            datas.Add(model);
            dictionary[typeof(T).Name] = datas;
        }

        public IEnumerable<T> Gets<T>() where T : TIdentity
        {
            var datas = new List<TIdentity>();
            if (dictionary.TryGetValue(typeof(T).Name, out datas))
            {
                return datas.OfType<T>();
            }
            return new List<T>();
        }

        public Exception SaveChanges()
        {
            foreach (var item in dictionary)
            {
                var datas = new List<TIdentity>();
                foreach (var data in item.Value)
                {
                    if (!dictionaryGlobal.TryGetValue(item.Key, out datas))
                    {
                        datas = new List<TIdentity>();
                        dictionaryGlobal.Add(item.Key, datas);
                    }
                    if (!datas.Any(n => n.ToString().Equals(data.ToString())))
                    {
                        datas.Add(data);
                    }
                    dictionaryGlobal[item.Key] = datas;
                }
            }
            return null;
        }

        public void Update<T>(T user) where T : TIdentity
        {
            var datas = new List<TIdentity>();
            if (dictionary.TryGetValue(typeof(T).Name, out datas))
            {
                var list = datas.OfType<T>();
                var data =datas.FirstOrDefault(n => n.Id == user.Id);
                datas.Remove(data);
                datas.Add(user);
            }
        }
    }
}
