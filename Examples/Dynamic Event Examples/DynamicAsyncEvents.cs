using EasyCodeForVivox;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;

public class DynamicAsyncEvents : MonoBehaviour
{
    [LoginEventAsync(LoginStatus.LoggedIn)]
    public async void DynamicEventAsyncVoid(ILoginSession loginSession)
    {
        var bytes = Encoding.Unicode.GetBytes(loginSession.LoginSessionId.DisplayName);
        using (FileStream fileStream = new FileStream($"{Directory.GetCurrentDirectory()}\\Assets\\playerName.txt", FileMode.Create, FileAccess.Write, FileShare.ReadWrite, bufferSize: 4096, useAsync: true))
        {
            await fileStream.WriteAsync(bytes, 0, bytes.Length);
        }
        Debug.Log("Done creating text file");
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
