using BuisnessLayer;
using BuisnessLayer.AccessLayer;
using BuisnessLayer.AccessLayer.IAccessLayer;
using BuisnessLayer.DBModels;
using BuisnessLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI
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
            services.AddCors(options=>
            {
                options.AddPolicy("AllowMyOrigin",options=> options.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                );
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
            }) ;

            services.AddControllers();
            services.AddDbContext<MenuOrderManagementContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"))
                );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo 
                {   Title = "Order API", 
                    Version = "1.0" ,
                    Description = "This API shows customer ordering from UI",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email="shreyasudupas@gmail.com",
                        Name="Shreyas Udupa S",
                        Url = new Uri("https://MenuOrderManagement.com")
                    }
                });
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.OAuth2
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
                });
                c.IncludeXmlComments(System.IO.Path.Combine(System.AppContext.BaseDirectory, "OrderAPI.xml"));
            });

            services.AddScoped<IOrderBL, OrderBL>();
            services.AddSingleton<IProducer, Producer>();
            services.AddSingleton<ISubsciber, SubsciberBreakfast>();

            services.AddHostedService<RabbitMQHosterService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c=>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Order API (V1.0)");
            });

            app.UseRouting();

            app.UseCors("AllowMyOrigin");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
