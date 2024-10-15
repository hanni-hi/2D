using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sonselectpage : MonoBehaviour
{
    //Sonselectpage: ����ڰ� �뷡�� �����ϸ�, ���� ������ ��ȯ�ϰ� �ش� ������ �ð��� �����մϴ�.
    public Image overlayImage; // ����� �̹����� ������ �ʵ�
    public Image songImage; // ���� �̹��� �ʵ�
    private Button button;  // ��ư ������Ʈ
    private bool isSelected = false; // �뷡�� ���õǾ����� ����


    void Start()
    {
        button = GetComponent<Button>();
        // ��ư�� Ŭ�� �̺�Ʈ�� SelectSong �޼��� �߰�
        button.onClick.AddListener(SelectSong);
        // Canvas ������Ʈ�� �������� �߰�

    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        if (!isSelected)
        {
            overlayImage.gameObject.SetActive(false); // ����� �̹��� ��Ȱ��ȭ
        }
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        if (!isSelected)
        {
            overlayImage.gameObject.SetActive(true); // ����� �̹��� �ٽ� Ȱ��ȭ
        }
    }

    private void SelectSong()
    {
        isSelected = true;
        string nextSceneName = "";
        float sceneDuration = 0f;

        switch (gameObject.name)
        {
            case "Button1-1":
                nextSceneName = "HardGame";
                sceneDuration = 158f;
                Debug.Log("�ϵ���ӽ����� �̵�");
                break;
            case "Button2-1":
                nextSceneName = "NormalGame";
                sceneDuration = 128f;
                Debug.Log("�븻���ӽ����� �̵�");
                break;
            case "Button3-1":
                nextSceneName = "EasyGame";
                sceneDuration = 164f;
                Debug.Log("�������ӽ����� �̵�");
                break;
        }

        // ���� �� �̸��� PlayerPrefs�� ����
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("SceneDuration", sceneDuration);
        PlayerPrefs.Save();

        SceneManager.LoadScene(nextSceneName);
    }
}
