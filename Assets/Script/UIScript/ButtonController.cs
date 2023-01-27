using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public List<GameObject> Star = new List<GameObject>();
    public bool finish;
    public int score,levelindex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Lvlcontrol();
        ListControl();
    }
    void ListControl()
    {
        

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

}
