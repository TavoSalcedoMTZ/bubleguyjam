using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndaExpansiva : MonoBehaviour
{
    public Transform puntodisparo1;
    public Transform puntodisparo2;
    public Transform puntodisparo3;
    public GameObject proyectilPrefab;
    private float disparoSpeed = 10f;
    private float tiempoVida = 1.5f;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión Hecha");

      
        Vector3 desplazamiento = new Vector3(0.5f, 0.5f, 0); 

        GameObject proyectil = Instantiate(proyectilPrefab, puntodisparo1.position + desplazamiento, puntodisparo1.rotation);
        GameObject proyectil2 = Instantiate(proyectilPrefab, puntodisparo2.position + desplazamiento, puntodisparo2.rotation);
        GameObject proyectil3 = Instantiate(proyectilPrefab, puntodisparo3.position + desplazamiento, puntodisparo3.rotation);

      
        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = proyectil2.GetComponent<Rigidbody2D>();
        Rigidbody2D rb3 = proyectil3.GetComponent<Rigidbody2D>();

       
        Collider2D collider1 = proyectil.GetComponent<Collider2D>();
        Collider2D collider2 = proyectil2.GetComponent<Collider2D>();
        Collider2D collider3 = proyectil3.GetComponent<Collider2D>();

        // Ignorar colisiones entre los proyectiles
        if (collider1 != null && collider2 != null && collider3 != null)
        {
            Physics2D.IgnoreCollision(collider1, collider2);
            Physics2D.IgnoreCollision(collider1, collider3); 
            Physics2D.IgnoreCollision(collider2, collider3);
        }

   
        if (rb != null && rb2 != null && rb3 != null)
        {
            rb.velocity = new Vector2(1, 0) * disparoSpeed; 
            rb2.velocity = new Vector2(0, 1) * disparoSpeed; 
            rb3.velocity = new Vector2(-1, 0) * disparoSpeed; 
        }

 
        Destroy(gameObject);
        Destroy(proyectil, tiempoVida);
        Destroy(proyectil2, tiempoVida);
        Destroy(proyectil3, tiempoVida);
    }
}
