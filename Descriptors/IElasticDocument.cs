using System;

namespace webApi.Descriptors
{
    public interface IElasticDocument
    {
        string IndexName { get; }
        string DocumentType { get; }
    }
}