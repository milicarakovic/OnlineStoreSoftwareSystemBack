using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryData;
using LibraryData.Interface;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Service
{
    public class LibraryProductType : IlibraryProductType
    {
        private ApplicationDbContext _db;

        public LibraryProductType(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool AddProductType(ProductType type)
        {
            try
            {
                _db.ProductType.Add(type);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteProductType(int ProductTypeID)
        {
            try
            {
                var type = _db.ProductType.Where(t => t.ID == ProductTypeID);
                if (type == null) return false;

                _db.Entry(type).State = EntityState.Deleted;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<ProductType> GetAllProductType()
        {
            try
            {
                return _db.ProductType;
            }
            catch
            {
                return null;
            }
        }
    }
}
