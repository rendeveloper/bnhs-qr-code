using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Abstracts;
using BnhsQrCode.Mappings;
using BnhsQrCode.Model;
using BnhsQrCode.Services;
using FluentNHibernate.Cfg;
using Microsoft.Extensions.DependencyInjection;

namespace BnhsQrCode.Helper
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<NHibernate.ISessionFactory>(factory =>
            {
                return Fluently
                    .Configure()
                    .Database(() =>
                    {
                        return FluentNHibernate.Cfg.Db.MsSqlConfiguration
                            .MsSql2012
                            .ShowSql()
                            .ConnectionString(connectionString);
                    })
                    .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<UserMap>()
                    .AddFromAssemblyOf<ScanHistoryMap>()
                    .AddFromAssemblyOf<UserImageFileMap>())
                    .BuildSessionFactory();
            });

            services.AddScoped<NHibernate.ISession>(factory =>
                factory
                    .GetServices<NHibernate.ISessionFactory>()
                    .First()
                    .OpenSession()
            );
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IScanHistoryService, ScanHistoryService>();
            services.AddScoped<IUserImageFileService, UserImageFileService>();

            return services;
        }
    }
}
