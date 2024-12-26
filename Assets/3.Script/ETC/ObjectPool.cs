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
    //BPM : BPM�� �ʴ� ��Ʈ�� ��ȯ�ϰ�, ������ �����ؼ� ��Ʈ �ӵ��� ������  
    public float beatPerSecond;

    private GameManager.SceneType currentscene;

    private void Awake()
    {
        Debug.Log("ObjectPool ����");


        SpawnObject = GameObject.Find("NoteManager") ;

        GameManager.SceneType? type = GameManager.instance.GetKeyByValue();
        if (type.HasValue)
        {
            currentscene = type.Value;      //���� ���� ��ųʸ� key ��
        }
        else
        {
            Debug.Log("��Ÿ�� ��ã�� ");
        }


        Panels = GameObject.FindGameObjectsWithTag("Activator"); // �гι迭�� �г��±װ��� ������Ʈ ����
        if(Panels.Length==0)
            {
            Debug.Log("�г��� �ϳ��� ����");
        }


        PanelCount = Panels.Length; //�г� ����
        Debug.Log($"�гΰ��� {PanelCount}");

        panelTransform = new Transform[PanelCount]; //�г�Ʈ������ �迭 ��ΰ����� �ʱ�ȭ 

        SpawnPoint = SpawnObject.transform;

        beatTempo = GameManager.instance.GetSceneData(currentscene).SceneBeat;

        beatPerSecond = beatTempo / 60f;

        SpeedMultiflier = GameManager.instance.GetSceneData(currentscene).SpeedMultifle;

        for(int i=0;i<PanelCount;i++)
        {
            panelTransform[i] = Panels[i].transform; //�гε� ��ġ ���� panelTransform �� ����
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
            Transform goalPanel = panelTransform[rand];  //��  ���� �����������

            float speed = beatPerSecond * SpeedMultiflier;

            GameObject spawned = NoteQ.Dequeue();
            spawned.SetActive(true);

            Debug.Log($"���� ť�� �ִ� ��Ʈ { NoteQ.Count }");

            spawned.transform.position = SpawnPoint.position;

            float moveStartTime = Time.time;

            //��Ʈ�� �̵�
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
