using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundkeeper : MonoBehaviour
{
    private static soundkeeper _instance;
    void Awake()
    {
        if (!_instance)
            _instance = this;
        else
            Destroy(this.gameObject);


        DontDestroyOnLoad(gameObject);
    }
}
