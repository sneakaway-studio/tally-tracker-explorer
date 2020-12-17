using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;

/**
 *  Class for individual taxonomy colors
 */
public class MarketingColor {
    public string category { get; set; }
    public string colorStr { get; set; }
    public Color color { get; set; }
}


public class ColorManager : Singleton<ColorManager> {
    // singleton
    protected ColorManager () { }
    //public static new ColorManager Instance;






    [Space (10)]
    [Header ("COLORS")]


    static string [] colors = {
        "5f1475", "5f1475", "f4e713", "8820aa", "42da82", "f413bc", "3f0236", "42da82", "31a8cb", "42da82", "f413bc", "ec391a", "6261a8", "ec391a", "4d7bbd", "4d7bbd", "6261a8", "5f1475", "31a8cb", "ec391a", "6939ac", "6261a8", "6939ac", "48daa3", "ec391a", "5f40bd", "f413bc", "5f40bd", "6939ac", "074381", "5f40bd", "6939ac", "6939ac", "074381", "5f40bd", "de2319", "058eb8", "5f1475", "6939ac", "5f1475", "074381", "de2319", "463260", "ae1ff1", "ae1ff1", "ae1ff1", "69004b", "5300e3", "129740", "f413bc", "ae1ff1", "de2319", "418fb0", "418fb0", "5f1475", "074381", "ae1ff1", "463260", "a43a9f", "0356d8", "ae1ff1", "f3af1f", "5f1475", "a6134c", "02b65c", "ef4138", "de2dca", "63fbf0", "63fbf0", "63fbf0", "5300e3", "5f1475", "8336bd", "3957c9", "3957c9", "a6134c", "a6134c", "a6134c", "a6134c", "a6134c", "ce218d", "074381", "5f1475", "ef4138", "ef4138", "ef4138", "90c053", "ef4138", "5fadd1", "f41182", "5f1475", "ef4138", "5f1475", "42da82", "5300e3", "5300e3", "5300e3", "5bd6fa", "1f4fbc", "5300e3", "60139b", "f413bc", "5f1475"
    };



    // dictionary of all marketing colors, with mids as keys 
    public static Dictionary<int, MarketingColor> MarketingColorDict = new Dictionary<int, MarketingColor> () {

        { 453, new MarketingColor{  category = "Spiritual", colorStr = "0078C2"} },
        { 596, new MarketingColor{  category = "Tech", colorStr = "00A7E5"} },
        { 391, new MarketingColor{  category = "Personal$", colorStr = "078591"} },
        { 274, new MarketingColor{  category = "Garden", colorStr = "0c9a47"} },
        { 52, new MarketingColor{  category = "Business", colorStr = "0E6897"} },
        { 338, new MarketingColor{  category = "Music", colorStr = "1DB954"} },
        { 677, new MarketingColor{  category = "Camping", colorStr = "21753E"} },
        { 464, new MarketingColor{  category = "Science", colorStr = "95a43f"} },
        { 552, new MarketingColor{  category = "Style", colorStr = "242960"} },
        { 483, new MarketingColor{  category = "Sports", colorStr = "253785"} },
        { 239, new MarketingColor{  category = "Hobbies", colorStr = "5b11db"} },
        { 1, new MarketingColor{  category = "Auto", colorStr = "74037f"} },
        { 640, new MarketingColor{  category = "TV", colorStr = "7ba2a3"} },
        { 473, new MarketingColor{  category = "Shopping", colorStr = "800245"} },
        { 42, new MarketingColor{  category = "Books", colorStr = "86037e"} },
        { 680, new MarketingColor{  category = "Gaming", colorStr = "96023b"} },
        { 123, new MarketingColor{  category = "Careers", colorStr = "9b0465"} },
        { 422, new MarketingColor{  category = "Pets", colorStr = "a74762"} },
        { 132, new MarketingColor{  category = "Education", colorStr = "be0441"} },
        { 653, new MarketingColor{  category = "Travel", colorStr = "d43422"} },
        { 201, new MarketingColor{  category = "Fine Art", colorStr = "d90623"} },
        { 324, new MarketingColor{  category = "Movies", colorStr = "ed2820"} },
        { 150, new MarketingColor{  category = "Events", colorStr = "f74229"} },
        { 379, new MarketingColor{  category = "News", colorStr = "f8061a"} },
        { 186, new MarketingColor{  category = "Relations", colorStr = "f9672c"} },
        { 441, new MarketingColor{  category = "Housing", colorStr = "febd34"} },
        { 286, new MarketingColor{  category = "Health", colorStr = "f9d24c"} },
        { 223, new MarketingColor{  category = "Living", colorStr = "fa9a6d"} },
        { 210, new MarketingColor{  category = "Food", colorStr = "fc7185"} },
        { 432, new MarketingColor{  category = "Culture", colorStr = "fc9731"} },
        { 110, new MarketingColor{  category = "Energy", colorStr = "fd27b0"} }
    };



    private void Awake ()
    {
        // convert strings to colors
        foreach (KeyValuePair<int, MarketingColor> m in MarketingColorDict) {
            MarketingColorDict [m.Key].color = GetColorFromString (m.Value.colorStr);
        }


    }


    /**
     *  Return a random color
     */
    public static Color GetRandomColor ()
    {
        return Random.ColorHSV ();
    }


    /**
     *  Return Color from array, if no index received then choose random 
     */
    public static Color GetColorFromArray (int index = -1)
    {
        // if no index received then get random 
        if (index == -1) {
            // get random index from array 
            index = (int)Random.Range (0, colors.Length - 1);
        }
        // parse string as Color
        return GetColorFromString (colors [index]);
    }
    /**
     *  Return Color from dict, if no index received then choose random 
     */
    public static Color GetColorFromDict (int index = -1)
    {
        // if no index received then get random 
        if (index == -1) {
            // create list of keys 
            List<int> keyList = new List<int> (MarketingColorDict.Keys);
            // select a random key from your list
            index = keyList [(int)Random.Range (0, keyList.Count - 1)];
        }
        // return the element
        return MarketingColorDict [index].color;
    }


    /** 
     *  Return a Color object from a string
     *  ^ Documentation https://docs.unity3d.com/ScriptReference/ColorUtility.TryParseHtmlString.html
     *  ^ Strings that begin with '#' will be parsed as hexadecimal
     *  ^ Strings that do not begin with '#' will be parsed as literal colors (red,green,...)
     */
    public static Color GetColorFromString (string colorStr)
    {
        Color c;

        // if string contains integers then a hex
        if (Regex.IsMatch (colorStr, @"^(?:[0-9a-fA-F]{6})$")) {
            // if no hash then add one
            if (!Regex.IsMatch (colorStr, @"^#"))
                colorStr = "#" + colorStr;
            // if hex is 7 chars then add alpha
            if (colorStr.Length == 7)
                colorStr += "FF";
        }
        //Debug.Log ("GetColorFromString() colorStr = " + colorStr);

        // if hex parses
        if (ColorUtility.TryParseHtmlString (colorStr, out c)) {
            // return color
            return c;
        } else {
            // else something bright and obviously wrong
            return Color.magenta;
        }
    }






}
