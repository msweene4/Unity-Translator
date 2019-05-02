using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TranslationList", menuName = "Tools2/List", order = 51)]
public class LocalString : ScriptableObject
{
    public string selectedLang;
    public string selectedKey;
    public List<LocalStringWrapper> myList;

    public string Content(string c)
    {
        //Remove puntuation from the string
        c = c.Replace(".", "");
        c = c.Replace("!", "");
        c = c.Replace("?", "");

        if (myList.Count == 0)
        {
            myList = new List<LocalStringWrapper>();
        }

        //Find the language used
        int m = 0;
        for (int i = 0; i < myList.Count; i++)
        {
            if (selectedLang == myList[i].Language.LanguageName)
                m = i;
        }
        string sentence = "";
        //Split the string into words by spaces
        foreach (string word in c.Split(' '))
        {
            sentence += myList[m].Language.getTranslation(word) +" ";
        }
        //Remove the last space
        return sentence.Trim();
        
        /*for (int i = 0; i < myList.Count; i++)
        {
            string trans = myList[i].Language.getTranslation(selectedKey);
            if (trans != null)
            {
                myList[i].Translation = trans;
            }
            else
                myList[i].Translation = "No Translation";
        }
        */
        /*for (int i = 0; i < myList.Count; i++)
        {
            if (selectedLang == myList[i].Language.LanguageName)
                return myList[i].Translation;
        }
        return "Language "+ selectedLang + " not found";
        */
    }
    
    public string[] stringArray()
    {
        if (myList.Count > 0)
        {
            string[] myR = new string[myList.Count];
            int i = 0;
            foreach (LocalStringWrapper l in myList)
            {
                myR[i] = l.Language.LanguageName;
                i++;
            }
            return myR;
        }
        else
            return null;
    }
    public void setLang(Languages l)
    {
        selectedLang = l.LanguageName;
    }
    public void setKey(string k)
    {
        selectedKey = k;
        updateTranslationsList();
    }
    public int getSelIndex()
    {
        if (selectedValid())
        {
            for (int i = 0; i < myList.Count; i++)
            {
                if (selectedLang == myList[i].Language.LanguageName)
                    return i;
            }
        }

        return -1;
    }

    public bool selectedValid()
    {
        for (int i = 0; i < myList.Count; i++)
        {
            if (selectedLang == myList[i].Language.LanguageName)
                return true;
        }

        return false;
    }

    public void updateTranslationsList()
    {
        foreach(LocalStringWrapper l in myList)
        {
            string c = l.Language.getTranslation(selectedKey);
            if (c != null)
            {
                l.Translation = c;
            }
            else
            {
                l.Translation = "No Translation";
            }
        }
    }
}
//A wrapper class which has a Language type variable which has the lang and content key-value pairs
[System.Serializable]
public class LocalStringWrapper
{
    //  public Language lang;
    public Languages Language; //Scriptable object class type
    public string Translation; //value of language
}


