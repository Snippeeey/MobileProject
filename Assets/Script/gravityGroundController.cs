using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class gravityGroundController : MonoBehaviour
{

    
    public float timetofall;

    private Rigidbody2D rg2d;
    // Start is called before the first frame update
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        rg2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(timetofall);
        rg2d.constraints = RigidbodyConstraints2D.None;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "-Player-")
        {
            Debug.Log("prout");
            StartCoroutine(Fall());

        }
        
          
        
    }
}
