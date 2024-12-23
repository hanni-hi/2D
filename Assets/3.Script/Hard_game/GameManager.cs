using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �߰�

public class GameManager : MonoBehaviour
{
    public VideoPlayer theVideo;

    public bool isPlaying;

    public NoteScroller theBS;

    public static GameManager instance;

    public int currentScore=0;
    public int ScoreperNote = 10;
    public int ScoreGoodNote = 5;
    public int ScoreGreatNote = 10;
    public int ScorePerfectNote = 15;

    public int currentCombo;
    public int Combotracker=0;
    public int[] ComboThresholds = new int[4]{4,8,12,16};


    public Text ScoreText;
    public Text Combo;
    
    public GameObject objectToDeactivate;

   // public string playerName;


    private void Awake()
    {

        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // GameManager ��ü�� ����

    }

        void Start()
    {
        isPlaying = false;

    }

    void Update()
    {
           
    }

    public void Initializing()
    {

        isPlaying = false;

        GameObject scoreText = GameObject.Find("Score");
        ScoreText = scoreText.GetComponent<Text>();

        GameObject comboText = GameObject.Find("Combo");
        Combo = comboText.GetComponent<Text>();

        objectToDeactivate = GameObject.FindWithTag("Deactivate");

       // theBS = GetComponent<NoteScroller>();
        GameObject videoObject = GameObject.FindWithTag("VideoPlayer");
        if (videoObject != null)
        {
            theVideo = videoObject.GetComponent<VideoPlayer>();
            if (theVideo != null)
            {
                theVideo.Stop();
            }
        }
        else
        {
            Debug.LogWarning("VideoPlayer �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�.");
        }

        ScoreText.text = "Score 0";
        currentCombo = 1;
        Combo.text = "Combo X " + currentCombo;

        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(true); // ���� �� ������Ʈ�� Ȱ��ȭ
        }
        



    }


    public void StartGame()
    {
        isPlaying = true;
        theBS.isStarted = true;

        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false); // Ŭ�� �� ������Ʈ ��Ȱ��ȭ
        }

        theVideo.Play();
    }

    public void ReturnToPreviousScene()
    {
        if (theVideo != null)
        {
            theVideo.Stop();
        }

        SceneManager.LoadScene("SongSelect"); // ���� ���� �̸����� ��ü

        isPlaying = false;
    }

    public void NoteHit()
    {
        // ComboThresholds �迭 ���� Ȯ��
        if (currentCombo - 1 < ComboThresholds.Length)
        {

            Combotracker++;

            // �޺� Ʈ��Ŀ�� ���� �޺��� �ش��ϴ� �Ӱ谪�� �ʰ��ϸ� �޺� ����
            if (ComboThresholds[currentCombo - 1] <= Combotracker)
            {
                Combotracker = 0;
                currentCombo++;

            }
        }
        Combo.text = "Combo X "+ currentCombo;

        currentScore += ScoreperNote*currentCombo;
        ScoreText.text = "Score " + currentScore;

    }    

    public void NoteMissed()
    {
        currentCombo = 1;
        Combotracker = 0;

        Combo.text = "Combo X " + currentCombo;

    }
}
