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
    public float rotationSpeed = 50;
    public GameObject bodyPrefab;
    public GameObject monsterHolder;

    private float dis;
    private Transform curBodyPart;
    private Transform prevBodyPart;

    // Start is called before the first frame update
    void Start()
    {
        bodyParts.Insert(0, leader);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        if (Input.GetKey(KeyCode.Space))
        {
            AddBodyPart();
        }
    }

    public void Move()
    {
        float curSpeed = speed;

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
        if (bodyParts.Count - 1 < maxBodyParts) 
        {
            GameObject part = Instantiate(bodyPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count - 1].rotation);
            Transform newPart = part.transform;
            newPart.SetParent(monsterHolder.transform);
            bodyParts.Add(newPart);
        }
    }
}
