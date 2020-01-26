using System.Collections;
using TMPro;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<TextMeshPro>();
        StartCoroutine(FadeText());   
    }

    private IEnumerator FadeText()
    {
        text.outlineWidth = 0.1f;
        float t = 0;
        while (t < 8)
        {
            t += Time.deltaTime * 1f;
            text.color = new Color(0, 0, 0, 1 - (t / 8));
            text.faceColor = new Color (0.12f,1,0, 1 - (t/8));
            
            yield return new WaitForEndOfFrame();
        }
      
        this.gameObject.SetActive(false);
    }
    
}
