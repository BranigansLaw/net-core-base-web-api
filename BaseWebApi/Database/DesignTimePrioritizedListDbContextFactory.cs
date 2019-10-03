using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace prioritizemeServices.Database
{
    /// <summary>
    /// The design time <see cref="PrioritizeMeDbContext"/> factory
    /// </summary>
    public class DesignTimePrioritizedListDbContextFactory : IDesignTimeDbContextFactory<PrioritizeMeDbContext>
    {
        /// <summary>
        ///     The SQL databases connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Creates a new instance of the factory
        /// </summary>
        /// <param name="connectionString">The SQL Db Connections string</param>
        public DesignTimePrioritizedListDbContextFactory(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException(nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        /// <summary>
        ///     WARNING: Do not sure this constructor. It is only included to be used by PowerShell for migrations.
        ///     Creates a new instance of the factory using the default appsettings.json file.
        /// </summary>
        public DesignTimePrioritizedListDbContextFactory()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            _connectionString = configuration.GetConnectionString(Startup.SqlConnectionSettingName);
        }

        /// <inheritdoc />
        public PrioritizeMeDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<PrioritizeMeDbContext> builder =
                new DbContextOptionsBuilder<PrioritizeMeDbContext>();

            builder.UseSqlServer(
                _connectionString,
                // Allows retry if there are not enough connections in the connection pool
                optionsBuilder => optionsBuilder.EnableRetryOnFailure());

            return new PrioritizeMeDbContext(builder.Options);
        }
    }
}
