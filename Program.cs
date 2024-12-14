using ConfigurationDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseOption>(
    builder.Configuration.GetSection($"{DatabaseOption.SectionName}")
);

builder.Services.Configure<DatabaseOption>(
    DatabaseOption.SystemDatabaseSectionName,
    builder.Configuration.GetSection($"{DatabaseOption.SectionName}:{DatabaseOption.SystemDatabaseSectionName}")
);
builder.Services.Configure<DatabaseOption>(
    DatabaseOption.BusinessDatabaseSectionName,
    builder.Configuration.GetSection($"{DatabaseOption.SectionName}:{DatabaseOption.BusinessDatabaseSectionName}")
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
