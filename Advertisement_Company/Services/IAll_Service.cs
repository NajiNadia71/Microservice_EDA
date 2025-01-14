using Advertisement_Company.Services;
using Advertisement_Company.DbContexts;
using Advertisement_Company.Entities;
using Advertisement_Company.DTO;
using Advertisement_Company.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Advertisement_Company.Services
{
    public interface IAll_Service
    {
     public IQueryable<ProductionDTO> GetProductionList(int id=0,int pageRow=5,int pageNumber = 0);
     public Response AddProduction(ProductionDTO productionDTO);
     public IQueryable<AdDTO> GetAdList(int id = 0, int pageRow = 5, int pageNumber = 0);
     public Response AddAd(AdDTO adDTO);
     
    }
}