namespace ProductApi.Interface
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);

    }
}
