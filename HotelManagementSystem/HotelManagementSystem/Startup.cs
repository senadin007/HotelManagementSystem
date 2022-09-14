//using HotelManagementSystem.Core.IConfiguration;
//using HotelManagementSystem.Data;
//using Microsoft.EntityFrameworkCore;

//namespace HotelManagementSystem
//{
//    public class Startup
//    {
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddMvc();
//            services.AddDbContext<AppDbContext>(
//                        options => options.UseInMemoryDatabase("HotelMS"));
//            //adding the Unit of work
//            services.AddScoped<IUnitOfWork, UnitOfWork>();
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//            }

//            app.UseRouting();
//            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=HotelController}/{action=Index}/{id?}");
//            });
//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            //app.UseAuthorization();

//            app.MapRazorPages();

//            app.Run();
//        }
//    }
//}
