//Important Noew : change this file for each service of entity
using Advertisement_Company.Services;
using Advertisement_Company.DbContexts;
using Advertisement_Company.Entities;
using Advertisement_Company.DTO;
using Advertisement_Company.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Advertisement_Company.Services
{
    public class All_Service : IAll_Service
    {
        private readonly SqliteDbContext _dbContext;
        private readonly ILogger<All_Service> _logger;
        public All_Service(SqliteDbContext dbContext, ILogger<All_Service> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
     
        public IQueryable<ProductionDTO> GetProductionList(int id=0,int pageRow=5,int pageNumber = 0)
        {
            try{
                _logger.LogInformation("GetProductionList");
                var Productions = _dbContext.Productions
                    .Select(item => new ProductionDTO
                    {
                        Id = item.Id,
                        Count = item.Count,
                        Title = item.Title,
                        ProductionTypeId = item.ProductionTypeId,
                        CreateDate = item.CreateDate.ToString(),
                        Comment = item.Comment,
                       
                    })
                    .OrderByDescending(i => i.CreateDate).Skip(pageRow * pageNumber)
                       .Take(pageRow).AsQueryable();
                if (id != 0)
                {
                    Productions = Productions.Where(p => p.Id == id);
                }
                return Productions;

            }

            catch (Exception ex)
            {
                _logger.LogError("GetProductionList", ex);
                return null;
            }

        }
        public Response AddProduction(ProductionDTO productionDTO)
        {
            var result = new Response();
            try
            {
                var production = new Production
                {
                    Count = productionDTO.Count,
                    Title = productionDTO.Title,
                    ProductionTypeId = productionDTO.ProductionTypeId,
                    CreateDate = DateTime.Now,
                    Comment = productionDTO.Comment
                };
                _dbContext.Productions.Add(production);
                _dbContext.SaveChanges();
                result.Message = "Production Added";
                result.Status = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "NotOK" + ex.Message;
                result.Status = false;
                return result;
            }
        }
        public IQueryable<AdDTO> GetAdList(int id = 0, int pageRow = 5, int pageNumber = 0)
        {
            try {
                _logger.LogInformation("GetAd");
                var Ads = _dbContext.Ads
                    .Select(item => new AdDTO
                    {
                        Id = item.Id,
                        Title = item.Title,
                        ProductionId = item.ProductionId,
                        ProductionName = item.Production.Title.ToString(),
                        CreateDate = item.CreateDate.ToString(),
                        Text = item.Text
                    })
                    .OrderByDescending(i => i.CreateDate).Skip(pageRow * pageNumber)
                       .Take(pageRow).AsQueryable();
                if ( id != 0)
                {
                    Ads = Ads.Where(p => p.Id == id);
                }
                return Ads;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAd", ex);
                return null;
            }
           
        }
        public Response AddAd(AdDTO adDTO)
        {
            var result = new Response();
            try
            {
                var ad = new Ad
                {
                    Title = adDTO.Title,
                    ProductionId = adDTO.ProductionId,
                    CreateDate = DateTime.Now,
                    Text = adDTO.Text
                };
                _dbContext.Ads.Add(ad);
                _dbContext.SaveChanges();
                result.Message = "Ad Added";
                result.Status = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "NotOK" + ex.Message;
                result.Status = false;
                return result;
            }
        }
   
    }
}