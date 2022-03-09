using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    [Header("TimerColors")]
    public Color Half, Danger,Normal;
    [Header("Images")]
    public Image Ball_Image;
    public Image Ball_Border_Image;
    public Text TouchToStart;
    public Image filler;
    [Header("Panels")]
    public GameObject Menu_Panel;
    public GameObject Play_Panel;
    public GameObject GameOver_Panel;
    [Header("Texts")]
    public Text Score_Text;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        filler.fillAmount = GameManager.instance.time / GameManager.instance.start_time;
        if(filler.fillAmount <= 0.50f)
        {
            filler.color = Half;
        }
        if(filler.fillAmount <= 0.35f)
        {
            filler.color = Danger;
            filler.transform.parent.GetComponent<Animator>().enabled = true;
        }
        if(filler.fillAmount >0.50f)
        {
            filler.color = Normal;
            filler.transform.parent.GetComponent<Animator>().enabled = false;
        }
    }
    private void Start()
    {
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
        Menu_Panel.SetActive(false);
        Play_Panel.SetActive(true);
        GameManager.instance.Post.gameObject.SetActive(true);
        GameManager.instance.Ball.gameObject.SetActive(true);
    }
    public void handle_onClickRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //GameOver_Panel.SetActive(false);
        //GameManager.instance.GameReset();
    }
    public void IncreaseScore()
    {
        GameManager.instance.Score++;
        Score_Text.text = GameManager.instance.Score.ToString();
    }
}

