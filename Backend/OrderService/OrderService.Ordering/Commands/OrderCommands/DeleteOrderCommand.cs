using MediatR;

namespace OrderService.Ordering.Commands.OrderCommands
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }
}