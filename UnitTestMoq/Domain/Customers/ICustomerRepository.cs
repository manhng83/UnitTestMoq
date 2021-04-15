using System;
using System.Collections.Generic;

namespace UnitTestMoq.Domain.Customers
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();

        Customer GetById(Guid customerId);

        void Add(Customer customer);

        void Update(Customer customer);

        void Delete(Guid customerId);

        void Save(Customer customer, int expectedVersion);
    }
}