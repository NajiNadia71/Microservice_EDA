using Microsoft.AspNetCore.Mvc;
using Advertisement_Company.DTO;
using Advertisement_Company.ViewModels;
using Advertisement_Company.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
//using AutoMapper.QueryableExtensions;
namespace Advertisement_Company.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AllController : ControllerBase
    {
        private readonly ILogger<AllController> _logger;
        
        private readonly IAll_Service all_Service;
        public AllController(ILogger<AllController> logger, IAll_Service _all_Service)
        {
            _logger = logger;
            all_Service = _all_Service;
        }
        [HttpGet("Test")]
        public IEnumerable<int> Test()
        {
            int[] arr = new int[5];
            for (int i = 0; i < 5; i++)
            {
                arr[i] = i;
            }
            return arr;
        }

        [HttpGet("GetProductions")]
        public IEnumerable<ProductionDTO> GetProductionList(int id=0,int pageRow = 5, int pageNumber = 0)
        {
            return all_Service.GetProductionList(id, pageRow, pageNumber);
        }
       
       
        [HttpGet("GetAds")]
        public IEnumerable<AdDTO> GetProductList(int id = 0, int pageRow = 5, int pageNumber = 0)
        {
            return all_Service.GetAdList(id,pageRow, pageNumber);
        }
       
    }
}
