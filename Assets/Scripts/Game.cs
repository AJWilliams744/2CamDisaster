using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Character Character;
    [SerializeField] private Canvas Menu;
    [SerializeField] private Canvas Hud;
    [SerializeField] private Transform CharacterStart;

    //Camera Follow Script
    [SerializeField] private CameraFollow camFollowFar;
    [SerializeField] private CameraFollow camFollowClose;
    [SerializeField] private GameObject camObjClose;

    [SerializeField] private MenuInteractions menuInteract;
    [SerializeField] private GameObject menuItems;
    [SerializeField] private PostProcessVolume postProcess;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Light directionalLight;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private AudioListener mainCamAudio;
    [SerializeField] private ChallengeSetter challengeSetter;

    [SerializeField] private RawImage screenFad;

    private RaycastHit[] mRaycastHits;
    private Character mCharacter;
    private Environment mMap;

    [SerializeField] private TextMeshProUGUI CounterTextBox;
    private int CounterLength = 30;

    private readonly int NumberOfRaycastHits = 1;

    void Start()
    {
        mRaycastHits = new RaycastHit[NumberOfRaycastHits];
        mMap = GetComponentInChildren<Environment>();
        mCharacter = Instantiate(Character, transform);

        //find mount and place to camera follow              
        camFollowClose.SetMount(GameObject.Find("MountC").transform);        

        mCharacter.SetSitting(true);
        mCharacter.SetRootMotion(true);
        menuInteract.SetCharacter(mCharacter);
        ShowMenu(true);

        characterController.SetCharacterController(mCharacter);
    }

    private void Update()
    {
        // Check to see if the player has clicked a tile and if they have, try to find a path to that 
        // tile. If we find a path then the character will move along it to the clicked tile. 
        if(Input.GetMouseButtonDown(0))
        {
            Ray screenClick = MainCamera.ScreenPointToRay(Input.mousePosition);
            int hits = Physics.RaycastNonAlloc(screenClick, mRaycastHits);
            if( hits > 0)
            {
                EnvironmentTile tile = mRaycastHits[0].transform.GetComponent<EnvironmentTile>();
                if (tile != null)
                {
                    List<EnvironmentTile> route = mMap.Solve(mCharacter.CurrentPosition, tile);
                    mCharacter.GoTo(route);
                }
            }
        }
        // if (Input.GetKeyDown(KeyCode.Escape)) ShowMenu(true); // To many resets, should be a seperate scene
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(SceneManager.sceneCount); //TODO dont be so lazy with this
    }

    public void ShowMenu(bool show)
    {
        if (Menu != null && Hud != null)
        {
            Menu.enabled = show;
            Hud.enabled = !show;

            if( show )
            {
                mCharacter.transform.position = CharacterStart.position;
                mCharacter.transform.rotation = CharacterStart.rotation;
                mMap.CleanUpWorld();
               // challengeSetter.Destroy(); May use later
            }
            else
            {
                mCharacter.transform.position = mMap.StartTile.Position;
                mCharacter.transform.rotation = Quaternion.identity;
                mCharacter.CurrentPosition = mMap.StartTile;
            }
        }
    }

    public void Generate()
    {

        audioManager.StartGameMusic();
        //mMap.GenerateWorld();

        StartCoroutine(PostProcessBlend());

       
    }

    private IEnumerator PostProcessBlend()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 1f;
            postProcess.weight = t;
            yield return new WaitForEndOfFrame();

        }
        StartGame();
    }

    private IEnumerator ReversePostProcessBlend()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 1f;
            postProcess.weight = 1 - t;
            yield return new WaitForEndOfFrame();

        }
       
    }

    //Handle all initializations
    private void StartGame()
    {
        mMap.GenerateWorld();
        ShowMenu(false);

        camObjClose.SetActive(true);
        mCharacter.SetSitting(false);
        menuItems.SetActive(false);

        camFollowClose.CameraOn();
        camFollowFar.CameraOn();

        mainCamAudio.enabled = false;

        inputHandler.StartGameControls();

        mCharacter.SetRootMotion(false);
        directionalLight.intensity = 0;

        //Spawns enemy and handle difficulty 
        challengeSetter.StartChallenge();
        CounterTextBox.text = CounterLength.ToString();
        StartCoroutine(StartCountDown());

    }

    private IEnumerator StartCountDown()
    {
        while (true)
        {            
            int counter = int.Parse(CounterTextBox.text);
            counter -= 1;
            if (counter <= -1)
            {
                counter = CounterLength;
                mMap.RotateAllTiles();
                audioManager.PlayMetalDoorSound();
                challengeSetter.WorldChanged();
            }
            CounterTextBox.text = counter.ToString();
            yield return new WaitForSeconds(1);
        }
    }

    public void IAmDead(int sceneLoad)
    {
        inputHandler.SetGameStart(false);
        camFollowFar.ForceStopFollow();
        StopAllCoroutines();
        audioManager.PlayDeath();
        StartCoroutine(FadeScreen(sceneLoad));
    }

    private IEnumerator FadeScreen(int sceneLoad)
    {
        float t = 0;
        while (t < 8)
        {
            t += Time.deltaTime * 1f;
            screenFad.color = new Color(0,0,0, (t / 4));
            audioManager.SetMasterVolume(-t * 6);
            yield return new WaitForEndOfFrame();
        }

        
        SceneManager.LoadScene(sceneLoad);
    }
       

    public void Exit()
    {
#if !UNITY_EDITOR
        Application.Quit();
#endif
    }
}
