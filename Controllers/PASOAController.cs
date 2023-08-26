using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PASOAController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PASOAController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string quiry = @"
                                select studentId, studentName, Gmail, convert(varchar(10), DateOfJoining,120) as DateOfJoining from dbo.pasoa
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Organization");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycommand = new SqlCommand(quiry, mycon))
                {
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(PASOA PASOA)
        {
            string quiry = @"
                                insert into dbo.pasoa ( studentId, studentName, Gmail, DateOfJoining )
                                values ( @studentId, @studentName, @Gmail, @DateOfJoining )
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Organization");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycommand = new SqlCommand(quiry, mycon))
                {
                    mycommand.Parameters.AddWithValue("@studentId", PASOA.studentId);
                    mycommand.Parameters.AddWithValue("@studentName", PASOA.studentName);
                    mycommand.Parameters.AddWithValue("@Gmail", PASOA.Gmail);
                    mycommand.Parameters.AddWithValue("@DateOfJoining", PASOA.DateOfJoining);
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(PASOA PASOA)
        {
            string quiry = @"
                                update dbo.pasoa set studentName = @studentName,
                                                        Gmail = @Gmail,
                                                        DateOfJoining = DateOfJoining 
                                                  where EmployeeId = @studentId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Organization");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycommand = new SqlCommand(quiry, mycon))
                {
                    mycommand.Parameters.AddWithValue("@studentId", PASOA.studentId);
                    mycommand.Parameters.AddWithValue("@studentName", PASOA.studentName);
                    mycommand.Parameters.AddWithValue("@Gmail", PASOA.Gmail);
                    mycommand.Parameters.AddWithValue("@DateOfJoining", PASOA.DateOfJoining);
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string quiry = @"
                                delete from dbo.pasoa where studentId = @studentId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Organization");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycommand = new SqlCommand(quiry, mycon))
                {
                    mycommand.Parameters.AddWithValue("@studentId", id);
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}
