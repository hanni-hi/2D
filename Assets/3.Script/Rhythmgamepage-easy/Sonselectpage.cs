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

        GameManager.SceneType sceneType = GameManager.SceneType.EGame;

        switch (gameObject.name)
        {
            case "Button1-1":
                sceneType = GameManager.SceneType.HGame;
                break;
            case "Button2-1":
                sceneType = GameManager.SceneType.NGame;
                break;
            case "Button3-1":
                sceneType = GameManager.SceneType.EGame;
                break;
        }

        SceneData sceneData = GameManager.instance.GetSceneData(sceneType);

        // ���� �� �̸��� PlayerPrefs�� ����
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("SceneDuration", sceneData.SceneDuration);
        PlayerPrefs.Save();

        SceneManager.LoadScene(sceneData.SceneName);
    }
}
