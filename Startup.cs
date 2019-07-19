using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using crudMongoDB.Data.Context;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using crudMongoDB.GraphQL;

namespace crudMongoDB
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
            cxMongoDB.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
            cxMongoDB.DataBaseName = Configuration.GetSection("MongoConnection:Database").Value;
            cxMongoDB.IsSSL = Convert.ToBoolean(Configuration.GetSection("MongoConnection:IsSSL").Value);

            services.AddScoped<IDependencyResolver>(x =>
            new FuncDependencyResolver(x.GetRequiredService));

            services.AddScoped<crudMongoQuery>();

            services.AddGraphQL(x =>
            {
                x.ExposeExceptions = true; //set true only in development mode. make it switchable.
            })
            .AddGraphTypes(ServiceLifetime.Scoped);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseGraphQL<crudMongoSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
