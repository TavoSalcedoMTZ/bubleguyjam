using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passWorld : MonoBehaviour
{

    public Enemigo Enemigo;
    public bool transportlevel;

    void Start()
    {
       transportlevel = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Enemigo.Vida == 0)
        {
            transportlevel=true;
        }

    }
}
    
