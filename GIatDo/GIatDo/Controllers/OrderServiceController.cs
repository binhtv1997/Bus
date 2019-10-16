using System;
using System.Collections.Generic;
using GiatDo.Model;
using GiatDo.Service.Service;
using GIatDo.ViewModel;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace GIatDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderServiceController : ControllerBase
    {
        private readonly IOrderSService _orderServiceService;
        private readonly IOrderService _orderService;
        private readonly IServiceService _serviceService;

        public OrderServiceController(IOrderSService orderServiceService, IOrderService orderService, IServiceService serviceService)
        {
            _orderServiceService = orderServiceService;
            _orderService = orderService;
            _serviceService = serviceService;
        }

        [HttpGet("GetById")]
        public ActionResult GetOrderService(Guid Id)
        {
            return Ok(_orderServiceService.GetOrderService(Id).Adapt<OrderServiceVM>());
        }

        //[HttpGet("GetAll")]
        //public ActionResult GetAllOrderService()
        //{
        //    return Ok(_orderServiceService.GetOrderServices(o=> !o.IsDelete).Adapt<List<OrderServiceVM>>());
        //}

        [HttpPost("CreateService")]
        public ActionResult CreateService([FromBody]OrderServiceCM model)
        {
            if (_orderService.GetOrder(model.OrderId.Value) == null)
                return BadRequest(401);
            if (_serviceService.GetService(model.ServiceId.Value) == null)
                return BadRequest(401);
            OrderService orderService = model.Adapt<OrderService>();
            orderService.IsDelete = false;
            _orderServiceService.CreateOrderService(orderService);
            _orderServiceService.Save();
            return Ok(200);
        }

        [HttpPut("UpdateService")]
        public ActionResult UpdateService([FromBody] OrderServiceUM model)
        {
            if (_orderService.GetOrder(model.OrderId.Value) == null)
                return NotFound(401);
            if (_serviceService.GetService(model.ServiceId.Value) == null)
                return NotFound(401);

            var a = _orderServiceService.GetOrderService(model.Id);
            if (a == null)
                return BadRequest(401);

            OrderService newOrderService = model.Adapt(a);
            _orderServiceService.UpdateOrderService(newOrderService);
            _orderServiceService.Save();
            return Ok(200);
        }

        [HttpPut("DeleteService")]
        public ActionResult DeleteService(Guid Id)
        {
            if (_orderServiceService.GetOrderService(Id) == null)
                return BadRequest(401);
            _orderServiceService.DeleteOrderService(_orderServiceService.GetOrderService(Id));
            _orderServiceService.Save();
            return Ok(200);
        }
    }
}