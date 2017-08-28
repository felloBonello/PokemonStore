using Microsoft.AspNet.Mvc;
using PokemonStore.Models;
using PokemonStore.ViewModels;
using Microsoft.AspNet.Http;
using System.Collections.Generic;
using System;
using PokemonStore.Utils;
namespace PokemonStore.Controllers
{
    public class BrandController : Controller
    {
        AppDbContext _db;
        public BrandController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            CatalogueViewModel vm = new CatalogueViewModel();
            // only build the catalogue once
            if (HttpContext.Session.GetObject<List<Brand>>(SessionVars.Brands) == null)
            {
                try
                {
                    BrandModel brandModel = new BrandModel(_db);
                    // now load the Brands
                    List<Brand> Brands = brandModel.GetAll();
                    HttpContext.Session.SetObject(SessionVars.Brands, Brands);
                    vm.SetBrands(HttpContext.Session.GetObject<List<Brand>>(SessionVars.Brands));
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Catalogue Problem - " + ex.Message;
                }
            }
            else
            {
                vm.SetBrands(HttpContext.Session.GetObject<List<Brand>>(SessionVars.Brands));
            }

            GetAllProducts();

            return View(vm);
        }
        public IActionResult SelectBrand(CatalogueViewModel vm)
        {
            BrandModel brandModel = new BrandModel(_db);
            ProductModel productModel = new ProductModel(_db);
            List<Product> items = productModel.GetAllByBrand(vm.BrandId);
            List<ProductViewModel> vms = new List<ProductViewModel>();

            if (items.Count > 0)
            {
                foreach (Product item in items)
                {
                    ProductViewModel mvm = new ProductViewModel();
                    mvm.Qty = 0;
                    mvm.BrandId = item.BrandId;
                    mvm.BrandName = brandModel.GetName(item.BrandId);
                    mvm.ProductName = item.ProductName;

                    mvm.Description = item.Description;
                    mvm.GraphicName = item.GraphicName;
                    mvm.CostPrice = item.CostPrice;
                    mvm.MSRP = item.MSRP;
                    mvm.QtyOnHand = item.QtyOnHand;
                    mvm.QtyOnBackOrder = item.QtyOnBackOrder;

                    mvm.Id = item.Id;
                    mvm.HP = item.HP;
                    mvm.ATTACK = item.Attack;
                    mvm.DEFENCE = item.Defence;
                    mvm.SPECIALATTACK = item.SpecialAttack;
                    mvm.SPECIALDEFENCE = item.SpecialDefence;
                    mvm.SPEED = item.Speed;
                    vms.Add(mvm);
                }
                ProductViewModel[] myCat = vms.ToArray();
                HttpContext.Session.SetObject(SessionVars.Catalogue, myCat);
            }
            vm.SetBrands(HttpContext.Session.GetObject<List<Brand>>(SessionVars.Brands));
            return View("Index", vm);
        }
        [HttpPost]
        public ActionResult SelectItem(CatalogueViewModel vm)
        {
            Dictionary<string, object> ShoppingCart;
            if (HttpContext.Session.GetObject<Dictionary<string, Object>>(SessionVars.ShoppingCart) == null)
            {
                ShoppingCart = new Dictionary<string, object>();
            }
            else
            {
                ShoppingCart = HttpContext.Session.GetObject<Dictionary<string, object>>(SessionVars.ShoppingCart);
            }
            ProductViewModel[] catalogue = HttpContext.Session.GetObject<ProductViewModel[]>(SessionVars.Catalogue);
            String retMsg = "";
            foreach (ProductViewModel item in catalogue)
            {
                if (item.Id == vm.Id)
                {
                    if (vm.Qty > 0) // update only selected item
                    {
                        item.Qty = vm.Qty;
                        retMsg = vm.Qty + " - item(s) Added!";
                        ShoppingCart[item.Id] = item;
                    }
                    else
                    {
                        item.Qty = 0;
                        ShoppingCart.Remove(item.Id);
                        retMsg = "item(s) Removed!";
                    }
                    vm.BrandId = item.BrandId;
                    break;
                }
            }
            ViewBag.AddMessage = retMsg;
            HttpContext.Session.SetObject(SessionVars.ShoppingCart, ShoppingCart);
            vm.SetBrands(HttpContext.Session.GetObject<List<Brand>>(SessionVars.Brands));
            return View("Index", vm);
        }

        public void GetAllProducts()
        {
            BrandModel brandModel = new BrandModel(_db);
            ProductModel productModel = new ProductModel(_db);
            List<Product> items = productModel.GetAll(); 
            List<ProductViewModel> pvml = new List<ProductViewModel>();
            if (items.Count > 0) 
            {
                foreach (Product item in items) 
                {
                    ProductViewModel pvm = new ProductViewModel();
                    pvm.Qty = 0;
                    pvm.BrandId = item.BrandId;
                    pvm.BrandName = brandModel.GetName(item.BrandId);
                    pvm.ProductName = item.ProductName;

                    pvm.Description = item.Description;
                    pvm.GraphicName = item.GraphicName;
                    pvm.CostPrice = item.CostPrice;
                    pvm.MSRP = item.MSRP;
                    pvm.QtyOnHand = item.QtyOnHand;
                    pvm.QtyOnBackOrder = item.QtyOnBackOrder;

                    pvm.Id = item.Id;
                    pvm.HP = item.HP;
                    pvm.ATTACK = item.Attack;
                    pvm.DEFENCE = item.Defence;
                    pvm.SPECIALATTACK = item.SpecialAttack;
                    pvm.SPECIALDEFENCE = item.SpecialDefence;
                    pvm.SPEED = item.Speed;
                    pvml.Add(pvm);
                }
                ProductViewModel[] myCat = pvml.ToArray(); 
                HttpContext.Session.SetObject("catalogue", myCat); 
            }
        }
    }
}