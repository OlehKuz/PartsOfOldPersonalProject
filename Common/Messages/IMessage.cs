using System;

namespace Common.Messages
{
    public interface IMessage
    {
        Guid Id { get; }
        long CreationDate { get; }
    }
}

