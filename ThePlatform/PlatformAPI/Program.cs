using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlatformAPI.AsyncDataServices;
using PlatformAPI.Data;
using PlatformAPI.Data.Repositories;
using PlatformAPI.SyncDataAPIs.Grpc;
using PlatformAPI.SyncDataAPIs.Http;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddHttpClient<IMessageBusClient, MessageBusClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddGrpc();

Console.WriteLine("--> Using InMem Db");
builder.Services.AddDbContext<AppDbContext>(opt =>
     opt.UseInMemoryDatabase("InMem"));

/*Console.WriteLine("--> Using SqlServer Db");
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));*/

var app = builder.Build();
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapGet("/protos/platforms.proto", async context =>
    {
        endpoints.MapGrpcService<GrpcPlatformService>();

        endpoints.MapGet("/protos/platforms.proto", async context =>
        {
            await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));
        });
    });
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

PrepDb.PrepPopulation(app);
