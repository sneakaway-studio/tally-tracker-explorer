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
    public void AddMonster (int _mid = -1)
    {
        // if at max monsters
        if (monsters.Count >= 8) return;

        if (_mid < 1) {
            // get a random mid from those in the game
            _mid = MonsterIndex.Instance.gameMids [(int)Random.Range (0, MonsterIndex.Instance.gameMids.Length)];
        }


        // Create new monster and make it a child of the next available spawn point
        GameObject monsterObj;
        monsterObj = Instantiate (monsterPrefab, spawnPoints [monsters.Count]);

        // call Init() on monster to get data / display animation
        monsterObj.GetComponent<Monster> ().Init (_mid);

        // rename
        monsterObj.name = "m" + _mid.ToString ();

        // add to list
        monsters.Add (monsterObj.transform);
    }

    // Removes a monster from the circle
    public void RemoveMonster (int _mid = -1)
    {
        if (_mid < 1 || monsters.Count < 1) return;

        for (int i = 0; i < monsters.Count; i++) {
            if (monsters [i].name == "m" + _mid) {
                // remove from game
                Destroy (monsters [i]);
                // remove from list
                monsters.RemoveAt (i);
                break;
            }
        }
    }


}
