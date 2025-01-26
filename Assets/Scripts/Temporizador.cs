using System.Collections;
using UnityEngine;

public class BubbleExplosion : MonoBehaviour
{
    public GameObject[] bubbles; 
    public float inflateTime = 1f; 
    public float maxSize = 2f; 
    public float explosionTime = 0.2f; 
    public float waitTime = 3.33f; 
    void Start()
    {
        StartCoroutine(InflateAndExplodeBubbles());
    }

    IEnumerator InflateAndExplodeBubbles()
    {
        for (int i = 0; i < bubbles.Length; i++)
        {
            
            yield return new WaitForSeconds(waitTime);

          
            yield return StartCoroutine(InflateBubble(bubbles[i]));

            yield return StartCoroutine(ExplodeBubble(bubbles[i]));
        }
    }

   
    IEnumerator InflateBubble(GameObject bubble)
    {
        Vector3 initialScale = bubble.transform.localScale;
        Vector3 targetScale = initialScale * maxSize; 

        float elapsedTime = 0f;
        while (elapsedTime < inflateTime)
        {
            elapsedTime += Time.deltaTime;
            float scaleFactor = Mathf.Lerp(1f, maxSize, elapsedTime / inflateTime);
            bubble.transform.localScale = initialScale * scaleFactor;
            yield return null;
        }

        
        bubble.transform.localScale = targetScale;
    }

    
    IEnumerator ExplodeBubble(GameObject bubble)
    {
        
        Vector3 initialScale = bubble.transform.localScale;
        Vector3 targetScale = initialScale * 3f; 

        float elapsedTime = 0f;
        while (elapsedTime < explosionTime)
        {
            elapsedTime += Time.deltaTime;
            float scaleFactor = Mathf.Lerp(1f, 3f, elapsedTime / explosionTime);
            bubble.transform.localScale = initialScale * scaleFactor;
            yield return null;
        }

      
        bubble.transform.localScale = targetScale;

    
        bubble.SetActive(false);
    }
}
