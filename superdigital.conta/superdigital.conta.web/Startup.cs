using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using superdigital.conta.data;
using superdigital.conta.model;
using superdigital.conta.model.Interfaces;
using superdigital.conta.service;
using Swashbuckle.AspNetCore.Swagger;

namespace superdigital.conta.web
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
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "sistema de credito SuperDigital",
                        Version = "v1",
                        Description = "sistema criado em asp.net core com mongodb"
                    });

                string nomeAplicacao = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string caminhoAplicacao = AppContext.BaseDirectory;
                string caminhoXmlDoc = Path.Combine(caminhoAplicacao, nomeAplicacao);

                c.IncludeXmlComments(caminhoXmlDoc);
            });


            services.AddOptions();
            services.Configure<AppConnectionSettings>(options => Configuration.GetSection("ConnectionStrings").Bind(options));

            services.AddScoped<MongoContext>();
            services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
            services.AddScoped<IContaCorrenteService, ContaCorrenteService>();

            services.AddScoped<ILancamentoRepository, LancamentoRepository>();
            services.AddScoped<ILancamentoService, LancamentoService>();
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Sistema de Credito");
            });
        }
    }
}
