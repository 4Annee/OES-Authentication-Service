using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationService.Data;
using AuthenticationService.Repositories;
using Microsoft.AspNetCore.Identity;
using AuthenticationService.Models;
using AuthenticationService.Services;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Steeltoe.Discovery.Client;
using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserServiceContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("UserServiceDb")));


builder.Services.AddDefaultIdentity<UserModel>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserServiceContext>();

builder.Services.AddDiscoveryClient(builder.Configuration);

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Add services to the container
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

/// Dependency Injection :

builder.Services.AddScoped<IYearRepository, YearRepository>();
builder.Services.AddScoped<ISectionRepository, SectionRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleManagementService, RoleManagementService>();

///  End Of Dependency Injection

var app = builder.Build();

/// In Dev Env Only :
CreateDatabaseEntities(builder);
/// DevEnv

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection()
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseDiscoveryClient();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static async void CreateDatabaseEntities(WebApplicationBuilder builder)
{
    using (var context = builder.Services.BuildServiceProvider().GetRequiredService<UserServiceContext>())
    {
        //context.Database.EnsureDeleted();
        var usermg = builder.Services.BuildServiceProvider().GetRequiredService<IUserRegistrationService>();
        var rolemg = builder.Services.BuildServiceProvider().GetRequiredService<IRoleManagementService>();
        if (context.Database.EnsureCreated())
        {
            if (!context.Roles.Any())
            {
                await rolemg.CreateRole(new() { Name = "Admin"});
                await rolemg.CreateRole(new() { Name = "Examiner"});
            }
            if (!context.Users.Any(u => u.UserName == "admin@esi-sba.dz"))
            {
                await usermg.RegisterAppUser(new()
                {
                    Birthday = DateTime.Parse("2000-01-01"),
                    CIDNumber = "None",
                    Email = "admin@esi-sba.dz",
                    FullName = "Admin",
                    Password = "@Admin123",
                    StudentCardId = "None"
                }) ;
                await usermg.RegisterAppUser(new()
                {
                    Birthday = DateTime.Parse("2000-01-01"),
                    CIDNumber = "None",
                    Email = "examiner@esi-sba.dz",
                    FullName = "Examiner",
                    Password = "@Exam123",
                    StudentCardId = "None"
                }) ;
                var useridadmin = context.Users.Where(u => u.UserName == "admin@esi-sba.dz").FirstOrDefault().Id;
                var useridexaminer = context.Users.Where(u => u.UserName == "examiner@esi-sba.dz").FirstOrDefault().Id;

                string adminroleid = context.Roles.FirstOrDefault(r=>r.Name == "Admin").Id;
                string examinerroleid = context.Roles.FirstOrDefault(r=>r.Name == "Examiner").Id;

                await rolemg.AddUserToRole(useridadmin,adminroleid);
                await rolemg.AddUserToRole(useridexaminer,examinerroleid);
            }
        }
    }
}