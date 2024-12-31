    using System;
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

        public NoteObject noteOBJ;

        public static GameManager instance;

        public int currentScore=0;

        public int currentCombo;
        public int Combotracker=0;
        public int[] ComboThresholds = new int[4]{4,8,12,16};

        public Text ScoreText;
        public Text Combo;
    
        public GameObject objectToDeactivate;

        public enum SceneType { EGame, NGame, HGame }; //��������,�븻����,�ϵ����
        public Dictionary<SceneType, SceneData> SceneDictionary;

        public Action WhenStart;

        private SceneType thisType;
        private string currentSceneName;
           private bool iscurrentSceneInDictionary;

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
                { SceneType.EGame, new SceneData("EasyGame", 99f, 164f,1f) },
                { SceneType.NGame, new SceneData("NormalGame",136f,128f,2f) },
                { SceneType.HGame, new SceneData("HardGame",100f,158f,3f)}

            };

            UpdateSceneStatus();
        }

        void Update()
        {

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

        public SceneType GetKeyByValue()
        {
            foreach (var val in SceneDictionary)
            {

                if (val.Value.SceneName==currentSceneName)
                {
                Debug.Log($"�� �̸� Ȯ��: {val.Value.SceneName}");
                    return val.Key;
                }
            }

            throw new System.Exception("�� �̸��� ����...?");
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
            WhenStart?.Invoke();

            isPlaying = true;
           // theBS.isStarted = true;

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

        public void OnSceneLoaded(Scene scene,LoadSceneMode mode)
        {
            currentSceneName = scene.name;

            if (currentSceneName.Contains("Game"))
            {
            UpdateSceneStatus();

                GameObject Score = GameObject.Find("Score");
                ScoreText = Score.GetComponent<Text>();

                GameObject combo = GameObject.Find("Combo");
                Combo = combo.GetComponent<Text>();
            }

        }

        private void UpdateSceneStatus()
        {
           iscurrentSceneInDictionary = SceneDictionary.Values.Any(sceneData=>sceneData.SceneName==currentSceneName);
        //linq�޼��� �� �ϳ��� any�� ����Ͽ� ��ųʸ� ������ ��ȸ�Ͽ� ���� �� �̸��� ������ �ִ��� Ȯ����
        //���� ����ɶ����� Ȯ���ؾ��ϹǷ� SceneLoaded �̺�Ʈ�� �����ص�

    }

    public void RegisterHit(string zone)
    {
        int scoreToAdd = 
            zone switch
        {
            "Perfect"=>15,
            "Great"=>10,
            "Activator"=>5,
            _=>0
        };

        UpdateCombo();
        currentScore += scoreToAdd * currentCombo;
        ScoreText.text = $"Score : {currentScore}";

    }

    private void UpdateCombo()
    {
        Combotracker++;
        if(Combotracker >=ComboThresholds[Mathf.Min(currentCombo-1,ComboThresholds.Length-1)])
        {
            Combotracker = 0;
            currentCombo++;

        }
        Combo.text = $"Combo : {currentCombo}";

    }

    public void RegisterMiss()
    {
        currentCombo = 1;
        Combotracker = 0;
        Combo.text = "Combo : 1";

    }

}

    public class SceneData
    {
        //�б����� : �����͵� ����� �ʿ䰡 ���� ������ 
        public string SceneName { get; } // {get; private set;} �� �޸� Ŭ���� ���ο����� ���� ������ �� ����
        public float SceneBeat { get; }
        public float SceneDuration { get; }
        public float SpeedMultifle { get; }

        public SceneData(string sceneName, float sceneBeat,float sceneDuration,float speedMultifle)
        {
            this.SceneName = sceneName;
            this.SceneBeat = sceneBeat;
            this.SceneDuration = sceneDuration;
            this.SpeedMultifle = speedMultifle;

        }

    }
