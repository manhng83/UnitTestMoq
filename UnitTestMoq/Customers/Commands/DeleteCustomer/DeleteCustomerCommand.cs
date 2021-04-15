using MediatR;

namespace UnitTestMoq.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}