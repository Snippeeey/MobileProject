using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public float forceGiven;
    private bool boost;
    private Rigidbody2D rg2d; 
    // Start is called before the first frame update
    void Start()
    {
        rg2d = FindObjectOfType<RiderController>().gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boost)
        {
            rg2d.AddForce(rg2d.transform.forward * forceGiven, ForceMode2D.Impulse);
            //rg2d.gameObject.SetActive(false)
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<RiderController>())
        {
            StartCoroutine(ImpulseTimer());
        }
    }
    IEnumerator ImpulseTimer()
    {
        boost = true; 
        yield return new WaitForSeconds(0.8f);
        boost = false; 
        
    }

}
