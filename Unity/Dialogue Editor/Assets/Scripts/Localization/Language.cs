using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Language", menuName = "Localization/Add Language", order = 1)]
public class Language : ScriptableObject
{
    public List<string> keys = new List<string>();
    public List<string> values = new List<string>();

    public string Get(string key) {
        if (keys.Contains(key)) return values[keys.IndexOf(key)];
        else return null;
    }
}
