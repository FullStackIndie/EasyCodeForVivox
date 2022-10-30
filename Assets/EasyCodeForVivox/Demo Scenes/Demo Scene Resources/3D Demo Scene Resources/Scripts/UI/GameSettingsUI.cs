using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
    [SerializeField] private Button _button;

    void Start()
    {
        _button.onClick.AddListener(() => LeaveGame());
    }

    private void LeaveGame()
    {
        if (NetworkManager.Singleton == null) { return; }
        NetCodeManager netCodeManager = FindObjectOfType<NetCodeManager>();
        if (netCodeManager != null)
        {
            netCodeManager.LeaveGame();
        }
    }
}
