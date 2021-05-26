using OrderAPI.Models;

namespace BuisnessLayer
{
    public interface IProducer
    {
        void TopicExchangeQueue(Order orderItems);
    }
}