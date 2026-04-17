using api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<IRecordCollectionService, RecordCollectionData>();

var app = builder.Build();

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();