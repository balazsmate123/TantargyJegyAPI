using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using TantargyJegyAPI.Models;
using static TantargyJegyAPI.DTOs;


namespace TantargyJegyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JegyController : ControllerBase
    {
        connect connect = new();
        private List<JegyekDTO> jegyek = new List<JegyekDTO>();

        [HttpGet]
        public ActionResult<IEnumerable<JegyekDTO>> Get()
        {

            try
            {
                connect.connection.Open();

                string sql = "SELECT * FROM jegyek";

                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var result = new JegyekDTO(
                        reader.GetGuid("Id"),
                        reader.GetInt16("Jegy"),
                        reader.GetString("Leiras"),
                        reader.GetDateTime("Created"));

                    jegyek.Add(result);
                }
                connect.connection.Close();
                return StatusCode(200, jegyek);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("{Id}")]
        public ActionResult<JegyekDTO> Get(Guid Id)
        {

            try
            {
                connect.connection.Open();

                string sql = "SELECT * FROM jegyek WHERE Id=@ID";

                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);
                cmd.Parameters.AddWithValue("Id", Id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var result = new JegyekDTO(
                    reader.GetGuid("Id"),
                    reader.GetInt16("Jegy"),
                    reader.GetString("Leiras"),
                    reader.GetDateTime("Created"));

                    connect.connection.Close();
                    return StatusCode(200, result);
                }
                else
                {
                    Exception e = new();
                    connect.connection.Close();
                    return StatusCode(404, e.Message);

                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        public ActionResult Delete(Guid Id)
        {
            try
            {
                connect.connection.Open();

                string sql = "DELETE FROM jegyek WHERE Id=@ID";

                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);
                cmd.Parameters.AddWithValue("Id", Id);
                MySqlDataReader reader = cmd.ExecuteReader();
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPut("{Id}")]
        public ActionResult<JegyekDTO> Put(UpdateJegyekDto updateJegyek, Guid Id)
        {
            try { 
            
                connect.connection.Open();

                string sql = "UPDATE jegyek SET Jegy=@Jegy, Leiras = @Leiras WHERE Id=@Id";

                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);

                cmd.Parameters.AddWithValue("Jegy", updateJegyek.Jegy);
                cmd.Parameters.AddWithValue("Leiras", updateJegyek.Leiras);
                cmd.Parameters.AddWithValue("Id", Id);
                cmd.ExecuteNonQuery();

                connect.connection.Close();
                return StatusCode(200);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<JegyekDTO> Post(CreateJegyekDto createJegyek)
        {

            var Jegyek = new Jegyek
            {
                Id = Guid.NewGuid(),
                Jegy = createJegyek.Jegy,
                Leiras = createJegyek.Leiras,
                Created = DateTimeOffset.Now
            };

            try
            {
                connect.connection.Open();

                string sql = $"INSERT INTO `jegyek`(`Id`, `Jegy`, `Leiras`, `Created`) VALUES (@Id,@Jegy,@Leiras,@Created)";


                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);

                cmd.Parameters.AddWithValue("Id", Jegyek.Id);
                cmd.Parameters.AddWithValue("Jegy", Jegyek.Jegy);
                cmd.Parameters.AddWithValue("Leiras", Jegyek.Leiras);
                cmd.Parameters.AddWithValue("Created", Jegyek.Created);

                cmd.ExecuteNonQuery();

                connect.connection.Close();

                return StatusCode(201, Jegyek);


            }
            catch
            {
                return BadRequest();
            }
            }
        }
    }
