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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IShipperService _shipperService;
        private readonly IStoreService _storeService;
        private readonly ICustomerService _customerService;
        private readonly IServiceService _serviceService;

        public AccountController(IAccountService accountService, IShipperService shipperService, IStoreService storeService, ICustomerService customerService, IServiceService serviceService)
        {
            _accountService = accountService;
            _shipperService = shipperService;
            _storeService = storeService;
            _customerService = customerService;
            _serviceService = serviceService;
        }

        [HttpPost]
        public ActionResult CreateAccount([FromBody] AccountCM model)
        {
            var result = _accountService.GetAccounts(a => a.User_Id.Equals(model.User_Id)).Where(s => !s.IsDelete);
            if (result.Count() > 0)
            {
                return BadRequest("User_Id Has Been Exist");
            }
            Account newAccount = model.Adapt<Account>();
            newAccount.IsDelete = false;
            _accountService.CreateAccount(newAccount);
            _accountService.Save();
            return Ok(201);
        }
        [HttpGet("GetById")]
        public ActionResult GetAccount(Guid Id)
        {
            return Ok(_accountService.GetAccount(Id).Adapt<AccountVM>());
        }
        [HttpGet("GetByUserId")]
        public ActionResult GetAccount(string Id)
        {
            return Ok(_accountService.GetAccounts(s => s.User_Id.Equals(Id)).Adapt<List<AccountVM>>());
        }
        [HttpDelete]
        public ActionResult DeleteAccount(Guid Id)
        {
            try
            {
                var result = _accountService.GetAccount(Id);
                if (result != null)
                {
                    if (_shipperService.GetShippers(s => s.AccountId == Id).Count() > 0)
                    {
                        var shipper = _shipperService.GetShippers(s => s.AccountId == Id).ToList();
                        _shipperService.DeleteShipper(shipper.FirstOrDefault());
                        _shipperService.Save();
                    }

                    if (_customerService.GetCustomers(c => c.AccountId == Id).Count() > 0)
                    {
                        var customer = _customerService.GetCustomers(s => s.AccountId == Id).ToList();
                        _customerService.DeleteCustomer(customer.FirstOrDefault());
                        _customerService.Save();
                    }

                    if (_storeService.GetStores(s => s.AccountId == Id).Count() > 0)
                    {
                        var store = _storeService.GetStores(s => s.AccountId == Id).ToList();

                        _storeService.DeleteStore(store.FirstOrDefault());
                        var listService = _serviceService.GetServices(s => s.StoreId == store[0].Id).ToList();
                        foreach (var item in listService)
                        {
                            _serviceService.DeleteService(item);
                        }
                        _serviceService.Save();
                        _storeService.Save();
                    }
                    _accountService.DeleteAccount(result);
                    _accountService.Save();
                    return Ok(201);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }
        [HttpPut]
        public ActionResult UpdateAccount([FromBody]AccountVM model)
        {
            var result = _accountService.GetAccount(model.Id);
            if (result == null)
            {
                return NotFound("Account Not Found");
            }
            _accountService.UpdateAccount(result);
            _accountService.Save();
            return Ok(201);
        }
    }
}