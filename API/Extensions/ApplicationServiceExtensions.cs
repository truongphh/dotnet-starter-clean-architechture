using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities;
using Application.Cloudinary;
using Application.Core;
using Application.Interfaces;
using Infrastructure.CloudinaryFunctionality;
using Infrastructure.Email;
using Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddScoped<IUserAccessor, UserAccessor>();

            // cloudinary settings
            services.AddScoped<ICloudinaryAccessor, CloudinaryAccessor>();
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));

            // sendgrid email service
            services.AddScoped<SendgridEmailService>();


            return services;
        }
    }
}