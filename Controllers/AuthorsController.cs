using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

[ApiController]
[Route("api/autores")]
public class AuthorsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var autores = new List<Auto>();

        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Nombre, Nacionalidad, FechaNacimiento, Sueldo FROM Autores";

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            autores.Add(new Auto
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Nacionalidad = reader.GetString(2),
                FechaNacimiento = DateTime.Parse(reader.GetString(3)),
                Sueldo = reader.GetDecimal(4)
            });
        }

        return Ok(autores);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Nombre, Nacionalidad, FechaNacimiento, Sueldo FROM Autores WHERE Id = $id";
        command.Parameters.AddWithValue("$id", id);

        using var reader = command.ExecuteReader();

        if (!reader.Read())
            return NotFound();

        var autor = new Auto
        {
            Id = reader.GetInt32(0),
            Nombre = reader.GetString(1),
            Nacionalidad = reader.GetString(2),
            FechaNacimiento = DateTime.Parse(reader.GetString(3)),
            Sueldo = reader.GetDecimal(4)
        };

        return Ok(autor);
    }

    [HttpPost]
    public IActionResult Post(Auto autor)
    {
        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO Autores (Nombre, Nacionalidad, FechaNacimiento, Sueldo)
            VALUES ($nombre, $nacionalidad, $fecha, $sueldo);
            """;

        command.Parameters.AddWithValue("$nombre", autor.Nombre);
        command.Parameters.AddWithValue("$nacionalidad", autor.Nacionalidad);
        command.Parameters.AddWithValue("$fecha", autor.FechaNacimiento.ToString("O"));
        command.Parameters.AddWithValue("$sueldo", autor.Sueldo);

        command.ExecuteNonQuery();

        return Ok(new
        {
            mensaje = "Autor registrado correctamente",
            datos = autor
        });
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Auto autor)
    {
        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE Autores
            SET Nombre = $nombre,
                Nacionalidad = $nacionalidad,
                FechaNacimiento = $fecha,
                Sueldo = $sueldo
            WHERE Id = $id
            """;

        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$nombre", autor.Nombre);
        command.Parameters.AddWithValue("$nacionalidad", autor.Nacionalidad);
        command.Parameters.AddWithValue("$fecha", autor.FechaNacimiento.ToString("O"));
        command.Parameters.AddWithValue("$sueldo", autor.Sueldo);

        var filas = command.ExecuteNonQuery();

        if (filas == 0)
            return NotFound();

        return Ok(new
        {
            mensaje = "Autor actualizado correctamente",
            id = id
        });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Autores WHERE Id = $id";
        command.Parameters.AddWithValue("$id", id);

        var filas = command.ExecuteNonQuery();

        if (filas == 0)
            return NotFound();

        return Ok(new
        {
            mensaje = "Autor eliminado correctamente",
            id = id
        });
    }
}
