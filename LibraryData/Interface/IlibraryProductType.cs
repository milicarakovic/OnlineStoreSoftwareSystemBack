using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Interface
{
    public interface IlibraryProductType
    {
        IEnumerable<ProductType> GetAllProductType();
        bool AddProductType(ProductType type);
        bool DeleteProductType(int ProductTypeID);
    }
}
