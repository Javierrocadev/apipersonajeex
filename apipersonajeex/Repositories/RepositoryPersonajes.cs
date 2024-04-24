using apipersonajeex.Data;
using apipersonajeex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace apipersonajeex.Repositories
{
    public class RepositoryPersonajes
    {
        private PersonajesContext context;

        public RepositoryPersonajes(PersonajesContext context)
        {
            this.context = context;
        }

        //metodos

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            return await this.context.Personajes.ToListAsync();
        }

        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            return await this.context.Personajes.FirstOrDefaultAsync(z => z.IdPersonaje == id);
        }

        public async Task InsertPersonajeAsync(int id, string nombre, string imagen, string serie)
        {
            Personaje personaje = new Personaje();
            personaje.IdPersonaje = id;
            personaje.Nombre = nombre;
            personaje.Imagen = imagen;
            personaje.Serie = serie;
            this.context.Personajes.Add(personaje);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdatePersonajeAsync(int id, string nombre, string imagen, string serie)
        {
            Personaje personaje = await this.FindPersonajeAsync(id);
            personaje.Nombre = nombre;
            personaje.Imagen = imagen;
            personaje.Serie= serie;
            await this.context.SaveChangesAsync();
        }



        public async Task DeletePersonajeAsync(int id)
        {
            Personaje personaje = await this.FindPersonajeAsync(id);
            this.context.Personajes.Remove(personaje);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<Personaje>> GetPersonajesPorSerie(string serie)
        {
            // Utiliza LINQ para filtrar los usuarios por el ID de la clase
            var personajes = await this.context.Personajes
                .Where(u => u.Serie == serie)
                .ToListAsync();

            return personajes;
        }

        public async Task<List<string>> GetSeriesDePersonajesAsync()
        {
            return await this.context.Personajes
                .Select(p => p.Serie)
                .Distinct()
                .ToListAsync();
        }

    }
}
