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
            Debug.Log("(����->��ī��Ʈȭ��)ScoutButtonClicked�޼��� ��������");
            Scout_button.onClick.AddListener(ScoutButtonClicked);
            Debug.Log("(����->��ī��Ʈȭ��)ScoutButtonClicked�޼���");
        }
    }

    public void ScoutButtonClicked()
    {
        Debug.Log("����ȯ�õ���");
        SceneManager.LoadScene("Gotcha_main");
    }

}
