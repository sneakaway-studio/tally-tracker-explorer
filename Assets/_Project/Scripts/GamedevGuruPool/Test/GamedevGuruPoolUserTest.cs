using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using GamedevGuru;

public class GamedevGuruPoolUserTest : MonoBehaviour
{
    [SerializeField] private AssetReference assetReferenceToInstantiate = null;

    IEnumerator Start()
    {

        Debug.Log("1. Wait for pool to warm up");
        yield return new WaitForSeconds(4f);

        Debug.Log("2. Take an object out of the pool");
        var pool = GamedevGuruPool.GetPool(assetReferenceToInstantiate);
        var newObject = pool.Take(transform);

        yield return new WaitForSeconds(4f);
        Debug.Log("3. Return it");
        pool.Return(newObject);

        yield return new WaitForSeconds(4f);
        Debug.Log("4. Disable the pool, freeing resources");
        pool.enabled = false;

        yield return new WaitForSeconds(4f);
        Debug.Log("5. Re-enable pool, put the asset back in memory");
        pool.enabled = true;
    }
}