using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeApi.Auth;
using RecipeApi.Helpers;
using RecipeApi.Models;
using RecipeApi.Models.Interfaces;
using RecipeApi.Models.Repos;

namespace RecipeApi
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
            StaticValues.ConnectionHelper = configuration.GetConnectionString("SQLiteConnection");
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<RecipeContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<RecipeContext>(o => o.UseSqlite(StaticValues.ConnectionHelper));

            services.AddTransient<IApiKeyRepo, ApiKeyRepo>();
            services.AddTransient<IAuthorizationHandler, KeyHandler>();
            services.AddHttpContextAccessor();
            services.AddAuthorization(options =>

            {

                options.AddPolicy(PolicyEnum.Admin.ToString(), policy =>

                    policy.Requirements.Add(new KeyRequirement(PolicyEnum.Admin)));

                options.AddPolicy(PolicyEnum.User.ToString(), policy =>

                    policy.Requirements.Add(new KeyRequirement(PolicyEnum.User)));

                options.AddPolicy(PolicyEnum.Reader.ToString(), policy =>

                    policy.Requirements.Add(new KeyRequirement(PolicyEnum.Reader)));

                options.AddPolicy(PolicyEnum.Lack.ToString(), policy =>

                    policy.Requirements.Add(new KeyRequirement(PolicyEnum.Lack)));

            });

            services           
                .AddMvcCore()                 
                .AddJsonFormatters()
                .AddDataAnnotations()
                .AddJsonOptions(o => o.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Nie znaleziono API.");
            });

            addDefaultKey();
        }
        private void addDefaultKey()

        {

            using (var db = new RecipeContext())

            {

                Key key = new Key
                {

                    Name = configuration[nameof(key.Name)],
                    Role = configuration[nameof(key.Role)],
                    ExpirationDate = new DateTime(2999, 12, 31)

                };

                if (db.Keys.Where(a => a.Name == key.Name).Count() == 0)

                {

                    db.Keys.Add(key);

                    db.SaveChanges();

                }

            }

        }



    }







}
