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
    public interface IAccountService
    {
        IQueryable<Account> GetAccounts();
        IQueryable<Account> GetAccounts(Expression<Func<Account, bool>> where);
        Account GetAccount(Guid Id);
        void CreateAccount(Account Account);
        void UpdateAccount(Account Account);
        void DeleteAccount(Account Account);
        void DeleteAccount(Expression<Func<Account, bool>> where);
        void Save();
    }
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IAccountRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateAccount(Account Account)
        {
            Account.DateCreate = DateTime.Now;
            Account.IsDelete = false;
            _repository.Add(Account);
        }

        public void DeleteAccount(Account Account)
        {
            Account.IsDelete = true;
            _repository.Update(Account);
        }

        public void DeleteAccount(Expression<Func<Account, bool>> where)
        {
            var ListAccount = _repository.GetMany(where);

            foreach (var Account in ListAccount)
            {
                _repository.Delete(Account);
            }
        }

        public Account GetAccount(Guid Id)
        {
            return _repository.GetById(Id);
        }

        public IQueryable<Account> GetAccounts()
        {
            return _repository.GetAll();
        }

        public IQueryable<Account> GetAccounts(Expression<Func<Account, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateAccount(Account Account)
        {
            _repository.Update(Account);
        }
    }
}
