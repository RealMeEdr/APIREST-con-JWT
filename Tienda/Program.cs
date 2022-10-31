using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Configuration.GetConnectionString("conexionProducto");

//Se utiliza esto d elos cors para no ser bloqueado desde una ruta diferente a la que estamos trabajando.   
var misReglasCors = "ReglasCors";
builder.Services.AddCors(option =>
    option.AddPolicy(name: misReglasCors,
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    )
);

//Obtenemos la secret Key
builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("TokenSettings").GetSection("SecretKey").ToString();
var keyBites = Encoding.UTF8.GetBytes(secretKey);

//Implementamos la authentication
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBites),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.Configuration.GetConnectionString("conexionProducto");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//No olvidar añadir estas Cors al finalizar lo del builder
app.UseCors(misReglasCors);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
