using System;
using System.Text;
using FundooManager.Interface;
using FundooManager.Manager;
using FundooRepository.Context;
using FundooRepository.Interface;
using FundooRepository.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FundooNotes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //all hosts
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicyAllHosts", 
                    builder =>
                    {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowAnyOrigin();
                    });
            });
            services.AddMvc();
            services.AddDbContextPool<UserContext>(
                        options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));
            //Users Dependency
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserManager, UserManager>();

            //Notes Dependency
            services.AddTransient<INoteRepository, NoteRepository>();
            services.AddTransient<INoteManager, NoteManager>();

            //Collaborator Dependency
            services.AddTransient<ICollaboratorRepository, CollaboratorRepository>();
            services.AddTransient<ICollaboratorManager, CollaboratorManager>();

            //Label Dependency
            services.AddTransient<ILabelRepository, LabelRepository>();
            services.AddTransient<ILabelManager, LabelManager>();

            //swagger implementation and authorization for api implementation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "FundooNotes", Description = "Notes App For You!!", Version = "1.0" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["SecretKey"]))
                };
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder app</param>
        /// <param name="env">IWebHostEnvironment env</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicyAllHosts");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "FundooNotes (V 1.0)");
            });
        }
    }
}
