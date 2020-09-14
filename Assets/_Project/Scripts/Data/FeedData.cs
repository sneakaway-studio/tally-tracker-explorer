using System;

[System.Serializable]
public class FeedData
{
    public string username { get; set; }
    public string avatarPath { get; set; }
    public string eventType { get; set; }
    public string eventData { get; set; }
    public string monsters { get; set; }
    public string trackers { get; set; }
    public DateTime createdAt { get; set; }
}

[System.Serializable]
public class AttackData : FeedData
{
    //"name": "DomainSwipe",
    //"type": "defense",
    //"selected": 1

    public string name { get; set; }
    public string type { get; set; }
    public bool selected { get; set; }
}

[System.Serializable]
public class BadgeData : FeedData
{
    //"name": "long-distance-scroller",
    //"level": 3

    public string name { get; set; }
    public int level { get; set; }
}

[System.Serializable]
public class ConsumableData : FeedData
{
    //"name": "dont-be-evil",
    //"slug": "dont-be-evil-cake",
    //"stat": "accuracy",
    //"type": "cake",
    //"value": 0

    public string name { get; set; }
    public string slug { get; set; }
    public string stat { get; set; }
    public string type { get; set; }
    public int value { get; set; }
}

[System.Serializable]
public class DisguiseData : FeedData
{
    //"name": "mask-carnival-sky",
    //"type": "mask"

    public string name { get; set; }
    public string type { get; set; }
}

[System.Serializable]
public class MonsterData : FeedData
{
    //"mid": 625,
    //"level": 5,
    //"captured": 1

    public int mid { get; set; }
    public int level { get; set; }
    public int captured { get; set; }
}

[System.Serializable]
public class TrackerData : FeedData
{
    //"tracker": "gstatic.com",
    //"captured": 1

    public string tracker { get; set; }
    public int captured { get; set; }
}

[System.Serializable]
public class StreamData : FeedData
{
    //"tracker": "gstatic.com",
    //"captured": 1

    public int score { get; set; }
    public int clicks { get; set; }
    public int likes { get; set; }
}