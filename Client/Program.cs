using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NonStdQuery.Backend.Representation.Validation;
using NonStdQuery.Client.Services;

namespace NonStdQuery.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped<CurrentSelection>();
            builder.Services.AddScoped<FieldsRegistry>();
            builder.Services.AddScoped<QueryValidator>();
            
            builder.Services.AddBaseAddressHttpClient();

            await builder.Build().RunAsync();
        }
    }
}
