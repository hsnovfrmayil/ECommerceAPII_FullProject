using ECommerceAPII.Infrastructure;
using ECommerceAPII.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices();

builder.Services.AddInfrastructureServices();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    //policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    policy.WithOrigins("http://127.0.0.1:5500", "https://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod();
}));

//builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
builder.Services.AddControllers();
      //.AddFluentValidation(fv =>
      //      fv.RegisterValidatorsFromAssemblyContaining<Program>());
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

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

