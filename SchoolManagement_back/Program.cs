using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.IRepository;
using SchoolManagement.Domain.Services;
using SchoolManagement.Infrastructure.Repository.EFCore;
using SchoolManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Ajouter les services au conteneur.
builder.Services.AddControllers();

// services
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IClassroomService, ClassroomService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

// Repository

builder.Services.AddScoped<IPersonRepository, EfCorePersonRepository>();
builder.Services.AddScoped<IRoleRepository, EfCoreRoleRepository>();
builder.Services.AddScoped<IClassroomRepository, EfCoreClassroomRepository>();
builder.Services.AddScoped<IGroupRepository, EfCoreGroupRepository>();
builder.Services.AddScoped<ILessonRepository, EfCoreLessonRepository>();
builder.Services.AddScoped<ISubjectRepository, EfCoreSubjectRepository>();


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
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthorization(); // Ajoute l'autorisation si nécessaire

app.MapControllers(); // Mappe les contrôleurs

app.Run();






// SchoolManagement
// │
// ├── SchoolManagement.API
// │   └── Controllers
// │       ├── ClassroomController.cs
// │       ├── GroupController.cs
// │       ├── LessonController.cs
// │       ├── PersonController.cs
// │       ├── RoleController.cs
// │       └── SubjectController.cs
// │
// ├── SchoolManagement.Domain
// │   ├── Entities
// │   │   ├── Person.cs
// │   │   ├── Group.cs
// │   │   ├── Role.cs
// │   │   └── PagedResult.cs
// │   │
// │   ├── IRepository
// │   │   ├── IClassroomRepository.cs
// │   │   ├── IGroupRepository.cs
// │   │   ├── ILessonRepository.cs
// │   │   ├── IPersonRepository.cs
// │   │   ├── IRoleRepository.cs
// │   │   └── ISubjectRepository.cs
// │   │
// │   └── Services
// │       ├── ClassroomService.cs
// │       ├── GroupService.cs
// │       ├── LessonService.cs
// │       ├── PersonService.cs
// │       ├── RoleService.cs
// │       └── SubjectService.cs
// │
// └── SchoolManagement.Infrastructure
//     ├── Repositories
//     │   ├── EFCore
//     │   │   ├── EfCoreClassroomRepository.cs
//     │   │   ├── EfCoreGroupRepository.cs
//     │   │   ├── EfCoreLessonRepository.cs
//     │   │   ├── EfCorePersonRepository.cs
//     │   │   ├── EfCoreRoleRepository.cs
//     │   │   └── EfCoreSubjectRepository.cs
//     │   │
//     │   └── SQL
//     │       ├── EfCoreClassroomRepository.cs
//     │       ├── EfCoreGroupRepository.cs
//     │       ├── EfCoreLessonRepository.cs
//     │       ├── EfCorePersonRepository.cs
//     │       ├── EfCoreRoleRepository.cs
//     │       └── EfCoreSubjectRepository.cs
//     │    
//     └── DbContext
//         └── SchoolManagementDbContext.cs
 