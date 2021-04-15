using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using UnitTestMoq.Models;
using UnitTestMoq.Services;
using FluentValidation;
using UnitTestMoq.Application.Behaviors;
using UnitTestMoq.DataAccess;
using UnitTestMoq.Domain.Customers;
using FluentValidation.AspNetCore;
using UnitTestMoq.Middlewares;

namespace UnitTestMoq
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
            //services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UnitTestMoq", Version = "v1" });
            });

            #region Connection String

            services.AddDbContext<AppDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("myconn")));

            #endregion Connection String

            services.AddScoped<IEmployeeService, EmployeeService>();

            // Cấu hình MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            //services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Cấu hình Exception filter trong đó có bao gồm FluentValidation
            //services.AddControllers(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
            services.AddControllers()
            // Cấu hình FluentValidation
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            // Add custom binder provider in Startup.cs ConfigureServices
            services.AddMvc(properties =>
            {
                properties.ModelBinderProviders.Insert(0, new JsonModelBinderProvider());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UnitTestMoq v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}