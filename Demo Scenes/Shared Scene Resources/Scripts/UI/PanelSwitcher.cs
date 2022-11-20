
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public void EnablePanel(string panelName)
    {
        List<GameObject> panels = FindPanels();

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

    private List<GameObject> FindPanels()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        List<GameObject> panels = new List<GameObject>();

        void AddPanel(GameObject obj)
        {
            if (obj != null)
            {
                panels.Add(obj);
            }
        }

        AddPanel(canvas.transform.Find("Login Panel").gameObject);
        AddPanel(canvas.transform.Find("Channel Panel").gameObject);
        AddPanel(canvas.transform.Find("Message Panel").gameObject);
        AddPanel(canvas.transform.Find("Audio Panel").gameObject);
        AddPanel(canvas.transform.Find("Mute Panel").gameObject);
        AddPanel(canvas.transform.Find("Admin Panel").gameObject);
        AddPanel(canvas.transform.Find("Join Game Panel")?.gameObject);

        return panels;
    }


}
