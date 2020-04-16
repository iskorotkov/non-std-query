using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using NonStdQuery.Backend.Representation.Data;

namespace NonStdQuery.Client.Services
{
    public class FieldsRegistry
    {
        public List<FieldInfo> Fields { get; private set; }

        private readonly HttpClient _client;

        public FieldsRegistry(HttpClient client)
        {
            _client = client;
        }

        public async Task Fetch()
        {
            Fields ??= await _client.GetJsonAsync<List<FieldInfo>>("api/fields");
        }
    }
}
