using Logic.DataTableObjects;
using System.Collections.Generic;

namespace Logic.Services.Interfaces
{
    public interface ICatalogService
    {
        public List<SushiDTO> GetSushiesCatalog();
        public decimal GetTotalPrice(Dictionary<string, int> sushies);
        public void ParseCatalog();
    }
}