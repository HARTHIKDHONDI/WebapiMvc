using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Net;
using System.Numerics;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

//using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        public readonly DataBaseContext _context;
        public OperationsController(DataBaseContext context)
        {
            _context = context;
        }
        /*[HttpGet]
          public IActionResult GetContents()
          {
              return Ok(_context.Customers.ToList());
          }*/
        [HttpGet]
        [Route("GetAllDetails")]
        public Customers GetAllDetails(string CustomerID)
        {
            Customers obj = new Customers();

            obj = _context.Customers.Find(CustomerID);

           // var cus = from s in _context.Customers where s.CustomerID==CustomerID select s;


            //return Ok(obj);
            return obj;


        }
        [HttpGet]
        [Route("GetSPCustomers")]

        public IEnumerable<Customers> GetSPCustomers()
        {

            string GetData = "Exec GetCustomerDetails ";
            return _context.Customers.FromSqlRaw(GetData).ToList();
        }

        [HttpGet]
        [Route("GetCustomerId")]

        public IEnumerable<Customers> GetCustomerId(string id, string phone)
        {
            IList<Customers> obj = null;

            List<SqlParameter> para = new List<SqlParameter>
            {
            new SqlParameter { ParameterName="@CustomerID",Value=id},
            new SqlParameter { ParameterName="@Phone", Value=phone}
            };
            string GetData = "Exec GetById @CustomerID,@Phone";
            obj = _context.Customers.FromSqlRaw<Customers>(GetData, para.ToArray()).ToList();
            if (obj.Count == 0)
            {
                Console.WriteLine("No Records");
                return obj;
            }
            else
            {
                return _context.Customers.FromSqlRaw<Customers>(GetData, para.ToArray()).ToList();
            }

        }
        [HttpPost]
        [Route("Insertdata")]
        public bool Insertdata(Customers OBJ)
        {

            if (ModelState.IsValid)
            {
                string sql = "Exec Insert_Customers @CustomerID,@CompanyName,@ContactName,@ContactTitle,@Address,@City,@Region,@PostalCode,@Country,@Phone,@Fax";
                //IList<Customers> obj = null;

                List<SqlParameter> para = new List<SqlParameter>()
                {
                new SqlParameter { ParameterName="@CustomerID",Value=OBJ.CustomerID},
                new SqlParameter { ParameterName="@CompanyName", Value=OBJ.ContactName},
                new SqlParameter { ParameterName="@ContactName",Value=OBJ.ContactName},
                new SqlParameter { ParameterName="@ContactTitle", Value=OBJ.ContactTitle},
                new SqlParameter { ParameterName="@Address", Value=OBJ.Address},
                new SqlParameter { ParameterName="@City", Value=OBJ.City},
                new SqlParameter { ParameterName="@Region", Value=OBJ.Region},
                new SqlParameter { ParameterName="@PostalCode", Value=OBJ.PostalCode},
                new SqlParameter { ParameterName="@Country", Value=OBJ.Country},
                new SqlParameter { ParameterName="@Phone", Value=OBJ.Phone},
                 new SqlParameter { ParameterName="@Fax", Value=OBJ.Fax}
                };


                var res = _context.Database.ExecuteSqlRaw(sql, para.ToArray());
                if (res != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;

            }
        }
      
        [HttpPut]
        [Route("Updatedata")]
        public  bool Updatedata(int CustomerID, Customers OBJ)
        {

            if (ModelState.IsValid)
            {
                List<SqlParameter> para = new List<SqlParameter>()
                {

                new SqlParameter { ParameterName="@CompanyName", Value=OBJ.ContactName},
                new SqlParameter { ParameterName="@ContactName",Value=OBJ.ContactName},
                new SqlParameter { ParameterName="@ContactTitle", Value=OBJ.ContactTitle},
                new SqlParameter { ParameterName="@Address", Value=OBJ.Address},
                new SqlParameter { ParameterName="@City", Value=OBJ.City},
                new SqlParameter { ParameterName="@Region", Value=OBJ.Region},
                new SqlParameter { ParameterName="@PostalCode", Value=OBJ.PostalCode},
                new SqlParameter { ParameterName="@Country", Value=OBJ.Country},
                new SqlParameter { ParameterName="@Phone", Value=OBJ.Phone},
                 new SqlParameter { ParameterName="@Fax", Value=OBJ.Fax},
                 new SqlParameter { ParameterName="@CustomerID",Value=CustomerID}
                };
                string sql = "Exec Update_Customers @CompanyName,@ContactName,@ContactTitle,@Address,@City,@Region,@PostalCode,@Country,@Phone,@Fax,@CustomerID";
                //IList<Customers> obj = null;
                int res = _context.Database.ExecuteSqlRaw(sql, para.ToArray());
                if (res > 0)
                {
                    return true;

                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;

            }
        }

        [HttpDelete]

        [Route("Delete_data")]
        public bool Delete_data(string CustomerID)
        {

            if (ModelState.IsValid)
            {
                string sql = "Exec Delete_Customers @CustomerID";
                //IList<Customers> obj = null;

                List<SqlParameter> para = new List<SqlParameter>()
                {


                 new SqlParameter { ParameterName="@CustomerID",Value=CustomerID}
                };


                var res = _context.Database.ExecuteSqlRaw(sql, para.ToArray());
                if (res != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;

            }
        }

        // Using Linq
        [HttpPost]
        [Route("InsertdataLinq")]
        
        public bool InsertdataLinq(Customers OBJ)
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

        [HttpPost]
        [Route("UpdatedataLinq")]

        public bool UpdatedataLinq(Customers OBJ)
        {

            if (ModelState.IsValid)
            {


                var res = _context.Update(OBJ);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

        }
        [HttpDelete]
        [Route("Deletedatalinq")]

        public bool Deletedatalinq(string CustomerID)
        {

            if (ModelState.IsValid)
            {

                Customers obj = new Customers();

                obj = _context.Customers.Find(CustomerID);

                _context.Remove(obj);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

        }


    }
}