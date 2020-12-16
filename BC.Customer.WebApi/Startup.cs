using Azure.Storage.Blobs;
using BC.Customer.Business.Managers;
using BC.Customer.Business.Mappers;
using BC.Customer.Business.Mappers.Customer;
using BC.Customer.Business.Validators;
using BC.Customer.Business.Wrappers.Customer;
using BC.Customer.Common;
using BC.Customer.Contracts.Common;
using BC.Customer.Contracts.Managers;
using BC.Customer.Contracts.Repos;
using BC.Customer.Data.CustomerDbContext;
using BC.Customer.Data.Mappers;
using BC.Customer.Data.Repos;
using BC.Customer.Entities.Common;
using BC.Customer.Entities.DTO;
using BC.Customer.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.IO;
using Serilog;

namespace BC.Customer.WebApi
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BC.Customer.Web", Version = "v1" });

            });
            #region Dbcontext

            services.AddDbContext<CustomerEntities>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Connectionstring")));
            #endregion

            #region Manager
            services.AddTransient<ICustomerManager, CustomerManager>();
            #endregion

            #region Repos
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            #endregion

            #region Mappers
            services.AddTransient<IMapper<IList<Message>, ServiceResponse>, ServiceErrorMapper>();
            services.AddTransient<IMapper<Object, ServiceResponse>, ServiceResponseMapper>();
            services.AddTransient<IEntityMapper, EntityMapper>();
            services.AddTransient<IMapper<CustomerDataMapperWrapper, Customers>, CustomerDataMapper>();
            #endregion

            #region util
            services.AddTransient<IErrorMessagesHandler, ErrorMessagesHandler>();
            #endregion

            #region Validators
            services.AddSingleton<IValidator<CustomerDataMapperWrapper>, CustomerValidator>();
            #endregion

            #region AzureBlob
            services.AddScoped(x => new BlobServiceClient(Configuration.GetValue<string>("AzureBlobStorage")));
            services.AddScoped<IBlobService, BlobService>();
            #endregion

            services.AddControllers();
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.SetIsOriginAllowed(_ => true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory() + Configuration.GetValue<string>("FilePath");
            if (!File.Exists(path))
            {

                loggerFactory.AddFile(path);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BC.Customer.WebApi v1"));
            }

            app.UseRouting();
            app.UseCors(x => x.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
