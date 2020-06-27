using FrostCommon;

namespace FrostCommon
{
    public interface IMessageProcessor
    {
        IMessage Process(IMessage message);
        int PortNumber { get; set; }
    }
}
