using fullAPInet6.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<UserInfoHandlingService>();
builder.Services.AddScoped<ListDataHandlingService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
               builder =>
               {
                   builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
               });
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");
app.UseRouting();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
