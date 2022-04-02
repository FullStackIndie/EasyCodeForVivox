using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace EasyCodeForVivox
{
    public static class EasyExtensions
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
            }
        }

    }
}