using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Repository;
using ECommerce.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
// Web uygulamasý için bir yapýlandýrma oluþturur.
// 'args', komut satýrý argümanlarýný içerir ve uygulamanýn farklý konfigürasyonlar ile baþlatýlmasýný saðlar.

builder.Services.AddControllersWithViews();
// Uygulamaya MVC (Model-View-Controller) mimarisini ekler.
// Bu sayede hem Controller sýnýflarýný (API endpoint) hem de View'larý (HTML sayfalarý) kullanabiliriz.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

app.UseStaticFiles();
// Uygulamanýn `wwwroot` klasöründeki CSS, JavaScript ve resim dosyalarý gibi statik dosyalarý sunmasýný saðlar.

app.UseRouting();
// Yönlendirme (routing) mekanizmasýný etkinleþtirir.
// Bu, gelen HTTP isteklerinin hangi controller ve action'a yönlendirileceðini belirler.* 
app.UseAuthentication();
app.UseAuthorization();
// Yetkilendirme mekanizmasýný etkinleþtirir.
// Bu, belirli bir kaynaða veya iþleme eriþim yetkisinin olup olmadýðýný kontrol eder.

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
// Varsayýlan bir rota tanýmlar.
// Eðer URL'de controller belirtilmezse `Home` controller'ý, action belirtilmezse `Index` action'ý çalýþtýrýlýr.
// `id?` ifadesi ise 'id' parametresinin opsiyonel olduðunu belirtir.

app.Run();
// Uygulamayý çalýþtýrýr ve HTTP isteklerini dinlemeye baþlar.
// Web sunucusu devreye alýnýr ve gelen istekler iþlenmeye baþlar.
