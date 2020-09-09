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
// - Anothe example of slicing sprite https://answers.unity.com/questions/943797/editor-script-to-slice-sprites.html
// - Unity Source Code https://tinyurl.com/y3fvjftx



// STEPS
// 1. Drag a folder of images into unity project
// 2. Select the files and Choose Tools > Convert and Slice Sprites
// 3. Select the files and repeat step 2 (I think the AssetDatabase has to update?)
// 4. Select the files and check Addressable
// 5. Delete previous from Assets / Addressables
// 6. Simply Addressable names
// 7. Add "monster" label
// 8. Run Unity


// This is only useful for spritesheets that need to be automatically sliced (Sprite Editor > Slice > Automatic)
public class SpriteSlicer
{



    /**
     *  Convert selected Texture2Ds to Sprites
     *  - technically this can be done in the editor but this way I can save settings and use similar method for slicing (below)
     */
    [MenuItem("Tools/Convert and Slice Sprites")]
    public static void ToSprite()
    {
        var textures = Selection.GetFiltered<Texture2D>(SelectionMode.Assets);
        // return if no selection
        if (textures.Length < 1)
        {
            Debug.Log("Please select files in the inspector to run this script");
            return;
        }
        // loop
        foreach (var texture in textures)
        {
            //Debug.Log("Processing Textures -> " + texture.name.ToString());
            ConvertTextureToSprite(texture);
        }
    }
    static void ConvertTextureToSprite(Texture2D texture)
    {
        Debug.Log("Processing Textures -> " + texture.name.ToString() + " " + texture.width + "," + texture.height);


        // Get Texture2D

        // get asset at path
        string path = AssetDatabase.GetAssetPath(texture);
        // create TextureImporter
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;


        // Texture2D / Sprite options

        // setting to false saves memory
        importer.isReadable = true;
        // change to sprite
        importer.textureType = TextureImporterType.Sprite;
        // change to spritesheets
        importer.spriteImportMode = SpriteImportMode.Multiple;
        // point in the Sprite object's coordinate space where the graphic is located
        importer.spritePivot = Vector2.down;


        // performance / visual

        // allow to increase visual quality at small sizes
        importer.mipmapEnabled = false;
        // filter mode, performance (high to low) = point, bi, tri
        // https://forum.unity.com/threads/filter-mode-point-bilinear-trilinear-which-is-the-cheapest.147523/
        importer.filterMode = FilterMode.Point;
        // image compression https://docs.unity3d.com/ScriptReference/TextureImporterCompression.html
        importer.textureCompression = TextureImporterCompression.Uncompressed;


        // Texture2D mesh options

        // spriteExtrude and spriteMeshType aren't exposed on TextureImporter
        // https://docs.unity3d.com/ScriptReference/TextureImporterSettings.html
        var textureImporterSettings = new TextureImporterSettings();
        importer.ReadTextureSettings(textureImporterSettings);
        // set mesh type to full rectangle (original size)
        textureImporterSettings.spriteMeshType = SpriteMeshType.FullRect;
        // number of blank pixels to leave between the edge of the graphic and the mesh.
        textureImporterSettings.spriteExtrude = 0;
        // add texture-specific settings to importer
        importer.SetTextureSettings(textureImporterSettings);




        // Sprite Slice options

        // number animation slices
        int slices = 3;
        // ?
        int extrudeSize = 0;
        // width / height of sprite slices
        // monsters-400h: 500w x 400h
        int sliceW = texture.width / slices;
        int sliceH = texture.height;


        // Create slices

        // generate sprite rects on texture,
        Rect[] rects = InternalSpriteUtility.GenerateAutomaticSpriteRectangles(texture, slices, extrudeSize);
        // get and sort list of rects added
        var rectsList = new List<Rect>(rects);
        rectsList = SortRects(rectsList, texture.width);


        // Save slices

        // get filename
        string filenameNoExtension = Path.GetFileNameWithoutExtension(path);
        // create sprite metadata
        var metas = new List<SpriteMetaData>();
        // the current rect we are adding
        int rectNum = 0;
        // loop through the rects in the sprite
        foreach (Rect rect in rectsList)
        {
            // create meta for individual sprite
            var meta = new SpriteMetaData();
            // set sprite pivot
            meta.pivot = Vector2.down;
            // set sprite alignment
            meta.alignment = (int)SpriteAlignment.BottomCenter;
            // set rect coordinates: X1,Y1 X2,Y2
            meta.rect = new Rect((rectNum * sliceW), 0, sliceW, sliceH);
            // update sprite name
            meta.name = filenameNoExtension + "_" + rectNum++;
            // add to metadata
            metas.Add(meta);

            Debug.Log("Adding sprite slice -> " + meta.name + " -> " + meta);
        }

        Debug.Log("Saving slices -> " + texture.name + " " + metas.ToString());

        importer.spritesheet = metas.ToArray();

        // save changes to AssetDatabase
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
