using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tools2/Language")]
public class Languages : ScriptableObject
{
    public string LanguageName;
    public List<string> keys;
    public List<string> translations;

    public string getTranslation(string key)
    {
        int num = containsTranslation(key);
        if (num != -1)
        {
            return translations[num];
        }
        return "NoTranslation";
    }
    public int containsTranslation(string key)
    {
        if (keys.Contains(key))
        {
            return keys.IndexOf(key);
        }
        return -1;
    }

    public string getKey(string t)
    {
        int num = containsKey(t);
        if (num != -1)
        {
            return keys[num];
        }
        return "NoKey";
    }

    public int containsKey(string t)
    {
        Debug.Log("containsKey" + t);
        if (translations.Contains(t))
        {
            
            return translations.IndexOf(t);
        }
        return -1;
    }

    
}

