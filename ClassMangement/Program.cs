using ClassMangement.Controllers;
using ClassMangement.Seeders;
using Common.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mock;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using Service.Services;
using System;
using System.Text;

// TODO: 
//��������� ������� ��� - ������ ������ �� ���� �"� ��� ����� ���� ������ ���������...
//���� ����� -������-���'� ������� ���������
//����� ��� ���� ����� ���� ������ ����� ��� ��� - ���� ��������� �� �� �����
//�� ���� ������� ����'�...
//���� ������� ���� �����.
//������ ����� ����������


var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
	option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	option.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});
});
builder.Services.AddExtentionControllers();
builder.Services.AddDbContext<IContext, Database>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			  .AddJwtBearer(option =>
			  option.TokenValidationParameters = new TokenValidationParameters
			  {
				  ValidateIssuer = true,
				  ValidateAudience = true,
				  ValidateLifetime = true,
				  ValidateIssuerSigningKey = true,
				  ValidIssuer = builder.Configuration["Jwt:Issuer"],
				  ValidAudience = builder.Configuration["Jwt:Audience"],
				  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

			  });
// enable cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
					  policy =>
					  {
						  policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
					  });
});
//
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

Console.WriteLine($" ENVIRONMENT: {app.Environment.EnvironmentName}");
if (app.Environment.IsDevelopment())
{
     using (var scope = app.Services.CreateScope())
      {
		//������� ���� Icontext ��� �� ���� ��� ������� ���� ��.
          var context = scope.ServiceProvider.GetRequiredService<Database>(); // �� IContext

		var sqlPath = Path.Combine(AppContext.BaseDirectory, "SeedData", "initial_data.sql");
		SqlSeeder.CheckSeederWorks(context, sqlPath);

		if (File.Exists(sqlPath))
		{
			if (!context.Classes.Any()) // �� Students ���'
			{
				SqlSeeder.RunSqlFromFile(context, sqlPath);
			}
		}
		else
		{
			Console.WriteLine(" ���� SQL �� ����: " + sqlPath);
		}
    }


    app.UseSwagger();
    app.UseSwaggerUI();
}

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();