using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameOverManager : MonoBehaviour
{
    forScore fs;

    private string KeyString = "HighScore";
    private int savedScore = 0;

    public Text Score_Text;


    public int Score_int = 0;

    // Start is called before the first frame update
    void Start()
    {
        savedScore = PlayerPrefs.GetInt(KeyString, 0);

        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }

        try
        {
            fs = GameObject.Find("forScore").GetComponent<forScore>();

            Score_int = fs.score_save;
        }
        catch
        {
            Score_int = 0;
        }


        if (savedScore < Score_int)
        {
            PlayerPrefs.SetInt(KeyString, Score_int);
        }
        Score_Text.text = "점수 : " + Score_int + "점";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onclick()
    {

        SceneManager.LoadScene("Main");
    }
}
