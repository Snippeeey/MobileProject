using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Number of star for ths level")]
    public int levelScore;


    [Header("LevelScore to get 2 Star ")]
    public int targetedScore;
    public int finalLoopScore;



    [Header("LevelTimer ")]
    private float targetedTimer; 
    public float timeLeft;
    private float Finaltimer; 



    public bool TimerOn = false;

    [Header("Timer  et score")]
    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI scoreTxT;

    public int levelchoosen;
    public bool levelUi , inMenu ;
    public GameObject panelMain, panelLevel, canvas, buttonGroup;

    public ButtonController buttonLiedToLevel;
    public ButtonSpawnScript bps;
    public int temporaryScores;
    private bool timerAdded, loopAdded;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.Log("Gamemanager is null");
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
      
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
    }
    // Start is called before the first frame update
    void Awake()
    {
        
        bps = FindObjectOfType<ButtonSpawnScript>();
        panelMain.GetComponent<CanvasGroup>().alpha = 1;
        panelMain.GetComponent<CanvasGroup>().interactable = true;
        panelMain.GetComponent<CanvasGroup>().blocksRaycasts = true;

        panelLevel.GetComponent<CanvasGroup>().alpha = 0;
        panelLevel.GetComponent<CanvasGroup>().interactable = false;
        panelLevel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        inMenu = true; 
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        Uicolorcontrol();
    }
    public void Finish()
    {
        buttonLiedToLevel.finish = true;

        StartCoroutine(LeaveScene());
       
    }
    public void Timer()
    {
        if (TimerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                timeLeft = 0;
                TimerOn = false;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerTxt.text = string.Format("{0:00}: {1:00}", minutes, seconds);
    }
    void ScoreController()
    {
        if (Finaltimer >= targetedTimer * 0.75 && !timerAdded  )
        {
            levelScore += 3;
            timerAdded = true; 
        }
        else if (Finaltimer >= targetedTimer * 0.5 && Finaltimer < targetedTimer * 0.75 && !timerAdded)
        {
            levelScore += 2;
            timerAdded = true;
        }
        else if (Finaltimer >= targetedTimer * 0.25 && Finaltimer < targetedTimer * 0.5 && !timerAdded)
        {
            levelScore += 1;
            timerAdded = true;
        }
        else 
        {
            levelScore += 0;
        }

        if(finalLoopScore >= targetedScore && !loopAdded)
        {
            levelScore += 2;
            loopAdded = true; 
        }
        else if (finalLoopScore >= targetedScore/2 && finalLoopScore < targetedScore && !loopAdded)
        {
            levelScore += 1;
            loopAdded = true;
        }
        else
        {
            levelScore += 0;
        }



        if (buttonLiedToLevel.highScore < levelScore )
        {
            buttonLiedToLevel.Score = levelScore;
            buttonLiedToLevel.highScore = buttonLiedToLevel.Score;

        }



    }
    
    public void LoadLevel( ButtonController button)
    {
       
        
        buttonLiedToLevel = button;
        levelchoosen = button.levelindex;
        targetedTimer = button.Timer; 
        timeLeft = button.Timer;
        targetedScore = button.Targetedloop;
        bps.SetAllbuttonFalse();
        SceneManager.LoadScene(levelchoosen, LoadSceneMode.Single);
        inMenu = false;        
        canvas.SetActive(false);       
        loopAdded = false;
        timerAdded = false; 
       
        


    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {

        scoreTxT = GameObject.FindGameObjectWithTag("ScoreTag").GetComponent<TextMeshProUGUI>();
        timerTxt = GameObject.FindGameObjectWithTag("TimerTag").GetComponent<TextMeshProUGUI>();
        TimerOn = true;
    }
    private void Uicolorcontrol()
    {
        int loop = FindObjectOfType<RiderController>().score;
        if (timeLeft >= targetedTimer * 0.75)
        {
            timerTxt.color = Color.yellow;
            timerTxt.outlineColor = Color.yellow;
        }
        else if (timeLeft >= targetedTimer * 0.5 && timeLeft < targetedTimer * 0.75 )
        {
            timerTxt.color = Color.white;
            timerTxt.outlineColor = Color.white;
        }
        else if (timeLeft >= targetedTimer * 0.25 && timeLeft < targetedTimer * 0.5 )
        {
            timerTxt.color = Color.gray;
            timerTxt.outlineColor = Color.white;
        }
        else
        {

        }
        if (loop >= targetedScore )
        {
            scoreTxT.color = Color.yellow;
            scoreTxT.outlineColor = Color.yellow;
        }
        else if (loop >= targetedScore / 2 && loop < targetedScore )
        {
            scoreTxT.color = Color.white;
            scoreTxT.outlineColor = Color.white;
        }
        else 
        {
            scoreTxT.color = Color.gray;
            scoreTxT.outlineColor = Color.white;
        }

    }
    public void RestartAfterDeath()
    {
        LoadLevel(buttonLiedToLevel);
    }
    public void PanelSwitch(GameObject panelToLoad)
    {
       
      
        if (panelMain.GetComponent<CanvasGroup>().alpha == 1)
        {
            /*panelMain.GetComponent<Animator>().SetTrigger("exit");
            panelToLoad.GetComponent<Animator>().SetTrigger("Enter");*/
            panelMain.GetComponent<CanvasGroup>().blocksRaycasts = false;
            panelMain.GetComponent<CanvasGroup>().alpha = 0;
            panelMain.GetComponent<CanvasGroup>().interactable = false;
            panelToLoad.GetComponent<CanvasGroup>().alpha = 1;
            panelToLoad.GetComponent<CanvasGroup>().interactable = true;
            panelToLoad.GetComponent<CanvasGroup>().blocksRaycasts = true;
            StartCoroutine(bps.ButtonSpawn());
            



        }
        else
        {
            /*panelLevel.GetComponent<Animator>().SetTrigger("exit");
            panelToLoad.GetComponent<Animator>().SetTrigger("Enter");*/
            panelLevel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            panelLevel.GetComponent<CanvasGroup>().alpha = 0;
            panelLevel.GetComponent<CanvasGroup>().interactable = false;
            panelToLoad.GetComponent<CanvasGroup>().alpha = 1;
            panelToLoad.GetComponent<CanvasGroup>().interactable = true;
            panelToLoad.GetComponent<CanvasGroup>().blocksRaycasts = true;
            bps.SetAllbuttonFalse();


        }
        
       
        
       
    }
    public void Restart()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    IEnumerator LeaveScene()
    {
        
        TimerOn = false;
        Finaltimer = timeLeft;
        finalLoopScore = FindObjectOfType<RiderController>().score;
        ScoreController();
        yield return new WaitForSeconds(1f);
        canvas.SetActive(true);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        StartCoroutine(bps.ButtonSpawn());
        inMenu = true;
        yield return null;
        
    }

}
