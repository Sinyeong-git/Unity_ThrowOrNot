using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainManager : MonoBehaviour
{
    public GameObject HelpPanel;
    public GameObject ScorePanel;
    public GameObject ExitPanel;
    public Text Score_text;
    private string KeyString = "HighScore";

    // Start is called before the first frame update
    void Start()
    {
        HelpPanel = GameObject.Find("HelpPanel");
        HelpPanel.SetActive(false);//헬프 패널 출력

        ScorePanel = GameObject.Find("ScorePanel");
        ScorePanel.SetActive(false);

        ExitPanel = GameObject.Find("ExitPanel");
        ExitPanel.SetActive(false);

        
        Score_text.text = "최고점수 : " + PlayerPrefs.GetInt(KeyString, 0) + " 점";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitPanel.SetActive(true);
        }
    }

    public void onclickStart()
    {
        Debug.Log("시작");
        SceneManager.LoadScene("Game");
    }

    public void onclickHelp()
    {
        HelpPanel.SetActive(true);//헬프 패널 출력
        Debug.Log("헬프");
    }

    public void onclickHelpPanel_Help()
    {
        HelpPanel.SetActive(false);//헬프 패널 출력
    }
    public void onclickHelpPanel_Score()
    {
        HelpPanel.SetActive(false);//헬프 패널 출력
        ScorePanel.SetActive(true);
    }



    public void onclickScore()
    {  
        Debug.Log("스코어");
        ScorePanel.SetActive(true);
    }
    public void onclickScorePanel_Help()
    {
        ScorePanel.SetActive(false);
        HelpPanel.SetActive(true);//헬프 패널 출력
    }
    public void onclickScorePanel_Score()
    {
        ScorePanel.SetActive(false);
    }

    public void onclickExitYes()
    {
        Application.Quit();
    }

    public void onclickExitNo()
    {
        ExitPanel.SetActive(false);
    }
}
