using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using NonStdQuery.Backend.Data.Queries;
using NonStdQuery.Backend.Representation.Data;

namespace NonStdQuery.Client.Services
{
    public class CurrentSelection
    {
        public Query Query { get; } = new Query();

        private readonly HttpClient _client;

        public CurrentSelection(HttpClient client)
        {
            _client = client;
        }

        public Task<ExecutionResult> Execute()
        {
            return _client.PostJsonAsync<ExecutionResult>("api/queries/execute", Query);
        }

        public Task<ExplanationResult> Explain()
        {
            return _client.PostJsonAsync<ExplanationResult>("api/queries/explain", Query);
        }
    }
}
