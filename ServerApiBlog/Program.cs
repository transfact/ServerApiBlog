
using Microsoft.EntityFrameworkCore;
using ServerApiBlog.Models;
using ServerApiBlog.Data;
var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<BlogContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogCRUDServerContext") ?? throw new InvalidOperationException("Connection string 'BlogCRUDServerContext' not found.")));



// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MemberBlogContext>(options => options.UseInMemoryDatabase("MemberBlog"));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
