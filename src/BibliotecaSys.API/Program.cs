using Serilog;
using BibliotecaSys.API.Extensions;
using BibliotecaSys.API.Mappings;
using BibliotecaSys.API.Middleware;
using BibliotecaSys.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Set up logging
builder.Host.UseSerilog(SerilogExtensions.InitializeSerilog(config));

// Register controllers and API endpoints
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Set up Swagger documentation
builder.Services.AddSwaggerGenWithOptions();

// DbContext and Identity
builder.Services.AddCustomDbContext<AppDbContext>(config);

// Add custom services
builder.Services.AddUnitOfWork();
builder.Services.AddGenericRepository();
builder.Services.AddServicesFromAssembly(AppDomain.CurrentDomain.FriendlyName);
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Build the application
var app = builder.Build();

// Apply custom migrations
app.ApplyMigrations<AppDbContext>();

// Set up Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Set up Serilog request logging
app.UseSerilogRequestLogging();

// Map application endpoints and controllers
app.MapApplicationEndpoints();
app.UseExceptionCatcherMiddleware();
app.MapControllers();

app.Run();