using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDTCore
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
                Debug.Log("Command added");
            }
        }

        void RemoveCommand(Command command)
        {
            if (_commands.ContainsKey(command.GetType()))
            {
                _commands.Remove(command.GetType());
                Debug.Log("Command removed");
            }
        }

        void ClearAll()
        {
            _commands.Clear();
            Debug.Log("Commands cleared");
        }

        void AddToHistory(Command command)
        {
            _commandsHistory.Add(command);
        }
    }
}