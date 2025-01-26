using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] objetos; 
    public GameObject[] posiciones;

    void Start()
    {
        RandomizeObjects();
    }

    void RandomizeObjects()
    {
        if (objetos.Length == 0 || posiciones.Length == 0)
        {
            Debug.LogWarning("Asegúrate de que los arrays de objetos y posiciones estén configurados.");
            return;
        }

        foreach (GameObject posicion in posiciones)
        {
           
            Transform parentTransform = posicion.transform.parent;

           
            GameObject randomPrefab = objetos[Random.Range(0, objetos.Length)];

           
            GameObject nuevoObjeto = Instantiate(randomPrefab, posicion.transform.position, posicion.transform.rotation);

           
            nuevoObjeto.transform.parent = parentTransform;

        
            Destroy(posicion);
        }
    }
}

