using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FireDrink.Models;
using System.Data;

namespace FireDrink.Models
{
    public class CarritodeCompras
    {
        FireDrinkBDContext storeBD = new FireDrinkBDContext();

        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public double igvfijo = 0.16;

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public static CarritodeCompras GetCart(HttpContextBase context)
        {
            var cart = new CarritodeCompras();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public static CarritodeCompras GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }


        public void AddToCart(PRODUCTO producto,int cantidad)
        {
            var cartItem = storeBD.CART.SingleOrDefault
                (c => c.CartId == ShoppingCartId && c.COD_PRO == producto.COD_PRO);

            if (cartItem == null)
            {
                cartItem = new CART
                {
                    COD_PRO = producto.COD_PRO,
                    CartId = ShoppingCartId,
                    Cantidad = cantidad,
                    FechaPedido = DateTime.Now
                };
                storeBD.CART.Add(cartItem);
            }
            else
            {
                cartItem.Cantidad++;
            }
            storeBD.SaveChanges();
        }

        public void DescontarToCart(PRODUCTO producto, int cantidad, int idCart)
        {
            var cartItem = storeBD.CART.SingleOrDefault
                (c => c.CartId == ShoppingCartId && c.COD_PRO == producto.COD_PRO);

            if (cartItem == null)
            {
                cartItem = new CART
                {
                    COD_PRO = producto.COD_PRO,
                    CartId = ShoppingCartId,
                    Cantidad = cantidad,
                    FechaPedido = DateTime.Now
                };
                storeBD.CART.Add(cartItem);
            }
            else
            {
                if (cartItem.Cantidad > 1)
                    cartItem.Cantidad--;
                else
                    EliminarDelCarrito(idCart);
            }
            storeBD.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            var cartItem = storeBD.CART.Single
                (cart => cart.CartId == ShoppingCartId && cart.COD_CART == id);
            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Cantidad > 1)
                {
                    cartItem.Cantidad--;
                    itemCount = cartItem.Cantidad;
                }
                else
                {
                    storeBD.CART.Remove(cartItem);
                }
                storeBD.SaveChanges();
            }

            return itemCount;
        }

        public int EliminarDelCarrito(int id)
        {
            var cartItem = storeBD.CART.Single
                (cart => cart.CartId == ShoppingCartId && cart.COD_CART == id);
            int itemCount = 0;

            storeBD.CART.Remove(cartItem);

            itemCount = storeBD.SaveChanges();


            return itemCount;
        }


        public void EmptyCart()
        {
            var cartItems = storeBD.CART.Where(cart => cart.CartId == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                storeBD.CART.Remove(cartItem);
            }
            storeBD.SaveChanges();
        }

        public List<CART> GetCartItems()
        {
            return storeBD.CART.Where(cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            int? count = (from cartItems in storeBD.CART
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Cantidad).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartItems in storeBD.CART
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Cantidad * cartItems.PRODUCTO.PRE_PRO).Sum();
            return total ?? 0;
        }
        
        public PEDIDO CreateOrder(PEDIDO pedido)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            foreach (var item in cartItems)
            {
                var detallePedido = new pedido_productos
                {
                    idproducto = item.COD_PRO,
                    idpedido = pedido.idpedido,
                    precio = item.PRODUCTO.PRE_PRO,
                    cantidad = item.Cantidad,
                    total = item.Cantidad * item.PRODUCTO.PRE_PRO,

                };
                orderTotal +=(decimal) (item.Cantidad * item.PRODUCTO.PRE_PRO);
                storeBD.pedido_productos.Add(detallePedido);
            }
           pedido.subTotal = orderTotal;
           pedido.igv = (decimal)igvfijo * orderTotal;
           pedido.totalpedido = pedido.subTotal + pedido.igv;
           
            storeBD.SaveChanges();
            EmptyCart();
            return pedido;

        }
        
        public string tablaDetalle()
        {
            string carritoTable = "";
            var cartItems = GetCartItems();
            foreach (var item in cartItems)
            {
                carritoTable += "<tr> <td>" + item.PRODUCTO.DES_PRO + "</td> <td>S/. " + item.PRODUCTO.PRE_PRO + " </td> <td>" + item.Cantidad + "</td> <td> S/. " + item.Cantidad * item.PRODUCTO.PRE_PRO + "</td> </tr>";
            }
            return carritoTable;
        }

    }
}