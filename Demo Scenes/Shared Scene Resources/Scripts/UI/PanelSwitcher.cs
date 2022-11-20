
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public void EnablePanel(string panelName)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        List<GameObject> panels = new List<GameObject>();

        panels.Add(canvas.transform.Find("Login Panel").gameObject);
        panels.Add(canvas.transform.Find("Channel Panel").gameObject);
        panels.Add(canvas.transform.Find("Message Panel").gameObject);
        panels.Add(canvas.transform.Find("Audio Panel").gameObject);
        panels.Add(canvas.transform.Find("Mute Panel").gameObject);
        panels.Add(canvas.transform.Find("Admin Panel").gameObject);
        panels.Add(canvas.transform.Find("Join Game Panel").gameObject);

        if (!panels.Any(g => g.name.Contains(panelName)))
        {
            return;
        }

        var panel = panels.Where(g => g.name.Contains(panelName)).FirstOrDefault();
        if (panel != null)
        {
            foreach (var p in panels)
            {
                if (!p.name.Contains(panelName) && !p.name.Contains("Chat Panel"))
                {
                    p.SetActive(false);
                }
            }
            panel.SetActive(true);
        }
        else
        {
            Debug.Log($"Could not find panel that contained '{panelName}' in the UI Canvas");
        }
    }


}
