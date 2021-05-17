using System.Threading.Tasks;

namespace Common.Messages
{
    public interface IMessageHandler<in T>
    {
        Task HandleAsync(T @event);
    }
}