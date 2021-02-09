using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Interface
{
    public interface IlibraryManufacturer
    {
        IEnumerable<Manufacturer> GetAllManufacturer();
        bool AddManufacturer(Manufacturer manufacturer);
        bool DeleteManufacturer(int ID);
    }
}
