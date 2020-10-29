using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Halic.Bussiness.Abstract;
using Halic.Bussiness.Concrate;
using Halic.Data.Abstract;
using Halic.Data.Concrate.EfCore;
using HalicHub.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HalicHub
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //user servis iþlemleri
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("server=.;database=DBHalicHubUp;integrated security=true;"));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();
            //ýdentity ayarlarý
            services.Configure<IdentityOptions>(options =>
            {
                //password
                options.Password.RequireDigit = true;//sayý
                options.Password.RequireLowercase = true;//küçük harf
                options.Password.RequireUppercase = true;//büyük harf
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;//alfa olsun
                //Lockout
                options.Lockout.MaxFailedAccessAttempts = 5;//yanlýþ deneme sayýsý--> kitler
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);//3 dk sonra açar
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;//her kullanýcýnýn farklý emaili olsun
                options.SignIn.RequireConfirmedEmail = false;//kullanýcý hesabý onaylasýn
            });

            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";//her login olan sipariþ verebilir ama adimini görüntülüyemez
                options.SlidingExpiration = true;//20 dk istek yapmazsan cookie silinir
                options.ExpireTimeSpan = TimeSpan.FromDays(365);//uygulmaya gelen 365 gün benim uygulama seni tanýr
                options.Cookie = new CookieBuilder
                {
                    HttpOnly=true,
                    Name=".HalicHub.Security.Cookie",
                    SameSite=SameSiteMode.Strict
                };

            });


            //------ katmanlý mimari iþlemleri
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleServices, ArticleManager>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryServices, CategoryManager>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorServices, AuthorManager>();

            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<ISliderServices, SliderManager>();

            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsServices, NewsManager>();

            services.AddScoped<INCategoryRepository, NCategoryRepository>();
            services.AddScoped<INCategoryServices, NCategoryManager>();

            services.AddScoped<IActivitiesRepository, ActivitiesRepository>();
            services.AddScoped<IActivitiesServices, ActivitiesManager>();

            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<IVideoServices, VideoManager>();

            services.AddScoped<IEMailRepository, EMailRepository>();
            services.AddScoped<IEMailServices, EMailManager>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IConfiguration configuration,UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                name: "adminrol",
                pattern: "admin/RoleList",
                defaults: new { controller = "Admin", action = "RoleList" }
            );

                endpoints.MapControllerRoute(
                    name: "adminrol",
                    pattern: "admin/RoleCreate",
                    defaults: new { controller = "Admin", action = "RoleCreate" }
                );
                endpoints.MapControllerRoute(
                  name: "adminroleedit",
                  pattern: "admin/role/{id?}",
                  defaults: new { controller = "Admin", action = "RoleEdit" }
              );

                endpoints.MapControllerRoute(
                 name: "yazarsearch",
                 pattern: "authorsearch",
                 defaults: new { Controller = "Home", action = "AuthorSearch" }
             );
                endpoints.MapControllerRoute(
                  name: "AccountLogin",
                  pattern: "Login",
                  defaults: new { controller = "Account", action = "Login" }
                  );

                endpoints.MapControllerRoute(
                   name: "AuthorListAdmin",
                   pattern: "Halic/Bt/Admin/YazarListesi",
                   defaults: new { controller = "Home", action = "AuthorListAdmin" }
                   );

                endpoints.MapControllerRoute(
                  name: "AuthorCreateAdmin",
                  pattern: "Halic/Bt/Admin/YazarGuncelle/{id?}",
                  defaults: new { controller = "Home", action = "AuthorEditAdmin" }
                  );

                endpoints.MapControllerRoute(
                 name: "AuthorCreateAdmin",
                 pattern: "Halic/Bt/Admin/YazarOlustur",
                 defaults: new { controller = "Home", action = "AuthorCreateAdmin" }
                 );

                //----------------------------
                endpoints.MapControllerRoute(
                   name: "ArticleListAdmin",
                   pattern: "Admin/MakaleListesi",
                   defaults: new { controller = "Home", action = "ArticleListAdmin" }
                   );

                endpoints.MapControllerRoute(
                  name: "ArticleCreateAdmin",
                  pattern: "Admin/MakeleGuncelle/{id?}",
                  defaults: new { controller = "Home", action = "ArticleEditAdmin" }
                  );

                endpoints.MapControllerRoute(
                 name: "ArticleCreateAdmin",
                 pattern: "Admin/MakeleOlustur",
                 defaults: new { controller = "Home", action = "ArticleCreateAdmin" }
                 );

                      //----------------------------------
                   //-----------------------------
                //--------------------------
                endpoints.MapControllerRoute(
                   name: "Videos",
                   pattern: "Halics/Video",
                   defaults: new { controller = "Home", action = "Video" }
                   );
                //--------------------------------------------------
                endpoints.MapControllerRoute(
                   name: "ActityDetails",
                   pattern: "Halic/Aktivite/{detail?}",
                   defaults: new { controller = "Home", action = "ActivitiesDetails" }
                   );

                //-------------------------------------
                endpoints.MapControllerRoute(
                   name: "HaberlerSearch",
                   pattern: "NewsSearch",
                   defaults: new { controller = "Home", action = "NewsSearch" }
                   );

                endpoints.MapControllerRoute(
                    name: "HaberlerDetails",
                    pattern: "Haberler/{url}",
                    defaults: new { controller = "Home", action = "NewsDetails" }
                    );

                endpoints.MapControllerRoute(
                   name: "NewsList",
                   pattern: "Halic/Haberler/{categories?}/{page?}",
                   defaults: new { controller = "Home", action = "NewsList" }
                   );
                //--------------------------
                endpoints.MapControllerRoute(
                   name: "AuthorDetails",
                   pattern: "Yazalarlar/{author}",
                   defaults: new { controller = "Home", action = "AuthorDetails" }
                   );

                endpoints.MapControllerRoute(
                    name: "AuthorList",
                    pattern: "Yazarlar",
                    defaults: new { controller = "Home", action = "AuthorList" }
                    );
                //-----------------------------------
                endpoints.MapControllerRoute(
                  name: "search",
                  pattern: "search",
                  defaults: new { Controller = "Home", action = "search" }
              );

                endpoints.MapControllerRoute(
                     name: "ArticlesDetails",
                     pattern: "makaleler/{url}",
                     defaults: new { controller = "Home", action = "ArticleDetails" }
                     );

                endpoints.MapControllerRoute(
                    name: "Articles",
                    pattern: "Halic/makaleler/{category?}/{page?}",
                    defaults: new { controller = "Home", action = "ArticleList" }
                    );
                //------------------------------------------------------------
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedIdentity.Seed(userManager, roleManager, configuration).Wait();
        
        }
    }
}
