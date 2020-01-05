using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource MenuMusic;
    [SerializeField]
    private AudioSource GameMusic;
    [SerializeField]
    private AudioClip JumpScare;
    [SerializeField]
    private AudioClip BigSound;

    [SerializeField]
    private AudioSource Insects;
    [SerializeField]
    private AudioSource Wind;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGameMusic()
    {

        GameMusic.PlayOneShot(JumpScare);
        MenuMusic.Stop();
        GameMusic.Play();
        GameMusic.loop = true;
        GameMusic.PlayOneShot(BigSound);
        Insects.Play();
        Wind.Play();
       // Rain.Play();
    }

   

    
}
