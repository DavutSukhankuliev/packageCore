using System;

namespace SDTCore.Runtime.Command
{
    public interface ICommand
    {
        public EventHandler Done { get; set; }
        CommandResult Execute();
        CommandResult Undo();
        CommandResult Redo();
        void Cancel();
    }
}