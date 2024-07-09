using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManagementMVC.Services;
using UserMgmtDAL.Entities;
using UserMgmtDAL.Repository;

var builder = WebApplication.CreateBuilder(args);
//1.Content root as to set 
//2.Json files will be configured for diffrent environments
//3.Add custom .json files
//4.Dependency injection container will be setup
//5.Kestrel environment will also be set up

//asking for injecting dependencies for MVC
builder.Services.AddControllersWithViews();

var constring = builder.Configuration.GetConnectionString("UserMgmtString");

builder.Services.AddDbContext<UserMgmtContext>((dbctxbuilder) =>
                dbctxbuilder.UseSqlServer(constring));

builder.Services.AddTransient<IRepository<User>, UserRepository>();
builder.Services.AddTransient<IAuth, AuthRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();

//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(corsopts =>
{
    corsopts.AddPolicy("slkpol", corspolbldr =>
    {
        corspolbldr.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });
});
var key= builder.Configuration.GetSection("Jwt:Key").Value;
var issuer = builder.Configuration.GetSection("Jwt:Issuer").Value;
var audience = builder.Configuration.GetSection("Jwt:Audience").Value;

builder.Services.AddAuthentication(authoptions =>
{
    authoptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    authoptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authoptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtoptions => {
    jwtoptions.IncludeErrorDetails = true;
    jwtoptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
    };
});


var app = builder.Build();



//Pipeline start below



//app.MapGet("/", () => "Hello World!");
//app.MapGet("/slk", () => "Hello World! SLK");

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(swaguiopts =>
    {
        swaguiopts.SwaggerEndpoint("/swagger/v1/swagger.json", "User management Angular app V1");
        swaguiopts.RoutePrefix = "";
    });
}else if (app.Environment.IsProduction())
{
    //  app.UseExceptionHandler("/home/error");
    app.UseExceptionHandler((appbuilder) =>
    {
        appbuilder.Run(async context =>
        {
            context.Response.ContentType = "text/html";
            //context.Response.ContentType="application/json"
            var expfeature = context.Features.GetRequiredFeature<IExceptionHandlerFeature>();

            var currentexp = expfeature.Error;
            string msg = "";
            if(currentexp is DivideByZeroException)
            {
                msg = "<h3 style='color:red'>You are trying to divide by zero</h3>";
            }else  if(currentexp is FileNotFoundException)
            {
               msg= "<h3 style='color:red'>File not found</h3>";
            }

            await context.Response.WriteAsync(msg);

        });
    });
}


app.UseStaticFiles();
//app.UseAuthentication();
//app.UseRouting();

//app.Use(async (context, requestdel) =>
//{
//    await context.Response.WriteAsync($"Hello world using a simple USE middleware {app.Environment.EnvironmentName}");
//    await requestdel();
//});



//app.Run(async context =>
//{
//   // code
//    await context.Response.WriteAsync("Hello world using a simple RUN middleware");
//});

//app
//.MapControllerRoute("homesearch2", "/MySLK/BLR/{action}/{name}/{loc?}",
//new { controller = "Home", action = "Find" });


//app
//.MapControllerRoute("homesearch1", "/MySLK/BLR/{action}/{name}/{loc?}",
//new { controller = "Home", action = "Find" });


//app
//.MapControllerRoute("default", "{controller}/{action}/{id?}",
//new { controller = "ManageUser", action = "Index" });

//app.MapDefaultControllerRoute();

app.UseCors("slkpol");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
