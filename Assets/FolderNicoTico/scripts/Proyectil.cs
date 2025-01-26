using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float velocidad = 15f;
    public int dano = 1;
    public float tiempoDeVida = 5f;

    private Vector2 direccion;

    public void Inicializar(Vector2 direccionDisparo)
    {
        direccion = direccionDisparo;  // Asignar la direcci�n al proyectil
    }

    private void Start()
    {
        // Destruir el proyectil despu�s de un tiempo si no impacta nada
        Destroy(gameObject, tiempoDeVida);
    }

    private void Update()
    {
        // Mover el proyectil en la direcci�n calculada
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detectar colisi�n con un objeto
        if (other.CompareTag("Target"))
        {
            // Acceder al componente de salud del enemigo (si tiene uno)
            JabonManage jabonManage = other.GetComponent<JabonManage>();
            if (jabonManage != null)
            {
                jabonManage.JabonDicrese(dano);
            }

            // Destruir el proyectil al impactar
            Destroy(gameObject);
        }
    }
}
