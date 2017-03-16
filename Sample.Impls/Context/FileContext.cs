using Newtonsoft.Json;
using Sample.Domains.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Impls.Context
{
    public class FileContext : Domains.Repositories.IContextWithTransaction
    {
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
            if (File.Exists(typeof(T).Name))
            {
                var list = JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(typeof(T).Name));
                if (!dictionary.ContainsKey(typeof(T).Name))
                {
                    dictionary.Add(typeof(T).Name, list.OfType<TIdentity>().ToList());
                }
                return list;
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
                    if (!File.Exists(item.Key))
                    {
                        datas = new List<TIdentity>();
                    }
                    else
                    {
                        datas = JsonConvert.DeserializeObject<List<object>>(File.ReadAllText(item.Key)).OfType<TIdentity>().ToList();
                    }
                    if (!datas.Any(n => n.ToString().Equals(data.ToString())))
                    {
                        datas.Add(data);
                    }
                    File.WriteAllText(item.Key, JsonConvert.SerializeObject(datas));
                }
            }
            dictionary = new Dictionary<string, List<TIdentity>>();
            return null;
        }

        public void Update<T>(T user) where T : TIdentity
        {

            var datas = Gets<T>().OfType<TIdentity>().ToList();
            if (dictionary.TryGetValue(typeof(T).Name, out datas))
            {
                var list = datas.OfType<T>();
                var data = datas.FirstOrDefault(n => n.Id == user.Id);
                datas.Remove(data);
                datas.Add(user);
            }


        }
    }
}
