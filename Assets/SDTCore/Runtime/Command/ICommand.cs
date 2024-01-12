using System;

namespace SDTCore
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