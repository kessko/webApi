using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.Model;

namespace webApi.Descriptors
{
    public interface IDescriptorFactory<TItem, TSearchRequest>
        where TItem : new()
    {
        SearchDescriptor<TItem> Create(TSearchRequest searchRequest);
    }
    public abstract class BaseDescriptorFactory<TItem> : IDescriptorFactory<TItem, ISearchRequest>
        where TItem : IElasticDocument, new()
    {
        public SearchDescriptor<TItem> Create(ISearchRequest searchRequest)
        {
            var descriptor = new SearchDescriptor<TItem>();
            var item = new TItem();

            return descriptor
                 .Skip(searchRequest.Skip)
                 .Top(searchRequest.Top)
                .Index(item.IndexName)
                .Type(item.DocumentType);
        }
    }

    public class ActivityDescriptorFactory : BaseDescriptorFactory<ActivityItem>, IDescriptorFactory<ActivityItem, ActivitySearchRequest>
    {
        public SearchDescriptor<ActivityItem> Create(ActivitySearchRequest searchRequest)
        {
            var descriptor = base.Create(searchRequest);

            return descriptor
                   .Query(q => q.Term(t => t.Type, searchRequest.ActivityType));
        }
    }



}
