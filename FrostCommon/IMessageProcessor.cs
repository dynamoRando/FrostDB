using FrostCommon;

namespace FrostCommon
{
    public interface IMessageProcessor
    {
        void Process(IMessage message);
        int PortNumber { get; set; }

        IMessage ProcessWithResult(IMessage message);
    }
}
