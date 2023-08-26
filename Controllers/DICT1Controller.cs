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
    public class DICT1Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DICT1Controller(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string quiry = @"
                                select studentId, studentName, Gmail, convert(varchar(10), DateOfJoining,120) as DateOfJoining from dbo.dict1
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
        public JsonResult Post(DICT1 DICT1)
        {
            string quiry = @"
                                insert into dbo.dict1 ( studentId, studentName, Gmail, DateOfJoining )
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
                    mycommand.Parameters.AddWithValue("@studentId", DICT1.studentId);
                    mycommand.Parameters.AddWithValue("@studentName", DICT1.studentName);
                    mycommand.Parameters.AddWithValue("@Gmail", DICT1.Gmail);
                    mycommand.Parameters.AddWithValue("@DateOfJoining", DICT1.DateOfJoining);
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(DICT1 DICT1)
        {
            string quiry = @"
                                update dbo.dict1 set studentName = @studentName,
                                                        Gmail = @Gmail,
                                                        DateOfJoining = @DateOfJoining 
                                                  where studentId = @studentId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Organization");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycommand = new SqlCommand(quiry, mycon))
                {
                    mycommand.Parameters.AddWithValue("@studentId", DICT1.studentId);
                    mycommand.Parameters.AddWithValue("@studentName", DICT1.studentName);
                    mycommand.Parameters.AddWithValue("@Gmail", DICT1.Gmail);
                    mycommand.Parameters.AddWithValue("@DateOfJoining", DICT1.DateOfJoining);
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
                                delete from dbo.dict1 where studentId = @studentId
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
        [HttpGet("{name}")]
        public JsonResult GetSingle(string name)
        {
            string quiry = @"
                                select * from dbo.dict1 where studentName = @studentName
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Organization");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycommand = new SqlCommand(quiry, mycon))
                {

                    mycommand.Parameters.AddWithValue("@studentName", name);
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}
