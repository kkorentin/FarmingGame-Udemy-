using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    //Singleton instance
    public static UIController instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {            
            Destroy(gameObject);
        }
    }


    public GameObject[] toolbarActivatorIcons;

    public TMP_Text timeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchTool(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTool(int selected)
    {
       foreach(GameObject icon in toolbarActivatorIcons)
       {
            icon.SetActive(false);
       }

        toolbarActivatorIcons[selected].SetActive(true);
    }

    public void UpdateTimeText(float currentTime)
    {
        switch (currentTime)
        {
            case < 12:
                timeText.text = Mathf.FloorToInt(currentTime) + " AM";
            break;
            case < 13:
                timeText.text = "12 PM";
            break;
            case < 24:
                timeText.text = Mathf.FloorToInt(currentTime - 12) + " PM";
            break;
            case < 25:
                timeText.text = "12 AM";
            break;
            case > 25:
                timeText.text = Mathf.FloorToInt(currentTime - 24) + " AM";
            break;
        }  
    }
}
