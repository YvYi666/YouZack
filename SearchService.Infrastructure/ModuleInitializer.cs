using Elasticsearch.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nest;
using SearchService.Domain;
using SearchService.WebAPI.Options;
using Zack.Commons;

namespace SearchService.Infrastructure
{
    internal class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IElasticClient>(sp =>
            {
                //  option.Value.Url
                var pool = new SingleNodeConnectionPool(new Uri("https://localhost:9200"));
                var option = sp.GetRequiredService<IOptions<ElasticSearchOptions>>();
                var settings = new ConnectionSettings(pool)
                    .BasicAuthentication("elastic", "hkeHvUNHmrSaXPUuBEod")
                    .CertificateFingerprint("d875abbb083233a016f633089d00184ae9a97200ab03aaf7e410dc71eeaa6097")
                    .EnableApiVersioningHeader();
                return new ElasticClient(settings);
            });
            services.AddScoped<ISearchRepository, SearchRepository>();
        }
    }
}
