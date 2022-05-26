using FWareHouse.common.entity;
using FWareHouse.common.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWareHouse.common
{

    class CurrentOrder
    {
        private static CurrentOrder instance;

        private List<OrderedProductModel> orderedProducts;
        public void addProduct(OrderedProductModel product)
        {
            orderedProducts.Add(product);
            Console.WriteLine("product added");
        }

        public void deleteProduct(int position)
        {
            orderedProducts.RemoveAt(position);
        }

        public List<OrderedProductModel> getOrder()
        {
            return orderedProducts;
        }

        private CurrentOrder()
        {
            orderedProducts = new List<OrderedProductModel>();
        }

        public static CurrentOrder getInstance()
        {
            if(instance == null)
            {
                instance = new CurrentOrder();
            }
            return instance;
        }

    }
}
