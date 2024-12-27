using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{

    private enum KeyToUse { q, w, e, r, t, y, u, i };
    [Space(10), Header("Panel의 버튼을 선택해주세요.")]
    [SerializeField]
    private KeyToUse selectedKey;
    [Space(10)]

    public Sprite defaultImage;
    public Sprite pressedImage;
    
    private SpriteRenderer theSR;
    private string keyString;
    private NoteObject noteobj;
         

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        keyString = selectedKey.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyString))
        {
            theSR.sprite = pressedImage;
            if (noteobj != null && noteobj.canbePressed)
            {

                noteobj.obtained = true;
                noteobj.canbePressed = false;

                GameManager.instance.NoteHit();

                noteobj.objPool.ReturnToPool(noteobj.gameObject);

                noteobj = null;
            }
        }
            if (Input.GetKeyUp(keyString))
            {
                theSR.sprite = defaultImage;

            }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Note")
        {
            noteobj = collision.GetComponent<NoteObject>();
            noteobj.canbePressed = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Note")
        {
            if (noteobj !=null && noteobj == collision.GetComponent<NoteObject>())
            {
                noteobj.canbePressed = false;
                noteobj = null;
            }
        }
    }

}
