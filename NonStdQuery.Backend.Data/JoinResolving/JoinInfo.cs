namespace NonStdQuery.Backend.Data.JoinResolving
{
    public class JoinInfo
    {
        public string ThisTable { get; set; }
        public string ThisColumn { get; set; }
        
        public string ForeignTable { get; set; }
        public string ForeignColumn { get; set; }

        public bool Equals(JoinInfo info)
        {
            return ThisTable == info.ThisTable
                   && ThisColumn == info.ThisColumn
                   && ForeignTable == info.ForeignTable
                   && ForeignColumn == info.ForeignColumn;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is JoinInfo info)
            {
                return Equals(info);
            }
            return base.Equals(obj);
        }
    }
}
