using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement; // 씬 전환을 위해 추가

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
        DontDestroyOnLoad(gameObject); // GameManager 자체는 유지

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
            Debug.LogWarning("VideoPlayer 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }

        ScoreText.text = "Score 0";
        currentCombo = 1;
        Combo.text = "Combo X " + currentCombo;

        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(true); // 시작 시 오브젝트를 활성화
        }

    }

    // Update is called once per frame
    void Update()
    {
     if(!isPlaying)
        {
                if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
                {
                    StartGame();
                }
        }
                if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
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
            objectToDeactivate.SetActive(false); // 클릭 시 오브젝트 비활성화
        }

        theVideo.Play();
    }

    private void ReturnToPreviousScene()
    {
        if (theVideo != null)
        {
            theVideo.Stop();
        }

        SceneManager.LoadScene("SongSelect"); // 이전 씬의 이름으로 교체
    }

    public void NoteHit()
    {
        // ComboThresholds 배열 범위 확인
        if (currentCombo - 1 < ComboThresholds.Length)
        {

            Combotracker++;

            // 콤보 트래커가 현재 콤보에 해당하는 임계값을 초과하면 콤보 증가
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
