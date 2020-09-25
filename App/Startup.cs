using App.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParceriaAPI.SeveralFunctions;

namespace App
{
    /* --> † 25/09/2020 - Luiz Lenire. <-- */

    public sealed class Startup
    {
        #region --> Constructors. <--

        public Startup()
        {
            IConfiguration iconfiguration = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true)
                                                                      .Build();

            GlobalAtributtes.Timeout = int.Parse(iconfiguration["AppSettings:Timeout"]);
            GlobalAtributtes.DatabaseType = iconfiguration["AppSettings:DatabaseType"] == "1" ? GlobalEnum.DatabaseType.SQLServer :
                                               iconfiguration["AppSettings:DatabaseType"] == "2" ? GlobalEnum.DatabaseType.MySQL :
                                               iconfiguration["AppSettings:DatabaseType"] == "3" ? GlobalEnum.DatabaseType.PostgreSQL :
                                                                                                      GlobalEnum.DatabaseType.SQLServer;

            GlobalAtributtes.Server = iconfiguration["AppSettings:Server"];
            GlobalAtributtes.Database = iconfiguration["AppSettings:Database"];
            GlobalAtributtes.User = iconfiguration["AppSettings:User"];
            GlobalAtributtes.Password = iconfiguration["AppSettings:Password"];
        }

        #endregion --> Constructors. <--   

        #region --> Public methods. <--

        public void ConfigureServices(IServiceCollection iServiceCollection)
        {
            iServiceCollection.AddControllersWithViews();
            iServiceCollection.AddTransient<IRepository, Repository>();
        }

        public void Configure(IApplicationBuilder iApplicationBuilder, IWebHostEnvironment iWebHostEnvironment)
        {
            if (iWebHostEnvironment.IsDevelopment()) iApplicationBuilder.UseDeveloperExceptionPage();
            else
            {
                iApplicationBuilder.UseExceptionHandler("/Home/Error");              
                iApplicationBuilder.UseHsts();
            }

            iApplicationBuilder.UseHttpsRedirection();
            iApplicationBuilder.UseStaticFiles();
            iApplicationBuilder.UseRouting();
            iApplicationBuilder.UseAuthorization();
            iApplicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #endregion --> Public methods. <--
    }
}
