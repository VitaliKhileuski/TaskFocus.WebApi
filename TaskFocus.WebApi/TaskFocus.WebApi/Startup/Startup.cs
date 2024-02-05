using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskFocus.Data;
using TaskFocus.WebApi.Core.Configs;
using TaskFocus.WebApi.Core.Constants;
using TaskFocus.WebApi.Presentation.Schemas;
using TaskFocus.WebApi.Startup.Validation;

namespace TaskFocus.WebApi.Startup
{
    public class Startup
    {
        readonly string CorsPolicyName = "TaskFocusCORS";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterOptions(Configuration);

            services.RegisterRepositories();

            services.RegisterServices();

            services.ConfigureDatabases(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    builder => builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithExposedHeaders("Content-Disposition"));
            });
            
            services.AddTokenAuthentication(Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition(SwaggerConstants.SecurityDefinitionName, new OpenApiSecurityScheme
                {
                    Name = SwaggerConstants.SecuritySchemeName,
                    Type = SecuritySchemeType.Http,
                    Scheme = SwaggerConstants.SecurityScheme,
                    In = ParameterLocation.Header,
                    Description = SwaggerConstants.SwaggerAuthorizationDescription
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = SwaggerConstants.SecurityScheme
                            }
                        },
                        new string[] { }
                    }
                });

                c.SwaggerDoc(SwaggerConstants.Version,
                    new OpenApiInfo { Title = SwaggerConstants.DocumentTitle, Version = SwaggerConstants.Version });

                c.SchemaFilter<EnumSchemaFilter>();
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.AllowInputFormatterExceptionMessages = true;
            });

            services.AddMvc(options => { options.Filters.Add<ValidationFilter>(); })
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>();
                    options.ImplicitlyValidateRootCollectionElements = true;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerConstants.Endpoint, SwaggerConstants.DocumentTitle);
                c.RoutePrefix = SwaggerConstants.RoutePrefix;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            Migrate(app);
        }

        private void Migrate(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<Context>();
            context?.Database.Migrate();
        }
    }
}