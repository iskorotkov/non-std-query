using System;

namespace NonStdQuery.Backend.Data.Queries
{
    public class Condition
    {
        public string AttributeName { get; set; }
        public object Value { get; set; }
        public Operation Operation { get; set; }
        public LinkMethod Link { get; set; }

        public int IntValue
        {
            get
            {
                if (Value is int x)
                {
                    return x;
                }

                Value = 0;
                return 0;
            }
            set => Value = value;
        }

        public string StringValue
        {
            get
            {
                if (Value is string x)
                {
                    return x;
                }

                Value = "";
                return "";
            }
            set => Value = value;
        }

        public bool BoolValue
        {
            get
            {
                if (Value is bool x)
                {
                    return x;
                }

                Value = false;
                return false;
            }
            set => Value = value;
        }

        public DateTime DateTimeValue
        {
            get
            {
                if (Value is DateTime x)
                {
                    return x;
                }

                Value = DateTime.Now;
                return DateTime.Now;
            }
            set => Value = value;
        }
    }
}
