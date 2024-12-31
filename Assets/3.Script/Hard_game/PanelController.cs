using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{

    private enum KeyToUse { q, w, e, r, t, y, u, i };
    [Space(10), Header("Panel�� ��ư�� �������ּ���.")]
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
            if (noteobj != null)
            {

            GameManager.instance.RegisterHit(noteobj.currentZone);
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

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Note")
        {
            if (noteobj !=null && noteobj == collision.GetComponent<NoteObject>())
            {
                noteobj = null;
            }
        }
    }

}
