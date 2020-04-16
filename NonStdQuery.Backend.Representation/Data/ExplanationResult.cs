using System.Collections.Generic;

namespace NonStdQuery.Backend.Representation.Data
{
    public class ExplanationResult
    {
        public string Sql { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
