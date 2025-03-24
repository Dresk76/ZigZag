using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBackground : MonoBehaviour
{
    [Header("AUDIO")]
    public static AudioBackground instancia;



    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else if (instancia != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
