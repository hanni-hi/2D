using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Ȩ��ư�� ������ ������������ �̵��մϴ�.
public class HomeLoader_gotcha : MonoBehaviour
{
    public string got_mainPageScene = "Main";

    public void LoadMainPage()
    {
        // SceneManager.LoadScene�� ����Ͽ� ���� �ε��մϴ�.
        SceneManager.LoadScene(got_mainPageScene);
    }

}
