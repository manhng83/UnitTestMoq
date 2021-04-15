using System;
using UnitTestMoq.Application.Customers.Commands.CreateCustomer;
using UnitTestMoq.Application.Customers.Commands.UpdateCustomer;
using UnitTestMoq.Domain.Base;
using UnitTestMoq.Domain.Customers.Events;

namespace UnitTestMoq.Domain.Customers
{
    public class Customer : AggregateRoot
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public Customer()
        {
        }

        public Customer(string name, string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
        }

        public Customer Create(CreateCustomerCommand command)
        {
            Id = Guid.Parse(command.Id);
            Name = command.Name;
            Address = command.Address;

            Version = -1;
            ApplyChange(new CustomerCreated()
            {
                Name = command.Name,
                Address = command.Address
            });
            return this;
        }

        public Customer Update(UpdateCustomerCommand command)
        {
            Id = Guid.Parse(command.Id);
            Name = command.Name;
            Address = command.Address;
            return this;
        }

        private void Apply(CustomerCreated @event)
        {
        }
    }
}