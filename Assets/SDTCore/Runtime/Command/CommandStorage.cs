using System;
using System.Collections.Generic;

namespace SDTCore.Runtime.Command
{
    public class CommandStorage
    {
        private Dictionary<Type, Command> _commands = new Dictionary<Type, Command>();
        private List<Command> _commandsHistory = new List<Command>();

        void AddCommand(Command command)
        {
            if (_commands.ContainsKey(command.GetType()))
            {
                _commands.Add(command.GetType(), command);
            }
        }

        void RemoveCommand(Command command)
        {
            if (_commands.ContainsKey(command.GetType()))
            {
                _commands.Remove(command.GetType());
            }
        }

        void ClearAll()
        {
            _commands.Clear();
        }

        void AddToHistory(Command command)
        {
            _commandsHistory.Add(command);
        }
    }
}