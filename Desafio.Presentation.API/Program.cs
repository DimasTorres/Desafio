using Desafio.Core.Application.Mapper;
using Desafio.Presentation.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MapperFactory));
builder.Services.AddControllers();

//Configure Database
builder.DbConnectionConfigure();

//Configure Ioc
builder.Services.ConfigureIoC();

//Configure Open Api
builder.Services.ConfigureOpenApi();

//Configure Authentication
builder.ConfigureAuthentication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
