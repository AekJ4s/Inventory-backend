using Inventory.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Inventory.Models
{
    public class LoginMetadata
    {

     public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    }

    public class LoginRequest{
    

    public string? Username { get; set; }

    public string? Password { get; set; }
    
    }

    [MetadataType(typeof(LoginMetadata))]
    
    public partial class Login
    {
        public static Login Create(YourDbContextClassName db,Login login)
        {
            login.CreateDate = DateTime.Now;
            login.UpdateDate = DateTime.Now;
            login.IsDeleted = false;
            db.Logins.Add(login);
            db.SaveChanges();

            return login;
        }

        public static Login GenToken(YourDbContextClassName db,Login login)
        {
            login.CreateDate = DateTime.Now;
            login.UpdateDate = DateTime.Now;
            login.IsDeleted = false;
            db.Logins.Add(login);
            db.SaveChanges();

            return login;
        }

    public static List<Login> GetAll(YourDbContextClassName db)
    {
        List<Login> returnThis = db.Logins.Where(q=> q.IsDeleted != true).ToList();
        return returnThis;
    }

     public static Login Update(YourDbContextClassName db, Login login)
        {
            login.UpdateDate = DateTime.Now;
            db.Entry(login).State = EntityState.Modified;
            db.SaveChanges();

            return login;
        }
        
        public static Login? GetByUserDetail(YourDbContextClassName db, Login login)
        {
            Login? returnThis = db.Logins.Where(x => x.IsDeleted != true && x.Username == login.Username && x.Password == login.Password).FirstOrDefault();
            return returnThis;
        }

      

        public static Login? Delete(YourDbContextClassName db, Login login)
        {
            if(GetByUserDetail(db,login)==null){
                return null;
            }
            else{
            Login? returnthis = GetByUserDetail(db,login);
            if(returnthis != null){
                returnthis.IsDeleted = true;
            }
            // db.Employees.Remove(employee); เป็นวิธีการลบแบบให้หายไปเลย
            db.Entry(login).State = EntityState.Modified; // Soft Delete
            db.SaveChanges();
            return returnthis;
            }
          
        }

    }
    
    public class UserWithToken{
        public Login User { get; set; }
        public string Tokens { get; set; }
    }

    
        
    
}