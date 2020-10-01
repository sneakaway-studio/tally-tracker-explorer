using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    // The character the monsters will follow
    public Transform leader;
    // List of monsters following the leader
    public List<Transform> monsters = new List<Transform>();
    // Maximum number of monsters
    public int maxMonsters = 10;
    // Minimum distance between monsters
    public float minDistance = 0.25f;
    // Distance away from waypoint to be considered "arrived"
    public float waypointRadius = 1.0f;
    // Speed to reach waypoint
    public float speed = 1;
    // Prefab of monsters being instantiated
    public GameObject monsterPrefab;
    // GameObject the monsters are children of
    public GameObject monsterHolder;
    // Rate of time a waypoint gets made
    public float waypointRate = 0.1f;
    // Max number of waypoints at a time
    public float waypointMax = 10;

    // DEBUG: Limits spacebar presses to one at a time
    private bool spawning = false;
    // Distance between current monster and target location
    private float dis;
    // Time the previous waypoint was created at
    private float prevWaypointTime = 0;
    // List of waypoints created by the leader
    private List<Vector3> waypoints = new List<Vector3>();
    // Current monster being moved
    private Transform currMonster;
    // Previous monster in the chain
    private Transform prevMonster;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START");
    }

    // FixedUpdate is called at a regular interval
    void FixedUpdate()
    {
        // Move all the monsters
        Move();
        
        // DEBUG: Add monsters with spacebar
        if (Input.GetKey(KeyCode.Space))
        {
            AddMonster();
        }
        else
        {
            spawning = false;
        }
    }

    // Moves the first monster to the next waypoint
    private void MoveToNextWaypoint()
    {
        // Defines current monster and speed
        float currSpeed = speed;
        currMonster = monsters[0];

        // Calculates distance between current monster and next waypoint
        dis = Vector3.Distance(waypoints[0], currMonster.position);

        // If current monster is close enough to waypoint
        if (dis <= waypointRadius)
        {
            // Remove the waypoint from the list and exit out
            waypoints.RemoveAt(0);
            return;
        }

        // Gets the position of the waypoint and zeroes the z axis
        Vector3 newPos = waypoints[0];
        newPos.z = 0;

        // Slerps position to waypoint position
        float T;
        T = Time.deltaTime * dis / waypointRadius * currSpeed;
        if (T > 0.5f)
            T = 0.5f;
        currMonster.position = Vector3.Slerp(currMonster.position, newPos, T);
    }

    // Moves ALL monsters; called on FixedUpdate
    public void Move()
    {
        // Defines current speed
        float currSpeed = speed;

        // If enough time has passed since last waypoint and there are less than max waypoints
        if (Time.time > prevWaypointTime + waypointRate && waypoints.Count < waypointMax)
        {
            // Add waypoint and reset timer
            waypoints.Add(leader.position);
            prevWaypointTime = Time.time;
        }

        // If a monster and a waypoint exists
        if (monsters.Count > 0 && waypoints.Count > 0)
        {
            // Moves the first monster in the chain to the next waypoint
            MoveToNextWaypoint();
        }

        // For each monster after the first monster in the chain...
        for (int i = 1; i < monsters.Count; i++)
        {
            // Set current monster and previous monster
            currMonster = monsters[i];
            prevMonster = monsters[i - 1];

            // Get distance between current and previous monsters
            dis = Vector3.Distance(prevMonster.position, currMonster.position);

            // Get position of previous monster with zeroed Z position
            Vector3 newPos = prevMonster.position;
            newPos.z = monsters[0].position.z;

            // Slerps position and rotation to previous monster in chain
            float T;
            T = Time.deltaTime * dis / minDistance * currSpeed;
            if (T > 0.5f)
                T = 0.5f;
            currMonster.position = Vector3.Slerp(currMonster.position, newPos, T);
            currMonster.rotation = Quaternion.Slerp(currMonster.rotation, prevMonster.rotation, T);
        }
    }

    // Adds a monster to the chain
    public void AddMonster()
    {
        // If monster count is less than max
        // DEBUG: and spacebar hasn't been pressed yet
        if (monsters.Count < maxMonsters && !spawning) 
        {
            // Instantiate the monster at leader if it's first monster, or previous monster if not first monster
            GameObject monsterObj;
            if (monsters.Count == 0)
                monsterObj = Instantiate(monsterPrefab, leader.position, leader.rotation);
            else
                monsterObj = Instantiate(monsterPrefab, monsters[monsters.Count - 1].position, monsters[monsters.Count - 1].rotation);

            // Get the transform of the monster
            Transform monster = monsterObj.transform;

            // Make monster the child of monsterHolder and add it to monsters
            monster.SetParent(monsterHolder.transform);
            monsters.Add(monster);

            // DEBUG: Spacebar cannot be held down
            spawning = true;
        }
    }
}
