using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.Initializing();
      //  GameManager.instance.theBS = GetComponent<NoteScroller>();
    }
}
