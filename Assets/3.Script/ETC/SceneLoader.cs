using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Ȩ��ư�� ������ ������������ �̵��մϴ�.
public class SceneLoader : MonoBehaviour
{
    public string mainPageScene = "Main";

    public void LoadMainPage()
    {
        // SceneManager.LoadScene�� ����Ͽ� ���� �ε��մϴ�.
        SceneManager.LoadScene(mainPageScene);
    }

}
