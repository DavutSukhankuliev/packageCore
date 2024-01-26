using System;
using System.Collections.Generic;

namespace SDTCore
{
    public class CommandStorage
    {
        private Dictionary<Type, Command> _commands = new Dictionary<Type, Command>();
        private List<Command> _historyCommands = new List<Command>();
        private int _historyIndex;

        internal void AddCommand(Command command)
        {
            if (_commands.ContainsKey(command.GetType()))
            {
                _commands.Remove(command.GetType());
            }
            _commands.Add(command.GetType(), command);
        }

        internal void RemoveCommand(Command command)
        {
            if (_commands.ContainsKey(command.GetType()))
            {
                _commands.Remove(command.GetType());
            }
        }

        private void ClearAll()
        {
            foreach (var command in _commands.Values)
            {
                command.Dispose();
            }
            _commands.Clear();
        }

        internal void AddToHistory(Command command)
        {
            if (_historyIndex < _historyCommands.Count)
            {
                _historyCommands.RemoveRange(_historyIndex, _historyCommands.Count - _historyIndex);
            }
            _historyCommands.Add(command);
            _historyIndex++;
        }

        public void UndoCommand()
        {
            if (_historyCommands.Count == 0)
            {
                return;
            }
            if (_historyIndex > 0)
            {
                _historyCommands[_historyIndex - 1].Undo();
                _historyIndex--;
            }
        }

        public void RedoCommand()
        {
            if (_historyCommands.Count == 0)
            {
                return;
            }
            if (_historyIndex < _historyCommands.Count)
            {
                _historyIndex++;
                _historyCommands[_historyIndex - 1].Redo();
            }
        }
    }
}