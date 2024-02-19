using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemStuff : MonoBehaviour
{
    public static EventSystemStuff Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
}
