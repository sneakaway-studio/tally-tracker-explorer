using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{

    public ApiRequest ApiRequest;

    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine(ApiRequest.GetRequest("https://tallysavestheinternet.com/api/feed/recent"));
      
    }

    // Update is called once per frame
    void Update()
    {

    }
}
