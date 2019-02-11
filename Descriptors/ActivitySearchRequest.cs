using webApi.Model;

namespace webApi.Descriptors
{
    public class ActivitySearchRequest : ISearchRequest
    {
        public int Skip { get; set; }
        public int Top { get; set; }
        public string SearchQuery { get; set; }
        public ActivityType? ActivityType { get; set; }
    }
    public interface ISearchRequest
    {
        int Skip { get; set; }

        int Top { get; set; }
        string SearchQuery { get; set; }
    }

}