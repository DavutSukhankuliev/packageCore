namespace SDTCore.Runtime.Command
{
    enum CommandStatus
    {
        Success,
        InProgress,
        Failed
    }
    public class CommandResult
    {
        private object _body;
        private CommandStatus _commandStatus = CommandStatus.Success;
    }
}