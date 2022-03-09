using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationService.Data;
using AuthenticationService.Repositories;
using Microsoft.AspNetCore.Identity;
using AuthenticationService.Models;
using AuthenticationService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserServiceContext")));


builder.Services.AddDefaultIdentity<UserModel>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserServiceContext>();


// Add services to the container
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IYearRepository,YearRepository>();
builder.Services.AddScoped<ISectionRepository,SectionRepository>();
builder.Services.AddScoped<IGroupRepository,GroupRepository>();
builder.Services.AddScoped<IUserRegistrationService,UserRegistrationService>();

var app = builder.Build();


using (var context = builder.Services.BuildServiceProvider().GetRequiredService<UserServiceContext>())
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

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
