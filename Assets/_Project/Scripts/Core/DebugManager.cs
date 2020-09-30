using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugManager : Singleton<DebugManager> {
    // singleton
    protected DebugManager () { }
    public static new DebugManager Instance;




    // dictionary of symbols
    private static Dictionary<string, string> symbolDictionary;

    private void Awake ()
    {
        // add all the symbols - reference https://www.w3schools.com/charsets/ref_emoji.asp
        symbolDictionary = new Dictionary<string, string> (){

            {"arrowRight", "\u2192"},
            {"arrowRefresh", "\u21BB"},

            {"circle", "\u25CF"},
            {"square", "\u25FC"},
            {"triangleUp", "\u25B2"},
            {"triangleRight", "\u25BA"},

            {"heart", "\u2665"},
            {"diamond", "\u2666"},

            {"phone", "\u260E"},
            {"flag", "\u2691"},
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
        return symbol.ToString ();
    }


}
