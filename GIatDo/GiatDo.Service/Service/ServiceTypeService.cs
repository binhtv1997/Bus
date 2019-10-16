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

    public interface IServiceTypeService
    {
        IQueryable<ServiceType> GetAll();
        IQueryable<ServiceType> GetServiceTypes(Expression<Func<ServiceType, bool>> where);
        ServiceType GetServiceType(Guid Id);
        void CreateServiceType(ServiceType ServiceType);
        void UpdateServiceType(ServiceType ServiceType);
        void DeleteServiceType(ServiceType ServiceType);
        void DeleteServiceType(Expression<Func<ServiceType, bool>> where);
        void Save();
    }
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceTypeRepository _repository;

        public ServiceTypeService(IServiceTypeRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateServiceType(ServiceType ServiceType)
        {
            _repository.Add(ServiceType);
        }

        public void DeleteServiceType(ServiceType ServiceType)
        {
            ServiceType.IsDelete = true;
            _repository.Update(ServiceType);
        }

        public void DeleteServiceType(Expression<Func<ServiceType, bool>> where)
        {
            var ListServiceType = _repository.GetMany(where);
            foreach (var i in ListServiceType)
            {
                _repository.Delete(i);
            }
        }

        public IQueryable<ServiceType> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<ServiceType> GetServiceTypes(Expression<Func<ServiceType, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public ServiceType GetServiceType(Guid Id)
        {
            return _repository.GetById(Id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateServiceType(ServiceType ServiceType)
        {
            _repository.Update(ServiceType);
        }
    }
}
