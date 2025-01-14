//Important Noew : change this file for each service of entity
using Production_Company.Services;
using Production_Company.DbContexts;
using Production_Company.Entities;
using Production_Company.DTO;
using Production_Company.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Production_Company.Services
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

        public IQueryable<ProductionDTO> GetProductionList(int id = 0, int pageRow = 5, int pageNumber = 0)
        {
            try
            {
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
                        ProductTypeName = item.ProductionType.Title.ToString()
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

    }
}