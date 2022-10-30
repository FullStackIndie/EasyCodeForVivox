using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "EasySettingsInstaller", menuName = "Installers/EasySettingsInstaller")]
public class EasySettingsInstaller : ScriptableObjectInstaller<EasySettingsInstaller>
{
    public EasySettingsSO EasySettings;

    public override void InstallBindings()
    {
        Container.Bind<EasySettingsSO>().FromInstance(EasySettings);
    }
}