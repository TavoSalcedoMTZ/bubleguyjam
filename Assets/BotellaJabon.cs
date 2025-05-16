using System.Collections;
using UnityEngine;

public class BotellaJabon : MonoBehaviour
{
   private JabonManage jabonManager;

    private void Start()
    {
        jabonManager = FindAnyObjectByType<JabonManage>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {

            jabonManager.JabonIncremense(7);
        
            StartCoroutine(WaitForSeconds());
        }
    }

    IEnumerator WaitForSeconds()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
