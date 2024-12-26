using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.Initializing();
      //  GameManager.instance.theBS = GetComponent<NoteScroller>();
    }

}
