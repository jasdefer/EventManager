﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DataLayer.Repository;
using DataLayer.Repository.DatabaseRepository;
using AutoMapper;
using BusinessLayer;
using DataTransfer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using EventApi.Services.Identity;

namespace EventApi
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRegionRepository, RegionDatabaseRepository>();
            services.AddTransient<IVistorRepository, VisitorDatabaseRepository>();
            services.AddTransient<RegionManager>();
            services.AddTransient<VisitorManager>();
            services.AddSingleton(_configuration);
            services.AddAutoMapper();
            services.AddIdentity<VisitorDto,IdentityRole>()
                .AddUserStore<UserStore>();
            services.Configure<IdentityOptions>(config =>
            {
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = (ctx) =>
                    {
                        if (ctx.Response.StatusCode == 200) ctx.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = (ctx) =>
                    {
                        if (ctx.Response.StatusCode == 200) ctx.Response.StatusCode = 403;
                        return Task.CompletedTask;
                    },
                };
            });
            services.AddTransient<IDataSeeder, TestSeed>();
            
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IDataSeeder seeder)
        {
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentity();
            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudience = _configuration["Token:Audience"],
                    ValidIssuer= _configuration["Token:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:JwtKey"])),
                    ValidateLifetime = true
                }
            });

            app.UseMvc();

            DatabaseContext.CreateDb(_configuration["ConnectionString"], true);
            seeder.SeedData();
        }
    }
}
