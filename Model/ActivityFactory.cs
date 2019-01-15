using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.Model
{
    public class ActivityFactory : IActivityFacotry
    {
        public ActivityItem Create(DataRow listSource)
        {
            if (listSource == null)
            {
                listSource = new InMemoryTable().GetRow();
            }

            var type = Enum.Parse(typeof(ActivityType), listSource["Type"]?.ToString());
            var bodyValue = listSource["Body"]?.ToString();
            ActivityBody body;
            switch (type)
            {
                case ActivityType.TaskRenamed:
                    body = JsonConvert.DeserializeObject<TaskRenamedBody>(bodyValue);
                    break;
                default:
                    throw new ArgumentException();
                    break;
            }


            return new ActivityItem { Body = body };

        }
    }

    public interface IDataRowHolder
    {
        DataRow GetRow();
    }
    public class InMemoryTable : IDataRowHolder
    {
        private DataTable _holder { get; set; }
        public InMemoryTable()
        {
            _holder = new DataTable("AdminActivityAction");
            //Define the table columns. For example:
            _holder.Columns.Add("Id", typeof(int));
            _holder.Columns.Add("UserName", typeof(string));
            _holder.Columns.Add("Type", typeof(int));
            _holder.Columns.Add("Location", typeof(string));
            _holder.Columns.Add("Body", typeof(string));

            //Add rows 
            _holder.Rows.Add(1, "TestUser", (int)ActivityType.TaskRenamed, "Some Location", "{\"OldName\":\"this is old name from db\"}");
        }

        public DataRow GetRow()
        {
            return _holder.Rows[0];
        }
    }

    public interface IActivityFacotry
    {
        ActivityItem Create(DataRow listSource);
    }
}
