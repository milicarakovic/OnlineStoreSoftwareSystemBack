using LibraryData;
using LibraryData.Interface;
using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Service
{
    public class LibraryProduct : IlibraryProduct
    {
        private ApplicationDbContext _db;

        public LibraryProduct(ApplicationDbContext db)
        {
            _db = db;
        }


        public IEnumerable<Product> GetAllProduct()
        {
            try
            {
                return _db.Product.Include(m => m.Manufacturer).Include(t => t.ProductType).Include(c=>c.Characteristics);
            }
            catch (Exception)
            {

                return null;
            }
        }
        public IEnumerable<Characteristics> GetAllCharacteristics(int IDProduct)
        {
            try
            {
                
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Product GetProduct(int ProductID)
        {
            try
            {
                return _db.Product.Include(m=>m.Manufacturer).Include(t=>t.ProductType).Include(c => c.Characteristics).SingleOrDefault(x => x.ID == ProductID);
                
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteProduct(int ProductID)
        {
            try 
            {
                var product = GetProduct(ProductID);
                
                if (product == null) return false;
                bool deleted = DeleteCharacteristicsForProduct(product.Characteristics);

                if (deleted)
                {
                    _db.Entry(product).State = EntityState.Deleted;
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(">>>>>>>" + e);
                return false;
            }
        }

        private bool DeleteCharacteristicsForProduct(IEnumerable<Characteristics> characteristics)
        {
            try
            {
                foreach (Characteristics characteristic in characteristics)
                {
                    _db.Entry(characteristic).State = EntityState.Deleted;
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

            
        }

        public bool AddProduct(Product product)
        {
            try 
            {
                var productType = _db.ProductType.SingleOrDefault(t => t.ID == product.ProductType.ID);
                var manufacturer = _db.Manufacturer.SingleOrDefault(m => m.ID == product.Manufacturer.ID);

                if (productType == null || manufacturer == null) return false;

                product.ProductType = productType;
                product.Manufacturer = manufacturer;
                _db.Add(product);
                _db.SaveChanges();
                return true;
            } 
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateProduct(int ProductID, Product product)
        {
            try
            {
                var manufacturer = _db.Manufacturer.SingleOrDefault(m => m.ID == product.Manufacturer.ID);
                var type = _db.ProductType.SingleOrDefault(t => t.ID == product.ProductType.ID);
               
                if (manufacturer == null || type == null) return false;             

                var pr = _db.Product
                    .Include(m => m.Manufacturer)
                    .Include(t => t.ProductType)
                    .Include(c=>c.Characteristics)
                    .SingleOrDefault(p => p.ID == ProductID);
                
                if (pr == null) return false;

                pr.Manufacturer = manufacturer;
                pr.ProductType = type;
                pr.Name = product.Name;
                pr.Price = product.Price;
                //pr.Characteristics = product.Characteristics;
                var oldChar = pr.Characteristics;
                pr.Characteristics = ManageCharacteristics(oldChar, product.Characteristics);
                _db.Entry(pr).CurrentValues.SetValues(product);
                _db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        private IEnumerable<Characteristics> ManageCharacteristics(IEnumerable<Characteristics> oldCharacteristics, IEnumerable<Characteristics> newCharacteristics)
        {
            List<Characteristics> old = oldCharacteristics.ToList();

            foreach(Characteristics c in oldCharacteristics){
                if(DoesContain(newCharacteristics, c) == false)
                {
                    old = DeleteCharacteristicsForProduct(old, c);
                }
                
            }
            foreach(Characteristics c in newCharacteristics)
            {
                if(DoesContain(old, c) == false)
                {
                    old.Add(c);
                }
            }
            return (IEnumerable<Characteristics>)old;
        }

        private List<Characteristics> DeleteCharacteristicsForProduct(List<Characteristics> old, Characteristics characteristic)
        {
            List<Characteristics> pom = old;
            foreach(Characteristics c in pom)
            {
                if(c.ID == characteristic.ID)
                {
                    pom.Remove(c);
                    return pom;
                }
            }
            return pom;
        }

        private bool DoesContain(IEnumerable<Characteristics> characteristics, Characteristics c)
        {
           foreach(Characteristics characteristic in characteristics)
            {
                if (characteristic.ID == c.ID) return true;
            }
            return false;
        }
    }
}
