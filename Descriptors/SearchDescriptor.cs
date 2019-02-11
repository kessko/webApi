using System;

namespace webApi.Descriptors
{
    public class SearchDescriptor<TItem> where TItem : new()
    {
        int _skip;
        int _top;
        private Func<TermDescriptor<TItem>, SearchDescriptor<TItem>> _queryFn;
        private string _index;
        private string _type;
        protected TItem _current;

        public SearchDescriptor()
        {
            _current = new TItem();
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
            return _queryFn(new TermDescriptor<TItem>(this));
        }
    }

    public class TermDescriptor<TItem> : SearchDescriptor<TItem>
        where TItem : new()
    {
        Func<TItem, object> _termFn;
        object _filed;
        private SearchDescriptor<TItem> _prev;

        public TermDescriptor(SearchDescriptor<TItem> descriptor)
        {
            _prev = descriptor;
        }

        public TermDescriptor<TItem> Term(Func<TItem, object> func, object value)
        {
            _termFn = func;
            _filed = _termFn(_current);
            return this;
        }
    }
}