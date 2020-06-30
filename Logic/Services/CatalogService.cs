using Logic.DataTableObjects;
using System.Collections.Generic;
using DataAccess;
using DataAccess.Entities;
using AutoMapper;
using System.Linq;
using Logic.Services.Interfaces;
using SushiHouseParser;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Logic.Services
{
    public class CatalogService : ICatalogService
    {
        readonly IUnitOfWork _db;
        readonly IMapper _mapper;
        readonly IParser _parser;
        public CatalogService(IUnitOfWork uow, 
            IMapper mapper, 
            IParser parser)
        {
            _db = uow;
            _mapper = mapper;
            _parser = parser;
        }

        public List<SushiDTO> GetSushiesCatalog()
        {
            var sushies = _mapper.Map<IEnumerable<SushiDTO>>(_db.SushiRepository.GetAll());
            return sushies.ToList();
        }

        public void ParseCatalog()
        {
            var parsedSushi = _parser.ParseCatalog();
            var sushi = _mapper.Map<List<Sushi>>(parsedSushi);
            _db.SushiRepository.CreateRange(sushi);
            _db.SaveChanges();
        }

        public decimal GetTotalPrice(Dictionary<string, int> sushies)
        {
            var catalog = _db.SushiRepository.GetAll().ToList();
            return catalog.Where(q => sushies.Keys.Contains(q.Name))
                                .Sum(q => q.Price * sushies[q.Name]);
        }
    }
}