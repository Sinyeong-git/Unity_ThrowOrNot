using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;
    public Animator animator4;
    public Animator animator5;
    public Animator GameOver;
    public Animator GameOver2;

    public float Timer_f;
    public Text Timer_text;
    public Text Score_Text;
    public Text alert_text;
    public int Score_int = 0;

    public AudioSource se1;
    public AudioSource se2;
    public AudioSource se3;
    public AudioSource se4;
    public AudioSource se5;

    bool inform = true;
    bool se1_b = false;
    bool se2_b = false;
    bool se3_b = false;


    bool ani1 = false;
    bool ani2 = false;
    bool ani3 = false;
    bool ani4 = false;

    bool playing = false;
    bool shaking = false;
    bool gameoverCount = false;

    double cooltime = 0.07f;
    double informtime = 0;


    float accelerometerUpdateInterval = 1.0f / 60.0f;
    // The greater the value of LowPassKernelWidthInSeconds, the slower the
    // filtered value will converge towards current input sample (and vice versa).
    float lowPassKernelWidthInSeconds = 1.0f;
    // This next parameter is initialized to 2.0 per Apple's recommendation,
    // or at least according to Brady! ;)
   
    //흔들기 인식 포스값 10 기준으로 작업 디버깅용으론 3
    float shakeDetectionThreshold = 10.0f;

    float lowPassFilterFactor;
    Vector3 lowPassValue;

    float shakingTimeLimitSave = 10;
    float shakingTime = 15;


    /* 타이밍 맞추기용 디버깅툴
    public void OnClickBasicButton()
    {
        Debug.Log(Timer_f);
    }
    */



    // Start is called before the first frame update

    forScore fs;


    void Start()
    {
        //요것저것 찾는 과정
        se1 = GameObject.Find("1").GetComponent<AudioSource>();
        se2 = GameObject.Find("2").GetComponent<AudioSource>();
        se3 = GameObject.Find("3").GetComponent<AudioSource>();
        se4 = GameObject.Find("4").GetComponent<AudioSource>();
        se5 = GameObject.Find("4").GetComponent<AudioSource>();

        fs = GameObject.Find("forScore").GetComponent<forScore>();
    }

    // Update is called once per frame
    void Update()
    {

        //흔들기 값 계산
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;

        //던질까 말까 감지 시
        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
        {
            shaking = true;
            if(gameoverCount == true)
            {
                gameoverCount = false;
                GameOver.SetBool("GameOver", false);
                GameOver2.SetBool("gameover", false);
                se3.Play();
            }
            if (se4.isPlaying)
            {
                se4.Pause();
            }

           // Debug.Log("Shake event detected at time " + Time.time);
            shakingTime = shakingTimeLimitSave;
        }

        //쉐이킹 쉬는 시간 감지
        shakingTime -= Time.deltaTime;
        //게임오버 루틴 계시
        
        if(shakingTime < 0 && gameoverCount == true)
        {
            Debug.Log("게임오버");
            fs.score_save = Score_int;
            SceneManager.LoadScene("GameOver");
        }
       
        else if (shakingTime < 5 && shakingTime > 0 && gameoverCount == false)
        {
            se3.Pause();
            se4.Play();
            gameoverCount = true;
            Handheld.Vibrate();
            Debug.Log("죽기5초전");
            GameOver.SetBool("GameOver", true);
            GameOver2.SetBool("gameover", true);
        }




        if (Timer_f >= 0 && inform == true)
        {
            inform = false;
            se1_b = true;
            Debug.Log("Inform 종료");
        }

        if (se1_b == true)
        {
            se1.Play();
            se1_b = false;
            se2_b = true;
            Debug.Log("se1 실행");
        }

        if (se2_b == true && !se1.isPlaying)
        {
            se2.Play();
            se2_b = false;
            se3_b = true;
            Debug.Log("se2 실행");
            Score_int += 1;
            playing = true;
            animator5.SetBool("gamestart", true);
        }

        if (se3_b == true && !se2.isPlaying && !se3.isPlaying && gameoverCount == false)
        {
            se3.Play();
            Debug.Log("se3 실행");
            Score_int += 1;
            animator5.SetBool("rotation", true);
        }


        if (Timer_f > 2.007342 + informtime - cooltime && ani1 == false)
        {
            ani1 = true;
            Debug.Log("에니메이션1 실행");
            animator1.SetBool("autuki1", true);
        }

        if (Timer_f > 2.923237 + informtime - cooltime && ani2 == false)
        {
            ani2 = true;
            Debug.Log("에니메이션2 실행");
            animator2.SetBool("ready1", true);
        }

        if (Timer_f > 3.808272 + informtime - cooltime && ani3 == false)
        {
            ani3 = true;
            Debug.Log("에니메이션3 실행");
            animator3.SetBool("autuki2", true);
        }

        if (Timer_f > 4.724135 + informtime - cooltime && ani4 == false)
        {
            ani4 = true;
            Debug.Log("에니메이션4 실행");
            animator4.SetBool("fire", true);
        }



        //타이머
        Timer_f += Time.deltaTime; 
        Timer_text.text = "time : " + Mathf.Round(Timer_f);
        Score_Text.text = "점수 : " + Score_int;

        alert_text.text = Mathf.Round(shakingTime) + "초 내로 춤추지 않을 시 게임오버";
    }

}
