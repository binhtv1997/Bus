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
    public interface IStoreService
    {
        IQueryable<Store> GetStores();
        IQueryable<Store> GetStores(Expression<Func<Store, bool>> where);
        Store GetStore(Guid Id);
        void CreateStore(Store Store);
        void UpdateStore(Store Store);
        void DeleteStore(Store Store);
        void DeleteStore(Expression<Func<Store, bool>> where);
        void Save();
    }
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public StoreService(IStoreRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateStore(Store Store)
        {
            Store.DateCreate = DateTime.Now;
            _repository.Add(Store);
        }

        public void DeleteStore(Store Store)
        {
            Store.IsDelete = true;
            _repository.Update(Store);
        }

        public void DeleteStore(Expression<Func<Store, bool>> where)
        {
            var ListStore = _repository.GetMany(where);

            foreach (var Store in ListStore)
            {
                _repository.Delete(Store);
            }
        }

        public Store GetStore(Guid Id)
        {
            return _repository.GetById(Id);
        }

        public IQueryable<Store> GetStores()
        {
            return _repository.GetAll();
        }

        public IQueryable<Store> GetStores(Expression<Func<Store, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateStore(Store Store)
        {
            _repository.Update(Store);
        }
    }
}
