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
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IStoreService _storeService;

        public ServiceController(IServiceService serviceService, IServiceTypeService serviceTypeService, IStoreService storeService)
        {
            _serviceService = serviceService;
            _serviceTypeService = serviceTypeService;
            _storeService = storeService;
        }

        [HttpGet("GetById")]
        public ActionResult GetService(Guid Id)
        {
            return Ok(_serviceService.GetService(Id).Adapt<ServiceVM>());
        }

        [HttpGet("GetByStore")]
        public ActionResult GetByStore(Guid StoreId)
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
            return Ok(listServiceType);
        }

        //[HttpGet("GetAll")]
        //public ActionResult GetAllService()
        //{
        //    return Ok(_serviceService.GetServices(s => !s.IsDelete).Adapt<List<ServiceVM>>());
        //}

        [HttpPost("CreateService")]
        public ActionResult CreateService([FromBody] ServiceCM model)
        {
            if (_serviceTypeService.GetServiceType(model.ServiceTypeId) == null)
            {
                return NotFound(401);
            }
            if (_storeService.GetStore(model.StoreId) == null)
            {
                return NotFound(401);
            }

            Services _services = model.Adapt<Services>();
            _services.IsDelete = false;
            _serviceService.CreateService(_services);
            _serviceService.Save();
            return Ok(201);
        }
        [HttpPut("UpdateService")]
        public ActionResult UpdateServiec([FromBody] ServiceUM model)
        {
            if (_serviceTypeService.GetServiceType(model.ServiceTypeId) == null)
            {
                return NotFound(401);
            }
            if (_storeService.GetStore(model.StoreId) == null)
            {
                return NotFound(401);
            }
            var service = _serviceService.GetService(model.Id);
            if (service == null)
            {
                return NotFound(401);
            }
            Services UServices = model.Adapt(service);
            _serviceService.UpdateService(UServices);
            _serviceService.Save();
            return Ok(201);
        }
        [HttpDelete("Delete")]
        public ActionResult DeleteService(Guid Id)
        {
            var service = _serviceService.GetService(Id);
            if (service == null)
            {
                return NotFound(401);
            }
            _serviceService.DeleteService(service);
            _serviceService.Save();
            return Ok(201);
        }
    }
}