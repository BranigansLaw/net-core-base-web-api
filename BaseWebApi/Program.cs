using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace prioritizemeServices
{
    /// <summary>
    /// The main entry point for the program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The Main method
        /// </summary>
        /// <param name="args">Application args</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates the web host
        /// </summary>
        /// <param name="args">Application args</param>
        /// <returns>The web host</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
