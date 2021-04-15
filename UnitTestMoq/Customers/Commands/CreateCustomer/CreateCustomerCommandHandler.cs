using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UnitTestMoq.Domain.Customers;

namespace UnitTestMoq.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, bool>
    {
        private readonly ICustomerRepository _repository;

        public CreateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer();
            _repository.Add(customer.Create(request));
            _repository.Save(customer, customer.Version);
            return Task.FromResult(true);
        }
    }
}