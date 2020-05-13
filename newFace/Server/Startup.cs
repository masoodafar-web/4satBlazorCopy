using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using newFace.Client;
using newFace.Server.Data;
using newFace.Server.Push;
using newFace.Server.Services;
using newFace.Server.Services.Education;
using newFace.Server.Services.Generic;
using newFace.Server.Services.Resource;
using newFace.Server.Services.Shop;
using newFace.Server.Services.User;
using newFace.Shared.Models;
using newFace.Shared.Repositories;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using newFace.Shared.Repositories.Shop;
using newFace.Shared.Repositories.User;
using newFace.Server.App_Start;
using newFace.Shared.Repository.Push;

namespace newFace.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<newFaceDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedPhoneNumber = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;

                })
                .AddEntityFrameworkStores<newFaceDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, newFaceDbContext>().AddProfileService<ProfileService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddRazorPages();
            services.AddDetection();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IVisionRepository, VisionRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISuggestionRepository, SuggestionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISendRepository, SendRepository>();
            services.AddScoped<ICommissionRepository, CommissionRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IGiftRepository, GiftRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductScaleRepository, ProductScaleRepository>();
            services.AddScoped<IShareholderRepository, ShareholderRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IProductSeenInfoRepository, ProductSeenInfoRepository>();
            services.AddScoped<IPointRepository, PointRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IFireBaseNotification, FireBaseNotification>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseDetection();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
