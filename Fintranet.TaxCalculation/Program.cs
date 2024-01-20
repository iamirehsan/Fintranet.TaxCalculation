using Fintranet.TaxCalculation.Api.Base.Extentions;
using Fintranet.TaxCalculation.Api.Base.Models;
using Microsoft.IdentityModel.Tokens;
using Fintranet.TaxCalculation.Api.Base.JsonConverters;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Fintranet.TaxCalculation.Repository.Implimentation;
using Fintranet.TaxCalculation.Repository;
using Fintranet.TaxCalculation.Service.ServiceInterfaces;
using Fintranet.TaxCalculation.Service.Serviceimplementations;
using Storm.JWTHelper;
using Storm.PerformanceEvaluation.Repository.Implimentation.Log;
using Microsoft.EntityFrameworkCore;
using Storm.PerformanceEvaluation.Repository.Implimentation.Log.LogRepositoryImplimentations;
using Fintranet.TaxCalculation.Repository.LogInterface;
using Storm.JWTHelper.Generate.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appSettings.json")
        .Build();
var appSetting = AppSettingInfo.AppSettingsInfo;

// Add services to the container.
builder.Services.AddRazorPages();
builder.WebHost.UseKestrel();
builder.WebHost.UseUrls(appSetting.HostAddress);

if (appSetting.HostRunAsConsole)
{
    builder.WebHost.UseHttpSys();
}

Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine("PerformanceEvaluation Host run at : " + DateTime.Now.ToString("f"));
Console.ResetColor();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    opt.JsonSerializerOptions.Converters.Add(new PersianDateTimeConverter());
    opt.JsonSerializerOptions.Converters.Add(new GuidJsonConverter());
    opt.JsonSerializerOptions.Converters.Add(new IntToStringConverter());
    opt.JsonSerializerOptions.Converters.Add(new LongToStringConverter());
    opt.JsonSerializerOptions.Converters.Add(new DictionaryInt32Converter());
    opt.JsonSerializerOptions.Converters.Add(new DictionaryInt64Converter());
    opt.JsonSerializerOptions.Converters.Add(new DictionaryTKeyEnumTValueConverter());
    opt.JsonSerializerOptions.WriteIndented = true;
});

//Config Services and Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceHolder, ServiceHolder>();
builder.Services.AddScoped<IExceptionLogger, ExceptionLogger>();
builder.Services.AddSingletonJWTServices();


builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Main")));
builder.Services.AddDbContext<LogContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Log")));
builder.Services.AddHealthChecks();

// configure jwt authentication
var jwtIssuerOptions = appSetting.JwtIssuerOptions;

SymmetricSecurityKey signingKey =
    new SymmetricSecurityKey(
        Encoding.ASCII.GetBytes(jwtIssuerOptions.SecretKey));

// Configure JwtIssuerOptions
builder.Services.Configure<JwtIssuerOptions>(options =>
{
    options.Issuer = jwtIssuerOptions.Issuer;
    options.Audience = jwtIssuerOptions.Audience;
    options.SecretKey = jwtIssuerOptions.SecretKey;
    options.ExpireTimeTokenInMinute = jwtIssuerOptions.ExpireTimeTokenInMinute;
    options.ValidTimeInMinute = jwtIssuerOptions.ValidTimeInMinute;
    options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
});

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = jwtIssuerOptions.Issuer,
    ValidIssuers = jwtIssuerOptions.Issuer.Split(new string[] { "," }, StringSplitOptions.None),
    ValidateAudience = true,
    ValidAudience = jwtIssuerOptions.Audience,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = signingKey,
    RequireExpirationTime = false,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions =>

{
    configureOptions.ClaimsIssuer = jwtIssuerOptions.Issuer;
    configureOptions.TokenValidationParameters = tokenValidationParameters;
    configureOptions.SaveToken = true;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        authBuilder => { authBuilder.RequireRole("Admin"); });

    options.AddPolicy("User",
        authBuilder => { authBuilder.RequireRole("User"); });
});
 

var app = builder.Build().Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("api/[projectName]/HealthChecks");
app.UseAuthentication();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.UseCustomCorrectionTokenMiddleware();


app.UseEndpoints(endpoints => { endpoints.MapControllers().RequireCors(MyAllowSpecificOrigins); });

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();

app.Run();
