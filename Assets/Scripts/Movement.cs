using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


/*********************************************/
/*  Mark Crawford                            */
/* CSC 350 H, Prof Tang                      */
/* Lab 7                                     */
/* 03/12/2024                                */
/*********************************************/




public class Movement : MonoBehaviour
{
    
    //movement vars
    [SerializeField]
    private float mvmtspd = 4f;
    float scalechg = 0.7f;

    [SerializeField]
    float jumpscale = 0f;

    // controlling jump
    bool canjump = true;
    int jumpcount = 0;


    // physics interact
    Rigidbody2D rb;
    BoxCollider2D cc;

    // collider coord
    Vector3 screendim;


    // collider dimensions
    float colliderhalfwidth, colliderhalfheight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<BoxCollider2D>();
        screendim = (cc.bounds.max - cc.bounds.min);
        colliderhalfwidth = screendim.x / 2;
        colliderhalfheight = screendim.y / 2;
        ScreenClamp.Initialize();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.W) && canjump)
        {    
            rb.AddForce(new Vector2(0f, jumpscale), ForceMode2D.Impulse);
            jumpcount++;
            if (jumpcount >= 2)
            {
                canjump = false;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(mvmtspd * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            // Duck
            transform.localScale = new Vector3(1f, scalechg, 1f);
        }

        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(mvmtspd * Time.deltaTime, 0f, 0f);
        }

        KeepInBounds();
    }

    // keep boxcollider in bounds
    public void KeepInBounds()
    {
        if (transform.position.x - colliderhalfwidth < ScreenClamp.ScreenLeft)
        {
            Debug.Log(ScreenClamp.ScreenLeft);
            Debug.Log(transform.position.x - colliderhalfwidth);
            transform.position = new Vector3(ScreenClamp.ScreenLeft + colliderhalfwidth, transform.position.y, transform.position.z);

        }
        if (transform.position.x + colliderhalfwidth > ScreenClamp.ScreenRight)
        {
            transform.position = new Vector3(ScreenClamp.ScreenRight - colliderhalfwidth, transform.position.y, transform.position.z); 
        }
        if (transform.position.y - colliderhalfheight < ScreenClamp.ScreenBottom)
        {
            transform.position = new Vector3(transform.position.x, ScreenClamp.ScreenBottom + colliderhalfheight, transform.position.z);
        }
        if (transform.position.y + colliderhalfheight > ScreenClamp.ScreenTop)
        {
            transform.position = new Vector3(transform.position.x, ScreenClamp.ScreenTop - colliderhalfheight, transform.position.z);
        }
    }

    // collision detection ground
   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Square")
        {
            jumpcount = 0;
            canjump = true;
        }
    }
  

}
