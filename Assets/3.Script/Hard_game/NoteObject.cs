using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canbePressed;
    public bool obtained;

    public KeyCode keytoPress;

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
        if(other.tag=="Activator")
        {
            Debug.Log("노트누름");
            canbePressed = true;
        }

        if (other.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeSelf)
        {
            if (other.tag == "Activator")
            {
                canbePressed = false;
                if (!obtained)
                {
                    Debug.Log("노트놎침");
                    if (GameManager.instance != null)
                    {
                        GameManager.instance.NoteMissed();
                    }
                }
                else
                {
                    Debug.LogError("GameManager instance is not set.");
                }
            }
        }
    }



}
