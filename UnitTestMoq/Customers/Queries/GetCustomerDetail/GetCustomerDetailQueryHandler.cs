using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnitTestMoq.Domain.Customers;

namespace UnitTestMoq.Application.Customers.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, CustomerDetailViewModel>
    {
        private readonly ICustomerRepository _repository;

        public GetCustomerDetailQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerDetailViewModel> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            var model = _repository.GetById(Guid.Parse(request.Id));
            var vm = new CustomerDetailViewModel
            {
                Name = model.Name,
                Address = model.Address
            };
            return await Task.FromResult(vm);
        }
    }
}