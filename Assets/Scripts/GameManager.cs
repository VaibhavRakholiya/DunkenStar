using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RDG;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform Shop;
    // Status of Game
    public enum status { Play,Pause,Over};
    public status GameStatus;
    // ------------------
    public Transform Post;
    public Player Ball;
    public int Score;
    public float time=0f,start_time;
    public int TotalDunks;
    private void Awake()
    {
        instance = this;
        GameStatus = status.Over;
        time = start_time;
        if(!PlayerPrefs.HasKey("TotalDunks"))
        {
            PlayerPrefs.SetInt("0", 1);
            PlayerPrefs.SetInt("TotalBought", 1);
            PlayerPrefs.SetInt("TotalDunks", 0);
            PlayerPrefs.SetInt("BestScore", 0);
        }
    }
    private void Start()
    {
        Ball.GetComponent<SpriteRenderer>().sprite = getCurrentBall();
    }
    private void Update()
    {
        if(time<=0f && GameStatus==status.Play)
        {
            GameOver();
        }
        if(GameStatus == status.Play)
        time -= 1f * Time.deltaTime;
    }
    public void GameOver()
    {
        Vibration.Vibrate(75);
        Post.gameObject.SetActive(false);
        Ball.gameObject.SetActive(false);
        GameStatus = status.Over;
        UI_Manager.instance.Continue_Button_Image.GetComponent<Button>().image.sprite = getCurrentBall();
        UI_Manager.instance.filler.transform.parent.GetComponent<Animator>().enabled = false;
        UI_Manager.instance.GameOver_Panel.SetActive(true);
    }
    private void IncreaseAchievements()
    {
        PlayerPrefs.SetInt("TotalDunks", PlayerPrefs.GetInt("TotalDunks")+1);
        switch (Score)
        {
            case 10: UnlockAcheivment(1);break;
            case 25: UnlockAcheivment(2);break;
            case 30: UnlockAcheivment(3);break;
            case 40: UnlockAcheivment(12);break;
            case 60: UnlockAcheivment(13);break;
            case 75: UnlockAcheivment(14);break;
            case 100: UnlockAcheivment(15);break;
            default:
                break;
        }
        switch (PlayerPrefs.GetInt("TotalDunks"))
        {
            case 10: UnlockAcheivment(4);break;
            case 50: UnlockAcheivment(5);break;
            case 75: UnlockAcheivment(6);break;
            case 100: UnlockAcheivment(7);break;
            case 150: UnlockAcheivment(8);break;
            case 200: UnlockAcheivment(9);break;
            case 350: UnlockAcheivment(10);break;
            case 300: UnlockAcheivment(11);break;
            default:
                break;
        }
    }
    private void UnlockAcheivment(int index)
    {
        PlayerPrefs.SetInt(index.ToString(), 1);
    }
    public void GeneratePost(bool left)
    {
        Vibration.Vibrate(30);
        time = start_time;
        if(start_time>10f)
        {
            if(start_time<20f)
            {
                start_time -= 0.50f;
            }
            else
            {
                start_time -= 1f;
            }
        }
        IncreaseAchievements();
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
        Post.gameObject.SetActive(true);
        Ball.gameObject.SetActive(true);
        UI_Manager.instance.GameOver_Panel.SetActive(false);
        time = start_time;
        GameStatus = status.Play;
    }
    public Sprite getCurrentBall()
    {
        return Shop.transform.GetChild(PlayerPrefs.GetInt("CurrentBall")).GetChild(0).GetComponent<Image>().sprite;
    }
    public void GamePause()
    {
        //Ball.gameObject.SetActive(false);
        //Post.gameObject.SetActive(false);
        Ball.GetComponent<SpriteRenderer>().color = UI_Manager.instance.Dark;
        Post.GetComponent<SpriteRenderer>().color = UI_Manager.instance.Dark;
        for (int i = 0; i < 4; i++)
        {
            if (i == 0 || i==1)
                Post.transform.GetChild(i).GetComponent<SpriteRenderer>().color = UI_Manager.instance.Dark_Red;
            if (i== 2 || i == 3)
                Post.transform.GetChild(i).GetComponent<SpriteRenderer>().color = UI_Manager.instance.Dark;

        }
        GameStatus = status.Pause;
        UI_Manager.instance.Pause_Panel.SetActive(true);
    }
}
