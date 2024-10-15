using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
    //GameSceneAutoReturn: ���� ������ ������ �ð��� ������ �ڵ����� ���� ������ ���ư��ϴ�.
    void Start()
    {
        // ���� ���� ���� �ð� ��������
        string previousScene = PlayerPrefs.GetString("PreviousScene", "SongSelect");
        float sceneDuration = PlayerPrefs.GetFloat("SceneDuration", 164f); // �⺻�� 60��

        // �ڷ�ƾ�� ���� ���� �ð� �Ŀ� �� ��ȯ
        StartCoroutine(ReturnToPreviousScene(previousScene, sceneDuration));
    }

    private IEnumerator ReturnToPreviousScene(string previousScene, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(previousScene);
    }
}
