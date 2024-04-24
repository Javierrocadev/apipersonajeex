using apipersonajeex.Models;
using apipersonajeex.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apipersonajeex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private RepositoryPersonajes repo;

        public PersonajesController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }


        [HttpGet]
        public async Task<ActionResult<List<Personaje>>>
            GetDepartamentos()
        {
            return await this.repo.GetPersonajesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Personaje>>
            FindDepartamento(int id)
        {
            return await this.repo.FindPersonajeAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostPersonaje
            (Personaje personaje)
        {
            await this.repo.InsertPersonajeAsync(personaje.IdPersonaje,personaje.Nombre,personaje.Imagen,personaje.Serie);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //PODEMOS PERSONALIZAR LA RESPUESTA
            if (await this.repo.FindPersonajeAsync(id) == null)
            {
                //NO EXISTE EL DEPARTAMENTO PARA ELIMINARLO
                return NotFound();
            }
            else
            {
                await this.repo.DeletePersonajeAsync(id);
                return Ok();
            }
        }


        [HttpPut]
        public async Task<ActionResult> PutPersonaje
           (Personaje personaje)
        {
            await this.repo.UpdatePersonajeAsync(personaje.IdPersonaje, personaje.Nombre, personaje.Imagen, personaje.Serie);
            return Ok();
        }


        [HttpGet("personajessPorSerie/{serie}")]
        public async Task<ActionResult<List<Personaje>>> GetPersonajesPorSerie(string serie)
        {
            try
            {
                var personajes = await repo.GetPersonajesPorSerie(serie);
                return Ok(personajes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("Series")]
        public async Task<ActionResult<List<string>>> GetSeriesDePersonajes()
        {
            var series = await repo.GetSeriesDePersonajesAsync();
            if (series == null || series.Count == 0)
            {
                return NotFound();
            }
            return series;
        }

    }
}
