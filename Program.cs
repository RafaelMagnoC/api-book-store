using api_bookStore.App.Config;
using api_BookStore.App.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddTransientConfiguration();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddCorsConfiguration();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddControllers();

WebApplication app = builder.Build();

app.UseMiddleware<CustomExceptionMiddleware>();

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