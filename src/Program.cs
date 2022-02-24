using ASPNETMongoDBExample;
using ASPNETMongoDBExample.Context;
using ASPNETMongoDBExample.Repository;

var builder = WebApplication.CreateBuilder(args);

// The next three lines are the important ones for our Mongo Example.
// We're setting up the settings, which will come from appsettings.json/appsettings.Development.json
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

// Then we're setting up the dependency injection for our context (which has the actual MongoClient,
// so it's Singleton) and our repository, which uses the context, as transient.
builder.Services.AddSingleton<IExampleMongoContext, ExampleMongoContext>();
builder.Services.AddTransient<ITodoRepository, TodoRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

