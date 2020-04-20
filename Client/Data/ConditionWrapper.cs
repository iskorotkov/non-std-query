using System;
using NonStdQuery.Backend.Data.Queries;
using NonStdQuery.Client.Pages;

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

        public Conditions ConditionsPage { get; set; }

        public Operation Operation
        {
            get => Condition.Operation;
            set
            {
                Condition.Operation = value;
                ConditionsPage?.Refresh();
            }
        }

        public int IntValue
        {
            get
            {
                if (Condition.Value is int x)
                {
                    return x;
                }

                Operation = Operation.Equal;
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

                Operation = Operation.Equal;
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

                Operation = Operation.Equal;
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

                Operation = Operation.Equal;
                Condition.Value = DateTime.Now;
                return DateTime.Now;
            }
            set => Condition.Value = value;
        }

        public double DoubleValue
        {
            get
            {
                if (Condition.Value is double x)
                {
                    return x;
                }

                Operation = Operation.Equal;
                Condition.Value = 0.0;
                return 0.0;
            }
            set => Condition.Value = value;
        }
    }
}
