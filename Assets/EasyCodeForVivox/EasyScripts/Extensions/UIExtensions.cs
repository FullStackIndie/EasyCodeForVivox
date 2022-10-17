using TMPro;
using UnityEngine.UI;

namespace EasyCodeForVivox.Extensions
{
    public static class UIExtensions
    {
        public static string GetSelected(this TMP_Dropdown dropdown)
        {
            int index = dropdown.value;
            string result;
            if (index >= 0 && index < dropdown.options.Count)
            {
                result = dropdown.options[index].text;
                return result;
            }
            return null;
        }

        public static void AddValue(this TMP_Dropdown dropdown, string valueToAdd)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = valueToAdd });
            dropdown.RefreshShownValue();
        }

        public static void RemoveValue(this TMP_Dropdown dropdown, string valueToRemove)
        {
            TMP_Dropdown.OptionData remove = dropdown.options.Find(x => x.text == valueToRemove);
            if (dropdown.options.Contains(remove))
            {
                dropdown.options.Remove(remove);
                dropdown.RefreshShownValue();
            }
        }


        public static void TurnOn(this Toggle toggle)
        {
            toggle.isOn = true;
        }

        public static void TurnOff(this Toggle toggle)
        {
            toggle.isOn = false;
        }



    }
}