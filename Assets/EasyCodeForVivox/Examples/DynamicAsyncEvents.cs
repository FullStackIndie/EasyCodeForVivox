using EasyCodeForVivox;
using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;

public class DynamicAsyncEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [LoginEventAsync(LoginStatus.LoggingIn)]
    public void LoginCallback(ILoginSession loginSession)
    {
        Debug.Log($"Invoking Async Event Dynamically from {nameof(LoginCallback)}");
    }

    [LoginEventAsync(LoginStatus.LoggedIn)]
    public async Task AsyncMethod(ILoginSession loginSession)
    {
        await Task.Run(() =>
        {
            for (int i = 0; i < 100; i++)
            {
                Debug.Log($"Async Method Event has been invoked");
            }
        });
    }
}
