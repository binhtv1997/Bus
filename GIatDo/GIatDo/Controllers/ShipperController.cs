using System;
using System.Collections.Generic;
using System.Linq;
using GiatDo.Model;
using GiatDo.Service.Service;
using GIatDo.Hubs;
using GIatDo.ViewModel;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GIatDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService _shipperService;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly IHubContext<CenterHub> _hub;

        public ShipperController(IShipperService shipperService, IOrderService orderService, ICustomerService customerService, IAccountService accountService, IHubContext<CenterHub> hubContext)
        {
            _shipperService = shipperService;
            _orderService = orderService;
            _customerService = customerService;
            _accountService = accountService;
            _hub = hubContext;
        }

        [HttpPost("CreateShipper")]
        public ActionResult CreateShipper([FromBody] CreateShipperVM shipper)
        {
            var result = _shipperService.GetShippers(s => s.Phone == shipper.Phone);
            if (result.Count() > 0)
            {
                return BadRequest("Phone Number Has Been Exist");
            }
            var _Account = _shipperService.GetShippers(s => s.AccountId == shipper.AccountId);

            if (!_Account.Any())
            {
                var newUser_Id = Guid.NewGuid();
                Account newAccount = new Account();
                newAccount.User_Id = newUser_Id.ToString();
                _accountService.CreateAccount(newAccount);
                _accountService.Save();
                Shipper newShipper = shipper.Adapt<Shipper>();
                newShipper.IsDelete = false;
                newShipper.AccountId = newAccount.Id;
                _shipperService.CreateShipper(newShipper);
            }
            else
            {
                Shipper newShipper = shipper.Adapt<Shipper>();
                newShipper.IsDelete = false;
                newShipper.AccountId = _Account.FirstOrDefault().Id;
                _shipperService.CreateShipper(newShipper);
            }
            _shipperService.Save();
            _hub.Clients.All.SendAsync("transferchartdata", "Hello");
            return Ok(201);
        }
        [HttpGet("GetAll")]
        public ActionResult GetShipper()
        {
            return Ok(_shipperService.GetShippers(s => !s.IsDelete).Adapt<List<ShipperVM>>());
        }
        [HttpGet("GetById")]
        public ActionResult GetShipper(Guid Id)
        {
            return Ok(_shipperService.GetShipper(Id).Adapt<ShipperVM>());
        }
        [HttpGet("GetOrderTake")]
        public ActionResult GetOrderTake(Guid Id)
        {
            var listOrder = _orderService.GetOrders(s => s.ShipperTakeId == Id).Adapt<List<OrderTakeVM>>();
            foreach (var i in listOrder)
            {
                i.CustomerName = _customerService.GetCustomer(i.CustomerId).Name;
            }
            return Ok(listOrder);
        }
        [HttpGet("GetOrderDelivery")]
        public ActionResult GetOrderDelevery(Guid Id)
        {
            var listOrder = _orderService.GetOrders(s => s.ShipperDeliverId == Id).Adapt<List<OrderDeleveryVM>>();
            foreach (var i in listOrder)
            {
                i.CustomerName = _customerService.GetCustomer(i.CustomerId).Name;
            }
            return Ok(listOrder);
        }
        [HttpPut("Update")]
        public ActionResult UpdateShipper(ShipperVM model)
        {
            var _shipper = _shipperService.GetShipper(model.Id);
            if (_shipper == null)
            {
                return NotFound();
            }
            if (_shipperService.GetShippers(s => s.Phone == model.Phone).Count() > 0)
            {
                return BadRequest("Phone Number Has Been Exist");
            }
            Shipper newShipper = model.Adapt(_shipper);
            _shipperService.UpdateShipper(newShipper);
            _shipperService.Save();
            return Ok(201);
        }
        [HttpGet("GetByUserID")]
        public ActionResult GetCustomerByAccountId(string Id)
        {
            var AccountId = _accountService.GetAccounts(a => a.User_Id.Equals(Id)).ToList();
            if (!AccountId.Any())
            {
                return NotFound();
            }
            var result = _shipperService.GetShippers(c => c.AccountId == AccountId[0].Id).ToList();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result[0].Adapt<CustomerVM>());
        }
    }
}