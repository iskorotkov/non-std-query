namespace NonStdQuery.Backend.Data.Queries
{
    public class Condition
    {
        public string AttributeName { get; set; }
        public object Value { get; set; }
        public Operation Operation { get; set; }
        public LinkMethod Link { get; set; }
    }
}
