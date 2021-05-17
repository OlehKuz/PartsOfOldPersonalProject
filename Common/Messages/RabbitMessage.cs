using System;
using Common.Helpers;
using Newtonsoft.Json;

namespace Common.Messages
{
    public abstract class RabbitMessage:IMessage
    {
        public Guid Id { get; }
        public long CreationDate { get; }

        protected RabbitMessage()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow.ToUnixTimeStamp();
        }
        [JsonConstructor]
        protected RabbitMessage(Guid id, long createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

    }
}