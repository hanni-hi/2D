using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> NoteQ = new Queue<GameObject>();

    private GameObject SpawnObject;
    private Transform SpawnPoint;
    private int NoteCount = 10;

    public GameObject NotePrefab;

    private GameManager.SceneType currentscene;

    private void Start()
    {
        GameManager.instance.Initializing();

        SpawnObject = GameObject.FindObjectOfType<ObjectPool>().gameObject ;

        GameManager.SceneType type = GameManager.instance.GetKeyByValue();
            currentscene = type;      //현재 씬의 딕셔너리 key 값


        SpawnPoint = SpawnObject.transform;

        for(int i=0; i<NoteCount;i++)
        {

        GameObject spawnedNote=Instantiate(NotePrefab);
            spawnedNote.transform.position = SpawnPoint.position;
            spawnedNote.SetActive(false);
            NoteQ.Enqueue(spawnedNote);

        }
     
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
        float elapsedTime=Time.time;

        float sDuration = GameManager.instance.GetSceneData(currentscene).SceneDuration;

        while (elapsedTime+sDuration>Time.time)
        {
            yield return new WaitForSeconds(5f);

            GameObject spawned = NoteQ.Dequeue();
            spawned.SetActive(true);

            Debug.Log($"현재 큐에 있는 노트 { NoteQ.Count }");
        }
    }

    public void ReturnToPool(GameObject note)
    {
        note.transform.position = SpawnPoint.position;
        note.SetActive(false);
        NoteQ.Enqueue(note);
    }
}
