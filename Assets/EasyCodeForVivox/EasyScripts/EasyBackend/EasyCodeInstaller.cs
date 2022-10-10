using EasyCodeForVivox;
using Zenject;

public class EasyCodeInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ILogin>().To<EasyLogin>().AsSingle();
        Container.Bind<IChannel>().To<EasyChannel>().AsSingle();
        Container.Bind<IAudioChannel>().To<EasyAudioChannel>().AsSingle();
        Container.Bind<ITextChannel>().To<EasyTextChannel>().AsSingle();
        Container.Bind<IAudioSettings>().To<EasyAudioSettings>().AsSingle();
        Container.Bind<IMessages>().To<EasyMessages>().AsSingle();
        Container.Bind<IUsers>().To<EasyUsers>().AsSingle();
        Container.Bind<ITextToSpeech>().To<EasyTextToSpeech>().AsSingle();
        Container.Bind<IMute>().To<EasyMute>().AsSingle();
    }
}