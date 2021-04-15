using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnitTestMoq.Domain.Customers;

namespace UnitTestMoq.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository _repository;

        public DeleteCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(Guid.Parse(request.Id));
            return Task.FromResult(true);
        }
    }
}