using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public static Test2 instance;
    public int count;
    public bool updateData;

    float direction = 1;
    public Bug prefab;

    public List<Bug> gameObjects;
    public List<Vector3> objPositions;

    private void Awake()
    {
        instance = this;
        gameObjects = new List<Bug>();
        objPositions = new List<Vector3>();
        for (int i = 0; i < count; i++)
        {
            Bug tmp = Instantiate(prefab, new Vector3(Random.Range(-3f, 3f), -5, 0), Quaternion.identity);
            tmp.index = i;
            gameObjects.Add(tmp);
            objPositions.Add(gameObjects[i].transform.position);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        //if (updateData)
        //{
        //    for (int i = 0; i < count; i++) {
        //        objPositions[i] += Vector3.up * dt;
        //        gameObjects[i].transform.position = objPositions[i];
        //    }
        //}
        //else {
        //    for (int i = 0; i < count; i++) {
        //        gameObjects[i].transform.Translate(Vector3.up * dt * direction);
        //    }
        //}
    }
}

public struct MyGameObjects {
    List<Vector3> positions;
}
