using Inventory.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Inventory.Models
{
    public class CatigoriesMetadata
    {
   public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }
    }

    [MetadataType(typeof(CatigoriesMetadata))]
    
    public partial class Catigory
    {
        public static Catigory Create(YourDbContextClassName db,Catigory catigory)
        {
            catigory.CreateDate = DateTime.Now;
            catigory.UpdateDate = DateTime.Now;
            catigory.IsDeleted = false;
            db.Catigories.Add(catigory);
            db.SaveChanges();

            return catigory;
        }

         public static List<Catigory> GetAll(YourDbContextClassName db)
        {
        List<Catigory> returnThis = db.Catigories.Where(q=> q.IsDeleted != true).ToList();
        return returnThis;
        }


    }

    
        
    
}