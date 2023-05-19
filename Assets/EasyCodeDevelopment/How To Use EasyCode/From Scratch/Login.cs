using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using VivoxUnity;

public class Login : MonoBehaviour
{
    public VivoxUnity.Client Client { get; private set; }
    public ILoginSession loginSession { get; private set; }

    public static event Action<ILoginSession> LoggingIn;
    public static event Action<ILoginSession> LoggedIn;
    public static event Action<ILoginSession> LoggedOut;
    public static event Action<ILoginSession> LoggingOut;

    public static event Action<ILoginSession> LoginAdded;
    public static event Action<ILoginSession> LoginRemoved;
    public static event Action<ILoginSession> LoginUpdated;

    private void Awake()
    {
        Client = new VivoxUnity.Client();
    }

    private void Start()
    {
        Client.Uninitialize();
        Client.Initialize();
    }

    private void OnApplicationQuit()
    {
        Client.Uninitialize();
    }

    private void Subscribe()
    {
        loginSession.PropertyChanged += OnLoginPropertyChanged;
        Client.LoginSessions.AfterKeyAdded += OnLoginAdded;
        Client.LoginSessions.BeforeKeyRemoved += OnLoginRemoved;
        Client.LoginSessions.AfterValueUpdated += OnLoginUpdated;
    }

    private void Unsubscribe()
    {
        loginSession.PropertyChanged -= OnLoginPropertyChanged;
        Client.LoginSessions.AfterKeyAdded -= OnLoginAdded;
        Client.LoginSessions.BeforeKeyRemoved -= OnLoginRemoved;
        Client.LoginSessions.AfterValueUpdated -= OnLoginUpdated;
    }


    public void LoginToVivox()
    {

    }

    public bool FilterChannelAndUserName(string nameToFilter)
    {
        char[] allowedChars = new char[] { '0','1','2','3', '4', '5', '6', '7', '8', '9',
    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n','o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I','J', 'K', 'L', 'M', 'N', 'O', 'P','Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
    '!', '(', ')', '+','-', '.', '=', '_', '~'};

        List<char> allowed = new List<char>(allowedChars);
        foreach (char c in nameToFilter)
        {
            if (!allowed.Contains(c))
            {
                if (c == ' ')
                {
                    Debug.Log($"Can't join channel, Channel name has space in it '{c}'");
                }
                else
                {
                    Debug.Log($"Can't join channel, Channel name has invalid character '{c}'");
                }
                return false;
            }
        }
        return true;
    }

    public void OnLoginPropertyChanged(object sender, PropertyChangedEventArgs propArgs)
    {
        var loginSession = (ILoginSession)sender;

        if (propArgs.PropertyName == "State")
        {
            switch (loginSession.State)
            {
                case LoginState.LoggingIn:
                    LoggingIn?.Invoke(loginSession);
                    break;
                case LoginState.LoggedIn:
                    LoggedIn?.Invoke(loginSession);
                    break;
                case LoginState.LoggingOut:
                    LoggingOut?.Invoke(loginSession);
                    break;
                case LoginState.LoggedOut:
                    LoggedOut?.Invoke(loginSession);
                    break;

                default:
                    Debug.Log($"Event Error - Logging In/Out events failed");
                    break;
            }
        }
    }

    public void OnLoginAdded(object sender, KeyEventArg<AccountId> accountId)
    {
        var source = (VivoxUnity.IReadOnlyDictionary<AccountId, ILoginSession>)sender;
        var loginSession = source[accountId.Key];
        LoginAdded?.Invoke(loginSession);
    }

    public void OnLoginRemoved(object sender, KeyEventArg<AccountId> accountId)
    {
        var source = (VivoxUnity.IReadOnlyDictionary<AccountId, ILoginSession>)sender;
        var loginSession = source[accountId.Key];
        LoginRemoved?.Invoke(loginSession);
    }

    public void OnLoginUpdated(object sender, ValueEventArg<AccountId, ILoginSession> valueEventArgs)
    {
        var loginSession = valueEventArgs.Value;
        LoginUpdated?.Invoke(loginSession);
    }


}
