using apipersonajeex.Data;
using apipersonajeex.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddTransient<RepositoryPersonajes>();
builder.Services.AddDbContext<PersonajesContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//A ESTE METODO LE PODEMOS INDICAR VARIAS OPCIONES
//DESDE EL TITULO DEL API, HASTA QUIEN LO HA HECHO
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Hospitales de viernes",
        Description = "Api realizada en viernes 05/04/2024 a las 12:41",
        Version = "v1",
        Contact = new OpenApiContact()
        {
            Name = "api 2024",
            Email = "javier@gmail.com"
        }
    });
});

var app = builder.Build();
app.UseSwagger();
//ESTO TIENE QUE VER CON EL COMPORTAMIENTO DE LA PAGINA SWAGGER
app.UseSwaggerUI(options =>
{
    //INDICAMOS DONDE ESTA EL ENDPOINT DE OPEN API
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json", name: "Api v1");
    //INDICAMOS QUE INDEX SERA LA PAGINA PRINCIPAL DE 
    //NUESTRO API
    options.RoutePrefix = "";
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
