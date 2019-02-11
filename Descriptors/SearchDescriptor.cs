using System;

namespace webApi.Descriptors
{
    public class SearchDescriptor<TItem>
    {
        int _skip;
        int _top;
        private Func<TermDescriptor<TItem>, SearchDescriptor<TItem>> _queryFn;
        private string _index;
        private string _type;

        public SearchDescriptor()
        {

        }

        public SearchDescriptor<TItem> Index(string index)
        {
            _index = index;
            return this;
        }

        public SearchDescriptor<TItem> Type(string type)
        {
            _type = type;
            return this;
        }
        public SearchDescriptor<TItem> Skip(int skip)
        {
            _skip = skip;
            return this;
        }
        public SearchDescriptor<TItem> Top(int top)
        {
            _top = top;
            return this;
        }
        public SearchDescriptor<TItem> Query(Func<TermDescriptor<TItem>, SearchDescriptor<TItem>> func)
        {
            _queryFn = func;
            return this;
        } 
    }

    public class TermDescriptor<TItem> : SearchDescriptor<TItem>
    {
        Func<TItem, object> _termFn;

        public TermDescriptor<TItem> Term(Func<TItem, object> func, object value)
        {
            _termFn = func;
            return this;
        }
    }
}