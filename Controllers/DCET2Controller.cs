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
    public class DCET2Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DCET2Controller(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;

        }
        [HttpGet]
        public JsonResult Get()
        {
            string quiry = @"
                                select studentId, studentName, Gmail, convert(varchar(10), DateOfJoining,120) as DateOfJoining from dbo.dcet2
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
        public JsonResult Post(DCET2 DCET2)
        {
            string quiry = @"
                                insert into dbo.dcet2 ( studentId, studentName, Gmail, DateOfJoining )
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
                    mycommand.Parameters.AddWithValue("@studentId", DCET2.studentId);
                    mycommand.Parameters.AddWithValue("@studentName", DCET2.studentName);
                    mycommand.Parameters.AddWithValue("@Gmail", DCET2.Gmail);
                    mycommand.Parameters.AddWithValue("@DateOfJoining", DCET2.DateOfJoining);
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(DCET2 DCET2)
        {
            string quiry = @"
                                update dbo.dcet2 set studentName = @studentName,
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
                    mycommand.Parameters.AddWithValue("@studentId", DCET2.studentId);
                    mycommand.Parameters.AddWithValue("@studentName", DCET2.studentName);
                    mycommand.Parameters.AddWithValue("@Gmail", DCET2.Gmail);
                    mycommand.Parameters.AddWithValue("@DateOfJoining", DCET2.DateOfJoining);
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
                                delete from dbo.dcet2 where studentId = @studentId
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
                                select * from dbo.dcet2 where studentName = @studentName
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
