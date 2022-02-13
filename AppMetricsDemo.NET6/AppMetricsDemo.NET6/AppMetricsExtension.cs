using App.Metrics;
using App.Metrics.Filtering;
using App.Metrics.Formatters.Json;
using Microsoft.AspNetCore.Mvc;


namespace AppMetricsDemo.Extensions
{
    public static class AppMetricsExtension
    {
        public static IServiceCollection AddMetricsExtension(this IServiceCollection services)
        {
            var filter = new MetricsFilter().WhereType(MetricType.Timer);
            var metrics = new MetricsBuilder().Report.ToConsole(options =>
            {
                options.FlushInterval = TimeSpan.FromSeconds(5);
                options.Filter = filter;
                options.MetricsOutputFormatter = new MetricsJsonOutputFormatter();
            }).Build();

            services.AddMetrics(metrics);
            services.AddMvcCore().AddMetricsCore();
            services.AddMetricsReportingHostedService();
            services.AddMetricsEndpoints();
            return services;

        }

        public static IHostBuilder AddWebTrackingExtension(this IHostBuilder host)
        {
            host.UseMetricsWebTracking();
            return host;
        }

        public static WebApplication UseMetricsExtension(this WebApplication webApp)
        {
            webApp.UseMetricsAllMiddleware();
            webApp.UseMetricsAllEndpoints();
            return webApp;
        }

    }
}
