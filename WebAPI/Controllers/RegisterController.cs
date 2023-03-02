using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public readonly DataBaseContext _context;
        public RegisterController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetReg")]

        public IEnumerable<reg> GetReg()
        {

            string GetData = "Exec getallreg";
            return _context.reg.FromSqlRaw<reg>(GetData).ToList();
        }
        // Using Linq
        [HttpPost]
        [Route("InsertdataLinq")]

        public bool InsertdataLinq(reg OBJ)
        {

            if (ModelState.IsValid)
            {

                var res = _context.Add(OBJ);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

        }
        /* [HttpGet]
         [Route("LoginLinq")]

         public reg LoginLinq(string Email, string Password)
         {

             reg obj = new reg();

             obj = _context.reg.Where(s => s.Email == Email & s.Password == Password).FirstOrDefault();

             //var res =  _context.reg. (where  s s.Email==Email && s.Password == Password ).firs;
             if (obj != null)
             {
                 return obj;
             }
             else
             {
                 return obj = null;
             }




         }*/
        [HttpGet]
        [Route("LoginLinq")]

        public dynamic LoginLinq(string Email, string Password)
        {


            //obj = _context.reg.Where(s => s.Email == Email & s.Password == Password).FirstOrDefault();

            
                var r = _context.reg.Where( s=>s.Email == Email & s.Password == Password ).Select(s=>s.Users_id);
                
                if (r != null)
                {
                    return r;
                }
                else
                {
                    return r=null;
                }
              
            

           
        }  
    }
}
