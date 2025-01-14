using Microsoft.AspNetCore.Mvc;
using Production_Company.DTO;
using Production_Company.ViewModels;
using Production_Company.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
//using AutoMapper.QueryableExtensions;
namespace Production_Company.Controllers
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
       
       
       
       
    }
}
