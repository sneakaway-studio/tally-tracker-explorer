

public class DataManager : Singleton<DataManager>
{
    // singleton
    protected DataManager() { }
    public static new DataManager Instance;




    public static string current;

    void Start()
    {

        StartCoroutine(ApiManager.GetRequest("https://tallysavestheinternet.com/api/feed/recent"));

    }

}
