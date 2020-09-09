using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


// Convert Texture2D to Sprite, then slice sprites
// - Executed using a command in the menu
// - Found here: https://gist.github.com/shadesbelow/8a6ddc54db795241f3cff539db6ea487
// - Used code from this example: https://github.com/toxicFork/Unity3D-TextureAtlasSlicer/blob/master/Editor/TextureAtlasSlicer.cs
// - Other ways exist, like the AssetPostProcessor https://docs.unity3d.com/ScriptReference/AssetPostprocessor.html
// - Class using ^ method https://forum.unity.com/threads/sprite-editor-automatic-slicing-by-script.320776/
// - Unity Source Code https://tinyurl.com/y3fvjftx

// have to run twice...


// This is only useful for spritesheets that need to be automatically sliced (Sprite Editor > Slice > Automatic)
public class AutoSpriteSlicer
{
    static string monsterSpritesPath = "-anim-sheet";



    [MenuItem("Tools/Convert Selected Textures to Sprites %&s")]
    public static void ToSprite()
    {
        var textures = Selection.GetFiltered<Texture2D>(SelectionMode.Assets);
        //Debug.Log("Processing Selection");
        foreach (var texture in textures)
        {
            //Debug.Log("Processing Textures -> " + texture.name.ToString());
            ProcessTexture(texture);
        }
    }

    [MenuItem("Tools/Slice Selected Spritesheets (1500x400) %&s")]
    public static void Slice()
    {
        var textures = Selection.GetFiltered<Texture2D>(SelectionMode.Assets);
        //Debug.Log("Processing Selection");
        foreach (var texture in textures)
        {
            //Debug.Log("Processing Textures -> " + texture.name.ToString());
            ProcessTexture(texture);
        }
    }


    static void ConvertTextureToSprite()
    {


    }


    static void ProcessTexture(Texture2D texture)
    {


        Debug.Log("Processing Textures -> " +
            texture.name.ToString() + " " +
            texture.width + "," + texture.height
            );


        string path = AssetDatabase.GetAssetPath(texture);
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;

        //importer.isReadable = true;
        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Multiple;
        importer.mipmapEnabled = false;
        importer.filterMode = FilterMode.Point;
        importer.spritePivot = Vector2.down;
        importer.textureCompression = TextureImporterCompression.Uncompressed;

        // need this stupid class because spriteExtrude and spriteMeshType aren't exposed on TextureImporter
        var textureSettings = new TextureImporterSettings();
        importer.ReadTextureSettings(textureSettings);
        textureSettings.spriteMeshType = SpriteMeshType.FullRect;
        textureSettings.spriteExtrude = 0;
        importer.SetTextureSettings(textureSettings);


        //Debug.Log("Made it this far!");




        // number animation frames
        int spriteFrames = 3;
        int extrudeSize = 0;

        // monsters-400h
        //int frameW = 500;
        //int frameH = 400;

        // width / height of sprite frame
        int frameW = texture.width / spriteFrames;
        int frameH = texture.height;

        Rect[] rects = InternalSpriteUtility.GenerateAutomaticSpriteRectangles(texture, spriteFrames, extrudeSize);
        var rectsList = new List<Rect>(rects);
        rectsList = SortRects(rectsList, texture.width);

        string filenameNoExtension = Path.GetFileNameWithoutExtension(path);
        var metas = new List<SpriteMetaData>();
        int rectNum = 0;

        foreach (Rect rect in rectsList)
        {
            Debug.Log("Adding sprite frame" + texture.name + "," + rectNum);
            var meta = new SpriteMetaData();
            meta.pivot = Vector2.down;
            meta.alignment = (int)SpriteAlignment.BottomCenter;
            // X1,Y1 X2,Y2
            meta.rect = new Rect((rectNum * frameW), 0, frameW, frameH);
            meta.name = filenameNoExtension + "_" + rectNum++;
            metas.Add(meta);
        }

        importer.spritesheet = metas.ToArray();

        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);


    }

    static List<Rect> SortRects(List<Rect> rects, float textureWidth)
    {
        List<Rect> list = new List<Rect>();
        while (rects.Count > 0)
        {
            Rect rect = rects[rects.Count - 1];
            Rect sweepRect = new Rect(0f, rect.yMin, textureWidth, rect.height);
            List<Rect> list2 = RectSweep(rects, sweepRect);
            if (list2.Count <= 0)
            {
                list.AddRange(rects);
                break;
            }
            list.AddRange(list2);
        }
        return list;
    }

    static List<Rect> RectSweep(List<Rect> rects, Rect sweepRect)
    {
        List<Rect> result;
        if (rects == null || rects.Count == 0)
        {
            result = new List<Rect>();
        }
        else
        {
            List<Rect> list = new List<Rect>();
            foreach (Rect current in rects)
            {
                if (current.Overlaps(sweepRect))
                {
                    list.Add(current);
                }
            }
            foreach (Rect current2 in list)
            {
                rects.Remove(current2);
            }
            list.Sort((a, b) => a.x.CompareTo(b.x));
            result = list;
        }
        return result;
    }
}
