using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using NonStdQuery.Backend.Data.Queries;
using NonStdQuery.Backend.Representation.Data;

namespace NonStdQuery.Client.Services
{
    public class CurrentSelection
    {
        public Query GetQuery() => _query;

        private int? _executedQueryHash;
        private int? _explainedQueryHash;

        public ExecutionResult ExecutionResult { get; set; }
        
        public ExplanationResult ExplanationResult { get; set; }
        
        public List<string> ResultsHeader { get; private set; } = new List<string>();

        public bool IsExecutionDirty => _executedQueryHash != null && _executedQueryHash != _query.GetHashCode();
        public bool IsExplanationDirty => _explainedQueryHash != null && _explainedQueryHash != _query.GetHashCode();
        
        private readonly HttpClient _client;
        private readonly Query _query = new Query();

        public CurrentSelection(HttpClient client)
        {
            _client = client;
        }

        public async Task Execute()
        {
            ResultsHeader = _query.SelectAttributes.ToList();
            _executedQueryHash = _query.GetHashCode();
            ExecutionResult = await _client.PostJsonAsync<ExecutionResult>("api/queries/execute", GetQuery());
        }

        public async Task Explain()
        {
            _explainedQueryHash = _query.GetHashCode();
            ExplanationResult = await _client.PostJsonAsync<ExplanationResult>("api/queries/explain", GetQuery());
        }
    }
}
