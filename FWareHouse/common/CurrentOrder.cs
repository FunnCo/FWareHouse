using FWareHouse.common.entity;
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

        private List<StoredProduct> orderedProducts;
        public void addProduct(StoredProduct product)
        {
            orderedProducts.Add(product);
        }

        public void deleteProduct(int position)
        {
            orderedProducts.RemoveAt(position);
        }

        public List<StoredProduct> getOrder()
        {
            return orderedProducts;
        }

        private CurrentOrder()
        {
            orderedProducts = new List<StoredProduct>();
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
