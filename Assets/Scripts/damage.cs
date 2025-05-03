using UnityEngine;
using UnityEngine.Events;
public class damage : MonoBehaviour
{
    public UnityEvent evento;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            evento.Invoke();
        }
    }
}
