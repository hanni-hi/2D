using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    //GameSceneAutoReturn: 게임 씬에서 설정된 시간이 지나면 자동으로 이전 씬으로 돌아갑니다.
    void Start()
    {
        // 이전 씬과 지속 시간 가져오기
        string previousScene = PlayerPrefs.GetString("PreviousScene", "SongSelect");
        float sceneDuration = PlayerPrefs.GetFloat("SceneDuration", 164f); // 기본값 60초

        // 코루틴을 통해 일정 시간 후에 씬 전환
        StartCoroutine(ReturnToPreviousScene(previousScene, sceneDuration));
    }

    private IEnumerator ReturnToPreviousScene(string previousScene, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(previousScene);
    }
}
