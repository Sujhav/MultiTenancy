using Application.Authorization;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repository;
using Infrastructure.Service.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistance(configuration);
            services.AddAuth(configuration);
            services.AddAuthorization();
            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MultiTenencyDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped
            );
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtsetting = new JwtSetting();

            configuration.Bind(JwtSetting.SectionName, jwtsetting);
            services.AddSingleton(Options.Create(jwtsetting));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtsetting.Issuer,
                    ValidAudience = jwtsetting.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsetting.Secret)),
                    //RoleClaimType = ClaimTypes.Role,
                }
                );


            return services;
        }
        public static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            //services.AddAuthorization(options =>
            //    options.AddPolicy("RequireAdmin", policy =>
            //    {
            //        policy.RequireRole("Admin");
            //    })
            //);
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAutorizationPolicyProvider>();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("RequireAdmin", policy =>
            //    {
            //        policy.Requirements.Add(new RoleRequirement("hr"));
            //    });
            //});
            return services;

        }
    }
}
