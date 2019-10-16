using System;
using System.Collections.Generic;
using System.Linq;
using GiatDo.Model;
using GiatDo.Service.Service;
using GIatDo.ViewModel;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace GIatDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly IShipperService _shipperService;
        private readonly ISlotService _slotService;
        private readonly IOrderService _orderService;

        public SlotController(IShipperService shipperService, ISlotService slotService, IOrderService orderService)
        {
            _shipperService = shipperService;
            _slotService = slotService;
            _orderService = orderService;
        }

        [HttpGet("GetSlot")]
        public ActionResult GetSlotAll()
        {
            return Ok(_slotService.GetSlots(s => !s.IsDelete).Adapt<List<SlotVM>>());
        }

        [HttpPost]
        public ActionResult CreateSlot([FromBody]SlotCM model)
        {
            Slot slot = model.Adapt<Slot>();
            slot.IsDelete = false;
            _slotService.CreateSlot(slot);
            _slotService.Save();
            return Ok(201);
        }
        [HttpDelete]
        public ActionResult DeleteSlot(Guid Id)
        {
            var model = _slotService.GetSlot(Id);
            if (model == null)
            {
                return NotFound();
            }
            _slotService.DeleteSlot(model);
            _slotService.Save();
            return Ok(201);
        }
        [HttpPut]
        public ActionResult UpdateSlot([FromBody] SlotVM model)
        {
            var test = _slotService.GetSlot(model.Id);
            if (test == null) { return NotFound(401); }
            _slotService.UpdateSlot(model.Adapt(test));
            _slotService.Save();
            return Ok(201);
        }
        [HttpGet("GetTotalPriceOfSlotByDay/{date}")]
        public ActionResult Get(string date)
        {
            try
            {
                var list = new List<SlotPriceVM>();
                List<Slot> slot = _slotService.GetSlots().ToList();
                DateTime dateFind = DateTime.Parse(date);
                int j = 1;
                for (int i = 0; i < slot.Count; i++)
                {
                    double total = 0;
                    var ele = _orderService.GetOrders().Where(o => o.SlotTakeId == slot.ElementAt(i).Id).Where(o => o.DateCreate.Date == dateFind.Date).ToList();
                    foreach (var e in ele)
                    {
                        total += e.TotalPrice;
                    }
                    SlotPriceVM s = new SlotPriceVM();
                    s.Value = total / 1000;
                    s.Key = "Slot " + j;
                    j++;
                    list.Add(s);
                }
                return Ok(list);
            }
            catch (Exception)
            {
                return BadRequest(401);
            }

        }
        [HttpGet("GetTotalNumber/{date}")]
        public ActionResult GetNumber(string date)
        {
            try
            {
                var list = new List<SlotPriceVM>();
                List<Slot> slot = _slotService.GetSlots().ToList();
                DateTime dateFind = DateTime.Parse(date);
                int i = 1;
                foreach (var item in slot)
                {
                    var ele = _orderService.GetOrders().Where(o => o.SlotTakeId == item.Id).Where(o => o.DateCreate.Date == dateFind.Date).ToList();

                    SlotPriceVM s = new SlotPriceVM();
                    s.Value = ele.Count;
                    s.Key = "Slot " + i;
                    i++;
                    list.Add(s);
                }
                return Ok(list);
            }
            catch (Exception)
            {
                return BadRequest(401);
            }
        }
        [HttpGet("NumberOrderByMonth")]
        public ActionResult AssessmentCaseByMonth()
        {

            var data = _orderService.GetOrders(o => !o.IsDelete).GroupBy(_ => _.DateCreate.Date).ToList();
            var result = new List<SlotPriceVM>();
            foreach (var item in data)
            {
                result.Add(new SlotPriceVM
                {
                    Key = item.Key,
                    Value = item.Count(),
                });
            }
            return Ok(result);
        }
    }
}