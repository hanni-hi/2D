using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotchaLoader : MonoBehaviour
{
    public string gotchaPageScene = "Gotcha";

    public void LoadGotchaPage()
    {
        // SceneManager.LoadScene�� ����Ͽ� ���� �ε��մϴ�.
        SceneManager.LoadScene(gotchaPageScene);
    }

}
