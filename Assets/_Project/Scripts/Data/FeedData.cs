using System;

[System.Serializable]
public class FeedData {
    //  "username": "unlikelyscroller68",
    //  "about": null,
    //  "avatarPath": "assets/img/avatars/avatar-pattern-glasses-sun-60s.png",
    //  "level": 12,
    //  "clicks": 624,
    //  "score": 1564,
    //  "time": 36200,
    //  "capturedTotal": 5,
    //  "missedTotal": 3,
    //  "pageActionScrollDistance": 145013,
    //  "trackersBlocked": 164,
    //  "trackersSeen": 949


    public string username { get; set; }
    public string avatarPath { get; set; }
    public int level { get; set; }
    public int clicks { get; set; }
    public int score { get; set; }
    public int time { get; set; }
    public int capturedTotal { get; set; }
    public int missedTotal { get; set; }
    public int pageActionScrollDistance { get; set; }
    public int trackersBlocked { get; set; }
    public int trackersSeen { get; set; }


    public string eventType { get; set; }
    public string eventData { get; set; }
    public string monsters { get; set; }
    public string trackers { get; set; }
    public DateTime createdAt { get; set; }
}

[System.Serializable]
public class AttackData : FeedData {
    //"name": "DomainSwipe",
    //"type": "defense",
    //"selected": 1

    public string _name { get; set; }
    public string _type { get; set; }
    public bool _selected { get; set; }
}

[System.Serializable]
public class BadgeData : FeedData {
    //"name": "long-distance-scroller",
    //"level": 3

    public string _name { get; set; }
    public int _level { get; set; }
}

[System.Serializable]
public class ConsumableData : FeedData {
    //"name": "dont-be-evil",
    //"slug": "dont-be-evil-cake",
    //"stat": "accuracy",
    //"type": "cake",
    //"value": 0

    public string _name { get; set; }
    public string _slug { get; set; }
    public string _stat { get; set; }
    public string _type { get; set; }
    public int _value { get; set; }
}

[System.Serializable]
public class DisguiseData : FeedData {
    //"name": "mask-carnival-sky",
    //"type": "mask"

    public string _name { get; set; }
    public string _type { get; set; }
}

[System.Serializable]
public class MonsterData : FeedData {
    //"mid": 625,
    //"level": 5,
    //"captured": 1

    public int _mid { get; set; }
    public int _level { get; set; }
    public int _captured { get; set; }
}

[System.Serializable]
public class TrackerData : FeedData {
    //"tracker": "gstatic.com",
    //"captured": 1

    public string _tracker { get; set; }
    public int _captured { get; set; }
}

[System.Serializable]
public class StreamData : FeedData {
    //"tracker": "gstatic.com",
    //"captured": 1

    public int _score { get; set; }
    public int _clicks { get; set; }
    public int _likes { get; set; }
}