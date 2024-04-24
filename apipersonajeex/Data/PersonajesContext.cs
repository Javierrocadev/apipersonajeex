using apipersonajeex.Models;
using Microsoft.EntityFrameworkCore;

namespace apipersonajeex.Data
{
    public class PersonajesContext : DbContext
    {
        public PersonajesContext(DbContextOptions<PersonajesContext> options) : base(options)
        {
        }

        public DbSet<Personaje> Personajes { get; set; }
    }
}
