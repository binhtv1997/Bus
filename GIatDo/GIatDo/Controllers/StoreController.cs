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
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IAccountService _accountService;
        private readonly IOrderSService _orderSService;
        private readonly IOrderService _orderService;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IServiceService _serviceService;
        private readonly ICustomerService _customerService;

        public StoreController(IStoreService storeService, IAccountService accountService, IOrderSService orderSService, IOrderService orderService, IServiceTypeService serviceTypeService, IServiceService serviceService, ICustomerService customerService)
        {
            _storeService = storeService;
            _accountService = accountService;
            _orderSService = orderSService;
            _orderService = orderService;
            _serviceTypeService = serviceTypeService;
            _serviceService = serviceService;
            _customerService = customerService;
        }

        [HttpPost]
        public ActionResult CreateStore([FromBody]StoreCM model)
        {
            try
            {
                var checkAccount = _accountService.GetAccount(model.AccountId.Value);
                if (checkAccount == null)
                    return NotFound("Not Found");
                var result = _storeService.GetStores(s => s.Phone == model.Phone);
                if (result.Count() > 0)
                {
                    return BadRequest("Phone has been exsit");
                }
                Store newStore = model.Adapt<Store>();
                newStore.IsDelete = false;
                newStore.IsActive = true;
                _storeService.CreateStore(newStore);
                _storeService.Save();
                return Ok(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("GetById")]
        public ActionResult GetStoreById(Guid Id)
        {
            return Ok(_storeService.GetStore(Id).Adapt<StoreVM>());
        }
        //[HttpGet("GetAll")]
        //public ActionResult GetAll()
        //{
        //    return Ok(_storeService.GetStores(s => !s.IsDelete).Where(s1 => s1.IsActive).Adapt<List<StoreVM>>());
        //}
        [HttpPut("UpdateStore")]
        public ActionResult Update([FromBody]StoreUM model)
        {
            try
            {
                _storeService.UpdateStore(model.Adapt<Store>());
                _storeService.Save();
                return Ok(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("GetAccountId")]
        public ActionResult GetStoreByUserId(Guid Id)
        {
            return Ok(_storeService.GetStores().Where(s => s.AccountId == Id).ToList()[0].Adapt<StoreVM>());
        }
        [HttpGet("GetStoreByRate")]
        public ActionResult GetStoreByRate(string Id)
        {
            List<Services> listService;
            if (Id.Equals("0"))
            {
                listService = _serviceService.GetServices(s => !s.IsDelete).ToList();
            }
            else
            {
                listService = _serviceService.GetServices(s => s.ServiceTypeId.ToString().Equals(Id)).Where(s => !s.IsDelete).ToList();
            }
            List<Store> list = new List<Store>();
            foreach (var item in listService)
            {
                var Store = _storeService.GetStore(item.StoreId);
                bool flag = false;
                foreach (var items in list)
                {
                    if (items.Id == item.StoreId)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    list.Add(Store);
                }
            }
            var result = list.OrderByDescending(s => s.Rate).Take(5);
            return Ok(result.Adapt<List<StoreVM>>());
        }
        [HttpGet("GetByUserID")]
        public ActionResult GetCustomerByAccountId(string Id)
        {
            var AccountId = _accountService.GetAccounts(a => a.User_Id.Equals(Id)).ToList();
            if (!AccountId.Any())
            {
                return NotFound();
            }
            var result = _storeService.GetStores(c => c.AccountId == AccountId[0].Id).ToList();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result[0].Adapt<StoreVM>());
        }


        [HttpGet("StoreUserHasUse")]
        public ActionResult GetStore(string CustomerId, string ServiceTypeId)
        {
            var order = _orderService.GetOrders(t => !t.IsDelete);
            var orderService = _orderSService.GetOrderServices(t => !t.IsDelete);
            var service = _serviceService.GetServices(t => !t.IsDelete);
            var store = _storeService.GetStores(t => !t.IsDelete);
            var customer = _customerService.GetCustomers(t => !t.IsDelete);
            var serviceType = _serviceTypeService.GetServiceTypes(t => !t.IsDelete);
            if (ServiceTypeId.Equals("0"))
            {
                var temp = from st in store
                           join s in service on st.Id equals s.StoreId
                           join se in serviceType on s.ServiceTypeId equals se.Id
                           join os in orderService on s.Id equals os.ServiceId
                           join o in order on os.OrderId equals o.Id
                           join c in customer on o.CustomerId equals c.Id
                           where c.Id.ToString().Equals(CustomerId)
                           select st;
                return Ok(temp.Distinct().Adapt<List<StoreVM>>());
            }
            else
            {
                var temp = from st in store
                           join s in service on st.Id equals s.StoreId
                           join se in serviceType on s.ServiceTypeId equals se.Id
                           join os in orderService on s.Id equals os.ServiceId
                           join o in order on os.OrderId equals o.Id
                           join c in customer on o.CustomerId equals c.Id
                           where se.Id.ToString().Equals(ServiceTypeId)
                           where c.Id.ToString().Equals(CustomerId)
                           select st;
                return Ok(temp.Distinct().Adapt<List<StoreVM>>());
            }
        }

        [HttpGet("SearchStoreByName")]
        public ActionResult SearchStoreByName(String Name)
        {
            var stores = _storeService.GetStores(s => s.Name.Contains(Name));
            return Ok(stores.Adapt<List<StoreVM>>());
        }

        [HttpGet("GetNearbyStores")]
        public ActionResult GetNearbyStores(string serviceId, double latitude, double longitude, double distances)
        {
            var store = _storeService.GetStores(s => !s.IsDelete);
            var service = _serviceService.GetServices(t => !t.IsDelete);
            var serviceType = _serviceTypeService.GetServiceTypes(t => !t.IsDelete);
            if (serviceId.Equals("0"))
            {
                var temp = from t in store
                           let distance = (6371 * Math.Acos(Math.Cos(DegreeToRadian(t.Latitude)) * Math.Cos(DegreeToRadian(latitude))
                           * Math.Cos(DegreeToRadian(longitude) - DegreeToRadian(t.Longitude)) + Math.Sin(DegreeToRadian(t.Latitude)) * Math.Sin(DegreeToRadian(latitude))))
                           join s in service on t.Id equals s.StoreId
                           join se in serviceType on s.ServiceTypeId equals se.Id
                           where distance < distances
                           orderby distance ascending
                           select t;
                return Ok(temp.Distinct().Adapt<List<StoreVM>>());
            }
            else
            {
                var temp = from t in store
                           let distance = (6371 * Math.Acos(Math.Cos(DegreeToRadian(t.Latitude)) * Math.Cos(DegreeToRadian(latitude))
                           * Math.Cos(DegreeToRadian(longitude) - DegreeToRadian(t.Longitude)) + Math.Sin(DegreeToRadian(t.Latitude)) * Math.Sin(DegreeToRadian(latitude))))
                           join s in service on t.Id equals s.StoreId
                           join se in serviceType on s.ServiceTypeId equals se.Id
                           where distance < distances
                           && se.Id.ToString().Equals(serviceId)
                           orderby distance ascending
                           select t;
                return Ok(temp.Distinct().Adapt<List<StoreVM>>());
            }

        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        [HttpGet("GetStoreByIdWithService")]
        public ActionResult GetStoreByIdWithService(Guid StoreId)
        {
            var result = _serviceService.GetServices(s => s.StoreId == StoreId);
            List<ServiceVM> listService = result.Adapt<List<ServiceVM>>();
            List<ServiceTypeVM> listServiceType = new List<ServiceTypeVM>();
            if (listService.Any())
            {
                foreach (ServiceVM services in listService)
                {
                    ServiceTypeVM serviceTypes = _serviceTypeService.GetServiceType(services.ServiceTypeId).Adapt<ServiceTypeVM>();
                    bool flag = false;
                    foreach (ServiceTypeVM temp in listServiceType)
                    {
                        if (temp.Id == serviceTypes.Id)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        serviceTypes.listService = new List<ServiceVM>();
                        serviceTypes.listService.Add(services);
                        listServiceType.Add(serviceTypes);
                    }
                    else
                    {
                        foreach (ServiceTypeVM temp in listServiceType)
                        {

                            if (temp.Id == serviceTypes.Id)
                            {
                                temp.listService.Add(services.Adapt<ServiceVM>());
                            }
                        }

                    }
                }
            }
            var stores = _storeService.GetStore(StoreId);
            StoreVM store = stores.Adapt<StoreVM>();
            store.ServiceTypes = listServiceType;
            return Ok(store);
        }
    }
}