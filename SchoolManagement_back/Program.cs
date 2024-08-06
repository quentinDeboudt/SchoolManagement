using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services au conteneur.
builder.Services.AddControllers(); // Ajoute les services de contrôleurs

// services
services.AddScoped<IPersonService, PersonService>();

// Configurer DbContext avec SQL Server
builder.Services.AddDbContext<SchoolManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurer Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SchoolManagementAPI", Version = "1.0" });
});

var app = builder.Build();

// Configurer le pipeline des requêtes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolManagementAPI v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization(); // Ajoute l'autorisation si nécessaire

app.MapControllers(); // Mappe les contrôleurs

app.Run();
