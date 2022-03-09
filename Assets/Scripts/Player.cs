using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float forcex,forcey;
    public bool left;
    private Rigidbody2D player_rb;
    public Transform Shadow;
    public GameObject Trail;
    private bool iscollided,isjumping;
    private void Awake()
    {
        instance = this;
        player_rb = this.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Shadow.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Shadow.transform.position = new Vector2(this.transform.position.x, Shadow.transform.position.y);
        Shadow.transform.localScale = new Vector2(this.transform.position.y/7.5f<0 ? -this.transform.position.y / 7.5f : 0 , this.transform.position.y/20f<0 ? -this.transform.position.y/20f : 0);
        if(this.transform.position.x < -2.74f)
        {
            StartCoroutine(changePosition(1));
        }
        if (this.transform.position.x > 2.74f)
        {
            StartCoroutine(changePosition(-1));
        }
        if (Input.GetMouseButtonDown(0))
        {
            move();
        }
    }
    private void move()
    {
        if (left)
        {
            //this.transform.position = Vector2.Lerp(this.transform.position, new Vector2(this.transform.position.x + 2f, this.transform.position.y + 2f), 0.5f);
            if(isjumping)
            {
                player_rb.AddForce(new Vector2(forcex, forcey)*1.5f);
            }
            else
            {
                player_rb.AddForce(new Vector2(forcex, forcey));
                isjumping = true;
            }
        }
        else
        {
            //this.transform.position = Vector2.Lerp(this.transform.position, new Vector2(this.transform.position.x + 2f, this.transform.position.y + 2f), 0.5f);
            if (isjumping)
            {
                player_rb.AddForce(new Vector2(-forcex, forcey) * 1.5f);
            }
            else
            {
                player_rb.AddForce(new Vector2(-forcex, forcey));
                isjumping = true;
            }
        }
    }
    private IEnumerator changePosition(int index)
    {
        Trail.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.50f);
        this.transform.position = new Vector2(2.74f * index, this.transform.position.y);
        Trail.gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Post1")
        {
            iscollided = true;
        }
        if(other.gameObject.tag == "Post2")
        {
            if(iscollided)
            {
                GameManager.instance.GeneratePost(left);
                left = !left;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Post2")
        {
            iscollided = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isjumping = false;
        }
    }
}
