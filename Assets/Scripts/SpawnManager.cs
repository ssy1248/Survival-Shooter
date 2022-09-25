using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public Transform CopyObj = null;

    void CreateMob()
    {
        Transform clone = GameObject.Instantiate(CopyObj);
        clone.position = transform.position;
        clone.gameObject.SetActive(true);

    }

    void Start()
    {
        CopyObj.gameObject.SetActive(false);
        this.InvokeRepeating("CreateMob", 0, 2f);

    }

    void Update()
    {
        
    }
}
