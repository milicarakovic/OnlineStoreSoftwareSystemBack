using LibraryData;
using LibraryData.Interface;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryService.Service
{
    public class LibraryManufacturer : IlibraryManufacturer
    {
        private ApplicationDbContext _db;

        public LibraryManufacturer(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool AddManufacturer(Manufacturer manufacturer)
        {
            try
            {
                _db.Manufacturer.Add(manufacturer);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteManufacturer(int ID)
        {
            try
            {
                var man = _db.Manufacturer.Where(t => t.ID == ID);
                if (man == null) return false;

                _db.Entry(man).State = EntityState.Deleted;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Manufacturer> GetAllManufacturer()
        {
            try 
            {
                return _db.Manufacturer;
            } 
            catch
            {
                return null;
            }
        }
    }
}
