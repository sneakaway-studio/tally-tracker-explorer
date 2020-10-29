using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public Transform leader;            // The character the monsters will follow
    private List<Transform> monsters = new List<Transform> (); // List of monsters following the leader
    public Transform[] spawnPoints;     // Array of spawn points for monsters
    public GameObject monsterPrefab;    // Prefab of monsters being instantiated

    TallyInputSystem inputs;

    // Start is called before the first frame update
    void Start ()
    {
        inputs = new TallyInputSystem();
        inputs.Debug.MonsterAdd.performed += ctx => AddMonster();
        inputs.Debug.MonsterAdd.Enable();
    }

    // FixedUpdate is called at a regular interval
    void FixedUpdate ()
    {
        
    }

    // Adds a monster to the circle
    public void AddMonster ()
    {
        // If not at max monsters
        if (monsters.Count < 8)
        {
            // Create new monster and make it a child of the next available spawn point
            GameObject monsterObj;
            monsterObj = Instantiate(monsterPrefab, spawnPoints[monsters.Count]);
            monsters.Add(monsterObj.transform);
        }
    }
}
