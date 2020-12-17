using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiOrkhan.Data;
using WebApiOrkhan.Middleware;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebApiOrkhan
{
    public class Startup
    {
        private IConfigurationRoot _confString;

        /*public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }*/
        
        public Startup(IHostingEnvironment hostEnv) {
            _confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContent>(options => options.UseNpgsql(_confString.GetConnectionString("DefaultConnection")));
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
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            /*app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            */
            
            app.UseRouting();
            app.UseAuthorization(); //НАЧИНАЮ РЕАЛИЗАЦИЮПО КНИГЕ АДАМА ФРИМАНА MVC CORE 2 с примерами для Профссионалов
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

/*
 * Вопросы
 * 1) Правильно ли логгировал и что по хорошему мы должны логгировать??
 * 2) на сколько нам важно обработать requestStream в удобный нам вид?? Как? (Смущает вид полученного)?
 * 4) CategoryController какой httpGet, Put, Path лучше использовать для задания "Изменение категории материалов"
 * и что мы должны вернуть пользователю?
 *
 * Цели
 * 1) Реализовать Авторизацию
 * 2) Реализовать Контроль доступов для пользователей
 *
 * Как
 * 1)Уже нашел все необходимое и примерно разобрался
 * 2)Основной ресурс это книга ASP NET COR MVC 2 для профессионалов
 * автор Адам Фриман
 * 
 */