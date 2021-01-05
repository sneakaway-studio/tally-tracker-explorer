using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class DebugManager : Singleton<DebugManager> {
    // singleton
    protected DebugManager () { }
    //public static new DebugManager Instance;




    // DISPLAY EVENT LOG

    [Space (10)]
    [Header ("DEBUG LOG")]

    public TMP_Text debugText;
    public ScrollRect debugScrollRect;
    public bool DEBUG;



    [SerializeField]
    public static bool status = true;

    // dictionary of symbols
    private static Dictionary<string, string> symbolDictionary;



    private void Awake ()
    {
        DEBUG = false;


        AddSymbols ();
        // clear display
        ClearDisplay ();

        // turn off logging outside of the editor
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif


    }


    public void UpdateDisplay (string str)
    {

        ////Debug.Log (str);
        //if (str != "") str = str + "<br>";
        //debugText.text += str;
        //UpdateScroll ();
    }
    public void ClearDisplay ()
    {
        debugText.text = "";
        //UpdateScroll ();
    }
    void UpdateScroll ()
    {
        // make the canvases update their positions - causes big performance spikes and is not needed for debugging
        //Canvas.ForceUpdateCanvases ();
        debugScrollRect.verticalNormalizedPosition = 0f;
    }


    /**
     *  Add symbols for debugging
     */
    void AddSymbols ()
    {
        // add all the symbols - reference https://www.w3schools.com/charsets/ref_emoji.asp
        symbolDictionary = new Dictionary<string, string> (){

            {"asterism", "\u2042"}, // ⁂

            {"arrowE", "\u2192"}, // →
            {"arrowNE", "\u2197"}, // ↗
            {"arrowRefresh", "\u21BB"}, // ↻

            {"circle", "\u25CF"},
            {"square", "\u25FC"},
            {"triangleUp", "\u25B2"},
            {"triangleRight", "\u25BA"},

            {"cloud", "\u2601"}, // ☁
            {"blackstar", "\u2605"}, // ★

            {"sound", "\u266B"}, // ♫
            {"heart", "\u2665"},
            {"diamond", "\u2666"},

            {"phone", "\u260E"}, // ☎
            {"flag", "\u2691"}, // ⚑
            {"gear", "\u2699"}, // ⚙
            {"smilingFace", "\u263B"},

            {"star", "\u272D"},
            {"asterisk", "\u2731"},


        };
    }

    /**
     *  Return a symbol
     */
    public static string GetSymbol (string name)
    {
        string symbol;
        symbolDictionary.TryGetValue (name, out symbol);
        if (symbol != null)
            return symbol.ToString ();
        return "";
    }



    /// <summary>
    /// Print a list of integers
    /// </summary>
    /// <param name="str"></param>
    /// <param name="list"></param>
    public void PrintList (string str, List<int> list)
    {
        if (!DEBUG) return;

        int i = 0;
        str += " [" + list.Count + "]: ";
        foreach (var item in list) {
            if (++i > 1) str += ", ";
            str += item.ToString ();
        }
        Debug.Log (str);
    }



    public void PrintDict (string str, Dictionary<int, TrailingMonster> dict)
    {
        if (!DEBUG) return;

        int i = 0;
        str += " [" + dict.Count + "]: ";
        foreach (KeyValuePair<int, TrailingMonster> t in dict) {
            if (++i > 1) str += ", ";
            str += dict [t.Key].mid + "-" + dict [t.Key].passes;
        }
        Debug.Log (str);
    }



    /// <summary>
    /// Return list of random integers
    /// </summary>
    /// <param name="lengthMin">Minimum list length</param>
    /// <param name="lengthMax">Maximum list length</param>
    /// <param name="rangeMin">Minimum range for each index</param>
    /// <param name="rangeMax">Maximum range for each index</param>
    /// <returns></returns>
    public List<int> GetListOfRandomInts (int lengthMin, int lengthMax, int rangeMin, int rangeMax)
    {
        // create list
        List<int> newList = new List<int> ();
        // use random number for length
        int numberToAdd = (int)Random.Range (lengthMin, lengthMax);
        // loop and add ints
        for (int i = 0; i < numberToAdd; i++) {
            // random int
            //newList [i] = (int)Random.Range (rangeMin, rangeMax);
            // random mid
            newList.Add (MonsterIndex.Instance.GetRandomMid (rangeMin, rangeMax));
        }
        return newList;
    }



}
