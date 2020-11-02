using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Attached to Player GameObject - Adds / removes monsters from the player
 */
public class MonsterController : MonoBehaviour {




    public Transform leader;            // The character the monsters will follow
    private List<Transform> monsters = new List<Transform> (); // List of monsters following the leader
    public Transform [] spawnPoints;     // Array of spawn points for monsters
    public GameObject monsterPrefab;    // Prefab of monsters being instantiated



    TallyInputSystem inputs;

    // Start is called before the first frame update
    void Start ()
    {
        //Debug.Log ("MonsterController " + gameObject.name);

        inputs = new TallyInputSystem ();
        inputs.Debug.MonsterAdd.performed += ctx => AddMonster ();
        inputs.Debug.MonsterAdd.Enable ();
    }



    // Adds a monster to the circle
    public void AddMonster ()
    {
        // if at max monsters
        if (monsters.Count >= 8) return;

        // get a random mid from those in the game
        int mid = MonsterIndex.Instance.gameMids [(int)Random.Range (0, MonsterIndex.Instance.gameMids.Length)];

        // Create new monster and make it a child of the next available spawn point
        GameObject monsterObj;
        monsterObj = Instantiate (monsterPrefab, spawnPoints [monsters.Count]);

        // call Init() on monster to get data / display animation
        monsterObj.GetComponent<MonsterAddSpritesParticleAnim> ().Init (mid);

        // add to list
        monsters.Add (monsterObj.transform);
    }

    // Removes a monster from the circle
    public void RemoveMonster (int mid = -1)
    {
        if (mid < 1) return;
    }


}
