using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRStuff : MonoBehaviour
{
    public static XRStuff Instance;

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
