//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.OpenApi.Models;
using OnlineRecruitmentSystem.Data; // Update with your actual namespace
using OnlineRecruitmentSystem.Models; // Where ApplicationUser is defined

var builder = WebApplication.CreateBuilder(args);

// 1. Add Connection to SQL Server via EF Core
builder.Services.AddDbContext<RecruitmentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add Identity Services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<RecruitmentDbContext>()
    .AddDefaultTokenProviders();
// 3. Enable CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Allow React app's URL
               .AllowAnyMethod() // Allow any HTTP method (GET, POST, etc.)
               .AllowAnyHeader(); // Allow any headers
    });
});

// 3. Add Authentication/Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// 4. Add Controllers
builder.Services.AddControllers();

// 5. Swagger (API documentation)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Online Recruitment API",
        Version = "v1"
    });
});

var app = builder.Build();
//app.UseCors("AllowAll");
// Enable CORS
app.UseCors("AllowReactApp");

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication(); // ?? Important - goes before Authorization
app.UseAuthorization();
app.UseStaticFiles(); // Add this line to serve static files like resumes


app.MapControllers();

app.Run();

