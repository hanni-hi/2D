using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//홈버튼을 누르면 메인페이지로 이동합니다.
public class SceneLoader : MonoBehaviour
{
    public string mainPageScene = "Main";

    public void LoadMainPage()
    {
        // SceneManager.LoadScene을 사용하여 씬을 로드합니다.
        SceneManager.LoadScene(mainPageScene);
    }

}
