using ApiSessions.Data;
using ApiSessions.life_time;
using ApiSessions.Middleware;
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
//imiddleare
builder.Services.AddTransient<TestMiddleware>();
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
app.Use(async (HttpContext context, RequestDelegate next) =>//it like endpoint but instead of controller it is middleware, it can be used to handle requests before reach controller and responses before send back to client, it can be used to handle authentication, authorization, logging, error handling, etc. middleware is executed in the order they are added to the pipeline
{
    Console.WriteLine($">>>Request: {context.Request.Method} {context.Request.Path}");
    await next(context);//call next middleware or controller
    Console.WriteLine($"<<<Response: {context.Response.StatusCode}");
});
/*//now you know life of request until reach controller and life of response until send back to client, now you can use logger to log these information to file or database to trace bugs and monitor performance, you can also use middleware to handle exceptions globally and return custom error response to client, you can also use middleware to handle authorization,CORS,compression, caching,
 >>>Request: PUT /api/TaskItem/65
info: ApiSessions.Controllers.TaskItemController[0]
      Starting update operation for task with id 65.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (47ms) [Parameters=[@id='?' (DbType = Int32)], CommandType='Text', CommandTimeout='30']
      SELECT TOP(1) [t].[Id], [t].[Created], [t].[Description], [t].[Pirority], [t].[Status], [t].[Title]
      FROM [tasks] AS [t]
      WHERE [t].[Id] = @id
warn: ApiSessions.Controllers.TaskItemController[0]
      Warning Task with id 65 not found. Object reference not set to an instance of an object.
fail: ApiSessions.Controllers.TaskItemController[0]
      An error occurred while updating task with id System.NullReferenceException: Object reference not set to an instance of an object.
         at ApiSessions.Service.TaskService.UpdateAsync(Int32 id, TaskItem newTask) in C:\Users\mmwmn\source\repos\ApiSessions\ApiSessions\Service\TaskService.cs:line 49
         at ApiSessions.Controllers.TaskItemController.Update(Int32 id, TaskItem newTask) in C:\Users\mmwmn\source\repos\ApiSessions\ApiSessions\Controllers\TaskItemController.cs:line 146.
crit: ApiSessions.Controllers.TaskItemController[0]
      Critical error occurred while updating task with id 65.
Update operation for task with id 65 has completed.
<<<Response: 404
 */

//app.Run(async (HttpContext context) =>//Run no next
//{
//    Console.WriteLine($">>>Request2: {context.Request.Method} {context.Request.Path}");
//  //  await next();//call next middleware or controller
//    Console.WriteLine($"<<<Response2: {context.Response.StatusCode}");
//});
//app.Map("/hello", hello =>// Map make new branch in the pipeline to handle specific endpoint, it can be used to handle specific endpoint without reach controller,
//{
//    app.Use(async (HttpContext context, RequestDelegate next) =>
//    {
//        Console.WriteLine($">>>Request2: {context.Request.Method} {context.Request.Path}");
//        await context.Response.WriteAsync("Hello World!");//return custom response in all endpoints
//        Console.WriteLine($"<<<Response2: {context.Response.StatusCode}");
//     } );
//});
/*| Middleware | الاستخدام                     |
| ---------- | ----------------------------- |
| `Use()`    | لكل الـ Requests              |
| `Run()`    | نهاية الـ Pipeline — بلا Next |
| `Map()`    | فرع لعنوان URL محدد           |
*/
//app.UseMiddleware<CustomMiddleware>();
//CustomMiddlewareExtensions.UseCustomMiddleware(app);
app.UseCustomMiddleware();//extension method to use custom middleware
app.UseTestMiddleware();//use middleware by implement imiddleware interface
app.Use(async (HttpContext context,RequestDelegate next) =>
{
    Console.WriteLine($">>>Request3: {context.Request.Method} {context.Request.Path}");
    await next(context);//call next middleware or controller
    Console.WriteLine($"<<<Response3: {context.Response.StatusCode}");
});
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

