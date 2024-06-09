using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using Application.Appointments;
using API.Services;
using Application.Interfaces;
using Infrastructure.Photos;
using Infrastructure.Security;

namespace API.Extensions
{
    /// <summary>
    /// Extension methods for configuring application services.
    /// </summary>
    public static class ApplicationServiceExtentions
    {
        /// <summary>
        /// Adds application services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="config">The <see cref="IConfiguration"/> instance.</param>
        /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                var connectionString = config.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
                opt.UseNpgsql(connectionString);
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("http://localhost:3000", "http://127.0.0.1:3000", "http://127.2.2.2:3000");
                });
            });

            services.AddMediatR(typeof(AppointmentsList.Handler));
            services.AddMediatR(typeof(AppointmentDetails.Handler).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<AppointmentCreate>();
            services.AddHttpContextAccessor();
            services.AddSignalR();
            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));

            services.AddScoped<IAppointmentUpdateSender, AppointmentUpdateSender>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();


            return services;
        }
    }
}
