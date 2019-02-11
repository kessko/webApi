using System;
using Newtonsoft.Json;

namespace webApi.Model
{
    [JsonConverter(typeof(ActivityItemJsonConverter))]
    public class ActivityItem
    {
        public static Type BodyType
        {
            get
            {
                return typeof(TaskRenamedBody);
            }
        }
        public ActivityType Type { get; set; }
        public ActivityBody Body { get; set; }
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }

        public static ActivityBody CreateEmptyBody(ActivityType activityType)
        {
            return Activator.CreateInstance(BodyType) as ActivityBody;
        }


    }

    public abstract class ActivityBody { }
    public class TaskRenamedBody : ActivityBody
    {
        public string NewName { get; set; }
        public string OldName { get; set; }
    }

    public class TaskConfiguratonChangedBody : TaskRenamedBody
    {
        public string[] OldFilters { get; set; }
        public string[] NewFilters { get; set; }
    }

    public enum ActivityType
    {
        TaskRenamed = 0,
        TaskRemoved = 1,
        TaskConfigurationChanged = 2,
        TaskCreated = 3
    }
}
