using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PokemonStore.Models
{
    public class ProductModel
    {
        /// <summary>
        ///  MenuItemModel - Model class representing a MenuItem
        ///     Author:     Evan Lauersen
        ///     Date:       Created: Feb 27, 2016
        ///     Purpose:    Model class to interface with DB and feed data to 
        ///                 Controller
        /// </summary>
        private AppDbContext _db;
        /// <summary>
        /// constructor should pass instantiated DbContext
        /// <summary>
        public ProductModel(AppDbContext context)
        {
            _db = context;
        }

        public string Decoder(string value)
        {
            Regex regex = new Regex(@"\\u(?<Value>[a-zA-Z0-9]{4})", RegexOptions.Compiled);
            return regex.Replace(value, "");
        }
        public List<Product> GetAll()
        {
           return _db.Products.ToList();
        }
        public List<Product> GetAllByBrand(int id)
        {
            //Console.WriteLine(_db.Products.Count());
            return _db.Products.Where(item => item.BrandId == id).ToList();
        }
        public List<Product> GetAllByBrandName(string catname)
        {
            Brand Brand = _db.Brands.First(catalogue => catalogue.Name == catname);
            return _db.Products.Where(item => item.BrandId == Brand.Id).ToList();
        }

        public Product GetById(string id)
        {
            Product prod = _db.Products.First(p => p.Id == id);
            return prod;
        }
    }
}
