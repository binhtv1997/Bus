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
    public interface IAdminService
    {
        IQueryable<Admin> GetAdmins();
        IQueryable<Admin> GetAdmins(Expression<Func<Admin, bool>> where);
        Admin GetAdmin(Guid Id);
        void CreateAdmin(Admin Admin);
        void UpdateAdmin(Admin Admin);
        void DeleteAdmin(Admin Admin);
        void DeleteAdmin(Expression<Func<Admin, bool>> where);
        void Save();
    }
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IAdminRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateAdmin(Admin Admin)
        {
            Admin.DateCreate = DateTime.Now;
            _repository.Add(Admin);
        }

        public void DeleteAdmin(Admin Admin)
        {
            Admin.IsDelete = true;
            _repository.Update(Admin);
        }

        public void DeleteAdmin(Expression<Func<Admin, bool>> where)
        {
            var ListAdmin = _repository.GetMany(where);

            foreach (var Admin in ListAdmin)
            {
                _repository.Delete(Admin);
            }
        }

        public Admin GetAdmin(Guid Id)
        {
            return _repository.GetById(Id);
        }

        public IQueryable<Admin> GetAdmins()
        {
            return _repository.GetAll();
        }

        public IQueryable<Admin> GetAdmins(Expression<Func<Admin, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateAdmin(Admin Admin)
        {
            _repository.Update(Admin);
        }
    }
}
