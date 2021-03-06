﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.Model
{
    public class ActivityFactory : IActivityFactory
    {
        public ActivityItem Create(DataRow listSource)
        {
            if (listSource == null)
            {
                throw new ArgumentNullException(nameof(listSource));
            }

            var type = Enum.Parse(typeof(ActivityType), listSource["Type"]?.ToString());
            var bodyValue = listSource["Body"]?.ToString();

            object body = JsonConvert.DeserializeObject(bodyValue, ActivityItem.BodyType);

            return new ActivityItem { Body = body as ActivityBody };

        }
    }

    public interface IDataRowHolder
    {
        DataRow GetRow(int index);
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
            _holder.Rows.Add(2, "Alex", (int)ActivityType.TaskConfigurationChanged, "Some Location2", "{\"NewFilters\":[\"AFR\",\"TF\"], \"OldFilters\":[\"TF\"], \"OldName\":\"this is other old name from db\"}");

        }

        public DataRow GetRow(int index)
        {
            return _holder.Rows[index];
        }
    }

    public interface IActivityFactory
    {
        ActivityItem Create(DataRow listSource);
    }
}
