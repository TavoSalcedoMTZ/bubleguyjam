using UnityEngine;

public class DisparoPlayer : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public GameObject proyectilExplosivoPrefab;
    public float disparoSpeed = 10f;   
    public float tiempoVida = 3f;       
    public Transform puntoDisparo;
    private JabonManage jabonmanage;
    public int UsoDeJabon;
    public int playernum;
      public bool typeshot;
    public bool canShoot;


    private void Start()
    {
        typeshot = false;
        jabonmanage=GetComponent<JabonManage>();
        canShoot = true;
    }
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.V))
        {
            if (!typeshot)
            {
                typeshot = true;
            }
            else if (typeshot)
            {
                typeshot=false;
            }
        }
  
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            if (playernum == 0)
            {
                Personaje1Shoot();
            }
            else if (playernum == 1)
            {
                Personaje2Shoot();
            }
            else if (playernum == 2)
            {
                Personaje3Shoot();
            }

        }
    }

    void DisparoBasico()
    {
        jabonmanage.JabonDicrese(1);
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);


        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = puntoDisparo.right * disparoSpeed; 
        }

   
        Destroy(proyectil, tiempoVida);
    }

    void Personaje1Shoot()
    {
        if (!typeshot)
        {
            DisparoBasico();
        }
        else if (typeshot){

            jabonmanage.JabonDicrese(3);

   
            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
            GameObject proyectil2 = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
            GameObject proyectil3 = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);

  
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = proyectil2.GetComponent<Rigidbody2D>();
            Rigidbody2D rb3 = proyectil3.GetComponent<Rigidbody2D>();

            if (rb != null && rb2 != null && rb3 != null)
            {

                rb.velocity = puntoDisparo.right * disparoSpeed;

          
                Vector2 direccionIzquierda = Quaternion.Euler(0, 0, -10) * puntoDisparo.right;
                Vector2 direccionDerecha = Quaternion.Euler(0, 0, 10) * puntoDisparo.right; 

         
                rb2.velocity = direccionIzquierda * disparoSpeed;
                rb3.velocity = direccionDerecha * disparoSpeed ;
            }
        }
    }
    void Personaje2Shoot()
    {
        if (!typeshot)
        {
            DisparoBasico();
        }
        else if (typeshot)
        {

            jabonmanage.JabonDicrese(5);


            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
            GameObject proyectil2 = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
            GameObject proyectil3 = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
            GameObject proyectil4 = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
            GameObject proyectil5 = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);

            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = proyectil2.GetComponent<Rigidbody2D>();
            Rigidbody2D rb3 = proyectil3.GetComponent<Rigidbody2D>();
            Rigidbody2D rb4 = proyectil4.GetComponent<Rigidbody2D>();
            Rigidbody2D rb5 = proyectil5.GetComponent<Rigidbody2D>();



            if (rb != null && rb2 != null && rb3 != null)
            {

                rb.velocity = puntoDisparo.right * disparoSpeed;
                rb2.velocity = puntoDisparo.right * disparoSpeed;
                rb3.velocity = puntoDisparo.right * disparoSpeed;
                rb4.velocity = puntoDisparo.right * disparoSpeed;
                rb5.velocity = puntoDisparo.right * disparoSpeed;

            }
            Destroy(proyectil, tiempoVida);
            Destroy(proyectil2, tiempoVida);
            Destroy(proyectil3, tiempoVida);
            Destroy(proyectil4, tiempoVida);
            Destroy(proyectil5, tiempoVida);
        }
    }


    void Personaje3Shoot()
    {
        if (!typeshot)
        {
            DisparoBasico();
        }
        else if (typeshot)
        {
            jabonmanage.JabonDicrese(3);
            GameObject proyectil = Instantiate(proyectilExplosivoPrefab, puntoDisparo.position, puntoDisparo.rotation);


            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = puntoDisparo.right * disparoSpeed;
            }


            Destroy(proyectil, tiempoVida);

        }
    }

}
