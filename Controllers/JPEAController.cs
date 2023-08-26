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
    public class JPEAController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public JPEAController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string quiry = @"
                                select studentId, studentName, Gmail, convert(varchar(10), DateOfJoining,120) as DateOfJoining from dbo.jpea
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
        public JsonResult Post(JPEA JPEA)
        {
            string quiry = @"
                                insert into dbo.jpea ( studentId, studentName, Gmail, DateOfJoining )
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
                    mycommand.Parameters.AddWithValue("@studentId", JPEA.studentId);
                    mycommand.Parameters.AddWithValue("@studentName", JPEA.studentName);
                    mycommand.Parameters.AddWithValue("@Gmail", JPEA.Gmail);
                    mycommand.Parameters.AddWithValue("@DateOfJoining", JPEA.DateOfJoining);
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(JPEA JPEA)
        {
            string quiry = @"
                                update dbo.jpea set studentName = @studentName,
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
                    mycommand.Parameters.AddWithValue("@studentId", JPEA.studentId);
                    mycommand.Parameters.AddWithValue("@studentName", JPEA.studentName);
                    mycommand.Parameters.AddWithValue("@Gmail", JPEA.Gmail);
                    mycommand.Parameters.AddWithValue("@DateOfJoining", JPEA.DateOfJoining);
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
                                delete from dbo.jpea where studentId = @studentId
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
