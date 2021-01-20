using System;

namespace OsirisTest.Contracts
{
    public class BaseMessage<T>
    {
        public DateTime GeneratedDateTime { get; set; }

        public T Message { get; set; }
    }
}
