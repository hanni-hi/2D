using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> NoteQ = new Queue<GameObject>();

    private GameObject SpawnObject = GameObject.Find("Image");
    private Transform SpawnPoint;
    private GameObject Note;
    private GameObject[] Notes = new GameObject[10];
    private GameObject[] Panels;
    private int PanelCount;
    private Transform[] panelTransform;
    private int NoteCount = 10;
    [SerializeField] private float SpeedMultiflier=1f;

    public GameObject NotePrefab;
    public float beatTempo;
    //BPM : BPM을 초당 비트로 변환하고, 배율을 적용해서 노트 속도를 조정함  
    public float beatPerSecond;

    private GameManager.SceneType currentscene;

    private void Awake()
    {
        GameManager.SceneType? type = GameManager.instance.GetKeyByValue();
        currentscene = type.Value;      //현재 씬의 딕셔너리 key 값

        Panels = GameObject.FindGameObjectsWithTag("Activator");

        PanelCount = Panels.Length; //패널 갯수

        panelTransform = new Transform[PanelCount];

        SpawnPoint = SpawnObject.transform;

        beatTempo = GameManager.instance.GetSceneData(currentscene).SceneBeat;

        beatPerSecond = beatTempo / 60f;

        for(int i=0;i<PanelCount;i++)
        {
            panelTransform[i] = Panels[i].transform; //패널들 위치 정보 panelTransform 에 담음
        }

        for(int i=0; i<NoteCount;i++)
        {

        GameObject spawnedNote=Instantiate(NotePrefab,SpawnPoint);
            spawnedNote.transform.SetParent(SpawnPoint);
            spawnedNote.SetActive(false);
            NoteQ.Enqueue(spawnedNote);

        }
     
    }


    private void Start()
    {

        StartCoroutine(StartNote());

    }
    void Update()
    {
        
    }

    private IEnumerator StartNote()
    {
        float elapsedTime=0;

        float sDuration = GameManager.instance.GetSceneData(currentscene).SceneDuration;

        while (elapsedTime<sDuration)
        {
            yield return new WaitForSeconds(5f);

            int rand = Random.Range(0, PanelCount);
            Transform goalPanel = panelTransform[rand];  //★  여기 오류생기는중

            float speed = beatPerSecond * SpeedMultiflier;

            GameObject spawned = NoteQ.Dequeue();
            spawned.SetActive(true);

            spawned.transform.position = SpawnPoint.position;

            float moveStartTime = Time.time;

            //노트의 이동
            while (Vector2.Distance(spawned.transform.position, goalPanel.position) > 1f)
            {
                spawned.transform.position = Vector2.MoveTowards(spawned.transform.position, goalPanel.position, speed * Time.deltaTime);

            yield return null;
            };

            spawned.transform.position = goalPanel.position;
            float moveTime = Time.time - moveStartTime;
        elapsedTime += moveTime;
        }
    }
}
