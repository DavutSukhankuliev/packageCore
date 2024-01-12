using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SDTCore
{
    public class InputSystem : MonoBehaviour
    {
        [SerializeField] private Text _text;
        
        private IInstantiator _instantiator;

        [Inject]
        void Inject(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                var command = _instantiator.Instantiate<Move>(new object[]{transform,Vector3.forward,1f,_text,"-UP       "});
                command.Execute();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                var command = _instantiator.Instantiate<Move>(new object[]{transform,Vector3.back,1f,_text,"-DOWN     "});
                command.Execute();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                var command = _instantiator.Instantiate<Move>(new object[]{transform,Vector3.left,1f,_text,"-LEFT     "});
                command.Execute();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                var command = _instantiator.Instantiate<Move>(new object[]{transform,Vector3.right,1f,_text,"-RIGHT    "});
                command.Execute();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                var command = _instantiator.Instantiate<Move>(new object[]{transform,Vector3.zero,0f,_text,""});
                command.Storage.RedoCommand();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                var command = _instantiator.Instantiate<Move>(new object[]{transform,Vector3.zero,0f,_text,""});
                command.Storage.UndoCommand();
            }
        }
    }
}


