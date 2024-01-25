using System;

namespace SDTCore
{
    public abstract class Command : ICommand, IDisposable
    {
        public EventHandler Done { get; set; }
        public CommandStorage Storage => _commandStorage;
        private CommandStorage _commandStorage;
        
        public Command(CommandStorage commandStorage)
        {
            _commandStorage = commandStorage;
            Storage.AddCommand(this);
            Done += OnDone;
        }

        public virtual CommandResult Execute()
        {
            Storage.AddToHistory(this);
            return new CommandResult();
        }

        public virtual CommandResult Undo()
        {
            return new CommandResult();
        }

        public virtual CommandResult Redo()
        {
            return new CommandResult();
        }

        public virtual void Cancel()
        {
            
        }
        
        public virtual void Dispose()
        {
            
        }

        protected virtual void OnDone(object sender, EventArgs e)
        {
            Storage.RemoveCommand(this);
        }
    }
}