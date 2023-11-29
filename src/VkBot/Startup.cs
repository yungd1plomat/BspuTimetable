using VkBot.Abstractions;
using VkBot.Commands;
using VkBot.Services;
using VkNet;
using VkNet.Abstractions;
using VkNet.Model;

namespace VkBot
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
            var token = Configuration["ACCESS_TOKEN"] ?? throw new InvalidOperationException("access token not found");
            services.AddScoped<IVkApi, VkApi>(provider =>
            {
                var api = new VkApi();
                api.Authorize(new ApiAuthParams()
                {
                    AccessToken = token,
                });
                return api;
            });
            services.AddControllers().AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
