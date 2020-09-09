using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;


// this version uses listeners to postprocess whenever sprites are loaded
// - might be good for more established dev workflow


public class SpriteAnimProcessor : AssetPostprocessor
{


    //    string monsterSpritesPath = "_Project/Sprites/monsters-400h";




    //    // before texture is imported 
    //    private void OnPreprocessTexture()
    //    {


    //        //Debug.Log(assetPath.ToString());

    //        // MONSTER SPRITES
    //        if (assetPath.Contains(monsterSpritesPath))
    //        {
    //            Debug.Log("OnPreprocessTexture overwriting defaults");

    //            // create TextureImporter
    //            TextureImporter textureImporter = assetImporter as TextureImporter;
    //            // set all to sprite, multiple (animations)
    //            textureImporter.textureType = TextureImporterType.Sprite;
    //            textureImporter.spriteImportMode = SpriteImportMode.Multiple;
    //            // allow mipmaps (small versions on screen)
    //            textureImporter.mipmapEnabled = false;
    //            // how the Texture is filtered when it gets stretched by 3D transformations
    //            textureImporter.filterMode = FilterMode.Point;
    //            textureImporter.spritePivot = Vector2.down;
    //            // do not compress
    //            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;



    //            var textureSettings = new TextureImporterSettings(); // need this stupid class because spriteExtrude and spriteMeshType aren't exposed on TextureImporter
    //            textureImporter.ReadTextureSettings(textureSettings);
    //            textureSettings.spriteMeshType = SpriteMeshType.Tight;
    //            textureSettings.spriteExtrude = 0;
    //            textureImporter.SetTextureSettings(textureSettings);


    //        }
    //    }
    //    // after texture is imported
    //    private void OnPostprocessTexture(Texture2D texture)
    //    {
    //        if (assetPath.ToLower().IndexOf(monsterSpritesPath) == -1)
    //            return;




    //        // # of animation frames
    //        int spriteSize = 3;
    //        int colCount = texture.width / spriteSize;
    //        int rowCount = texture.height / spriteSize;




    //        int minimumSpriteSize = 16;
    //        int extrudeSize = 0;
    //        Rect[] rects = InternalSpriteUtility.GenerateAutomaticSpriteRectangles(texture, minimumSpriteSize, extrudeSize);

    //        var rectsList = new List<Rect>(rects);
    //        rectsList = SortRects(rectsList, texture.width);


    //        string filenameNoExtension = Path.GetFileNameWithoutExtension(path);
    //        var metas = new List<SpriteMetaData>();
    //        int rectNum = 0;

    //        foreach (Rect rect in rectsList)
    //        {
    //            var meta = new SpriteMetaData();
    //            meta.pivot = Vector2.down;
    //            meta.alignment = (int)SpriteAlignment.BottomCenter;
    //            meta.rect = rect;
    //            meta.name = filenameNoExtension + "_" + rectNum++;
    //            metas.Add(meta);
    //        }

    //        importer.spritesheet = metas.ToArray();

    //        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);





    //    }
    //}

    ////// after texture is imported
    ////public void OnPostprocessTexture(Texture2D texture)
    ////{
    ////    if (assetPath.ToLower().IndexOf(monsterSpritesPath) == -1)
    ////        return;


    ////    //    var A_Sprite = rects.OrderBy(r => r.width * r.height).First().center;
    ////    //    int colCount = rects.Where(r => r.Contains(new Vector2(r.center.x, A_Sprite.y))).Count();
    ////    //    int rowCount = rects.Where(r => r.Contains(new Vector2(A_Sprite.x, r.center.y))).Count();
    ////    //Vector2Int spriteSize = new Vector2Int(texture.width / colCount, texture.height / rowCount);

    ////    //List<SpriteMetaData> metas = new List<SpriteMetaData>();

    ////    //for (int r = 0; r < rowCount; ++r)
    ////    //{
    ////    //    for (int c = 0; c < colCount; ++c)
    ////    //    {
    ////    //        SpriteMetaData meta = new SpriteMetaData();
    ////    //        meta.rect = new Rect(c * spriteSize.x, r * spriteSize.y, spriteSize.x, spriteSize.y);
    ////    //        meta.name = string.Format("#{3} {0} ({1},{2})", Path.GetFileNameWithoutExtension(assetImporter.assetPath), c, r, r * colCount + c);
    ////    //        metas.Add(meta);
    ////    //    }
    ////    //}

    ////    //    TextureImporter textureImporter = (TextureImporter)assetImporter;
    ////    //    textureImporter.spritesheet = metas.ToArray();
    ////    //    AssetDatabase.Refresh();

    ////}


    //static List<Rect> SortRects(List<Rect> rects, float textureWidth)
    //{
    //    List<Rect> list = new List<Rect>();
    //    while (rects.Count > 0)
    //    {
    //        Rect rect = rects[rects.Count - 1];
    //        Rect sweepRect = new Rect(0f, rect.yMin, textureWidth, rect.height);
    //        List<Rect> list2 = RectSweep(rects, sweepRect);
    //        if (list2.Count <= 0)
    //        {
    //            list.AddRange(rects);
    //            break;
    //        }
    //        list.AddRange(list2);
    //    }
    //    return list;
    //}

    //static List<Rect> RectSweep(List<Rect> rects, Rect sweepRect)
    //{
    //    List<Rect> result;
    //    if (rects == null || rects.Count == 0)
    //    {
    //        result = new List<Rect>();
    //    }
    //    else
    //    {
    //        List<Rect> list = new List<Rect>();
    //        foreach (Rect current in rects)
    //        {
    //            if (current.Overlaps(sweepRect))
    //            {
    //                list.Add(current);
    //            }
    //        }
    //        foreach (Rect current2 in list)
    //        {
    //            rects.Remove(current2);
    //        }
    //        list.Sort((a, b) => a.x.CompareTo(b.x));
    //        result = list;
    //    }
    //    return result;
    //}



}