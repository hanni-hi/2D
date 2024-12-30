using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private GameObject[] Panels;
    private int PanelCount;
    private Transform[] PanelTransforms;
    private Rigidbody2D rb;
    private GameManager.SceneType currentsceneType;
    private float SpeedMultiflier;
    private Vector2 direction;
    private float speed;

    public bool canbePressed;
    public bool obtained;
    public string currentZone;
    public float beatTempo;
    //BPM : BPM을 초당 비트로 변환하고, 배율을 적용해서 노트 속도를 조정함  
    public float beatPerSecond;

    public ObjectPool objPool;

    private void Awake()
    {
        objPool = FindObjectOfType<ObjectPool>();
   // }
   //
   // private void Start()
   // {
        GameManager.SceneType? type = GameManager.instance.GetKeyByValue();
        if(type.HasValue)
        {
            currentsceneType = type.Value;
        }
        else
        {
            Debug.Log("씬타입 못찾음");
        }

        canbePressed = false;

        Panels = GameObject.FindGameObjectsWithTag("Activator"); // 패널배열에 패널태그가진 오브젝트 담음

        PanelCount = Panels.Length;

        PanelTransforms = new Transform[PanelCount]; //패널트랜스폼 배열 배널갯수로 초기화

        for(int i=0; i<PanelCount; i++)
        {
            PanelTransforms[i] = Panels[i].transform; //패널들 위치 정보 panelTransform 에 담음
        }

        foreach (var panel in Panels)
        {
            Debug.Log($"Panel Name: {panel.name}, Position: {panel.transform.position}");
        }

        beatTempo = GameManager.instance.GetSceneData(currentsceneType).SceneBeat;
        beatPerSecond = beatTempo / 60f;

        SpeedMultiflier = GameManager.instance.GetSceneData(currentsceneType).SpeedMultifle;

        speed = beatPerSecond * SpeedMultiflier;


        int rand = Random.Range(0,PanelCount);
        Transform goalPanel = PanelTransforms[rand];

        rb = gameObject.GetComponent<Rigidbody2D>();

        direction = (goalPanel.position-gameObject.transform.position).normalized;
    }

    void Update()
    {
        if(gameObject.activeSelf)
        {
            rb.velocity = direction * speed;
            Debug.Log($"Direction: {direction}");

        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      //      canbePressed = true;
        if (other.tag == "Perfect")
        {
            if (currentZone != "Perfect")
            {
                currentZone = "Perfect";
        Debug.Log($"지금 위치한 존은~ {currentZone}");

            }
        }
        else if (other.tag == "Great")
        {
            if (currentZone != "Perfect" && currentZone != "Great")
            {
                currentZone = "Great";
        Debug.Log($"지금 위치한 존은~ {currentZone}");
            }
        }
        else if (other.tag == "Activator")
        {
            if (currentZone == null)
            {
                currentZone = "Activator";
        Debug.Log($"지금 위치한 존은~ {currentZone}");
            }
        }



        if (other.tag == "Boundary")
        {
            objPool.ReturnToPool(gameObject);
            currentZone = null;
        }



    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeSelf)
        {
            if (other.tag == "Activator")
            {
                canbePressed = false;
                currentZone = null;
                obtained = false;

                if (!obtained)
                {
                    Debug.Log("노트놎침");
                    if (GameManager.instance != null)
                    {
                        GameManager.instance.NoteMissed();
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        obtained = false;
    }


}
