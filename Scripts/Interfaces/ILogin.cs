using System.ComponentModel;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface ILogin
    {
        void Subscribe(ILoginSession loginSession);
        void Unsubscribe(ILoginSession loginSession);
        void LoginToVivox(string userName, bool joinMuted = false);
        void LoginToVivox<T>(string userName, T value, bool joinMuted = false);
        void Logout(string userName);
        void Logout<T>(string userName, T value);
        void OnLoginPropertyChanged(object sender, PropertyChangedEventArgs propArgs);
    }
}
