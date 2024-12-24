using System.Linq;
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

    public enum SceneType { EGame, NGame, HGame };
    public Dictionary<SceneType, SceneData> SceneDictionary;


    private SceneType thisType;
    private string currentSceneName;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // GameManager ��ü�� ����

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

        void Start()
    {
        isPlaying = false;


        SceneDictionary = new Dictionary<SceneType, SceneData>
        {
            { SceneType.EGame, new SceneData("EasyGame", 99f, 164f) },
            { SceneType.NGame, new SceneData("NormalGame",136f,128f) },
            { SceneType.HGame,new SceneData("HardGame",100f,158f)}

        };

    }

    void Update()
    {
        bool iscurrentSceneInDictionary = GameManager.instance.SceneDictionary.Values
            .Any(sceneData=>sceneData.SceneName==currentSceneName);
        //linq�޼��� �� �ϳ��� any�� ����Ͽ� ��ųʸ� ������ ��ȸ�Ͽ� ���� �� �̸��� ������ �ִ��� Ȯ����


        if (iscurrentSceneInDictionary)
        {

            if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
            {
                StartGame();

            }
            if (Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư Ŭ��
            {
                ReturnToPreviousScene();
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public SceneData  GetSceneData(SceneType sceneType)
    {
        if(SceneDictionary.TryGetValue(sceneType,out SceneData data))
        {
            return data;
        }
        return null;

    }

    public SceneType? GetKeyByValue()
    {
        foreach(var val in SceneDictionary)
        {
            if(val.Value.SceneName==currentSceneName)
            {
                return val.Key;
            }
        }
        return null;
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

  //  public IEnumerator Timer()
  //  {
  //     // float sDuration=SceneDictionary
  //
  //      yield return null;
  //  }

    public void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        currentSceneName = scene.name;
    }

}

public class SceneData
{
    //�б����� : �����͵� ����� �ʿ䰡 ���� ������ 
    public string SceneName { get; private set; }
    public float SceneBeat { get; private set; }
    public float SceneDuration { get; private set; }

    public SceneData(string sceneName, float sceneBeat,float sceneDuration)
    {
        this.SceneName = sceneName;
        this.SceneBeat = sceneBeat;
        this.SceneDuration = sceneDuration;

    }

}
