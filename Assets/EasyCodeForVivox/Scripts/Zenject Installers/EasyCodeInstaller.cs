using EasyCodeForVivox;
using EasyCodeForVivox.Events;
using EasyCodeForVivox.Events.Internal;
using Zenject;

public class EasyCodeInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EasyLogin>().AsSingle();
        Container.Bind<EasyChannel>().AsSingle();
        Container.Bind<EasyAudioChannel>().AsSingle();
        Container.Bind<EasyTextChannel>().AsSingle();
        Container.Bind<EasyAudio>().AsSingle();
        Container.Bind<EasyMessages>().AsSingle();
        Container.Bind<EasyUsers>().AsSingle();
        Container.Bind<EasyTextToSpeech>().AsSingle();
        Container.Bind<EasyMute>().AsSingle();
        Container.Bind<EasyEvents>().AsSingle();
        Container.Bind<EasyEventsAsync>().AsSingle();
    }
}