using GiatDo.Data.Infrastructure;
using GiatDo.Data.Repositories;
using GiatDo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GiatDo.Service.Service
{
    public interface ICustomerService
    {
        IQueryable<Customer> GetCustomers();
        IQueryable<Customer> GetCustomers(Expression<Func<Customer, bool>> where);
        Customer GetCustomer(Guid Id);
        void CreateCustomer(Customer Customer);
        void UpdateCustomer(Customer Customer);
        void DeleteCustomer(Customer Customer);
        void DeleteCustomer(Expression<Func<Customer, bool>> where);
        void Save();
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateCustomer(Customer Customer)
        {
            Customer.DateCreate = DateTime.Now;
            Customer.IsDelete = false;
            _repository.Add(Customer);
        }

        public void DeleteCustomer(Customer Customer)
        {
            Customer.IsDelete = true;
            _repository.Update(Customer);
        }

        public void DeleteCustomer(Expression<Func<Customer, bool>> where)
        {
            var ListCustomer = _repository.GetMany(where);

            foreach (var Customer in ListCustomer)
            {
                _repository.Delete(Customer);
            }
        }

        public Customer GetCustomer(Guid Id)
        {
            return _repository.GetById(Id);
        }

        public IQueryable<Customer> GetCustomers()
        {
            return _repository.GetAll();
        }

        public IQueryable<Customer> GetCustomers(Expression<Func<Customer, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateCustomer(Customer Customer)
        {
            _repository.Update(Customer);
        }
    }
}
