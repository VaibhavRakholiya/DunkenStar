using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform Post;
    public GameObject Ball;
    public int Score;
    public float time=0f,start_time;
    private void Awake()
    {
        instance = this;
        time = start_time;
    }
    private void Update()
    {
        if(time<=0f)
        {
            GameOver();
        }
        time -= 1f * Time.deltaTime;
    }
    public void GameOver()
    {
        UI_Manager.instance.filler.transform.parent.GetComponent<Animator>().enabled = false;
        UI_Manager.instance.GameOver_Panel.SetActive(true);
    }
    public void GeneratePost(bool left)
    {
        Vibration.Vibrate(20);
        time = start_time;
        UI_Manager.instance.IncreaseScore();
        if(left)
        {
            Post.transform.position = new Vector2(-2f, Random.Range(-2f, 1.75f));
            Post.transform.localScale = new Vector2(-Post.transform.localScale.x, Post.transform.localScale.y);
        }
        else
        {
            Post.transform.position = new Vector2(2f, Random.Range(2f, 1.75f));
            Post.transform.localScale = new Vector2(-Post.transform.localScale.x, Post.transform.localScale.y);
        }
    }
    public void GameReset()
    {

    }
   
}
