using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using PokemonStore.ViewModels;
using System.Threading.Tasks;
using PokemonStore.Models;

namespace PokemonStore.Models
{
    public class ShoppingCartModel
    {
        private AppDbContext _db;
        public ShoppingCartModel(AppDbContext ctx)
        {
            _db = ctx;
        }
        public int AddOrder(Dictionary<string, object> items, string user)
        {
            int ShoppingCartId = -1;
            using (_db)
            {
                // we need a transaction as multiple entities involved
                using (var _trans = _db.Database.BeginTransaction())
                {
                    try
                    {
                        ShoppingCart ShoppingCart = new ShoppingCart();
                        ShoppingCart.UserId = user;
                        ShoppingCart.DateCreated = System.DateTime.Now;
                        ShoppingCart.Subtotal = 0.0M;
                        ShoppingCart.Tax = 0.0M;
                        ShoppingCart.TotalPrice = 0.0M;

                        // calculate the totals and then add the ShoppingCart row to the table
                        foreach (var key in items.Keys)
                        {
                            ProductViewModel item =
                            JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(items[key]));
                            if (item.Qty > 0)
                            {
                                ShoppingCart.Subtotal += item.MSRP * item.Qty;
                            }
                            ShoppingCart.Tax = ShoppingCart.Subtotal * 0.13M;
                            ShoppingCart.TotalPrice = ShoppingCart.Subtotal * 1.13M;
                        }
                        _db.ShoppingCarts.Add(ShoppingCart);
                        _db.SaveChanges();
                        // then add each item to the Cartitems table
                        foreach (var key in items.Keys)
                        {
                            ProductViewModel item =
                            JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(items[key]));
                            if (item.Qty > 0)
                            {
                                CartItem line = new CartItem();
                                line.Product = _db.Products.FirstOrDefault(p => p.Id == item.Id);

                                if (item.Qty <= item.QtyOnHand)
                                {
                                    CartItem cartItem = new CartItem();
                                    cartItem.QtySold += item.Qty;
                                    cartItem.ProductId = item.Id;
                                    cartItem.QtyOrdered = item.Qty;
                                    cartItem.QtySold = item.Qty;
                                    cartItem.QtyBackOrdered = 0;
                                    cartItem.ShoppingCartId = ShoppingCart.Id;
                                    _db.CartItems.Add(cartItem);

                                    _db.SaveChanges();

                                    line.Product.QtyOnHand -= item.Qty;
                                    item.QtyOnHand -= item.Qty;
                                    _db.Products.Update(line.Product);

                                    _db.SaveChanges();
                                }
                                else
                                {
                                    CartItem cartItem = new CartItem();
                                    cartItem.ProductId = item.Id;
                                    cartItem.QtyOrdered = item.Qty;
                                    cartItem.QtySold = item.QtyOnHand;
                                    cartItem.QtyBackOrdered += item.Qty - item.QtyOnHand;
                                    cartItem.ShoppingCartId = ShoppingCart.Id;
                                    _db.CartItems.Add(cartItem);

                                    _db.SaveChanges();

                                    line.Product.QtyOnHand = 0;
                                    item.QtyOnHand = 0;
                                    _db.Products.Update(line.Product);

                                    _db.SaveChanges();
                                }

                            }
                        }
                        // test trans by uncommenting out these 3 lines
                        //int x = 1;
                        //int y = 0;
                        //x = x / y;
                        _trans.Commit();
                        ShoppingCartId = ShoppingCart.Id;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _trans.Rollback();
                    }
                }
            }
            return ShoppingCartId;
        }

        public List<ShoppingCart> GetCarts(string uid)
        {
            try
            {
                return _db.ShoppingCarts.Where(t => t.UserId == uid).ToList();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        //public List<ShoppingCartViewModel> GetCartDetails(int cid, string uid)
        //{
        //    List<ShoppingCartViewModel> allDetails = new List<ShoppingCartViewModel>();
        //    // LINQ way of doing INNER JOINS
        //    var results = from sc in _db.Set<ShoppingCart>()
        //                  join ci in _db.Set<CartItem>() on sc.Id equals ci.ShoppingCartId
        //                  join p in _db.Set<Product>() on ci.ProductId equals p.Id
        //                  where (sc.UserId == uid && sc.Id == cid)
        //                  select new ShoppingCartViewModel
        //                  {
        //                      Id = sc.Id,
        //                      UserId = uid,
        //                      TotalPrice = sc.TotalPrice,
        //                      Tax = sc.Tax,
        //                      Subtotal = sc.Subtotal,
        //                      Description = p.Description,
        //                      Qty = ci.QtySold,
        //                      DateCreated = sc.DateCreated.ToString("yyyy/MM/dd - hh:mm tt")
        //                  };
        //    allDetails = results.ToList<ShoppingCartViewModel>();
        //    return allDetails;
        //}

        public async Task<List<ShoppingCartViewModel>> GetCartDetailsAsync(int tid, string uid)
        {
            List<ShoppingCartViewModel> allDetails = new List<ShoppingCartViewModel>();
            // LINQ way of doing INNER JOINS
            var results = from sc in _db.Set<ShoppingCart>()
                          join ci in _db.Set<CartItem>() on sc.Id equals ci.ShoppingCartId
                          join p in _db.Set<Product>() on ci.ProductId equals p.Id
                          where (sc.UserId == uid && sc.Id == tid)
                          select new ShoppingCartViewModel
                          {
                              Id = p.Id,
                              UserId = uid,
                              TotalPrice = sc.TotalPrice,
                              Tax = sc.Tax,
                              Subtotal = sc.Subtotal,
                              Product = p.ProductName,
                              MSRP = p.MSRP,
                              QtyS = ci.QtySold,
                              QtyO= ci.QtyOrdered,
                              QtyB = ci.QtyBackOrdered,  
                              Extended = ci.QtyOrdered * p.MSRP,
                              DateCreated = sc.DateCreated.ToString("yyyy/MM/dd - hh:mm tt")
                          };
            allDetails = await results.ToListAsync<ShoppingCartViewModel>();
            return allDetails;
        }
    }
}
