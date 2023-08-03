using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newme.Catalog.Domain.Messaging;
using Newme.Catalog.Domain.Repositories;
using Newme.Catalog.Application.Consulting.Repositories;
using Newme.Catalog.Infrastructure.Messaging;
using Newme.Catalog.Infrastructure.Persistence;
using Newme.Catalog.Infrastructure.Persistence.Repositories;
using Newme.Catalog.Infrastructure.Consulting;
using Newme.Catalog.Infrastructure.Consulting.Repositories;
using Newme.Catalog.Infrastructure.Consulting.Mappings;

namespace Newme.Catalog.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
        {            
            services
                .AddSqlServer()
                .AddMongo()
                .AddRepositories()
                .AddMessageBus();

            return services;
        }

        private static IServiceCollection AddSqlServer(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddDbContext<CatalogCommandContext>(opt => {
                opt.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            return services;
        }

        private static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton<MongoDbOptions>(sp => {
                var options = new MongoDbOptions();
                var configuration = sp.GetService<IConfiguration>();

                configuration.GetSection("Mongo").Bind(options);

                return options;
            });

            services.AddSingleton<IMongoClient>(sp => {
                var options = sp.GetService<MongoDbOptions>();

                var client = new MongoClient(options.ConnectionString);
                var db = client.GetDatabase(options.Database);

                return client;
            });

            services.AddSingleton(sp => {
                BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
                BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));

                MongoMapper.Configure();

                var options = sp.GetService<MongoDbOptions>();
                var mongoClient = sp.GetService<IMongoClient>();

                var db = mongoClient.GetDatabase(options.Database);

                return db;
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductCommandRepository, ProductCommandRepository>();

            services.AddScoped(typeof(IBaseQueryRepository<>), typeof(BaseQueryRepository<>));
            services.AddScoped(typeof(IDifferentiatialQueryRepository<>), typeof(DifferentiatialQueryRepository<>));
            
            services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
            services.AddScoped<IGenderQueryRepository, GenderQueryRepository>();
            services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
            services.AddScoped<IColorQueryRepository, ColorQueryRepository>();
            services.AddScoped<ISizeQueryRepository, SizeQueryRepository>();

            services.AddScoped<IProductQueryRepository, ProductQueryRepository>();

            return services;
        }

        private static IServiceCollection AddMessageBus(this IServiceCollection services) {
            services.AddScoped<IMessageBusServer, RabbitMqService>();
            
            return services;
        }
    }
}