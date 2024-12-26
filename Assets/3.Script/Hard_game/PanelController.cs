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

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        keyString = selectedKey.ToString();
    }

    void Update()
    {
        if(Input.GetKeyDown(keyString))
        {
            theSR.sprite = pressedImage;

        }
       
        if(Input.GetKeyUp(keyString))
        {
            theSR.sprite = defaultImage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      //  canbePressed = true;
      //
      //
      //  if(collision.tag=="Perfect")
      //  {
      //      if(currentZone != "Perfect")
      //      {
      //          currentZone = "Perfect";
      //
      //      }
      //  }
      //  else if(collision.tag=="Great")
      //  {
      //      if(currentZone!="Perfect"&&currentZone!="Great")
      //      {
      //          currentZone = "Great";
      //      }
      //  }
      //  else if(collision.tag=="Activator")
      //  {
      //      if(currentZone==null)
      //      {
      //          currentZone = "Activator";
      //      }
      //  }
      //
      //
      //
      //  if(collision.tag== "Boundary")
      //  {
      //      objPool.ReturnToPool(gameObject);
      //      currentZone = null;
      //  }

    }

   // private void OnTriggerExit2D(Collider2D collision)
   // {
   //     if(collision.tag== "Activator")
   //     {
   //     canbePressed = false;
   //         currentZone = null;
   //     }
   // }

}
