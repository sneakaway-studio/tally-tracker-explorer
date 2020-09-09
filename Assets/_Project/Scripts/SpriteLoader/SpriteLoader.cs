using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using System.Threading.Tasks;
using System.Linq;


// 1. load sprites frames from addressables
// 2. instantiate prefabs
// 3. populate sprite animation frames on prefabs

// notes on Addressables and Sprites
// https://github.com/omundy/dig250-game-art-dev/blob/master/reference-sheets/Unity-Working-with-Assets.md#addressables


public class SpriteLoader : MonoBehaviour
{
    // labels for sprite
    public string[] lables;

    // prefab to show sprite
    public GameObject prefab;

    // dict to store sprites
    public Dictionary<string, GameObject> sprites = new Dictionary<string, GameObject>();

    // count total added 
    public int count = 0;


    // on start
    async void Start()
    {
        try
        {
            // get all the matching addressables locations
            var locations = await Addressables.LoadResourceLocationsAsync(lables, Addressables.MergeMode.UseFirst).Task;

            // return if no results 
            if (locations == null || locations.Count < 1) return;



            foreach (var loc in locations)
            {
                // get location name
                var spriteName = loc.PrimaryKey;

                // don't allow duplicates
                if (sprites.ContainsKey(spriteName)) continue;


                Debug.Log("Loading " + (count++) + "/" + locations.Count + " - " + spriteName + " - " + loc.GetType());


                // instantiate prefab and position
                Vector3 spawnPosition = new Vector3(Random.Range(-30, 30), Random.Range(-20, 20), Random.Range(-15, 5));
                GameObject obj = (GameObject)Instantiate(prefab, spawnPosition, Quaternion.identity);

                // set name, parent, store
                obj.name = spriteName;
                obj.transform.parent = gameObject.transform;
                sprites[spriteName] = obj;

                // load each spritesheet frame
                for (var i = 0; i < 3; i++)
                {
                    //var sprite = await Addressables.LoadAssetAsync<Sprite>(location).Task;
                    //sprites[loc.InternalId] = sprite;

                    // load sheet
                    Addressables.LoadAssetAsync<Sprite>(spriteName + "[" + spriteName + "_" + i + "]").Completed += OnSpriteLoaded;
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("error" + e.ToString());
        }
    }


    void OnSpriteLoaded(AsyncOperationHandle<Sprite> op)
    {
        if (op.Result == null)
        {
            Debug.LogError("op.Result was null");
            return;
        }

        // split str to get name and sprite num 
        string[] arr = op.Result.name.Split('_');

        //Debug.Log(arr[0] + " -> " + arr[1]);

        // add result to FrameAnimation on sprite prefab
        sprites[arr[0]].GetComponent<FrameAnimation>().sprites[int.Parse(arr[1])] = op.Result;
    }



}
