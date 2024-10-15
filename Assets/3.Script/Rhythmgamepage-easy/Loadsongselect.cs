using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//스카우트 버튼을 클릭하면 가챠메인페이지로 이동합니다.
public class Loadsongselect : MonoBehaviour
{
    [SerializeField] private Button Live_button;

    void Awake()
    {
        if (Live_button != null)
        {
            Live_button.onClick.AddListener(Live_buttonClicked);
        }
    }

    public void Live_buttonClicked()
    {
        Debug.Log("씬전환시도중");
        SceneManager.LoadScene("SongSelect");
    }

}
