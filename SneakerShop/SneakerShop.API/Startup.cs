using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GraphQL;
using GraphQL.Authorization;
using GraphQL.DataLoader;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SneakerShop.API.GraphQL;
using SneakerShop.API.GraphQL.Admin;
using SneakerShop.API.GraphQL.NoSQL;
using SneakerShop.API.GraphQL.Open;
using SneakerShop.API.GraphQL.Restricted;
using SneakerShop.API.Models;
using SneakerShop.API.Services;
using SneakerShop.Models;
using SneakerShop.Models.Data;
using SneakerShop.Models.Repositories;
using SneakerShop.NoSQLModels;
using SneakerShop.NoSQLModels.Data;
using SneakerShop.NoSQLModels.Repositories;

namespace SneakerShop.API
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors();
            services.AddDbContext<SneakerShopContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SneakerShopContext>();
            services.AddScoped(typeof(IProductRepo), typeof(ProductRepo));
            services.AddScoped(typeof(ISupplierRepo), typeof(SupplierRepo));
            services.AddScoped(typeof(IOrderRepo), typeof(OrderRepo));


            // JWT AUTH
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.Audience = Configuration.GetSection("Tokens:Audience").Value;
                  options.ClaimsIssuer = Configuration.GetSection("Tokens:Issuer").Value;
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["Tokens:Issuer"],
                      ValidAudience = Configuration["Tokens:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey
                          (Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                  };
                  options.SaveToken = false;
              });


            // GraphQL v1
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<OpenSchema>();
            services.AddScoped<RestrictedSchema>();
            services.AddScoped<AdminSchema>();
            services.AddScoped<NoSQLSchema>();
            services.AddScoped<DataLoaderDocumentListener>();

            services.AddGraphQL(options => { options.ExposeExceptions = true; }) //toont excepties 

                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddDataLoader()
                ;

            services.AddScoped(typeof(IGenericRepo<Order>), typeof(GenericRepo<Order>));
            services.AddScoped(typeof(IGenericRepo<OrderItem>), typeof(GenericRepo<OrderItem>));
            services.AddScoped(typeof(IUserRepo), typeof(UserRepo));
            services.AddTransient<UserSeeder>();


            // No SQL
            services.AddTransient<NoSQLSeeder>();
            services.AddSingleton<OrdersDBContext>();
            services.Configure<MongoSettings>(Configuration.GetSection(nameof(MongoSettings)));
            services.AddSingleton<IMongoSettings>(sp => sp.GetRequiredService<IOptions<MongoSettings>>().Value);
            services.AddSingleton<IOrderItemNoSQLRepo, OrderItemNoSQLRepo>();
            services.AddSingleton<IOrderNoSQLRepo, OrderNoSQLRepo>();

            var mappingConfig = new MapperConfiguration(mc
                =>
            {
                mc.AddProfile(new ProductProfile());
                mc.AddProfile(new OrderProfile());
                mc.AddProfile(new SupplierProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "SneakerShop", Version = "v1.0" });
            });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            app.UseSwagger(); //enable swagger 
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger"; //path naar swagger pagina: /swagger/index.html 
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Callgate v1.0");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
