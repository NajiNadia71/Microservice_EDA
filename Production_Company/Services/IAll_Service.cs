using Production_Company.Services;
using Production_Company.DbContexts;
using Production_Company.Entities;
using Production_Company.DTO;
using Production_Company.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Production_Company.Services
{
    public interface IAll_Service
    {
     public IQueryable<ProductionDTO> GetProductionList(int id=0,int pageRow=5,int pageNumber = 0);
     public Response AddProduction(ProductionDTO productionDTO);
     
     
    }
}