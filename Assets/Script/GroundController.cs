using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public bool kill,animated,done = true;
    public float timeToStart, rotationTime, rotationValue ;
    private float newRotZ;
    
    // Start is called before the first frame update
    void Awake()
    {
       
        
        newRotZ = transform.localEulerAngles.z;
        
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if(animated)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, newRotZ);
        }
   
    }
    IEnumerator Rotate()
    {
       
       
        done = false; 
        yield return new WaitForSeconds(timeToStart);
        float timeElapsed = 0;
        while (timeElapsed < rotationTime)
        {
            newRotZ = Mathf.Lerp(transform.localEulerAngles.z, transform.localEulerAngles.z + 1, timeElapsed / rotationTime);
            //Debug.Log(transform.localEulerAngles);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
            
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<RiderController>() == true && done && animated)
        {
            StartCoroutine(Rotate());
        }
    }
}
