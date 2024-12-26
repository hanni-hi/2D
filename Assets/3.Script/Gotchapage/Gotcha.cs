using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//��ī��Ʈ ��ư�� Ŭ���ϸ� ��í������������ �̵��մϴ�.
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
