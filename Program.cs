using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using SMS.Data;
using SMS.HealthCheckDemo;
using SMS.Middleware;
using SMS.Repositorys;
using SMS.Swaggers;
using StudentManagementSystem.Services.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//----------------------- Configure Serilog------------------------
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // Set minimum log level
    .WriteTo.Console()          // Optional: logs to console
    .WriteTo.File(
        "Logs/log-.txt",        // Folder "Logs" and daily rolling file
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

builder.Host.UseSerilog();

//----------------------------------------------------------------------------
//------------------------------ Controllers-------------------------------------
builder.Services.AddControllers();

//---------------------- Add response caching service--------------------------------------

builder.Services.AddResponseCaching();

// ---------------- API Versioning ----------------
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// ---------------- Swagger ----------------
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ---------------- Database ----------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ---------------- Dependency Injection ----------------
// Repository

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<IFeeRepository, FeeRepository>();


//// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.AddScoped<IFeeService, FeeService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IJwtService, JwtService>();

//// JWT Service
builder.Services.AddScoped<IJwtService, JwtService>();
// ---------------- JWT Authentication ----------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

// ---------------- Custom Validation ----------------
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value!.Errors.Count > 0)
            .Select(x => x.Value!.Errors.First().ErrorMessage)
            .ToList();

        return new BadRequestObjectResult(new
        {
            StatusCode = 400,
            Message = "Validation Failed",
            Errors = errors
        });
    };
});
//-------------------Healthceck-------------------------
builder.Services.AddHealthChecks()
                .AddCheck<CustomeHealthCheck>("custom_check")
                .AddNpgSql(connectionString: builder.Configuration.GetConnectionString("DefaultConnection")!,
                  name: "postgresql",
                 failureStatus: HealthStatus.Unhealthy,
                 timeout: TimeSpan.FromSeconds(5));

//----------------------------------------
var app = builder.Build();

// ---------------- Swagger ----------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(

                $"/swagger/{description.GroupName}/swagger.json",

                $"Demo  {description.GroupName.ToUpperInvariant()}");

        }

    });

}

// ---------------- Middleware ----------------
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

//--------------------Healthcheck-------------------
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = System.Text.Json.JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description
            }),
            duration = report.TotalDuration
        });
        await context.Response.WriteAsync(result);
    }
});
//------------------------correlation id-----------------------------
app.UseMiddleware<CorrelationIdMiddleware>();

app.MapGet("/", (HttpContext context) =>
{
    var correlationId = context.Items["X-Correlation-ID"];
    return Results.Ok(new { Message = "Hello World", CorrelationId = correlationId });
});


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();