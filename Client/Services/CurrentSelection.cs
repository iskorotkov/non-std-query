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
        public Query GetQuery()
        {
            IsDirty = true;
            return _query;
        }

        public ExecutionResult ExecutionResult { get; set; }
        
        public ExplanationResult ExplanationResult { get; set; }
        
        public List<string> ResultsHeader { get; private set; } = new List<string>();

        public bool IsDirty { get; private set; }
        
        private readonly HttpClient _client;
        private readonly Query _query = new Query();

        public CurrentSelection(HttpClient client)
        {
            _client = client;
        }

        public async Task Execute()
        {
            ResultsHeader = _query.SelectAttributes.ToList();
            ExecutionResult = await _client.PostJsonAsync<ExecutionResult>("api/queries/execute", GetQuery());
            IsDirty = false;
        }

        public async Task Explain()
        {
            ExplanationResult = await _client.PostJsonAsync<ExplanationResult>("api/queries/explain", GetQuery());
            IsDirty = false;
        }
    }
}
