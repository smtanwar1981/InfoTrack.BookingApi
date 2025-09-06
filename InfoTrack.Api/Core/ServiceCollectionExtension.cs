using InfoTrack.Application.Interfaces;
using InfoTrack.Application.Services;
using InfoTrack.Domain.Repositories;
using InfoTrack.Infrastructure.Data;
using InfoTrack.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            /********** Add in memory database context **********/
            services.AddDbContext<InfoTrackDbContext>(options =>
            {
                options.UseInMemoryDatabase("InfoTrackDbContext");
            });

            /********** Registring Dependencies **********/
            services
                .AddScoped<IBookingSlotRepository, BookingSlotRepository>()
                .AddScoped<IBookingSlotService, BookingSlotService>();

            services.AddControllers();

            /********** Swagger Configuration **********/
            services.AddEndpointsApiExplorer().AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "InfoTrack Booking Slot Api",
                    Description = "An InfoTrack Microservice for buyers to book a session from 9am to 5pm."
                });
            });

            return services;
        }
    }
}
