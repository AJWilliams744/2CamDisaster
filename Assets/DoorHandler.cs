using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    private int[] doorNumbers;

    [SerializeField] private GameObject doors;

    [SerializeField] TMPro.TextMeshPro pedistalText;

    private int total;

    private int CurrentTotal;
    // Start is called before the first frame update
    private void Start()
    {   
        total += PlayerPrefs.GetInt("0");
        total += PlayerPrefs.GetInt("1");
        total += PlayerPrefs.GetInt("2");
        total += PlayerPrefs.GetInt("3");

        pedistalText.text = total.ToString();
        pedistalText.outlineWidth = 0.1f;
    }

    public bool AddNumber(int inValue)
    {
        CurrentTotal += inValue;
        if (total == CurrentTotal)
        {
            TriggerWin();
            return true;
        }
        return false;
      
    }

    private void TriggerWin()
    {
        Color col = new Color(0, 1, 0, 0.1f);
        foreach (DoorAnimator anim in doors.GetComponentsInChildren<DoorAnimator>())
        {
            anim.TriggerDoorOpen();
            anim.SetDoorColour(col);
        }
    }
}
