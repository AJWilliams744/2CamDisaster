using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCenter : MonoBehaviour
{
    Vector3 targetScale = new Vector3(10, 25, 1);
    Vector3 center = new Vector3(0, 0.1f, -28);
    private Vector3 startPosition;
    private Vector3 startScale;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        startScale = this.transform.localScale;

        StartCoroutine(MoveToCenterOverTime());
    }

    private IEnumerator MoveToCenterOverTime()
    {
        float t = 0;
        float time = 120;
        while (t < time)
        {
            t += Time.deltaTime * 1f;
            this.transform.position = Vector3.Lerp(startPosition, center, t / time);
            this.transform.localScale = Vector3.Lerp(startScale, targetScale, t / time);
            yield return new WaitForEndOfFrame();
        }
    }
}
