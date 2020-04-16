using System;
using NonStdQuery.Backend.Data.Queries;

namespace NonStdQuery.Client.Data
{
    public class ConditionWrapper
    {
        public Condition Condition { get; set; }

        public ConditionWrapper()
        {
            
        }

        public ConditionWrapper(Condition condition)
        {
            Condition = condition;
        }
        
        public int IntValue
        {
            get
            {
                if (Condition.Value is int x)
                {
                    return x;
                }

                Condition.Value = 0;
                return 0;
            }
            set => Condition.Value = value;
        }

        public string StringValue
        {
            get
            {
                if (Condition.Value is string x)
                {
                    return x;
                }

                Condition.Value = "";
                return "";
            }
            set => Condition.Value = value;
        }

        public bool BoolValue
        {
            get
            {
                if (Condition.Value is bool x)
                {
                    return x;
                }

                Condition.Value = false;
                return false;
            }
            set => Condition.Value = value;
        }

        public DateTime DateTimeValue
        {
            get
            {
                if (Condition.Value is DateTime x)
                {
                    return x;
                }

                Condition.Value = DateTime.Now;
                return DateTime.Now;
            }
            set => Condition.Value = value;
        }
    }
}
