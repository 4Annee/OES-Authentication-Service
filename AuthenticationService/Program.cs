using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationService.Data;
using AuthenticationService.Repositories;
using Microsoft.AspNetCore.Identity;
using AuthenticationService.Models;
using AuthenticationService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserServiceContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("UserServiceDb")));


builder.Services.AddDefaultIdentity<UserModel>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserServiceContext>();


// Add services to the container
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static async void CreateDatabaseEntities(WebApplicationBuilder builder)
{
    using (var context = builder.Services.BuildServiceProvider().GetRequiredService<UserServiceContext>())
    {
        context.Database.EnsureDeleted();
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