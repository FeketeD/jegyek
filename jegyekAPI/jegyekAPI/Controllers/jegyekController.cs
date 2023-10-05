using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using jegyekAPI.Models;
using static jegyekAPI.Dtos;

namespace jegyekAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class jegyekController : ControllerBase
    {
        Connect connect = new();
        private List<JegyekDto> jegyek = new List<JegyekDto>();

        [HttpGet]
        public ActionResult<IEnumerable<JegyekDto>> Get()
        {
            try
            {
                connect.connection.Open();

                string sql = "SELECT * FROM jegyek";

                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var result = new JegyekDto(
                            reader.GetGuid("Id"),
                            reader.GetInt32("Jegy"),
                            reader.GetString("Desc"),
                            reader.GetDateTime("Created")
                        );
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
        public ActionResult<JegyekDto> Get(Guid Id)
        {

            try
            {
                connect.connection.Open();

                string sql = "SELECT * FROM jegyek WHERE Id = @ID";

                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);

                cmd.Parameters.AddWithValue("Id", Id);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var result = new JegyekDto(
                            reader.GetGuid("Id"),
                            reader.GetInt32("Jegy"),
                            reader.GetString("Desc"),
                            reader.GetDateTime("Created")
                        );
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

        [HttpPost]
        public ActionResult<Jegyek> Post(CreateJegyekDto createJegy)
        {

            var jegyek = new Jegyek
            {
                Id = Guid.NewGuid(),
                Jegy = createJegy.Jegy,
                Desc = createJegy.Desc,
                Created = DateTimeOffset.Now
            };

            try
            {
                connect.connection.Open();

                string sql = $"INSERT INTO `jegyek`(`Id`, `Jegy`, `Desc`, `Created`) VALUES (@Id,@Jegy,@Desc,@Created)";


                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);

                cmd.Parameters.AddWithValue("Id", jegyek.Id);
                cmd.Parameters.AddWithValue("Jegy", jegyek.Jegy);
                cmd.Parameters.AddWithValue("Desc", jegyek.Desc);
                cmd.Parameters.AddWithValue("Created", jegyek.Created);

                cmd.ExecuteNonQuery();

                connect.connection.Close();

                return StatusCode(201, jegyek);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete]

        public ActionResult Delete(Guid Id)
        {
            try
            {
                connect.connection.Open();

                string sql = "DELETE FROM jegyek WHERE Id = @Id";

                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);

                cmd.Parameters.AddWithValue("Id", Id);

                MySqlDataReader reader = cmd.ExecuteReader();
                connect.connection.Close();

                return StatusCode(200, $"Sikeresen törölted az adatbázisból a(z) {Id} ID val ellátott jegyet.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{Id}")]
        public ActionResult<JegyekDto> Put(UpdateJegyekDto updateJegyek, Guid Id)
        {
            try
            {
                connect.connection.Open();

                string sql = "UPDATE `jegyek` SET `Jegy`=@Jegy,`Desc`=@Desc WHERE Id=@Id";

                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);

                cmd.Parameters.AddWithValue("Jegy", updateJegyek.Jegy);
                cmd.Parameters.AddWithValue("Desc", updateJegyek.Desc);

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
    }
}
