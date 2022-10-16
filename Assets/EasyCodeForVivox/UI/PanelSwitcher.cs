
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] List<GameObject> _panels = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    public void EnablePanel(string panelName)
    {
        var panel = _panels.Where(g => g.name.Contains(panelName)).FirstOrDefault();
        if (panel != null)
        {
            foreach (var p in _panels)
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
