using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLeave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Leave()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
        GameManager.Instance.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        /*GameManager.Instance.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        GameManager.Instance.gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);*/
        GameManager.Instance.PanelSwitch(GameManager.Instance.panelMain);

       
    }
}
