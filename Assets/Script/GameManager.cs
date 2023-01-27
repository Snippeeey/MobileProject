using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{

    public int levelchoosen;
    public GameObject panelMain, panelLevel,canvas;

    public ButtonController buttonLiedToLevel;
    public int temporaryScores; 

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



    // Start is called before the first frame update
    void Awake()
    {
        panelMain.GetComponent<CanvasGroup>().alpha = 1;
        panelMain.GetComponent<CanvasGroup>().interactable = true;
        panelMain.GetComponent<CanvasGroup>().blocksRaycasts = true;

        panelLevel.GetComponent<CanvasGroup>().alpha = 0;
        panelLevel.GetComponent<CanvasGroup>().interactable = false;
        panelLevel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Finish()
    {
        buttonLiedToLevel.finish = true;

        StartCoroutine(LeaveScene());
       



    }
    public void LoadLevel( ButtonController button)
    {
       
        buttonLiedToLevel = button;
        levelchoosen = button.levelindex; 
        SceneManager.LoadScene(levelchoosen, LoadSceneMode.Single);
        canvas.SetActive(false);

    }
    public void PanelSwitch(GameObject panelToLoad)
    {
        if (panelMain.GetComponent<CanvasGroup>().alpha == 1 )
        {
            /*panelMain.GetComponent<Animator>().SetTrigger("exit");
            panelToLoad.GetComponent<Animator>().SetTrigger("Enter");*/
            panelMain.GetComponent<CanvasGroup>().blocksRaycasts = false; 
            panelToLoad.GetComponent<CanvasGroup>().alpha = 1;
            panelToLoad.GetComponent<CanvasGroup>().interactable = true;
            panelToLoad.GetComponent<CanvasGroup>().blocksRaycasts = true; 
        }
        else
        {
            /*panelLevel.GetComponent<Animator>().SetTrigger("exit");
            panelToLoad.GetComponent<Animator>().SetTrigger("Enter");*/
            panelLevel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            panelToLoad.GetComponent<CanvasGroup>().alpha = 1;
            panelToLoad.GetComponent<CanvasGroup>().interactable = true;
            panelToLoad.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        
       
    }
    public void Restart()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    IEnumerator LeaveScene()
    {
        yield return new WaitForSeconds(1f);
        canvas.SetActive(true);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}
