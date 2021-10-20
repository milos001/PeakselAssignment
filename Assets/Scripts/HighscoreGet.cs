using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreGet : MonoBehaviour
{
    private int highscore;
    
    // Start is called before the first frame update
    void Start()
    {
        //get highscore from PlayerPrefs and set it as our text variable in TMP component
        highscore = PlayerPrefs.GetInt("highscore");
        GetComponent<TextMeshProUGUI>().text = "" + highscore;
        
        //if highscore is 0/first time oppening the game hide the score number and highscore text
        if (highscore == 0)
        {
            gameObject.SetActive(false);
        }
    }
}