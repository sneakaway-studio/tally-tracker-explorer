using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableSpriteLoader : MonoBehaviour
{



    //IEnumerator Start()
    //{

    //    Debug.Log("1. Wait for pool to warm up");
    //    yield return new WaitForSeconds(4f);

    //    Debug.Log("2. Take an object out of the pool");
    //    var pool = GamedevGuruPool.GetPool(assetReferenceToInstantiate);
    //    var newObject = pool.Take(transform);

    //    yield return new WaitForSeconds(4f);
    //    Debug.Log("3. Return it");
    //    pool.Return(newObject);

    //    yield return new WaitForSeconds(4f);
    //    Debug.Log("4. Disable the pool, freeing resources");
    //    pool.enabled = false;

    //    yield return new WaitForSeconds(4f);
    //    Debug.Log("5. Re-enable pool, put the asset back in memory");
    //    pool.enabled = true;
    //}


    public AssetReferenceSprite[] sprites;

    public AssetReferenceSprite newSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Load());
    }


    IEnumerator Load()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            System.Random rand = new System.Random();
            int r = rand.Next(0, 3);
            Debug.Log("Loading # " + r);
            sprites[r].LoadAssetAsync().Completed += SpriteLoaded;
        }
    }


    private void SpriteLoaded(AsyncOperationHandle<Sprite> obj)
    {
        switch (obj.Status)
        {
            case AsyncOperationStatus.Succeeded:

                Debug.Log(obj.ToString());


                spriteRenderer.sprite = obj.Result;
                break;
            case AsyncOperationStatus.Failed:
                Debug.LogError("Sprite load failed.");
                break;
            default:
                // case AsyncOperationStatus.None:
                break;
        }
    }



}
