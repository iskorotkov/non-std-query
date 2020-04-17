namespace NonStdQuery.Backend.Representation.Validation
{
    public class AttributeError : ValidationError
    {
        public string AttributeName { get; set; }
    }
}
