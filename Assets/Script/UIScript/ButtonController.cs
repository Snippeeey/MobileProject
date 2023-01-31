using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public List<GameObject> Star = new List<GameObject>();
    public bool finish;

    [Header("BestScore")]
    public int highScore;
    public int levelindex , Targetedloop, Score;
    public float Timer; 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StarSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        Lvlcontrol();
        StarToChange();
    }
    void StarToChange()
    {
        for (int i  = 0; i < Star.Count; i++)
        { 

        }

    }

    
    void Lvlcontrol()
    {
        if(!finish)
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
    public void StarSetActiveFalse()
    {
        for (int i = 0; i < Star.Count; i++)
        {           
            Star[i].SetActive(false);
        }
    }
    public IEnumerator StarSpawn()
    {
        for (int i = 1; i < Star.Count+1; i++)
        {
            yield return new WaitForSeconds(0.1f);
            if(Score >= i )
            {
                Star[i-1].GetComponent<Image>().color = Color.white;
            }
            else
            {
                Star[i-1].GetComponent<Image>().color = Color.red;
            }
            Star[i-1].SetActive(true);
        }
    }

}
