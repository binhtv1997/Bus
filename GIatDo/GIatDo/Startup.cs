using GiatDo.Data;
using GiatDo.Data.Infrastructure;
using GiatDo.Data.Repositories;
using GiatDo.Service.Service;
using GIatDo.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;

namespace GIatDo
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
            services.AddDbContext<GiatDoDbContext>();

            #region DI solutions
            //add for data
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IAdminService, AdminService>();


            services.AddTransient<IServiceTypeRepository, ServiceTypeRepository>();
            services.AddTransient<IServiceTypeService, ServiceTypeService>();

            services.AddTransient<ISlotService, SlotService>();
            services.AddTransient<ISlotRepository, SlotRepository>();

            services.AddTransient<IShipperService, ShipperService>();
            services.AddTransient<IShipperRepository, ShipperRepository>();

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            services.AddTransient<ISlotService, SlotService>();
            services.AddTransient<ISlotRepository, SlotRepository>();

            services.AddTransient<IServiceService, ServiceService>();
            services.AddTransient<IServiceRepository, ServiceRepository>();

            services.AddTransient<IOrderService, OrderServices>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddTransient<IOrderSService, OrderSService>();
            services.AddTransient<IOrderServiceRepository, OrderServiceRepository>();

            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IStoreRepository, StoreRepository>();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            #endregion

            #region Setup1
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            #endregion

            #region Swagger
            services.AddSwagger();
            #endregion

            #region Cors
            services.AddCors(options =>
            options.AddPolicy("AllowAll", builder => builder
                                    .WithOrigins("http://localhost:4200", "http://localhost:4201")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials()));
            #endregion

            services.AddSignalR();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseAuthentication();
            app.UseStaticFiles();

            #region Swagger
            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;

                settings.GeneratorSettings.Title = "GIATDO API";

                settings.GeneratorSettings.OperationProcessors.Add(new OperationSecurityScopeProcessor("Bearer"));

                settings.GeneratorSettings.DocumentProcessors.Add(new SecurityDefinitionAppender("Bearer",
                    new SwaggerSecurityScheme
                    {
                        Type = SwaggerSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = "Copy 'Bearer ' + valid JWT token into field",
                        In = SwaggerSecurityApiKeyLocation.Header
                    }));
            });
            #endregion
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseSignalR(routes =>
            {
                routes.MapHub<CenterHub>("/centerHub");
            });
            app.UseMvc();

        }
    }
}
