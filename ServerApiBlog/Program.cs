
using Microsoft.EntityFrameworkCore;
using ServerApiBlog.Models;
using ServerApiBlog.Data;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "https://localhost:7281/swagger/index.html"
                                              ).AllowCredentials().AllowAnyHeader().AllowAnyMethod();
                      });
});

//builder.Services.AddDbContext<BlogContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogCRUDServerContext") ?? throw new InvalidOperationException("Connection string 'BlogCRUDServerContext' not found.")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:3000",
                                              "https://localhost:7281");
                      });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MemberBlogContext>(options => options.UseLazyLoadingProxies().UseInMemoryDatabase("MemberBlog"));
//builder.Services.AddDbContext<BlogListContext>(options => options.UseInMemoryDatabase("Blog"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();

app.Run();
