using Backend.Business_Logic_Layer.Interfaces;
using Backend.Business_Logic_Layer.Services;
using Backend.Data_Access_Layer.Context;
using Backend.Data_Access_Layer.Implementation;
using Backend.Data_Access_Layer.Interface;
using Backend.Infrastructure;
using Business_Logic_Layer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentDbConnection")));

// Repositories
builder.Services.AddScoped<IStudentRepo, StudentRepo>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<StudentService>();

//builder.Services.AddScoped<IEmailService, EmailService>();


var app = builder.Build();

// Configure HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
