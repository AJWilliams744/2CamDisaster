using TMPro;
using UnityEngine;

public enum Difficulty { Easy, Medium, Hard, Extreme }
public class ChallengeSetter : MonoBehaviour
{
    private GameObject Torch;

    private Difficulty difficulty;

    [SerializeField] private GameObject SpotLight;
    [SerializeField] private EnemyHandler enemyHandler;

    private string diffcultyBase = "DIFICULTY";
    [SerializeField] private TextMeshProUGUI difficultyText;
    

    private void Start()
    {        
        difficulty = Difficulty.Medium;
        IncrementDifficulty();
    }
    private void SetChallenge(Difficulty inDifficulty)
    {
        difficulty = inDifficulty;
    }

    public void WorldChanged()
    {
        enemyHandler.MapChanged();
    }

    public void Reset()
    {
        enemyHandler.ResetAllEnemy();
    }

    public void Destroy()
    {
        enemyHandler.DestroyAllEnemies();
    }

    public void StartChallenge()
    {
        Torch = GameObject.Find("TorchLight");
        SpotLight.SetActive(true);
        switch (difficulty)
        {
            case Difficulty.Easy:
                Easy();
                break;
            case Difficulty.Medium:
                Medium();
                break;
            case Difficulty.Hard:
                Hard();
                break;
            case Difficulty.Extreme:
                Extreme();
                break;
        }
        enemyHandler.StartHunt();
    }

    public void IncrementDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:

                difficulty = Difficulty.Medium;
                difficultyText.text = diffcultyBase + "\n[Medium] Trial Run, I think I need to practice first";

                break;
            case Difficulty.Medium:

                difficulty = Difficulty.Hard;
                difficultyText.text = diffcultyBase + "\n[Hard] Forgetful Insomia, as it should be played";

                break;
            case Difficulty.Hard:

                difficulty = Difficulty.Extreme;
                difficultyText.text = diffcultyBase + "\n[Extreme] Darkness is my friend";

                break;
            case Difficulty.Extreme:

                difficulty = Difficulty.Easy;
                difficultyText.text = diffcultyBase + "\n[Easy] Child mode, Sometimes life is just too hard";

                break;
        }
    }

    //Everything on
    // 3 Creatures
    private void Easy()
    {
        enemyHandler.SetEnemyCount(2); //Child mode, Sometimes life is just too hard
       
    }
    //Everything on
    //5 Creatures
    private void Medium() //Trial Run, I think I need to practice first
    {
        enemyHandler.SetEnemyCount(5);
        
    }
    //No SpotLight
    //5 Creatures
    private void Hard() // Forgetful Insomia, as it should be played
    {
        SpotLight.GetComponent<Light>().intensity = 0;
        enemyHandler.SetEnemyCount(5);

    }

    //No SpotLight
    //Very Short Torch
    //10 Creatures
    private void Extreme() //I Wish I Could See || Darkness is my friend || I live in my parents basement
    {
        SpotLight.GetComponent<Light>().intensity = 0;
        Torch.GetComponent<Light>().range = 10;
        Torch.GetComponent<Light>().spotAngle = 150;
        enemyHandler.SetEnemyCount(10);
    }
}
