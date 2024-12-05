using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.DbInitializer;
using ECommerce.DataAccess.Repository;
using ECommerce.DataAccess.Repository.IRepository;
using ECommerce.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Stripe;



var builder = WebApplication.CreateBuilder(args);
// Web uygulamasý için bir yapýlandýrma oluþturur.
// 'args', komut satýrý argümanlarýný içerir ve uygulamanýn farklý konfigürasyonlar ile baþlatýlmasýný saðlar.

builder.Services.AddControllersWithViews();
// Uygulamaya MVC (Model-View-Controller) mimarisini ekler.
// Bu sayede hem Controller sýnýflarýný (API endpoint) hem de View'larý (HTML sayfalarý) kullanabiliriz.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<StripeSetting>(builder.Configuration.GetSection("Stripe"));


builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.AddAuthentication().AddFacebook(option => {
    option.AppId = "903386981922894";
    option.AppSecret = "28ad8306026a994ca96103a26b41f665";
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddRazorPages();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();
// Uygulama yapýlandýrmasýný tamamlayarak, çalýþtýrýlabilir bir web uygulamasý oluþturur.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Uygulama geliþtirme ortamýnda deðilse (örneðin, Production ortamýnda),
    // bir hata meydana geldiðinde kullanýcýyý '/Home/Error' adresine yönlendirir.

    app.UseHsts();
    // HTTP Strict Transport Security (HSTS) protokolünü etkinleþtirir.
    // Bu protokol, tarayýcýlarýn yalnýzca HTTPS üzerinden baðlantý kurmasýný saðlar ve güvenliði artýrýr.
}

app.UseHttpsRedirection();
// HTTP isteklerini otomatik olarak HTTPS isteklerine yönlendirir.
// Bu, kullanýcýlarýn verilerinin þifrelenmiþ bir þekilde iletilmesini saðlar.

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseStaticFiles();
// Uygulamanýn `wwwroot` klasöründeki CSS, JavaScript ve resim dosyalarý gibi statik dosyalarý sunmasýný saðlar.

app.UseRouting();
// Yönlendirme (routing) mekanizmasýný etkinleþtirir.
// Bu, gelen HTTP isteklerinin hangi controller ve action'a yönlendirileceðini belirler.* 
app.UseAuthentication();
app.UseAuthorization();// Yetkilendirme mekanizmasýný etkinleþtirir.
app.UseSession();

SeedDatabase();

// Bu, belirli bir kaynaða veya iþleme eriþim yetkisinin olup olmadýðýný kontrol eder.
app.MapRazorPages();


app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
// Varsayýlan bir rota tanýmlar.
// Eðer URL'de controller belirtilmezse `Home` controller'ý, action belirtilmezse `Index` action'ý çalýþtýrýlýr.
// `id?` ifadesi ise 'id' parametresinin opsiyonel olduðunu belirtir.

app.Run();
// Uygulamayý çalýþtýrýr ve HTTP isteklerini dinlemeye baþlar.
// Web sunucusu devreye alýnýr ve gelen istekler iþlenmeye baþlar.
void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}