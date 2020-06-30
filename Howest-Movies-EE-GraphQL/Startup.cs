using AutoMapper;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Howest_Movies_EE_DAL.Extensions;
using Howest_Movies_EE_GraphQL.GraphQLTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.InitRepositories(Configuration);

            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<MovieType>();
            services.AddSingleton<MovieDetailType>();
            services.AddSingleton<PersonType>();
            services.AddSingleton<PersonDetailType>();
            services.AddSingleton<GenreType>();
            services.AddSingleton<GenreDetailType>();

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
