var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        builder => builder
            .WithOrigins("http://localhost:5173")  // Allow your frontend app
            .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
            .AllowAnyHeader()  // Allow any header
            .AllowCredentials());  // If you're using credentials like cookies or Authorization headers
});

var app = builder.Build();

// Use CORS middleware **before** Authorization and Controllers
app.UseCors("AllowVueApp");

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();  // Authorization should come after CORS

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
