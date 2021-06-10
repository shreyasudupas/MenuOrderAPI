using BuisnessLayer;
using BuisnessLayer.AccessLayer;
using BuisnessLayer.AccessLayer.IAccessLayer;
using BuisnessLayer.AccessLayer.IModels;
using BuisnessLayer.DBModels;
using BuisnessLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using OrderAPI.Filters;
using OrderAPI.Middleware;
using OrderAPI.SwaggerOptionsFilters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

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

            //Adding Swagger
            services.AddSwaggerGen(c =>
            {
                var titleBase = "MenuMangement API";
                var description = "This is a Web API for Menu operations";
                var License = new OpenApiLicense()
                {
                    Name = "MIT"
                };
                var contact = new OpenApiContact
                {
                    Email = "shreyasudupas@gmail.com",
                    Name = "Shreyas Udupa S",
                    Url = new Uri("https://MenuOrderManagement.com")
                };

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = titleBase + " v1",
                    Version = "v1",
                    Description = description,
                    Contact = contact,
                    License = License
                });

                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = titleBase + " v2",
                    Version = "v2",
                    Description = description,
                    Contact = contact,
                    License = License
                });
                

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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

                //Adding Custom header Attribute
                c.OperationFilter<CustomHeaderSwaggerAttribute>();

                c.IncludeXmlComments(System.IO.Path.Combine(System.AppContext.BaseDirectory, "OrderAPI.xml"));
            });

            //Adding Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
            }) ;

            services.AddAuthorization(options=>
            {
                options.AddPolicy("AllowUserAccess", policy => policy.Requirements.Add(new UserRequirement("User")));
            });

            services.AddControllers(options=>
            {
                options.Conventions.Add(new GroupingByNamespaceConvention());
            });

            
            services.AddDbContext<MenuOrderManagementContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"))
            );

            //register for properties
            services.AddScoped<IProfileUser, ProfileUser>();

            //Register for classes
            services.AddScoped<IOrderBL, OrderBL>();
            services.AddScoped<IUserBL, UserBL>();
            services.AddSingleton<IProducer, Producer>();
            services.AddSingleton<ISubsciber, SubsciberBreakfast>();

            services.AddHostedService<RabbitMQHosterService>();

            //register all the Authorization Handlers here
            services.AddScoped<IAuthorizationHandler, CheckIfUserHandler>();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MenuOrder v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "MenuOrder v2");
            });

            app.UseRouting();

            app.UseCors("AllowMyOrigin");

            app.UseAuthentication();

            app.UseMiddleware<GetUserMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        public class GroupingByNamespaceConvention : IControllerModelConvention
        {
            public void Apply(ControllerModel controller)
            {
                var controllerNamespace = controller.ControllerType.Namespace;
                var apiVersion = controllerNamespace.Split(".").Last().ToLower();
                if (!apiVersion.StartsWith("v")) { apiVersion = "v1"; }
                controller.ApiExplorer.GroupName = apiVersion;
            }
        }
    }
}
