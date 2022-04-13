using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using SimpleFileSystem.Abstractions;

namespace SimpleFileSystem.DependencyInjection
{
    public static class SimpleFileSystemDiConfig
    {
        public static IServiceCollection AddSimpleFileSystem(this IServiceCollection services,
            Func<IFileSystemConfiguration> config)
        {
            var configuration = config();

            Directory.CreateDirectory(configuration.BasePath);

            services.AddSingleton(_ => configuration);

            return services
                .AddSingleton<IFileSystemManager, FileSystemManager>()
                .AddSingleton<IFileSystemUploader, FileSystemManager>()
                .AddSingleton<IFileSystemWriter, FileSystemManager>()
                .AddSingleton<IFileSystemReader, FileSystemManager>()
                .AddSingleton<IFileSystemRemoval, FileSystemManager>()
                .AddSingleton<IFileDownloader, FileSystemManager>();
        }
    }
}