using TemplatePOC.Core.Damages;
using Wolverine;
using Wolverine.Http;
using TemplatePOC.Core.Plugin;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.AddConsole();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//In PROD, this will probably be changed to looking in a dedicated plugin folder
var pluginAssemblies = builder.Services.RegisterTypesFromAssemblies<IWolverineExtension>("..\\CustomerAPlugin\\bin\\Debug\\net7.0\\");

builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(TemplatePOC.Core.Damages.ReportDamagedShipment).Assembly); //include our core handlers in scanning
    foreach (var plugin in pluginAssemblies) //tell wolverine to scan the plugin for any handlers/middleware/etc.
    {
        opts.Discovery.IncludeAssembly(plugin);
    }
});

var app = builder.Build();

app.MapWolverineEndpoints(opts =>
{

});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("Application is ready to run");

app.Run();
