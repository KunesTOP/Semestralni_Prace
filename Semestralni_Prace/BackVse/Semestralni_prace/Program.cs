var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(360);
    options.Cookie.Name= "Loggin";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


//TODO podívat se na todle ještì, jak vlastnì funguje...
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AllowAnonymousToPage("/Home/Login");
    options.Conventions.AuthorizeFolder("/Logger");
});


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
app.UseSession();
//app.UseAuthorization();

app.MapControllerRoute(
    name: "pacient",
    pattern: "{controller=Pacient}/{action=PacientProfile}/{id?}"
    );


app.Run();
