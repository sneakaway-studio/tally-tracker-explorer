using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SortedCreatedAssets : MonoBehaviour
{
    [SerializeField] private List<string> _labels = new List<string>() { };





    void Start()
    {
        SortWaitToCreate(_labels);
    }

    private async Task SortWaitToCreate(List<string> labels)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(labels.ToArray(), Addressables.MergeMode.UseFirst).Task;

        foreach (var location in locations)
        {
            //Debug.Log(location.PrimaryKey);
            Addressables.InstantiateAsync(location);
        }
    }


}