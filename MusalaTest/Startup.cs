using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusalaTest.Application.Dtos.Gateways;
using MusalaTest.Application.Dtos.PeripheralDevices;
using MusalaTest.Application.UseCases.Gateway;
using MusalaTest.Application.UseCases.PeripheralDevices;
using MusalaTest.Domain.Entities;
using MusalaTest.Domain.Interfaces;
using MusalaTest.Persistence;
using MusalaTest.Persistence.Repositories;

namespace MusalaTest
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MusalaContext>(opt =>
                opt.UseInMemoryDatabase("musala"));

            services.AddControllers();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.ShouldUseConstructor = ci => !ci.IsPrivate;
            }, typeof(Startup));

            // UseCases
            services.AddScoped<IUseCase<GatewayDto, CreateGatewayDto>, CreateGatewayUseCase>();
            services.AddScoped<IUseCase<List<GatewayDto>, string>, FetchAllGateways>();
            services.AddScoped<IUseCase<GatewayDetailsDto, FetchGatewaysDetailsDto>, FetchGatewaysDetails>();
            services.AddScoped<IUseCase<string, RemovePeripheralDeviceDto>, RemovePeripheralDeviceUseCase>();
            services.AddScoped<IUseCase<string, AddDeviceDto>, AddDeviceUseCase>();

            //Repositories
            services.AddScoped<IBaseRepository<Gateways>, GatewayRepository>();
            services.AddScoped<IGatewayRepository, GatewayRepository>();
            services.AddScoped<IBaseRepository<PeripheralDevice>, PeripheralRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}