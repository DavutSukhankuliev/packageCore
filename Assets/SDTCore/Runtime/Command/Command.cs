using System;
using UnityEngine;

namespace SDTCore
{
    public abstract class Command : ICommand
    {
        public EventHandler Done { get; set; }

        public virtual CommandResult Execute()
        {
            Debug.Log("Command Executed");
            return new CommandResult();
        }

        public virtual CommandResult Undo()
        {
            Debug.Log("Command Undo");
            return new CommandResult();
        }

        public virtual CommandResult Redo()
        {
            Debug.Log("Command Redo");
            return new CommandResult();
        }

        public virtual void Cancel()
        {
            
        }
    }
}