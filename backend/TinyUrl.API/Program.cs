using Microsoft.EntityFrameworkCore;
using TinyUrl.API.Mappers;
using TinyUrl.Common.Configuration.Interfaces;
using TinyUrl.Common.Configuration.Models;
using TinyUrl.DAL;
using TinyUrl.Services.Interfaces;
using TinyUrl.Services.Services;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Development,
    WebRootPath = "wwwroot"

});

builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddSingleton<IUrlViewMapper, UrlViewMapper>();
builder.Services.AddSingleton<IDatabaseConfiguration>((_) => builder.Configuration.GetSection("ConnectionStrings").Get<DatabaseConfiguration>());
builder.Services.AddDbContext<TinyUrlDbContext>(options =>
{
    var connetionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connetionString, new MySqlServerVersion(new Version(11, 1, 2)));
});


builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TinyUrl API", Version = "v1" });
});

builder.Services.AddCors();
builder.Services.AddControllers();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var corsDomains = builder.Configuration.GetSection("Security").Get<SecurityConfiguration>().CorsDomain.Split(';');
app.UseCors(options => options
.WithOrigins(corsDomains)
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseHsts();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
