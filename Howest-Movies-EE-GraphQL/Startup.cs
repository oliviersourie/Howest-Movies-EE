using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Howest_Movies_EE_DAL.Models;
using Howest_Movies_EE_DAL.Repositories;
using Howest_Movies_EE_GraphQL.GraphQLTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Howest_Movies_EE_GraphQL
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
            services.AddControllers();
            services.AddDbContext<MoviesContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SchoolDb")));

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<StudentType>();
            services.AddSingleton<CursusType>();
            services.AddScoped<RootQuery>();
            services.AddScoped<IDependencyResolver>(d => new FuncDependencyResolver(d.GetRequiredService));
            services.AddScoped<ISchema, RootSchema>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseGraphiQl();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
