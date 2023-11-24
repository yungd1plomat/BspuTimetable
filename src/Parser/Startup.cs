using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Parser.Abstractions;
using Parser.Data;
using Parser.Services;

namespace Parser
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["CONNECTION_STRING"] ?? throw new InvalidOperationException("connection string not found");
            services.AddDbContext<ScheduleDbContext>(options => options.UseNpgsql(connectionString, opts => opts.EnableRetryOnFailure(10)));
            services.AddScoped<IBspuApi, BspuApi>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddHostedService<ScheduleParserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ScheduleDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
