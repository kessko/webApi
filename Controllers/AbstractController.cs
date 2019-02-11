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
        private IActivityFactory _activityFacotry;
        private IDataRowHolder _dataRowHolder;

        public AbstractController(IActivityFactory activityFacotry, IDataRowHolder dataRowHolder)
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
        [Route("create/{index}")]
        public ActivityItem CreateItem(int index)
        {
            return _activityFacotry.Create(_dataRowHolder.GetRow(index));
        }

        [HttpPut]
        public IActionResult Put([FromBody]ActivityItem activityItem)
        {
            _holder.Add(activityItem);


            return Ok();
        }


        [HttpPut]
        [Route("generic")]
        public IActionResult Put([FromBody]ActivityItemBase<TaskRenamedBody> activityItem)
        {
            return new JsonResult(activityItem);
        }
    }
}