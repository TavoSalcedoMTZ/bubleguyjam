using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ObstaculeDamage : MonoBehaviour
{
    public UnityEvent obstaculoColission;
    public bool isDamaged = false;
    private bool coroutineRunning = false;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            isDamaged = true;

            if (!coroutineRunning)
            {
                StartCoroutine(DañoConstante());
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            isDamaged = false;
        }
    }

    IEnumerator DañoConstante()
    {
        coroutineRunning = true;

        while (isDamaged)
        {
            obstaculoColission.Invoke();
            yield return new WaitForSeconds(0.5f);
        }

        coroutineRunning = false;
    }
}
