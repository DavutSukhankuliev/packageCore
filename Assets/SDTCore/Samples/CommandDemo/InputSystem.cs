using System;
using UnityEngine;
using Zenject;

namespace SDTCore
{
    public class InputSystem : MonoBehaviour
    {
        private IInstantiator _instantiator;

        [Inject]
        void Inject(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                var command = _instantiator.Instantiate<Move>(new object[]{transform,Vector3.forward,1f});
                command.Execute();
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                var command = _instantiator.Instantiate<Move>(new object[]{transform,Vector3.back,1f});
                command.Execute();
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                var command = _instantiator.Instantiate<Move>(new object[]{transform,Vector3.left,1f});
                command.Execute();
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                var command = _instantiator.Instantiate<Move>(new object[]{transform,Vector3.right,1f});
                command.Execute();
            }
        }
    }
}