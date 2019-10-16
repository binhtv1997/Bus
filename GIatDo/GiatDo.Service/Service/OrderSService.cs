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
    public interface IOrderSService
    {
        IQueryable<OrderService> GetOrderServices();
        IQueryable<OrderService> GetOrderServices(Expression<Func<OrderService, bool>> where);
        OrderService GetOrderService(Guid Id);
        void CreateOrderService(OrderService Order);
        void UpdateOrderService(OrderService Order);
        void DeleteOrderService(OrderService Order);
        void DeleteOrderService(Expression<Func<OrderService, bool>> where);
        void Save();
    }
    public class OrderSService : IOrderSService
    {
        private readonly IOrderServiceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderSService(IOrderServiceRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateOrderService(OrderService OrderService)
        {
            _repository.Add(OrderService);
        }

        public void DeleteOrderService(OrderService OrderService)
        {
            OrderService.IsDelete = true;
            _repository.Update(OrderService);
        }

        public void DeleteOrderService(Expression<Func<OrderService, bool>> where)
        {
            var listService = _repository.GetMany(where);
            foreach(var i in listService)
            {
                _repository.Delete(i);
            }
        }
        public OrderService GetOrderService(Guid Id)
        {
            return _repository.GetById(Id);
        }

        public IQueryable<OrderService> GetOrderServices()
        {
            return _repository.GetAll();
        }

        public IQueryable<OrderService> GetOrderServices(Expression<Func<OrderService, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
        public void UpdateOrderService(OrderService OrderService)
        {
            _repository.Update(OrderService);
        }
    }
}
