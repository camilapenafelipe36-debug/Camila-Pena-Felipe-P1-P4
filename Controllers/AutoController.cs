using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AutoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            mensaje = "API funcionando correctamente",
            nombre = "Camila Pena Felipe"
        });
    }

    [HttpPost]
    public IActionResult Post(Auto auto)
    {
        return Ok(new
        {
            mensaje = "Datos registrados correctamente",
            datos = auto
        });
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Auto auto)
    {
        return Ok(new
        {
            mensaje = "Datos actualizados correctamente",
            id = id,
            datos = auto
        });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok(new
        {
            mensaje = "Autor eliminado correctamente",
            id = id
        });
    }
}
