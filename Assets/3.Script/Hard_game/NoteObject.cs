using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canbePressed;
    public bool obtained;
    public string currentZone;

    public KeyCode keytoPress;

    private ObjectPool objPool;

    private void Awake()
    {
        objPool = FindObjectOfType<ObjectPool>();
    }

    private void Start()
    {
        canbePressed = false;
    }

    // Update is called once per frame
    void Update()
    {
    if(Input.GetKeyDown(keytoPress))
        {
            if(canbePressed)
            {
                gameObject.SetActive(false);
                obtained = true;

                if (GameManager.instance != null)
                {
                    GameManager.instance.NoteHit();
                }
                else
                {
                    Debug.LogError("GameManager instance is not set.");
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
            canbePressed = true;
        if (other.tag == "Perfect")
        {
            if (currentZone != "Perfect")
            {
                currentZone = "Perfect";

            }
        }
        else if (other.tag == "Great")
        {
            if (currentZone != "Perfect" && currentZone != "Great")
            {
                currentZone = "Great";
            }
        }
        else if (other.tag == "Activator")
        {
            if (currentZone == null)
            {
                currentZone = "Activator";
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
                if (!obtained)
                {
                    Debug.Log("³ëÆ®†ôÄ§");
                    if (GameManager.instance != null)
                    {
                        GameManager.instance.NoteMissed();
                    }
                }
            }
        }
    }



}
