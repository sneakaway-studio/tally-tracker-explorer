using System;

[System.Serializable]
public class FeedData
{
    public string username { get; set; }
    public string avatarPath { get; set; }
    public string item { get; set; }
    public string type { get; set; }
    public string name { get; set; }
    public int level { get; set; }
    public string stat { get; set; }
    public int captured { get; set; }
    public DateTime date { get; set; }

    // "username": "andaughter",
    // "avatarPath": null,
    // "item": "monster",
    // "type": "0",
    // "name": "628",
    // "level": 22,
    // "stat": "0",
    // "captured": 0,
    // "date": "2020-08-20T17:44:39.000Z"

}
