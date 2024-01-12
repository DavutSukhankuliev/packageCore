using System;

namespace SDTCore.Runtime.Command
{
    public class Command : ICommand
    {
        public EventHandler Done { get; set; }
        public CommandResult Execute()
        {
            throw new NotImplementedException();
        }

        public CommandResult Undo()
        {
            throw new NotImplementedException();
        }

        public CommandResult Redo()
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }
    }
}