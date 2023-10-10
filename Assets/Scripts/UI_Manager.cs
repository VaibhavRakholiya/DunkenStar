using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    public GridLayoutGroup ShopGrid;
    [Header("TimerColors")]
    public Color Half, Danger,Normal,Dark,White,Dark_Red,Red;
    [Header("Images")]
    public Image Ball_Image;
    public Image Ball_Border_Image;
    public Text TouchToStart;
    public Image filler;
    public Image Title_ball;
    [Header("FillerBars")]
    public Image TotalBought_Bar;
    [Header("Panels")]
    public GameObject Menu_Panel;
    public GameObject Play_Panel;
    public GameObject GameOver_Panel;
    public GameObject Shop_Panel;
    public GameObject Click_Panel;
    public GameObject Continue_Button_Image;
    public GameObject Pause_Panel;
    [Header("Texts")]
    public Text Score_Text;
    public Text Total_Bought;
    public Text BestScore_Text;
    public Text BestScore_GameOver_Text;

    private void Awake()
    {
        instance = this;
        //ShopGrid.cellSize =new Vector2(Screen.height * 0.075f,Screen.height*0.075f);
    }
    private void Update()
    {
        filler.fillAmount = GameManager.instance.time / GameManager.instance.start_time;
        if(filler.fillAmount <= 0.50f)
        {
            filler.color = Half;
            filler.transform.parent.GetComponent<Animator>().SetBool("animate1", true);
            filler.transform.parent.GetComponent<Animator>().SetBool("animate2", false);
        }
        if (filler.fillAmount <= 0.35f)
        {
            filler.color = Danger;
            filler.transform.parent.GetComponent<Animator>().SetBool("animate1", false);
            filler.transform.parent.GetComponent<Animator>().SetBool("animate2", true);
        }
        if (filler.fillAmount >=0.50f)
        {
            filler.color = Normal;
            filler.transform.parent.GetComponent<Animator>().SetBool("animate1", false);
            filler.transform.parent.GetComponent<Animator>().SetBool("animate2", false);
        }
        if(Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.GameStatus == GameManager.status.Play)
        {
            if(!Pause_Panel.activeInHierarchy)
            {
                GameManager.instance.GamePause();
            }
            else
            {
                handle_onClick_Play();
            }
        }
    }
    private void Start()
    {
        BestScore_Text.text = "BEST : " + PlayerPrefs.GetInt("BestScore").ToString() ;
        // Changing the skin of title image.
        Ball_Image.sprite = GameManager.instance.getCurrentBall();
        StartCoroutine(Ball_Animation());
        StartCoroutine(Text_Animation());
    }
    public IEnumerator Ball_Animation()
    {
        Ball_Image.GetComponent<Animator>().SetBool("animate", true);
        yield return new WaitForSeconds(0.25f);
        Ball_Border_Image.GetComponent<Animator>().SetBool("animate", true);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Ball_Animation());
    }
    private IEnumerator Text_Animation()
    {
        TouchToStart.GetComponent<Animator>().SetBool("animate", true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(Text_Animation());
    }
    public void onClickPlayButton()
    {
        GameManager.instance.GameStatus = GameManager.status.Play;
        Menu_Panel.SetActive(false);
        Play_Panel.SetActive(true);
        GameManager.instance.Post.gameObject.SetActive(true);
        GameManager.instance.Ball.gameObject.SetActive(true);
    }
    public void handle_onClickRetry()
    {
        // AdManager.instance.ShowFullScreenAd(); // Ad Placement.
        GameManager.instance.GameRestart();
        //GameOver_Panel.SetActive(false);
        //GameManager.instance.GameReset();
    }
    public void handle_onClickShopBackButton()
    {
        Shop_Panel.SetActive(false);
        Menu_Panel.SetActive(true);
        // Changing the skin of title image.
        Ball_Image.sprite = GameManager.instance.getCurrentBall();
    }
    public void IncreaseScore()
    {
        GameManager.instance.Score++;
        if (PlayerPrefs.GetInt("BestScore") < GameManager.instance.Score)
            PlayerPrefs.SetInt("BestScore", GameManager.instance.Score);
        Score_Text.text = GameManager.instance.Score.ToString();
        PlayerPrefs.SetInt("TotalDunks", PlayerPrefs.GetInt("TotalDunks") + 1);
    }
    public void handle_onClick_ShopButton()
    {
        Total_Bought.text = PlayerPrefs.GetInt("TotalBought").ToString() + "/15";
        TotalBought_Bar.fillAmount = PlayerPrefs.GetInt("TotalBought") / 15f;
        Shop_Panel.SetActive(true);
        Menu_Panel.SetActive(false);
    }

    public void handle_onClick_CloseClickPanelButton()
    {
        Click_Panel.GetComponent<Animator>().SetBool("animate", true);
        Invoke("close_panel", 0.5f);
    }
    private void close_panel()
    {
        Click_Panel.SetActive(false);
        Click_Panel.transform.parent.gameObject.SetActive(false);
    }
    public void handle_onClick_ContinueButton()
    {
        //GameManager.instance.GameReset();
        // AdManager.instance.ShowRewardedAd(0); // Ad Placement
    }
    public void handle_onClick_RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.sports.basketball.games.dunkenstar");
    }
    public void handle_onClick_Play()
    {
        //GameManager.instance.Post.gameObject.SetActive(true);
        //GameManager.instance.Ball.gameObject.SetActive(true);
        GameManager.instance.Ball.GetComponent<SpriteRenderer>().color = White;
        GameManager.instance.Post.GetComponent<SpriteRenderer>().color = White;
        for (int i = 0; i < 4; i++)
        {
            if (i == 0 || i == 1)
                GameManager.instance.Post.transform.GetChild(i).GetComponent<SpriteRenderer>().color = Red;
            if (i == 2 || i == 3)
                GameManager.instance.Post.transform.GetChild(i).GetComponent<SpriteRenderer>().color = White;

        }
        Pause_Panel.SetActive(false);
        GameManager.instance.GameStatus = GameManager.status.Play;
    }
    public void handle_onClick_HomeButton()
    {
        GameManager.instance.GameRestart();
    }
    public void handle_onClick_PauseButton()
    {
        GameManager.instance.GamePause();
    }
}

