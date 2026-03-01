using ApiSessions.Data;
using ApiSessions.life_time;
using ApiSessions.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();//add visual to endpoints
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ItaskIService,TaskService>();
//builder.Services.AddScoped<ItaskIService, FakeService>();//by one word i use memory instead of DB


//life time
builder.Services.AddTransient<TransientService>();
builder.Services.AddSingleton<SingletonService>();
builder.Services.AddScoped<ScopedService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();//remove this package

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapOpenApi();
}
/* app.use,app.run,app.mapcontrollers are middlewares,app.run is the last middleware to handle request and return response to client,app.use is used to add middleware to the pipeline,app.mapcontrollers is used to map controllers to endpoints
 * middleware is a software component that is used to handle requests before reach controller and responses before send back to client, it can be used to handle authentication, authorization, logging, error handling, etc. middleware is executed in the order they are added to the pipeline
*/

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
//1)install packages
//use packages
//model+fluent api
//appssetings+dbcontext.cs
//controller
//add-migration,update-database
//2)use Dependancy injection by interface(resopnce on validiation and data)
//edit controller(call Iservice) and program(which implemention of interface i want use)
//different between singleton,transient,scoped
//3) try catch and error handling and logger to handle server logs to trace bugs
//middleware to handle exception globally and return custom error response to client

