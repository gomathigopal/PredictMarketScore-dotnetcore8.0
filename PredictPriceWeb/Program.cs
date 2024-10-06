var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {

            //you can configure your custom policy
            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/PredictScore/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseCors(
   );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PredictScore}/{action=Index}/{id?}");

app.Run();
