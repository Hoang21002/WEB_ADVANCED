using System.Net;

var builder = WebApplication.CreateBuilder(args);
{
    /*Them cac dich vu duoc yeu cau boi MVC Framework*/
    builder.Services.AddControllersWithViews();
}
var app = builder.Build();
{
    /*Cau hinh HTTP Request pipeline*/

    /*Them middleware de hien thi thong bao loi*/

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Blog/Error");
        /*Them middleware cho viec ap dung HSTS (them header
            Strict-TransportContext-Security vao HTTP Response)*/

        app.UseHsts();
    }

    /*Them middleware de chuyen huong HTTP sang HTTPS*/
    app.UseHttpsRedirection();

    /*Them middleware phuc vu cac yeu cau lien quan toi cac
        tap tin noi dung tinh nhu hinh anh, css,...*/
    app.UseStaticFiles();

    /*Them middleware lua chon endpoint phu hop nhat de 
        xu ly mot HTTP request*/
    app.UseRouting();

    /*Dinh nghia route template, route constraint cho cac endpoints
        ket hop voi cac action trong cac controller*/
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Blog}/{action=Index}/{id?}");
}

/*app.MapGet("/", () => "Hello World!");*/

app.Run();
