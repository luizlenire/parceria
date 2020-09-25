using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ParceriaAPI.SeveralFunctions;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Api
{
    /* --> † 24/09/2020 - Luiz Lenire. <-- */

    public sealed class Startup
    {
        #region --> Public static properties. <--    

        public static ResponseAPI responseAPI { get; set; }

        #endregion --> Public static properties. <--

        #region --> Constructors. <--

        public Startup()
        {
            IConfiguration iconfiguration = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true)
                                                                      .Build();

            GlobalAtributtes.TokenExpire = double.Parse(iconfiguration["AppSettings:TokenExpire"]);
            GlobalAtributtes.Timeout = int.Parse(iconfiguration["AppSettings:Timeout"]);

            GlobalAtributtes.DatabaseType = iconfiguration["AppSettings:DatabaseType"] == "1" ? GlobalEnum.DatabaseType.SQLServer :
                                               iconfiguration["AppSettings:DatabaseType"] == "2" ? GlobalEnum.DatabaseType.MySQL :
                                               iconfiguration["AppSettings:DatabaseType"] == "3" ? GlobalEnum.DatabaseType.PostgreSQL :
                                                                                                      GlobalEnum.DatabaseType.SQLServer;

            GlobalAtributtes.Server = iconfiguration["AppSettings:Server"];
            GlobalAtributtes.Database = iconfiguration["AppSettings:Database"];
            GlobalAtributtes.User = iconfiguration["AppSettings:User"];
            GlobalAtributtes.Password = iconfiguration["AppSettings:Password"];

            responseAPI = new ResponseAPI();
        }

        #endregion --> Constructors. <--

        #region --> Public methods. <--

        public void ConfigureServices(IServiceCollection iServiceCollection)
        {
            iServiceCollection.AddControllers();
            iServiceCollection.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            AuthenticationJwT();
            Swagger();
            HealthChecks();

            #region --> Sub-methods. <--

            void AuthenticationJwT()
            {
                iServiceCollection.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenService.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            }

            void Swagger()
            {
                iServiceCollection.AddSwaggerGen(x =>
                {
                    x.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "ParceriaAPI",
                        Version = "1.0.0",
                        Description = "API para demonstração",
                        Contact = new OpenApiContact
                        {
                            Name = "Luiz Lenire",
                            Email = "luizlenire@outlook.com",
                        },

                    });
                    x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                });
            }

            void HealthChecks()
            {
                if (GlobalAtributtes.DatabaseType == GlobalEnum.DatabaseType.SQLServer)
                {
                    iServiceCollection.AddHealthChecks()
                                      .AddSqlServer(GlobalAtributtes.Connectionstring, "SELECT 1;", "Databse connection");
                }

                iServiceCollection.AddHealthChecksUI(setupSettings: x =>
                {
                    x.AddHealthCheckEndpoint("Connectivity API.", "/hc");
                    x.MaximumHistoryEntriesPerEndpoint(50);
                }).AddInMemoryStorage();
            }

            #endregion --> Sub-methods. <--
        }

        public void Configure(IApplicationBuilder iApplicationBuilder, IWebHostEnvironment iWebHostEnvironment)
        {
            if (iWebHostEnvironment.IsDevelopment()) iApplicationBuilder.UseDeveloperExceptionPage();

            iApplicationBuilder.UseMiddleware(typeof(CorsMiddleware));
            iApplicationBuilder.UseHttpsRedirection();
            iApplicationBuilder.UseRouting();
            iApplicationBuilder.UseAuthorization();
            iApplicationBuilder.UseAuthentication();
            iApplicationBuilder.UseEndpoints(x => x.MapControllers());

            iApplicationBuilder.UseCors(x => x.AllowAnyOrigin()
                                              .AllowAnyMethod()
                                              .AllowAnyHeader());

            Swagger();
            HealthChecks();

            #region --> Sub-methods. <--

            void Swagger()
            {
                iApplicationBuilder.UseSwagger();
                iApplicationBuilder.UseSwaggerUI(x => x.SwaggerEndpoint($"{(string.IsNullOrWhiteSpace(x.RoutePrefix) ? "." : "..")}/swagger/v1/swagger.json", "ParceriaAPI V1"));
            }

            void HealthChecks()
            {
                iApplicationBuilder.UseHealthChecks("/hc", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                })
                .UseHealthChecksUI(x =>
                {
                    x.UIPath = "/hc-ui";
                    x.ApiPath = "/hc-api";
                });
            }

            #endregion --> Sub-methods. <--
        }

        #endregion --> Public methods. <--
    }
}
