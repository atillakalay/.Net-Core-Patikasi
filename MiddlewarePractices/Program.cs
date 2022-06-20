using MiddlewarePractices.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

//app.Run(async context => Console.WriteLine("Middleware 1."));

// app.Run(async context => Console.WriteLine("Middleware 2."));

//  app.Use(async(context,next)=>{
//     Console.WriteLine("Middleware 1 Başladı: ");
//     await next.Invoke();
//     Console.WriteLine("Middleware 1 Sonlandırılıyor.");
//  });

//  app.Use(async(context,next)=>{
//     Console.WriteLine("Middleware 2 Başladı: ");
//     await next.Invoke();
//     Console.WriteLine("Middleware 2 Sonlandırılıyor.");
//  });

app.UseHello();


app.Use(async (context, next) =>
{
    Console.WriteLine("Use Middleware Tetiklendi: ");
    await next.Invoke();
});

app.Map("/example", internalApp =>
    internalApp.Run(async context =>
    {
        Console.WriteLine("/example middleware tetiklendi!");
        await context.Response.WriteAsync("/example tetiklendi");
    }));

app.MapWhen(x => x.Request.Method == "GET", internalApp =>
{
    internalApp.Run(async context =>
    {
        app.MapWhen(x => x.Request.Method == "GET", async internalApp =>
        {
            Console.WriteLine("Middleware Tetiklendi.");
            await context.Response.WriteAsync("Middleware Tetiklendi.");
        });
    });
});
