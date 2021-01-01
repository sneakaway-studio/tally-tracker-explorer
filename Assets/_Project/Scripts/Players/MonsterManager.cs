using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Attached to Player GameObject - Adds / removes monsters from the player
 */
public class MonsterManager : MonoBehaviour {

    public Transform leader;            // player to follow
    private List<Transform> monsters = new List<Transform> (); // List of monsters following the leader
    public Transform [] spawnPoints;    // Array of spawn points for monsters
    public GameObject monsterPrefab;    // Prefab of monsters being instantiated

    // testing
    TallyInputSystem inputs;

    void Start ()
    {
        //Debug.Log ("MonsterManager " + gameObject.name);

        // testing
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
            _mid = MonsterIndex.Instance.GetRandomMid ();
        }

        // create name for obj
        string name = _mid.ToString ();

        // if already exists then exit
        var exists = monsters.Find (item => item.name.Equals (name));
        if (exists != null) {
            //Debug.Log ("MONSTER ALREADY EXISTS IN LIST");
            return;
        }

        // Create new monster and make it a child of the next available spawn point
        GameObject obj = Instantiate (monsterPrefab, spawnPoints [monsters.Count]);
        // call Init() on monster to get data / display animation
        obj.GetComponent<Monster> ().Init (_mid);
        // set name in Unity Editor
        obj.name = name;
        // add to list
        monsters.Add (obj.transform);
    }

    // Removes a monster from the circle
    public void RemoveMonster (int _mid = -1)
    {
        if (_mid < 1 || monsters.Count < 1) return;

        for (int i = 0; i < monsters.Count; i++) {
            if (monsters [i].name == _mid.ToString ()) {
                // remove from game
                Destroy (monsters [i].gameObject);
                // remove from list
                monsters.RemoveAt (i);
                break;
            }
        }
    }

    public void RemoveAllMonsters ()
    {
        for (int i = 0; i < monsters.Count; i++) {
            // remove from game
            Destroy (monsters [i].gameObject);
        }
        monsters.Clear ();
    }

}
