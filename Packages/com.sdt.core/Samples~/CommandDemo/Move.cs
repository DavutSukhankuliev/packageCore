using System;
using UnityEngine;
using UnityEngine.UI;

namespace SDTCore
{
    public class Move : Command
    {
        private Transform _objectToMove;
        private Vector3 _direction;
        private float _distance;

        private Text _text;
        private string _textDirection;

        public Move(
            CommandStorage commandStorage, 
            Transform objectToMove, 
            Vector3 direction, 
            float distance, 
            Text text,
            string textDirection) : base(commandStorage)
        {
            _objectToMove = objectToMove;
            _direction = direction;
            _distance = distance;
            _text = text;
            _textDirection = textDirection;
        }

        public override CommandResult Execute()
        {
            _objectToMove.position += _direction * _distance;
            _text.text += _textDirection;
            _text.text += Environment.NewLine;
            
            Done?.Invoke(this, EventArgs.Empty);
            return base.Execute();
        }

        public override CommandResult Undo()
        {
            _objectToMove.position -= _direction * _distance;
            _text.text = _text.text.Remove(_text.text.Length - 12, 12);
            
            Done?.Invoke(this, EventArgs.Empty);
            return base.Undo();
        }

        public override CommandResult Redo()
        {
            _objectToMove.position += _direction * _distance;
            _text.text += _textDirection;
            _text.text += Environment.NewLine;
            
            Done?.Invoke(this, EventArgs.Empty);
            return base.Redo();
        }
    }
}

