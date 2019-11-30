using FrostCommon;

namespace FrostCommon
{
    public interface IMessageProcessor
    {
        void Process(IMessage message);
    }
}
