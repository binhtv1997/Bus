using System;
using System.Collections.Generic;
using System.Linq;
using GiatDo.Model;
using GiatDo.Service.Service;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace GIatDo.ViewModel
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;

        public CustomerController(ICustomerService customerService, IAccountService accountService)
        {
            _customerService = customerService;
            _accountService = accountService;
        }
        [HttpPost]
        public ActionResult CreateCustomer([FromBody] CustomerCM model) 
        {
            try
            {
                var checkAccount = _accountService.GetAccount(model.AccountId);
                if (checkAccount == null)
                {
                    return NotFound(400);
                }
                var result = _customerService.GetCustomers().Where(a => a.Phone == model.Phone);
                if (result.Count() > 0)
                {
                    return BadRequest("Phone Number Has Been Exsit");
                }
                Customer newCustomer = model.Adapt<Customer>();
                newCustomer.IsDelete = false;
                _customerService.CreateCustomer(newCustomer);
                _customerService.Save();
                return Ok(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpGet("GetById")]
        public ActionResult GetCustomer(Guid Id)
        {
            var result = _customerService.GetCustomer(Id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result.Adapt<CustomerVM>());
        }
        [HttpPut("UpdateCustomer")]
        public ActionResult UpdateCustomer([FromBody] CustomerVM model)
        {
            var result = _customerService.GetCustomer(model.Id);
            if (result == null)
            {
                return BadRequest();
            }
            Customer newCustomer = model.Adapt(result);
            _customerService.UpdateCustomer(newCustomer);
            _customerService.Save();
            return Ok(201);
        }
    
        [HttpGet("GetByAccountID")]
        public ActionResult GetCustomerByAccountId(Guid Id)
        {
            var result = _customerService.GetCustomers(c => c.AccountId == Id);
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result.Adapt<List<CustomerVM>>());
        }
        [HttpGet("GetByUserID")]
        public ActionResult GetCustomerByUserId(string Id)
        {
            var AccountId = _accountService.GetAccounts(a => a.User_Id.Equals(Id)).ToList();
            if (!AccountId.Any())
            {
                CreateAccount(Id);
                //get created account 
                var accountCreated = _accountService.GetAccounts(t => t.User_Id.Equals(Id)).ToList();
                Customer customer = CreateCustomer(accountCreated);
                return Ok(customer.Adapt<CustomerVM>());
            }
            var result = _customerService.GetCustomers(c => c.AccountId == AccountId[0].Id).ToList();
            if (!result.Any())
            {
                Customer customer = CreateCustomer(AccountId);
                return Ok(customer.Adapt<CustomerVM>());
            }
            return Ok(result[0].Adapt<CustomerVM>());
        }

         private Account CreateAccount(String UId)
        {
            Account account = new Account();
            account.User_Id = UId;
            _accountService.CreateAccount(account);
            _accountService.Save();
            return account;
        }

        private Customer CreateCustomer(List<Account> accountCreated)
        {
            Customer customer = new Customer();
            customer.Rate = 0;
            customer.AccountId = accountCreated[0].Id;
            _customerService.CreateCustomer(customer);
            _customerService.Save();
            return customer;
        }
    }
}