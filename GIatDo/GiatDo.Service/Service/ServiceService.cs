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
    public interface IServiceService
    {
        IQueryable<Services> GetServices();
        IQueryable<Services> GetServices(Expression<Func<Services, bool>> where);
        Services GetService(Guid Id);
        void CreateService(Services Service);
        void UpdateService(Services Service);
        void DeleteService(Services Service);
        void DeleteService(Expression<Func<Services, bool>> where);
        void Save();
    }
    public class ServiceService : IServiceService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceRepository _repository;

        public ServiceService(IUnitOfWork unitOfWork, IServiceRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public void CreateService(Services Service)
        {
            _repository.Add(Service);
        }

        public void DeleteService(Services Service)
        {
            Service.IsDelete = true;
            _repository.Update(Service);
        }

        public void DeleteService(Expression<Func<Services, bool>> where)
        {
            var listService = _repository.GetMany(where);
            foreach(var i in listService)
            {
                _repository.Delete(i);
            }
        }

        public Services GetService(Guid Id)
        {
            return _repository.GetById(Id);
        }

        public IQueryable<Services> GetServices()
        {
            return _repository.GetAll();
        }

        public IQueryable<Services> GetServices(Expression<Func<Services, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateService(Services Service)
        {
            _repository.Update(Service);
        }
    }
}
