using System;

namespace SDTCore
{
    public interface ICommand
    {
        EventHandler Done { get; set; }
        CommandResult Execute();
        CommandResult Undo();
        CommandResult Redo();
        void Cancel();
    }
}