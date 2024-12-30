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
    //BPM : BPM�� �ʴ� ��Ʈ�� ��ȯ�ϰ�, ������ �����ؼ� ��Ʈ �ӵ��� ������  
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
            Debug.Log("��Ÿ�� ��ã��");
        }

        canbePressed = false;

        Panels = GameObject.FindGameObjectsWithTag("Activator"); // �гι迭�� �г��±װ��� ������Ʈ ����

        PanelCount = Panels.Length;

        PanelTransforms = new Transform[PanelCount]; //�г�Ʈ������ �迭 ��ΰ����� �ʱ�ȭ

        for(int i=0; i<PanelCount; i++)
        {
            PanelTransforms[i] = Panels[i].transform; //�гε� ��ġ ���� panelTransform �� ����
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
        Debug.Log($"���� ��ġ�� ����~ {currentZone}");

            }
        }
        else if (other.tag == "Great")
        {
            if (currentZone != "Perfect" && currentZone != "Great")
            {
                currentZone = "Great";
        Debug.Log($"���� ��ġ�� ����~ {currentZone}");
            }
        }
        else if (other.tag == "Activator")
        {
            if (currentZone == null)
            {
                currentZone = "Activator";
        Debug.Log($"���� ��ġ�� ����~ {currentZone}");
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
                    Debug.Log("��Ʈ��ħ");
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
