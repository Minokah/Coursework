using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using static Unity.VisualScripting.Icons;

public class LocalizationEditor : EditorWindow
{
    /*
        Keys:
        ["Textbox1", "MyString2"]


        Strings:
        [
            [
                ["English", "English text for Textbox1"],
                ["French", "French text for Textbox1"]
            ],
            [
                ["English", "English text 2 for MyDialogue2"],
                ["French", "French text for MyDialgue2"]
            ]
        ]
    */

    List<string> keys = new List<string>();
    List<Dictionary<string, string>> strings = new List<Dictionary<string, string>>();

    string foundEnglish = "English (en.asset): Unloaded";
    string foundFrench = "French (fr.asset): Unloaded";
    string foundSpanish = "Spanish (sp.asset): Unloaded";

    Language english, french, spanish;
    GUIStyle englishColour = new GUIStyle();
    GUIStyle frenchColour = new GUIStyle();
    GUIStyle spanishColour = new GUIStyle();

    Vector2 scrollPos;

    bool loaded = false;

    [MenuItem ("Tools/Localization Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LocalizationEditor));
    }

    void OnGUI() {
        EditorGUILayout.LabelField("Localization Editor");
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Need to add a Language?");
        EditorGUILayout.LabelField("Assets > Resources > Localization > Right Click >");
        EditorGUILayout.LabelField("Create > Localization > Add Language");
        GUILayout.Space(10);

        // Find language labels
        EditorGUILayout.LabelField(foundEnglish, englishColour);
        EditorGUILayout.LabelField(foundFrench, frenchColour);
        EditorGUILayout.LabelField(foundSpanish, spanishColour);

        // Load the files
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Load")) Load();

        if (keys == null || strings == null || keys.Count != strings.Count) loaded = false;
        else if (english != null) loaded = true;

        if (!loaded)
        {
            englishColour.normal.textColor = Color.yellow;
            frenchColour.normal.textColor = Color.yellow;
            spanishColour.normal.textColor = Color.yellow;
            foundEnglish = "English (en.asset): Unloaded";
            foundFrench = "French (fr.asset): Unloaded";
            foundSpanish = "Spanish (sp.asset): Unloaded";
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            if (GUILayout.Button("Save")) Save();
            EditorGUILayout.EndHorizontal();

            // Add a string
            if (GUILayout.Button("Add String"))
            {
                Dictionary<string, string> languages = new Dictionary<string, string>();
                languages.Add("English", "");
                languages.Add("French", "");
                languages.Add("Spanish", "");

                // Add key to key list and languages for that key into dialogue
                keys.Add($"NewDialog{strings.Count + 1}");
                strings.Add(languages);
            }

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            // Refresh dialogue listing in GUI
            for (int i = 0; i != keys.Count; i++)
            {
                GUILayout.Space(10);
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(30)); // Index

                // Remove string
                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    keys.RemoveAt(i);
                    strings.RemoveAt(i);
                    GUILayout.EndHorizontal();
                    break;
                }

                // Remove string up if possible
                if (GUILayout.Button("↑", GUILayout.Width(30)))
                {
                    if (i > 0) {
                        string key = keys[i];
                        Dictionary<string, string> entry = strings[i];
                        keys.RemoveAt(i);
                        strings.RemoveAt(i);
                        keys.Insert(i - 1, key);
                        strings.Insert(i - 1, entry);
                        GUILayout.EndHorizontal();
                        break;
                    }
                }

                // Move string down
                if (GUILayout.Button("↓", GUILayout.Width(30)))
                {
                    if (i < keys.Count - 1) {
                        string key = keys[i];
                        Dictionary<string, string> entry = strings[i];
                        keys.RemoveAt(i);
                        strings.RemoveAt(i);
                        keys.Insert(i + 1, key);
                        strings.Insert(i + 1, entry);
                        GUILayout.EndHorizontal();
                        break;
                    }
                }

                keys[i] = EditorGUILayout.TextField(keys[i]); // Key name
                GUILayout.EndHorizontal();

                Dictionary<string, string> languages = strings[i];

                // Make fields for languages EN FR and SP
                if (english != null)
                {
                    string en = languages.GetValueOrDefault("English");
                    if (en == default || en == null)
                    {
                        en = "";
                        languages["English"] = "";
                    }
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("English", GUILayout.Width(100));
                    languages["English"] = EditorGUILayout.TextField(languages["English"]);
                    GUILayout.EndHorizontal();
                }

                if (french != null)
                {
                    string fr = languages.GetValueOrDefault("French");
                    if (fr == default || fr == null)
                    {
                        fr = "";
                        languages["French"] = "";
                    }
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("French", GUILayout.Width(100));
                    languages["French"] = EditorGUILayout.TextField(languages["French"]);
                    GUILayout.EndHorizontal();
                }

                if (spanish != null)
                {
                    string sp = languages.GetValueOrDefault("Spanish");
                    if (sp == default || sp == null)
                    {
                        sp = "";
                        languages["Spanish"] = "";
                    }
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Spanish", GUILayout.Width(100));
                    languages["Spanish"] = EditorGUILayout.TextField(languages["Spanish"]);
                    GUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndScrollView();
        }
    }

    // Load language asset files
    void Load() {
        keys.Clear();
        strings.Clear();

        // Load Languages from files
        english = AssetDatabase.LoadAssetAtPath<Language>("Assets/Resources/Localization/en.asset");
        french = AssetDatabase.LoadAssetAtPath<Language>("Assets/Resources/Localization/fr.asset");
        spanish = AssetDatabase.LoadAssetAtPath<Language>("Assets/Resources/Localization/sp.asset");
        LoadPack(english, "English", "en.asset", ref foundEnglish, englishColour);
        LoadPack(french, "French", "fr.asset", ref foundFrench, frenchColour);
        LoadPack(spanish, "Spanish", "sp.asset", ref foundSpanish, spanishColour);
    }

    void LoadPack(Language pack, string lang, string asset, ref string fileStatus, GUIStyle colour)
    {
        if (pack != null)
        {
            fileStatus = $"{lang} ({asset}): Found";
            colour.normal.textColor = Color.green;

            // Iterate through language pack
            for (int i = 0; i != pack.keys.Count; i++)
            {
                // Already has the key?
                if (keys.Contains(pack.keys[i]))
                {
                    // Get the dictionary and add language to it
                    strings[i].Add(lang, pack.values[i]);
                }
                // Otherwise, add new entries in both
                else
                {
                    keys.Add(pack.keys[i]);
                    Dictionary<string, string> stored = new Dictionary<string, string>();
                    stored.Add(lang, pack.values[i]);
                    strings.Add(stored);
                }
            }
        }
        else
        {
            fileStatus = fileStatus = $"{lang} ({asset}): Not found";
            colour.normal.textColor = Color.red;
        }
    }

    // Save to asset file to be read in game
    void Save()
    {
        if (english != null)
        {
            // English
            english.keys.Clear();
            english.values.Clear();
            for (int i = 0; i != keys.Count; i++)
            {
                english.keys.Add(keys[i]);
                english.values.Add(strings[i]["English"]);
            }
        }

        // French
        if (french != null)
        {
            french.keys.Clear();
            french.values.Clear();
            for (int i = 0; i != keys.Count; i++)
            {
                french.keys.Add(keys[i]);
                french.values.Add(strings[i]["French"]);
            }
        }

        // Spanish
        if (spanish != null)
        {
            spanish.keys.Clear();
            spanish.values.Clear();
            for (int i = 0; i != keys.Count; i++)
            {
                spanish.keys.Add(keys[i]);
                spanish.values.Add(strings[i]["Spanish"]);
            }
        }

        EditorUtility.SetDirty(english);
        EditorUtility.SetDirty(french);
        EditorUtility.SetDirty(spanish);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
