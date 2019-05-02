using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateTransList{
  
    public Languages mylang;

    public static LocalString Create()
    {
        LocalString asset = ScriptableObject.CreateInstance<LocalString>();

        AssetDatabase.CreateAsset(asset, "Assets/TranslationList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }

}
