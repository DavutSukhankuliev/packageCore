# SDTCoreDocumentation
<details><summary>Details</summary>

##### 1. Purpose
##### 2. Dependencies
##### 3. Technologies
##### 4. How to use

</details>

# Purpose
The package meant to be a module for core patterns in development via [Zenject](https://github.com/modesttree/Zenject).

# Dependencies

- Zenject 9.2.0-stcf3

# Technologies

- Dependency Injection
- Pattern: Command

# How to use

First of all you need to create `*YourIsntaller* : MonoInstaller<*YourInstaller*>` and bind required containers.

```c#
public class CommandDemoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<CommandStorage>()
            .AsSingle();
    }
}
```

Then the class which inherites the `Command.cs` need to be created in order to define the logic of your command.

```c#
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
```
Then you need to create some class which will run the Bootstrap. In this case the Monobehaviour class was used.

```c#
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
```

The work of the current example is shown in the samples of the package.
