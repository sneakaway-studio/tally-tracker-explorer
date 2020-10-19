using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugManager : Singleton<DebugManager> {
    // singleton
    protected DebugManager () { }
    public static new DebugManager Instance;

    [SerializeField]
    public static bool status = true;

    // dictionary of symbols
    private static Dictionary<string, string> symbolDictionary;

    private void Awake ()
    {

        AddSymbols ();


        // turn off logging outside of the editor
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif


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


}
