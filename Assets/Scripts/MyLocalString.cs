using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MyLocalString : MonoBehaviour
{
    Text myText;
    public Text myTrans;
    public LocalString mystring;

    void Start()
    {
        myText = GetComponent<Text>();
        SetText();
    }

    void SetText()
    {
        EditorUtility.SetDirty(mystring);
        if(mystring!=null)
            myTrans.text = mystring.Content(myText.text);
    }

    void Update()
    {
        SetText();
    }
}
