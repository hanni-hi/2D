using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//��ī��Ʈ ��ư�� Ŭ���ϸ� ��í������������ �̵��մϴ�.
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
        Debug.Log("����ȯ�õ���");
        SceneManager.LoadScene("SongSelect");
    }

}
