using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using Newme.Catalog.Application.AutoMapper;
using Newme.Catalog.Application.Interface;
using Newme.Catalog.Application.Services;
using Newme.Catalog.Application.Validations;
using Newme.Catalog.Application.Factories;
using Newme.Catalog.Application.Subscribers;
using Newme.Catalog.Application.Services.Externals;
using Newme.Catalog.Application.Services.AmazonS3.Externals;
using Newme.Catalog.Application.Services.Externals.AmazonS3.Models;
using Microsoft.Extensions.Configuration;

namespace Newme.Catalog.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services
                .AddSubscribers()
                .AddValidators()
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
                .AddFactories()
                .AddApplicationServices()
                .AddAutoMapperConfiguration();

            return services;
        }

        private static IServiceCollection AddSubscribers(this IServiceCollection services) {
            services.AddHostedService<PurchaseOrderCreatedSubscriber>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RegisterNewProductCommandValidation>(ServiceLifetime.Scoped);

            return services;
        }

        private static IServiceCollection AddFactories(this IServiceCollection services)
        {
            services.AddScoped<IProductConsultingModelFactory, ProductConsultingModelFactory>();
            
            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            
            // var awsKeyId = configuration!.GetValue<string>("AwsS3:KeyId");
            // var awsKeySecret = configuration!.GetValue<string>("AwsS3:KeySecret");

            var awsS3Model = new AwsS3Model();
            configuration!.GetSection("AwsS3").Bind(awsS3Model);

            var amazonS3ConfigModel = new AmazonS3ConfigModel();
            amazonS3ConfigModel.Create(awsS3Model);
            services.AddSingleton(amazonS3ConfigModel);

            services.AddScoped<IAmazonS3Service, AmazonS3Service>();
            services.AddScoped<IProductApplicationService, ProductApplicationService>();
            services.AddScoped<ICategoryApplicationService, CategoryApplicationService>();
            services.AddScoped<IColorApplicationService, ColorApplicationService>();
            services.AddScoped<ISizeApplicationService, SizeApplicationService>();

            return services;
        }

        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(InputModelToCommandMappingProfile),
                typeof(ConsultingModelToViewModelMappingProfile)
            );

            return services;
        }
    }
}