using EasyCodeForVivox;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "EasySettingsInstaller", menuName = "Installers/EasySettingsInstaller")]
public class EasySettingsInstaller : ScriptableObjectInstaller<EasySettingsInstaller>
{
    public EasySettings EasySettings;

    public override void InstallBindings()
    {
        Container.Bind<EasySettings>().FromInstance(EasySettings);
    }
}