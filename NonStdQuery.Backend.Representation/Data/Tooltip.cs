using System.Collections.Generic;

namespace NonStdQuery.Backend.Representation.Data
{
    public class Tooltip
    {
        public string FieldName { get; set; }
        public List<string> Words { get; set; } = new List<string>();
    }
}
