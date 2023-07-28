//using GeneralStoreMVC.Data;
using System.Text;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.CommentServices;
using Services.PostServices;
using Services.RegionServices;
using Services.TrailServices;
using Services.UserServices;




var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("TrailWatchDb"))) ;
builder.Services.AddScoped<IPostService,PostService>();
builder.Services.AddScoped<IRegionService,RegionService>();
builder.Services.AddScoped<ITrailService,TrailService>();
builder.Services.AddScoped<ICommentService,CommentService>();
builder.Services.AddScoped<IUserService,UserService>();
// builder.Services.AddHttpContextAccessor();
// //dotnet add .\WebAPI\ package system.IdentityModel.Tokens.Jwt
// //dotnet add .\WebAPI\ package Microsoft.AspNetCore.Authentication.JwtBearer

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
// {
//     options.RequireHttpsMetadata = false;
//     options.SaveToken = true;
//     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidIssuer = builder.Configuration["Jwt:Issuer"],
//         ValidAudience = builder.Configuration["Jwt:Audience"],
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration?["Jwt:Key"] ?? "none"))
//     };
// });

builder.Services.AddDefaultIdentity<UserEntity>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
// Configure what happens when a logged out user tries to access an authorized route
builder.Services.ConfigureApplicationCookie(options =>
{
options.LoginPath = "/Account/Login";
});
// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
