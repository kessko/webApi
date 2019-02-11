using System;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace webApi.Model
{
    public class ActivityItemJsonConverter : JsonCreationConverter<ActivityItem>
    {
        protected override ActivityItem Create(JObject jObject, Type objectType)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException(nameof(jObject));
            }
            var typeValue = jObject[nameof(ActivityItem.Type)]?.Value<string>();
            if (!Enum.TryParse(typeValue, out ActivityType type))
            {
                throw new ArgumentException($"{typeValue} is not a part of {nameof(ActivityType)} enum.");
            }

            // todo logic of choosing body should be encapsulated.
            ActivityBody body = null;
            switch (type)
            {
                case ActivityType.TaskRenamed:
                    body = new TaskRenamedBody();
                    break;
                case ActivityType.TaskRemoved:
                    // todo
                    break;
                case ActivityType.TaskConfigurationChanged:
                    body = new TaskConfiguratonChangedBody();
                    break;
                case ActivityType.TaskCreated:
                    // todo
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(ActivityType));
            }

            return new ActivityItem
            {
                Body = body
            };
        }
    }
}
