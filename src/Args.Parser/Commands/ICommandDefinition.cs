namespace Args.Parser.Commands
{
    interface ICommandDefinition : ICommandDefinitionMetadata
    {
        void RegisterOption(string fullForm, char? abbrForm, string description);
    }
}
