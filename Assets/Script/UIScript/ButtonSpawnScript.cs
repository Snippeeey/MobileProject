using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawnScript : MonoBehaviour
{
    public List<GameObject> button = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAllbuttonFalse()
    {
        for (int i = 0; i < button.Count; i++)
        {
            
            button[i].SetActive(false);
            button[i].GetComponent<ButtonController>().StarSetActiveFalse();
        }
    }
    public IEnumerator ButtonSpawn()
    {
        for (int i = 0; i < button.Count; i++)
        {
           yield return new WaitForSeconds(0.2f);
           button[i].SetActive(true);
           StartCoroutine(button[i].GetComponent<ButtonController>().StarSpawn());
        }
    }
}
