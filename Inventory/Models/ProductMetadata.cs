using Inventory.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Inventory.Models
{
    public class ProductMetadata
    {
    public int Id { get; set; }

    public string? Name { get; set; }
    
    public string? Description { get; set; }

    public int? CategoryID { get; set; }

    public int? StockQuantity { get; set; }

    public int? Price { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public bool IsDeleted {get; set;}
    }

    [MetadataType(typeof(ProductMetadata))]
    
    public partial class Product
    {
        public static Product Create(YourDbContextClassName db,Product product)
        {
            product.CreateDate = DateTime.Now;
            product.UpdateDate = DateTime.Now;
            product.IsDeleted = false;
            db.Products.Add(product);
            db.SaveChanges();

            return product;
        }

    public static List<Product> GetAll(YourDbContextClassName db)
    {
        List<Product> returnThis = db.Products.Where(q=> q.IsDeleted != true).Include(c=> c.Category ).ToList();
        return returnThis;
    }

     public static Product Update(YourDbContextClassName db, Product product)
        {
            product.UpdateDate = DateTime.Now;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return product;
        }
        
     public static Product GetById(YourDbContextClassName db,int id)
        {
            Product? returnThis = db.Products.Where(q => q.Id == id && q.IsDeleted != true).FirstOrDefault();
            return returnThis ?? new Product();
        }

        public static Product Delete(YourDbContextClassName db, int id)
        {
            Product product= GetById(db,id);
            product.IsDeleted = true;
            // db.Employees.Remove(employee); เป็นวิธีการลบแบบให้หายไปเลย
            db.Entry(product).State = EntityState.Modified; // Soft Delete
            db.SaveChanges();

            return product;
        }

    }
    
    

    
        
    
}