using EasyCodeForVivox;
using System;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using VivoxUnity;

public class EasyEventsAsync
{

    public static async Task OnLoggingInAsync(ILoginSession loginSession)
    {
        try
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            foreach (var method in RuntimeEvents.DynamicEvents[LoginStatusAsync.LoggingInAsync])
            {
                var parameters = method.GetParameters();
                if (parameters.Length != 1 || parameters[0].ParameterType != typeof(ILoginSession))
                {
                    continue;
                }
                var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                await Task.Run(() =>
                {
                    Parallel.ForEach(gameObjects, gameObject =>
                    {
                        method?.Invoke(gameObject, new object[] { loginSession });
                    });
                });
            }
            stopwatch.Stop();
            Debug.Log($"Async Runtime Events Elapsed Time [hours:minutes:seconds.milliseconds] {stopwatch.Elapsed}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLoggedInAsync)}");
            Debug.LogException(ex);
            throw;
        }
    }

    public static async Task OnLoggingInAsync<T>(ILoginSession loginSession, T value)
    {
        try
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            foreach (var method in RuntimeEvents.DynamicEvents[LoginStatusAsync.LoggingInAsync])
            {
                var parameters = method.GetParameters();
                if (parameters.Length != 2 || parameters[0].ParameterType != typeof(ILoginSession) && parameters[1].ParameterType != typeof(T))
                {
                    continue;
                }
                var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                await Task.Run(() =>
                {
                    Parallel.ForEach(gameObjects, gameObject =>
                    {
                        method?.Invoke(gameObject, new object[] { loginSession, value });
                    });
                });
            }
            stopwatch.Stop();
            Debug.Log($"Async Runtime Events Elapsed Time [hours:minutes:seconds.milliseconds] {stopwatch.Elapsed}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLoggedInAsync)}");
            Debug.LogException(ex);
            throw;
        }
    }

    public static async Task OnLoggedInAsync(ILoginSession loginSession)
    {
        try
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            foreach (var method in RuntimeEvents.DynamicEvents[LoginStatusAsync.LoggedInAsync])
            {
                var parameters = method.GetParameters();
                if (parameters.Length != 1 || parameters[0].ParameterType != typeof(ILoginSession))
                {
                    continue;
                }
                var gameObjects = GameObject.FindObjectsOfType(method.DeclaringType);
                await Task.Run(() =>
                {
                    Parallel.ForEach(gameObjects, gameObject =>
                    {
                        method?.Invoke(gameObject, new object[] { loginSession });
                    });
                });
            }
            stopwatch.Stop();
            Debug.Log($"Async Runtime Events Elapsed Time [hours:minutes:seconds.milliseconds] {stopwatch.Elapsed}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error Invoking events for {nameof(EasyEvents)}.{nameof(OnLoggedInAsync)}");
            Debug.LogException(ex);
            throw;
        }
    }

}
