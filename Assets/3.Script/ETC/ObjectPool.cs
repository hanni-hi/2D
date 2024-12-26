using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> NoteQ = new Queue<GameObject>();

    private GameObject SpawnObject;
    private Transform SpawnPoint;
    private GameObject Note;
    private GameObject[] Notes = new GameObject[10];
    private GameObject[] Panels;
    private int PanelCount;
    private Transform[] panelTransform;
    private int NoteCount = 10;
    [SerializeField] private float SpeedMultiflier;

    public GameObject NotePrefab;
    public float beatTempo;
    //BPM : BPM을 초당 비트로 변환하고, 배율을 적용해서 노트 속도를 조정함  
    public float beatPerSecond;

    private GameManager.SceneType currentscene;

    private void Awake()
    {
        Debug.Log("ObjectPool 시작");


        SpawnObject = GameObject.Find("NoteManager") ;

        GameManager.SceneType? type = GameManager.instance.GetKeyByValue();
        if (type.HasValue)
        {
            currentscene = type.Value;      //현재 씬의 딕셔너리 key 값
        }
        else
        {
            Debug.Log("씬타입 못찾음 ");
        }


        Panels = GameObject.FindGameObjectsWithTag("Activator"); // 패널배열에 패널태그가진 오브젝트 담음
        if(Panels.Length==0)
            {
            Debug.Log("패널이 하나도 없음");
        }


        PanelCount = Panels.Length; //패널 갯수
        Debug.Log($"패널갯수 {PanelCount}");

        panelTransform = new Transform[PanelCount]; //패널트랜스폼 배열 배널갯수로 초기화 

        SpawnPoint = SpawnObject.transform;

        beatTempo = GameManager.instance.GetSceneData(currentscene).SceneBeat;

        beatPerSecond = beatTempo / 60f;

        SpeedMultiflier = GameManager.instance.GetSceneData(currentscene).SpeedMultifle;

        for(int i=0;i<PanelCount;i++)
        {
            panelTransform[i] = Panels[i].transform; //패널들 위치 정보 panelTransform 에 담음
            Debug.Log($"{panelTransform[i]}");
        }

        for(int i=0; i<NoteCount;i++)
        {

        GameObject spawnedNote=Instantiate(NotePrefab);
            spawnedNote.transform.position = SpawnPoint.position;
          // spawnedNote.transform.SetParent(SpawnPoint);
            spawnedNote.SetActive(false);
          //  Vector2 goalScale = spawnedNote.transform.localScale * 10f;
           // spawnedNote.transform.localScale = goalScale;
            NoteQ.Enqueue(spawnedNote);

        }
     
    }

    private void Start()
    {


    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        GameManager.instance.WhenStart += NoteStart;
        
    }

    private void OnDestroy()
    {
    GameManager.instance.WhenStart -= NoteStart;
        
    }

    void NoteStart()
    {
        StartCoroutine(StartNote());

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

            Debug.Log($"현재 큐에 있는 노트 { NoteQ.Count }");

            spawned.transform.position = SpawnPoint.position;

            float moveStartTime = Time.time;

            //노트의 이동
            while (Vector2.Distance(spawned.transform.position, goalPanel.position) > 0.1f)
            {
                spawned.transform.position = Vector2.MoveTowards(spawned.transform.position, goalPanel.position, speed * Time.deltaTime);

            yield return null;
            };

            spawned.transform.position = goalPanel.position;
            float moveTime = Time.time - moveStartTime;
        elapsedTime += moveTime;
        }
    }

    public void ReturnToPool(GameObject note)
    {
        note.SetActive(false);
        NoteQ.Enqueue(note);
    }
}
