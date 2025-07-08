using Microsoft.EntityFrameworkCore;
using GradeBook.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlite("Data Source = GradeBook.sqlite"));
var app = builder.Build();

app.MapControllers();

app.Run();

