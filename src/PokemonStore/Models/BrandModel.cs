﻿using System.Collections.Generic;
using System.Linq;

namespace PokemonStore.Models
{
    public class BrandModel
    {
        private AppDbContext _db;
        public BrandModel(AppDbContext ctx)
        {
            _db = ctx;
        }
        public List<Brand> GetAll()
        {
            return _db.Brands.ToList<Brand>();
        }
        public string GetName(int id)
        {
            Brand brand = _db.Brands.First(c => c.Id == id);
            return brand.Name;
        }
    }
}
