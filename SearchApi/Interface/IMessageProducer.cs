namespace SearchApi.Interface
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
        void ReceiveMessageUser();
        void ReceiveMessageProduct();
    }
}
