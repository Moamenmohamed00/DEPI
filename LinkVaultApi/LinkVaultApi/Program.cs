using LinkVaultApi.Data;
using LinkVaultApi.Middlewares;
using Microsoft.EntityFrameworkCore;
using LinkVaultApi.Services.Category;
using LinkVaultApi.Services.Notes;
using LinkVaultApi.Services.BookMark;
using LinkVaultApi.Services.BookMarkNote;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IBookMarkService, BookMarkService>();
builder.Services.AddScoped<IBookMarkNoteService, BookMarkNoteService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
