using SDTCore;
using Zenject;

namespace SDTCore
{
    public class CommandDemoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<CommandStorage>()
                .AsSingle();
        }
    }
}