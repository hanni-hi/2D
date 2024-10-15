using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotchaLoader : MonoBehaviour
{
    public string gotchaPageScene = "Gotcha";

    public void LoadGotchaPage()
    {
        // SceneManager.LoadScene을 사용하여 씬을 로드합니다.
        SceneManager.LoadScene(gotchaPageScene);
    }

}
