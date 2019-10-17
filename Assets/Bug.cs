using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public int index;
    Transform _transform;
    Test2 manager;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        manager = Test2.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.updateData)
        {
            manager.objPositions[index] += Vector3.up * Time.deltaTime;
            _transform.position = manager.objPositions[index];
        }
        else {
            _transform.Translate(Vector3.up * Time.deltaTime);
        }

    }
}
