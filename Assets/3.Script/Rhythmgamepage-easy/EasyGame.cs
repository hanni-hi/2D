using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.Initializing();
        GameManager.instance.theBS = GetComponent<NoteScroller>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            GameManager.instance.StartGame();
        }
        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
        {
            GameManager.instance.ReturnToPreviousScene();
        }

    }
}
