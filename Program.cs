using DSW_3P_PR01.Repositories;
using DSW_3P_PR01.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de dependencias
builder.Services.AddScoped<IMisionRepository, MisionJsonRepository>();
builder.Services.AddScoped<IMisionService, MisionService>();

var app = builder.Build();

// Habilitar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Mapear los controladores
app.MapControllers();

app.Run();