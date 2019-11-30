using FrostCommon;

namespace FrostDB.Interface
{
    public interface IMessageProcessor
    {
        void Process(IMessage message);
    }
}
