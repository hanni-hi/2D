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

    public GameObject NotePrefab;
    public float beatTempo;

    private void Awake()
    {
        Panels = GameObject.FindGameObjectsWithTag("Activator");

        PanelCount = Panels.Length;
        
        SpawnPoint = SpawnObject.transform;

        for(int i=0;i<PanelCount;i++)
        {
            panelTransform[i] = Panels[i].transform;
        }

        for(int i=0; i<NoteCount;i++)
        {

        GameObject spawnedNote=Instantiate(NotePrefab,SpawnPoint);
            spawnedNote.transform.SetParent(SpawnPoint);
            spawnedNote.SetActive(false);
            NoteQ.Enqueue(spawnedNote);

        }

        if(SceneManager.GetActiveScene().Equals("EasyGame"))
        {
            beatTempo = 99f;
        }
        else if(SceneManager.GetActiveScene().Equals("NormalGame"))
        {
            beatTempo = 136f;
        }
        else if(SceneManager.GetActiveScene().Equals("HardGame"))
        {
            beatTempo = 100f;
        }

     
    }


    private void Start()
    {

        StartCoroutine(StartNote());

    }

    private IEnumerator StartNote()
    {
        yield return new WaitForSeconds(5f);

        GameObject spawned= NoteQ.Dequeue();
        spawned.SetActive(true);

        spawned.transform.localPosition -= new Vector3(0f,beatTempo*Time.deltaTime,0f);


    }

    
    void Update()
    {
        
    }
}
