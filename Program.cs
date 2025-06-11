using HuginnLogAPI.Repository;
using HuginnLogAPI.Repository.Implementations;
using HuginnLogAPI.Services;
using NHibernate.Cfg;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddTransient<UserService>();
builder.Services.AddControllers();

var connectionString = builder.Configuration
    .GetConnectionString("Default");
// criar implementacao para ISessionFactory
builder.Services.AddSingleton(c =>
{
    var config = new Configuration().Configure();
    config.DataBaseIntegration(
        x => x.ConnectionString = connectionString
    );
    return config.BuildSessionFactory();
});
builder.Services.AddTransient<IRepository, RepositoryNHibernate>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("HugginLog Api")
            .WithTheme(ScalarTheme.Kepler)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

