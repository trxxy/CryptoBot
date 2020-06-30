using DataAccess.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccess.Repositories
{
    public class JsonSet<TEntity>
        where TEntity:Entity
    {
        const string JSON_FILE_FORMAT = ".json";

        public List<TEntity> Entities { get; set; }
        public bool IsSetChanged { get; set; }
        public string EntityPath { get; set; }

        public JsonSet(ref Action action, string rootDirectoryPath)
        {
            EntityPath = rootDirectoryPath + typeof(TEntity).Name + JSON_FILE_FORMAT;
            var deserialized = File.ReadAllText(EntityPath);
            var entities = Deserialize<List<TEntity>>(deserialized);

            Entities = entities != null ? entities : new List<TEntity>();

            action += Save;
        }

        private void Save()
        {
            if (IsSetChanged)
            {
                var serializedString = Serialize(Entities);
                File.WriteAllText(EntityPath, serializedString);
            }
        }
        private string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        private T Deserialize<T>(string deserializedString)
        {
            try
            {
                var value = JsonConvert.DeserializeObject<T>(deserializedString);
                return value;
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
