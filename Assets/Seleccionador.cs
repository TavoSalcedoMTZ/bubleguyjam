using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Seleccionador : MonoBehaviour
{
    public static int indexDisparo = 0; 

    public void setDisparoPlayer(int index)
    {
       indexDisparo = index;
    }

    public int getDisparoPlayer()
    {
        return indexDisparo;
    }
}
