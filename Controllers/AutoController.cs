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
            nombre = "Camila Peña Felipe"
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
}