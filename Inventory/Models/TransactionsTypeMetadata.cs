using Inventory.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Inventory.Models
{
    public class TransactionTypeMetadata
    {

    public int? Id { get; set; }

    public string? Name { get; set; }
    }

    [MetadataType(typeof(TransactionTypeMetadata))]
    
    public partial class TransactionType
    {
        public static TransactionType Create(YourDbContextClassName db,TransactionType transactiontype)
        {
            transactiontype.CreateDate = DateTime.Now;
            transactiontype.UpdateDate = DateTime.Now;
            transactiontype.IsDeleted = false;
            db.TransactionTypes.Add(transactiontype);
            db.SaveChanges();

            return transactiontype;
        }

         public static List<TransactionType> GetAll(YourDbContextClassName db)
    {
        List<TransactionType> returnThis = db.TransactionTypes.Where(q=> q.IsDeleted != true).Include(p => p.Transactions).ToList();
        
        
        return returnThis;
    }

    public static TransactionType Delete(YourDbContextClassName db, int id)
        {
            TransactionType transactionType= GetById(db,id);
            transactionType.IsDeleted = true;
            // db.Employees.Remove(employee); เป็นวิธีการลบแบบให้หายไปเลย
            db.Entry(transactionType).State = EntityState.Modified; // Soft Delete
            db.SaveChanges();

            return transactionType;
        }

    public static TransactionType GetById(YourDbContextClassName db,int id)
        {
            TransactionType? returnThis = db.TransactionTypes.Where(q => q.Id == id && q.IsDeleted != true).FirstOrDefault();
            return returnThis ?? new TransactionType();
        }
    }

    
        
    
}