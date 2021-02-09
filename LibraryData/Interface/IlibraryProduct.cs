using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Interface
{
    public interface IlibraryProduct
    {
        IEnumerable<Product> GetAllProduct();
        Product GetProduct(int ProductID);
        bool DeleteProduct(int ProductID);
        IEnumerable<Characteristics> GetAllCharacteristics(int IDProduct);
        bool AddProduct(Product product);

        bool UpdateProduct(int ProductID, Product product);
    }
}
