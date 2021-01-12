using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabaevTask5.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BabaevTask5.Data;
using BabaevTask5.Data.Interfaces;
using BabaevTask5.Data.Models;
using BabaevTask5.Data.Repository;
//using BabaevTask5.Data.User;
using BabaevTask5.Middleware;
using Microsoft.AspNetCore.Identity;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

/*
 *
 * Что предстоить сделать
 *
 * 
 * 1)Разобраться в инъекциях
 *   --избавиться от using Microsoft.AspNetCore.Hosting; в классах типа репозиторий
 * 2)Как сделать так чтобы контроллеры возвращали некую универсальную прослойку
 *   --переделать всё под сваггер
 * 3)Как правильно обрабатывать ошибки и что возвращать
 * 4)Связать типы Материалов в Enum ------------------------------------------------------ СДЕЛАНО
 * 5)Сделать регистрацию ----------------------------------------------------------------- СДЕЛАНО
 * 6)Авторизацию 
 * 7)Доделать контроль ролей
 * 8)Исследовать вопрос с мапперами 
 */

namespace BabaevTask5
{
    public class Startup
    {
        //private IConfigurationRoot _confString;
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        

        /*public Startup(IHostingEnvironment hostEnv) {
            _confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }*/
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
             Ранее у меня было так
             services.AddDbContext<AppDbContent>(options => 
                options.UseNpgsql(_confString.GetConnectionString("DefaultConnection")));*/
            
            // добавление ApplicationDbContext для взаимодействия с базой данных учетных записей
            services.AddDbContext<AppDbContent>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                options.Lockout.MaxFailedAccessAttempts = 5;
            }).AddEntityFrameworkStores<AppDbContent>();

            services.AddTransient<IMaterial, MaterialRepository>();
            services.AddTransient<IVersion, VersionRepository>();
            services.AddControllersWithViews();
            services.AddMvc();
            /*// Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            /*
            Keep in mind that the order in which middleware is added can make 
            a difference in how the application behaves. 
            Since the middleware this post is dealing with is logging.
            */
            app.UseRequestResponseLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            /*app.UseAuthorization();*/
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}