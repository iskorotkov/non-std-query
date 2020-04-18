using Microsoft.AspNetCore.Components;
using NonStdQuery.Backend.Data.Serialization;
using NonStdQuery.Backend.Representation.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NonStdQuery.Client.Services
{
    public class TooltipsRegistry
    {
        public List<Tooltip> Tooltips { get; private set; }

        private readonly HttpClient _client;
        private readonly ValueDeserializer _deserializer;

        public TooltipsRegistry(HttpClient client, ValueDeserializer deserializer)
        {
            _client = client;
            _deserializer = deserializer;
        }

        public async Task Fetch()
        {
            if (Tooltips != null)
            {
                return;
            }

            Tooltips = await _client.GetJsonAsync<List<Tooltip>>("api/tooltips");

            foreach (var tooltip in Tooltips)
            {
                for (var i = 0; i < tooltip.Items.Count; i++)
                {
                    tooltip.Items[i] = _deserializer.Deserialize(tooltip.Items[i], tooltip.Type);
                }
            }
        }
    }
}
