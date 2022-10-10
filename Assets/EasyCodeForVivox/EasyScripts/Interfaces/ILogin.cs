using System;
using System.ComponentModel;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public interface ILogin
    {
        void Subscribe(ILoginSession loginSession);
        void Unsubscribe(ILoginSession loginSession);
        void LoginToVivox(ILoginSession loginSession, Uri serverUri, string userName, bool joinMuted = false);
        void Logout(ILoginSession loginSession);
        void OnLoginPropertyChanged(object sender, PropertyChangedEventArgs propArgs);
    }
}
