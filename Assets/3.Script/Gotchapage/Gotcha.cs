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
            Scout_button.onClick.AddListener(ScoutButtonClicked);
        }
    }

    public void ScoutButtonClicked()
    {
        SceneManager.LoadScene("Gotcha_main");
    }

}
