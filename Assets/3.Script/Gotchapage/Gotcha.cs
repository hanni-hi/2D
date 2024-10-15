using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//스카우트 버튼을 클릭하면 가챠메인페이지로 이동합니다.
public class Gotcha : MonoBehaviour
{
    [SerializeField] private Button Scout_button;

    void Awake()
    {
        if(Scout_button != null)
        {
            Debug.Log("(메인->스카우트화면)ScoutButtonClicked메서드 시작지점");
            Scout_button.onClick.AddListener(ScoutButtonClicked);
            Debug.Log("(메인->스카우트화면)ScoutButtonClicked메서드");
        }
    }

    public void ScoutButtonClicked()
    {
        Debug.Log("씬전환시도중");
        SceneManager.LoadScene("Gotcha_main");
    }

}
