using System;
using Args.Parser.Options;

namespace Args.Parser.Arguments
{
    abstract class ArgumentDefinition<TValue>
    {
        internal IOptionDefinitionMetadata Option { get; }

        internal TValue Value { get; private set; }

        bool BeenSet { get; set; }

        protected ArgumentDefinition(IOptionDefinitionMetadata option)
        {
            Option = option;
        }

        internal void SetValue(TValue value)
        {
            if (BeenSet) throw new NotSupportedException("You have set the value before.");

            Value = value;
            BeenSet = true;
        }
    }
}
