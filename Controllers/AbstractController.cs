using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Model;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbstractController : ControllerBase
    {
        private IActivityFacotry _activityFacotry;
        private IDataRowHolder _dataRowHolder;

        public AbstractController(IActivityFacotry activityFacotry, IDataRowHolder dataRowHolder)
        {
            _activityFacotry = activityFacotry;
            _dataRowHolder = dataRowHolder;
        }
        static List<ActivityItem> _holder = new List<ActivityItem> {
            new ActivityItem {
                Id = 1,
                UserName = "TestUser",
                Type =ActivityType.TaskRenamed,
                Location = "Some Location",
                Body = new TaskRenamedBody
                {
                    NewName = "renamed name",
                    OldName =  "this is a old name"
                }
            },
            new ActivityItem
            {
                Id = 2,
                UserName = "Alex",
                Type = ActivityType.TaskConfigurationChanged,
                Location = "Some Location2",
                Body  = new TaskConfiguratonChangedBody
                {
                    NewFilters = new []{"AFR","TF" },
                    OldFilters = new []{"TF"}
                }

            }
        };


        [HttpGet]
        [Route("items")]
        public IEnumerable<ActivityItem> GetItems()
        {
            return _holder;
        }


        [HttpGet]
        [Route("create")]
        public ActivityItem CreateItem()
        {
            return _activityFacotry.Create(_dataRowHolder.GetRow());
        }
    }
}