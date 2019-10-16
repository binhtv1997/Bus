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
    public interface IShipperService
    {
        IQueryable<Shipper> GetShippers();
        IQueryable<Shipper> GetShippers(Expression<Func<Shipper, bool>> where);
        Shipper GetShipper(Guid Id);
        void CreateShipper(Shipper Shipper);
        void UpdateShipper(Shipper Shipper);
        void DeleteShipper(Shipper Shipper);
        void DeleteShipper(Expression<Func<Shipper, bool>> where);
        void Save();
    }
    public class ShipperService : IShipperService
    {
        private readonly IShipperRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ShipperService(IShipperRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateShipper(Shipper Shipper)
        {
            _repository.Add(Shipper);
        }

        public void DeleteShipper(Shipper Shipper)
        {
            Shipper.IsDelete = true;
            _repository.Update(Shipper);
        }

        public void DeleteShipper(Expression<Func<Shipper, bool>> where)
        {
            var ListShipper = _repository.GetMany(where);

            foreach (var Shipper in ListShipper)
            {
                _repository.Delete(Shipper);
            }
        }

        public Shipper GetShipper(Guid Id)
        {
            return _repository.GetById(Id);
        }

        public IQueryable<Shipper> GetShippers()
        {
            return _repository.GetAll();
        }

        public IQueryable<Shipper> GetShippers(Expression<Func<Shipper, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateShipper(Shipper Shipper)
        {
            _repository.Update(Shipper);
        }
    }
}
