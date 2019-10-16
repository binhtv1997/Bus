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
    public interface IOrderService
    {
        IQueryable<Order> GetOrders();
        IQueryable<Order> GetOrders(Expression<Func<Order, bool>> where);
        Order GetOrder(Guid Id);
        void CreateOrder(Order Order);
        void UpdateOrder(Order Order);
        void DeleteOrder(Order Order);
        void DeleteOrder(Expression<Func<Order, bool>> where);
        void Save();
    }
    public class OrderServices : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderServices(IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateOrder(Order Order)
        {
            Order.DateCreate = DateTime.Now;
            _repository.Add(Order);
        }

        public void DeleteOrder(Order Order)
        {
            Order.IsDelete = true;
            _repository.Update(Order);
        }

        public void DeleteOrder(Expression<Func<Order, bool>> where)
        {
            _repository.Delete(where);
        }

        public Order GetOrder(Guid Id)
        {
            return _repository.GetById(Id);
        }

        public IQueryable<Order> GetOrders()
        {
            return _repository.GetAll();
        }

        public IQueryable<Order> GetOrders(Expression<Func<Order, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateOrder(Order Order)
        {
            _repository.Update(Order);
        }
    }
}
