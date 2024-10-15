using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �߰�

public class GameManager : MonoBehaviour
{
    public VideoPlayer theVideo;
    public AudioSource theMusic;

    public bool isPlaying;

    public NoteScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int ScoreperNote=10;
    public int ScoreGoodNote =5;
    public int ScorePerfectNote =15;

    public int currentCombo;
    public int Combotracker;
    public int[] ComboThresholds;


    public Text ScoreText;
    public Text Combo;

    public GameObject objectToDeactivate;

    private void Awake()
    {

        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // GameManager ��ü�� ����

    }

        // Start is called before the first frame update
        void Start()
    {

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

    // Update is called once per frame
    void Update()
    {
     if(!isPlaying)
        {
                if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
                {
                    StartGame();
                }
        }
                if (Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư Ŭ��
                {
                    ReturnToPreviousScene();
                }
           
    }

    private void StartGame()
    {
        isPlaying = true;
        theBS.isStarted = true;

        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false); // Ŭ�� �� ������Ʈ ��Ȱ��ȭ
        }

        theVideo.Play();
    }

    private void ReturnToPreviousScene()
    {
        if (theVideo != null)
        {
            theVideo.Stop();
        }

        SceneManager.LoadScene("SongSelect"); // ���� ���� �̸����� ��ü
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
