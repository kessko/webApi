using System;
using webApi.Model;

namespace webApi.Controllers
{
    public class ActivityItemBase<TBody> where TBody : ActivityBody, new()
    {
        public TBody Body { get; set; }
        Type BodyType => typeof(TBody);

        public TBody Create()
        {
            return new TBody();
        }
    }

    
}