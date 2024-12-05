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
// Web uygulamas� i�in bir yap�land�rma olu�turur.
// 'args', komut sat�r� arg�manlar�n� i�erir ve uygulaman�n farkl� konfig�rasyonlar ile ba�lat�lmas�n� sa�lar.

builder.Services.AddControllersWithViews();
// Uygulamaya MVC (Model-View-Controller) mimarisini ekler.
// Bu sayede hem Controller s�n�flar�n� (API endpoint) hem de View'lar� (HTML sayfalar�) kullanabiliriz.
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
// Uygulama yap�land�rmas�n� tamamlayarak, �al��t�r�labilir bir web uygulamas� olu�turur.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Uygulama geli�tirme ortam�nda de�ilse (�rne�in, Production ortam�nda),
    // bir hata meydana geldi�inde kullan�c�y� '/Home/Error' adresine y�nlendirir.

    app.UseHsts();
    // HTTP Strict Transport Security (HSTS) protokol�n� etkinle�tirir.
    // Bu protokol, taray�c�lar�n yaln�zca HTTPS �zerinden ba�lant� kurmas�n� sa�lar ve g�venli�i art�r�r.
}

app.UseHttpsRedirection();
// HTTP isteklerini otomatik olarak HTTPS isteklerine y�nlendirir.
// Bu, kullan�c�lar�n verilerinin �ifrelenmi� bir �ekilde iletilmesini sa�lar.

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseStaticFiles();
// Uygulaman�n `wwwroot` klas�r�ndeki CSS, JavaScript ve resim dosyalar� gibi statik dosyalar� sunmas�n� sa�lar.

app.UseRouting();
// Y�nlendirme (routing) mekanizmas�n� etkinle�tirir.
// Bu, gelen HTTP isteklerinin hangi controller ve action'a y�nlendirilece�ini belirler.* 
app.UseAuthentication();
app.UseAuthorization();// Yetkilendirme mekanizmas�n� etkinle�tirir.
app.UseSession();

SeedDatabase();

// Bu, belirli bir kayna�a veya i�leme eri�im yetkisinin olup olmad���n� kontrol eder.
app.MapRazorPages();


app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
// Varsay�lan bir rota tan�mlar.
// E�er URL'de controller belirtilmezse `Home` controller'�, action belirtilmezse `Index` action'� �al��t�r�l�r.
// `id?` ifadesi ise 'id' parametresinin opsiyonel oldu�unu belirtir.

app.Run();
// Uygulamay� �al��t�r�r ve HTTP isteklerini dinlemeye ba�lar.
// Web sunucusu devreye al�n�r ve gelen istekler i�lenmeye ba�lar.
void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}