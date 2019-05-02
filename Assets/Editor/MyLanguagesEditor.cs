using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MyLanguagesEditor : EditorWindow
{
    public LocalString list;
    public int previousIndex = 0;
    public int index = 0;
    public int keyI = 0;
    public int prevKeyI = 0;
    public int viewIndex = -1;
    [MenuItem("Tools2 Window/Language Editor")]
    static void init()
    {
        EditorWindow.GetWindow(typeof(MyLanguagesEditor));
    }
    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            list = AssetDatabase.LoadAssetAtPath(objectPath, typeof(LocalString)) as LocalString;
        }
        index = list.getSelIndex();
        previousIndex = index;
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("Tools2 Language Editor", EditorStyles.boldLabel);
        if (list != null)
        {
            //Opens the translation list in the inspector
            if (GUILayout.Button("Show Translation List"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = list;
            }
        }
        //Opens a translationList asset file
        if (GUILayout.Button("Open Translation List"))
        {
            OpenItemList();
        }
        //Creates a new translationList asset file
        if (GUILayout.Button("Create New Translation List"))
        {
            CreateNewItemList();
        }
        GUILayout.EndVertical();

        GUILayout.Space(20);

        

        if (list.myList.Count>0)
        {
            index = EditorGUILayout.Popup("Selected Display Language ", index, list.stringArray(), GUILayout.ExpandWidth(false));
            GUILayout.Space(20);
            //popup value changed
            if (previousIndex != index)
            {
                previousIndex = index;
                list.setLang(list.myList[index].Language);
            }

            keyI = EditorGUILayout.Popup("Selected Key ", keyI, list.myList[0].Language.keys.ToArray(), GUILayout.ExpandWidth(false));
            GUILayout.Space(20);
            //popup value changed
            if (prevKeyI != keyI)
            {
                prevKeyI = keyI;
                list.setKey(list.myList[0].Language.keys[keyI]);
            }


            GUILayout.Label("Translations");
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button(" < ", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
                else
                    viewIndex = list.myList.Count;
            }
            GUILayout.Space(5);
            if (GUILayout.Button(" > ", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < list.myList.Count)
                {
                    viewIndex++;
                }
                else
                    viewIndex = 1;
            }

            GUILayout.Space(60);

            if (GUILayout.Button(" + ", GUILayout.ExpandWidth(false)))
            {
                AddItem();
            }
            if (GUILayout.Button(" - ", GUILayout.ExpandWidth(false)))
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            if (list.myList == null)
                Debug.Log("oops");

            if (list.myList.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Translation", viewIndex, GUILayout.ExpandWidth(false)), 1, list.myList.Count);
                EditorGUILayout.LabelField("of   " + list.myList.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                list.myList[viewIndex - 1].Language.LanguageName = EditorGUILayout.TextField("Language", list.myList[viewIndex - 1].Language.LanguageName, GUILayout.ExpandWidth(false));
                list.myList[viewIndex - 1].Translation = EditorGUILayout.TextField("Translation", list.myList[viewIndex - 1].Translation, GUILayout.ExpandWidth(false));
            }
            else
            {
                GUILayout.Label("This Language List is Empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(list);
        }
    }
    //create a new item list
    void CreateNewItemList()
    {
        if (!AssetDatabase.Contains(list))
        {
            viewIndex = 1;
            list = CreateTransList.Create();
            if (list)
            {
                list.myList = new List<LocalStringWrapper>();
                string aPath = AssetDatabase.GetAssetPath(list);
                EditorPrefs.SetString("ObjectPath", aPath);
            }
        }
        else
        {
            Debug.Log("List already exists. If list has been deleated manually create a new list in the assets>create>tools2>list and open the new list in the editor window. Remember to attach the new translation list to the Text object.");
        }
    }
    //open the item list
    void OpenItemList()
    {
        string ofPath = EditorUtility.OpenFilePanel("Select  Language List", "", "");
        if (ofPath.StartsWith(Application.dataPath))
        {
            string aPath = ofPath.Substring(Application.dataPath.Length - "Assets".Length);
            list = AssetDatabase.LoadAssetAtPath(aPath, typeof(LocalString)) as LocalString;
            if (list.myList == null)
                list.myList = new List<LocalStringWrapper>();
            if (list)
            {
                EditorPrefs.SetString("ObjectPath", aPath);
            }
        }
    }
    //Adding a new lang-content pair
    void AddItem()
    {
        LocalStringWrapper newItem = new LocalStringWrapper();
        newItem.Translation = "New Translation";
        list.myList.Add(newItem);
        viewIndex = list.myList.Count;
    }
    //delete a langp-content pair
    void DeleteItem(int index)
    {
        list.myList.RemoveAt(index);
    }
}
