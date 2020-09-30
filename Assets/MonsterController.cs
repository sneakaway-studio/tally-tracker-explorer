using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform leader;
    public List<Transform> bodyParts = new List<Transform>();
    public int maxBodyParts = 10;
    public float minDistance = 0.25f;
    public float leaderDistance = 1.0f;
    public float speed = 1;
    public GameObject bodyPrefab;
    public GameObject monsterHolder;
    public float waypointRate = 0.1f;
    public float waypointMax = 10;

    private bool spawning = false;
    private float dis;
    private float prevWaypointTime = 0;
    private List<Vector3> waypoints = new List<Vector3>();
    private Transform curBodyPart;
    private Transform prevBodyPart;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        if (Input.GetKey(KeyCode.Space))
        {
            AddBodyPart();
        }
        else
        {
            spawning = false;
        }
    }

    private void MoveToNextWaypoint()
    {
        float curSpeed = speed;
        curBodyPart = bodyParts[0];

        dis = Vector3.Distance(waypoints[0], curBodyPart.position);
        if (dis <= leaderDistance)
        {
            waypoints.RemoveAt(0);
            return;
        }

        Vector3 newPos = waypoints[0];
        newPos.z = 0;

        float T;
        T = Time.deltaTime * dis / leaderDistance * curSpeed;
        if (T > 0.5f)
            T = 0.5f;
        curBodyPart.position = Vector3.Slerp(curBodyPart.position, newPos, T);
    }

    public void Move()
    {
        float curSpeed = speed;

        if (Time.time > prevWaypointTime + waypointRate && waypoints.Count < waypointMax)
        {
            waypoints.Add(leader.position);
            prevWaypointTime = Time.time;
        }

        if (bodyParts.Count > 0 && waypoints.Count > 0)
        {
            MoveToNextWaypoint();
        }

        for (int i = 1; i < bodyParts.Count; i++)
        {
            curBodyPart = bodyParts[i];
            prevBodyPart = bodyParts[i - 1];

            dis = Vector3.Distance(prevBodyPart.position, curBodyPart.position);

            Vector3 newPos = prevBodyPart.position;
            newPos.z = bodyParts[0].position.z;

            float T;
            if (i == 1)
                T = Time.deltaTime * dis / leaderDistance * curSpeed;
            else
                T = Time.deltaTime * dis / minDistance * curSpeed;

            if (T > 0.5f)
                T = 0.5f;
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newPos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, prevBodyPart.rotation, T);
        }
    }

    public void AddBodyPart()
    {
        if (bodyParts.Count < maxBodyParts && !spawning) 
        {
            GameObject part;
            if (bodyParts.Count == 0)
                part = Instantiate(bodyPrefab, leader.position, leader.rotation);
            else
                part = Instantiate(bodyPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count - 1].rotation);
            Transform newPart = part.transform;
            newPart.SetParent(monsterHolder.transform);
            bodyParts.Add(newPart);
            spawning = true;
        }
    }
}
