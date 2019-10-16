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
    public interface ISlotService
    {
        IQueryable<Slot> GetSlots();
        IQueryable<Slot> GetSlots(Expression<Func<Slot, bool>> where);
        Slot GetSlot(Guid Id);
        void CreateSlot(Slot Slot);
        void UpdateSlot(Slot Slot);
        void DeleteSlot(Slot Slot);
        void DeleteSlot(Expression<Func<Slot, bool>> where);
        void Save();
    }
    public class SlotService : ISlotService
    {
        private readonly ISlotRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SlotService(ISlotRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateSlot(Slot Slot)
        {
            _repository.Add(Slot);
        }

        public void DeleteSlot(Slot Slot)
        {
            Slot.IsDelete = true;
            _repository.Update(Slot);
        }

        public void DeleteSlot(Expression<Func<Slot, bool>> where)
        {
            var ListSlot = _repository.GetMany(where);

            foreach (var Slot in ListSlot)
            {
                _repository.Delete(Slot);
            }
        }

        public Slot GetSlot(Guid Id)
        {
            return _repository.GetById(Id);
        }

        public IQueryable<Slot> GetSlots()
        {
            return _repository.GetAll();
        }

        public IQueryable<Slot> GetSlots(Expression<Func<Slot, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateSlot(Slot Slot)
        {
            _repository.Update(Slot);
        }
    }
}
