using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.Model
{
    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }
        protected abstract T Create(JObject jObject, Type objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            if(objectType == null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }
            if(serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }
            if(reader.TokenType == JsonToken.None)
            {
                return null;
            }

            var jObject = JObject.Load(reader);
            T target = Create(jObject, objectType);
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // todo
            throw new NotImplementedException();
        }
    }
}
