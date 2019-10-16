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
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly ISlotService _slotService;
        private readonly ICustomerService _customerService;
        private readonly IShipperService _shipperService;
        private readonly IOrderSService _orderServiceService;
        private readonly IServiceService _serviceService;
        private readonly IStoreService _storeService;

        public OrderController(IOrderService orderService, ISlotService slotService, ICustomerService customerService,
            IShipperService shipperService, IOrderSService orderServiceService,
            IServiceService serviceService, IStoreService storeService)
        {
            _orderService = orderService;
            _slotService = slotService;
            _customerService = customerService;
            _shipperService = shipperService;
            _orderServiceService = orderServiceService;
            _serviceService = serviceService;
            _storeService = storeService;
        }

        [HttpPost("CreateOrder")]
        public ActionResult CreateOrder([FromBody] OrderCM model)
        {
            var timeCreate = DateTime.Now;
            DateTime? timeDelivery = model.DeliveryTime;
            DateTime? takeTime = model.TakeTime;
            var checkSlot = _slotService.GetSlots().Where(t1 => t1.TimeStart <= timeCreate.TimeOfDay).Where(t2 => t2.TimeEnd >= timeCreate.TimeOfDay).ToList();
            var deliver = _slotService.GetSlots().Where(t1 => t1.TimeStart <= timeDelivery.Value.TimeOfDay).Where(t2 => t2.TimeEnd >= timeDelivery.Value.TimeOfDay).ToList();
            var take = _slotService.GetSlots().Where(t1 => t1.TimeStart <= takeTime.Value.TimeOfDay).Where(t2 => t2.TimeEnd >= takeTime.Value.TimeOfDay).ToList();
            if (checkSlot.Any())
            {
                var checkCus = _customerService.GetCustomer(model.CustomerId.Value);
                if (checkCus == null)
                {
                    return NotFound();
                }

                Order newOrder = model.Adapt<Order>();
                newOrder.IsDelete = false;
                newOrder.SlotTakeId = take[0].Id;
                newOrder.SlotDeliveryId = deliver[0].Id;
                newOrder.CreateTime = timeCreate;
                _orderService.CreateOrder(newOrder);
                _orderService.Save();

                foreach (var item in model.ListOrderServices)
                {
                    OrderService orderService = item.Adapt<OrderService>();
                    orderService.OrderId = newOrder.Id;
                    _orderServiceService.CreateOrderService(orderService);
                }
                _orderServiceService.Save();
                return Ok(201);

            }
            else
            {
                return BadRequest("Dont Have Any Slot");
            }
        }

        [HttpGet("GetByCustomerId")]

        public ActionResult GetOrderByCustomerId(Guid Id)
        {
            var checkCus = _customerService.GetCustomer(Id);
            if (checkCus == null)
            {
                return NotFound("Not Found Customer");
            }
            var result = _orderService.GetOrders(o => o.CustomerId == Id);
            return Ok(result.Adapt<List<OrderVM>>());
        }
        [HttpPut("UpdateShipperTake")]
        public ActionResult UpdateShipperTake([FromBody]UpdateShipperTake model)
        {
            var CheckShipper = _shipperService.GetShipper(model.ShipperId);
            if (CheckShipper == null)
            {
                return NotFound("Shipper Not Found");
            }
            var _order = _orderService.GetOrders(o => o.Id == model.OrderId).AsEnumerable().ElementAt(0);
            _order.ShipperTakeId = CheckShipper.Id;
            _orderService.UpdateOrder(_order);
            _orderService.Save();
            return Ok(201);
        }
        [HttpPut("UpdateShipperDelivery")]
        public ActionResult UpdateShipperDelivery([FromBody]UpdateShipperDelivery model)
        {
            var CheckShipper = _shipperService.GetShipper(model.ShipperId);
            if (CheckShipper == null)
            {
                return NotFound("Shipper Not Found");
            }
            var _order = _orderService.GetOrders(o => o.Id == model.OrderId).AsEnumerable().ElementAt(0);
            _order.ShipperDeliverId = CheckShipper.Id;
            _orderService.UpdateOrder(_order);
            _orderService.Save();
            return Ok(201);
        }
        [HttpPut("UpdateOrderStatus")]
        public ActionResult UpdateOrderStatus([FromBody]UpdateOrderStatus model)
        {
            try
            {
                if (model.OrderId == null || model.Status == null)
                {
                    return BadRequest();
                }
                var order = _orderService.GetOrder(model.OrderId);
                order.Status = model.Status.ToLower();
                _orderService.UpdateOrder(order);
                _orderService.Save();
                return Ok(201);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteOrder")]
        public ActionResult Delete(Guid Id)
        {
            var checkOrder = _orderService.GetOrder(Id);
            if (checkOrder == null)
            {
                return NotFound("Not Found Order");
            }
            var listOrderService = _orderServiceService.GetOrderServices(s => s.OrderId == Id).ToList();
            foreach (var item in listOrderService)
            {
                _orderServiceService.DeleteOrderService(item);
            }
            _orderService.DeleteOrder(checkOrder);
            _orderService.Save();
            _orderServiceService.Save();
            return Ok(201);
        }
        [HttpGet("GetOrderByStatus")]
        public ActionResult GetOrderByStatus(String status, Guid storeId)
        {
            try
            {
                var order = _orderService.GetOrders(t => !t.IsDelete);
                var orderService = _orderServiceService.GetOrderServices(t => !t.IsDelete);
                var service = _serviceService.GetServices(t => !t.IsDelete);
                var store = _storeService.GetStores(t => !t.IsDelete);
                var temp = from t in order
                           join o in orderService on t.Id equals (o.OrderId)
                           join s in service on o.ServiceId equals (s.Id)
                           join st in store on s.StoreId equals (st.Id)
                           where t.Status.Equals(status)
                           where st.Id.Equals(storeId)
                           select t;
                List<Order> list = temp.ToList();
                return Ok(list.Adapt<List<OrderVM>>());
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return Ok();
        }
        [HttpGet("GetOrderTakeBySlotAndDate")]
        public ActionResult GetOrder(string date, Guid SlotId)
        {
            try
            {
                List<OrderAVM> list = new List<OrderAVM>();
                DateTime dateFind = DateTime.Parse(date);
                if (SlotId == Guid.Empty)
                {

                    List<OrderAVM> listOrder = _orderService.GetOrders(o => !o.IsDelete).Where(o1 => o1.DateCreate.Date == dateFind.Date).Adapt<List<OrderAVM>>();
                    foreach (var item in listOrder)
                    {
                        var orderservice = _orderServiceService.GetOrderServices(t => !t.IsDelete).Where(t1 => t1.OrderId == item.Id).FirstOrDefault();
                        var service = _serviceService.GetServices(t => !t.IsDelete).Where(t1 => t1.Id == orderservice.ServiceId).FirstOrDefault();
                        var store = _storeService.GetStores(t => !t.IsDelete).Where(t1 => t1.Id == service.StoreId).FirstOrDefault();
                        if (item.ShipperTakeId.HasValue)
                        {
                            var shipperTake = _shipperService.GetShippers(t => t.Id == item.ShipperTakeId).FirstOrDefault();
                            item.ShipperTakeName = shipperTake.Name;
                        }
                        if (item.ShipperDeliverId.HasValue)
                        {
                            var shipperDelivery = _shipperService.GetShippers(t => t.Id == item.ShipperDeliverId.Value).FirstOrDefault();
                            item.ShipperDeliveryName = shipperDelivery.Name;
                        }
                        item.StoreName = store.Name;
                        ChangeSTT(item, SlotId);
                    }
                    return Ok(listOrder);
                }

                var result1 = _orderService.GetOrders(o => !o.IsDelete)
                    .Where(o1 => o1.DateCreate.Date == dateFind.Date)
                    .Where(o2 => o2.SlotTakeId == SlotId);

                var result2 = _orderService.GetOrders(o => !o.IsDelete)
                   .Where(o1 => o1.DateCreate.Date == dateFind.Date)
                   .Where(o2 => o2.SlotDeliveryId == SlotId);
                var result = result1.Union(result2);
                foreach (var item in result)
                {
                    var orderservice = _orderServiceService.GetOrderServices(t => !t.IsDelete).Where(t1 => t1.OrderId == item.Id).FirstOrDefault();
                    var service = _serviceService.GetServices(t => !t.IsDelete).Where(t1 => t1.Id == orderservice.ServiceId).FirstOrDefault();
                    var store = _storeService.GetStores(t => !t.IsDelete).Where(t1 => t1.Id == service.StoreId).FirstOrDefault();
                    OrderAVM temp = item.Adapt<OrderAVM>();
                    temp.StoreName = store.Name;
                    if (item.ShipperTakeId.HasValue)
                    {
                        var shipperTake = _shipperService.GetShippers(t => t.Id == item.ShipperTakeId).FirstOrDefault();
                        temp.ShipperTakeName = shipperTake.Name;
                    }
                    if (item.ShipperDeliverId.HasValue)
                    {
                        var shipperDelivery = _shipperService.GetShippers(t => t.Id == item.ShipperDeliverId.Value).FirstOrDefault();
                        temp.ShipperDeliveryName = shipperDelivery.Name;
                    }
                    temp.StoreName = store.Name;
                    ChangeSTT(temp, SlotId);
                    list.Add(temp);
                }
                return Ok(list);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        public void ChangeSTT(OrderAVM item, Guid SlotId)
        {
            if (item.Status.Equals("ongoing"))
            {
                item.STT = "Khách vừa đặt hàng - cần tìm shipper";
            }
            else if (item.Status.Equals("taken"))
            {
                item.STT = "Đơn hàng đang mang về kho";
            }
            else if (item.Status.Equals("onwarehousetake"))
            {
                item.STT = "Đơn hàng đã mang về kho - chuyển cho cửa hàng";
            }
            else if (item.Status.Equals("onstore"))
            {
                item.STT = "Cửa hàng đã nhận đồ";
            }
            else if (item.Status.Equals("washed"))
            {
                item.STT = "Đơn hàng đã giặt xong cần mang về kho";
            }
            else if (item.Status.Equals("onwarehousedelivery"))
            {
                item.STT = "Đơn hàng đã xử lý xong - cần tìm shipper";
            }
            else if (item.Status.Equals("ondelivery"))
            {
                item.STT = "Đơn hàng đang được trả cho khách";
            }
            else if (item.Status.Equals("done"))
            {
                item.STT = "Đơn hàng đã hoàn thành";
            }
            else if (item.Status.Equals("cancel"))
            {
                item.STT = "Đơn hàng đã bị huỷ";
            }


            if (item.SlotDeliveryId == SlotId)
            {
                item.Process = "Thời Gian Trả Hàng";
            }
            else if (item.SlotTakeId == SlotId)
            {
                item.Process = " Thời Gian Nhận hàng";
            }
        }
    }
}