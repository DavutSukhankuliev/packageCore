using SDTCore;
using UnityEngine;

namespace SDTCore
{
    public class Move : Command
    {
        private Transform _objectToMove;
        private Vector3 _direction;
        private float _distance;

        public Move(Transform objectToMove, Vector3 direction, float distance)
        {
            _objectToMove = objectToMove;
            _direction = direction;
            _distance = distance;
        }

        public override CommandResult Execute()
        {
            _objectToMove.position += _direction * _distance;

            return base.Execute();
        }
    }
}