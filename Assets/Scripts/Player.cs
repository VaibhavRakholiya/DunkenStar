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
    public TrailRenderer Trail;
    private bool iscollided,onground;
    public float isjumping=0f;
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
        if (Input.GetMouseButtonDown(0) && GameManager.instance.GameStatus == GameManager.status.Play)
        {
            //player_rb.angularDrag = 0f;
            move();
        }
        if (player_rb.velocity.x > 1.0f)
            this.transform.Rotate(0f, 0f, -1.5f);
        if (player_rb.velocity.x < -1.0f)
            this.transform.Rotate(0f, 0f, 1.5f);
    }
    private void move()
    {
        if (left)
        {
            //this.transform.position = Vector2.Lerp(this.transform.position, new Vector2(this.transform.position.x + 2f, this.transform.position.y + 2f), 0.5f);
            //player_rb.AddForce(new Vector2(forcex, forcey ));
            if (this.transform.position.y < 5.32f)
            {
                player_rb.velocity = Vector2.zero;
                player_rb.AddForce(new Vector2(forcex, forcey));
            }
             if (isjumping < 2)
                isjumping+=0.50f;
        }
        else
        {
            //this.transform.position = Vector2.Lerp(this.transform.position, new Vector2(this.transform.position.x + 2f, this.transform.position.y + 2f), 0.5f);
            if (this.transform.position.y < 5.32f)
            {
                player_rb.velocity = Vector2.zero;
                player_rb.AddForce(new Vector2(-forcex, forcey));
            }
                
                //player_rb.AddForce(new Vector2(-forcex, forcey));
            if (isjumping < 2)
                isjumping+=0.50f;
        }
    }
    private IEnumerator changePosition(int index)
    {
        Trail.emitting = false;
        yield return new WaitForSeconds(0.50f);
        this.transform.position = new Vector2(2.25f * index, this.transform.position.y);
        yield return new WaitForSeconds(0.50f);
        Trail.emitting = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Post2")
        {
            if(iscollided)
            {
                GameManager.instance.GeneratePost(left);
                iscollided = false;
                left = !left;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Post1")
        {
            iscollided = true;
            Invoke("TurnOffCollied", 0.5f);
        }
        
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onground = true;
        }
    }
    private void TurnOffCollied()
    {
        iscollided = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isjumping = 1;
            //player_rb.angularDrag = 1.5f;
            onground = false;
        }
    }
    public void ChangeSkin()
    {
        this.GetComponent<SpriteRenderer>().sprite = GameManager.instance.getCurrentBall();
    }
}
