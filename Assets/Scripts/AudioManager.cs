using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    private AudioSource BackgroundMusic;

    [SerializeField]
    private AudioSource Insects;
    [SerializeField]
    private AudioSource Wind;
    [SerializeField]
    private AudioClip MetalDoor;    
    [SerializeField]
    private AudioSource ZombieBite;
    [SerializeField]
    private AudioSource KeyFoundAudio;

    [SerializeField]
    private AudioMixer masterAudio;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundMusic.Play();
        Cursor.lockState = CursorLockMode.None;
        
        Cursor.visible = true;
        masterAudio.SetFloat("Volume", 0);
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

    public void PlayMetalDoorSound()
    {
        Insects.PlayOneShot(MetalDoor);
    }

    public void SetMasterVolume(float inValue)
    {
        masterAudio.SetFloat("Volume", inValue);
    }

    public void PlayDeath(int sceneLoad)
    {
        if(sceneLoad == 5)
        {
            ZombieBite.Play();
        }
        else
        {
            PlayMetalDoorSound();
        }
        
    }

    public void KeyFound()
    {
        KeyFoundAudio.Play();
    }

   

    
}
