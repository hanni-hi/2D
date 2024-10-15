using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sonselectpage : MonoBehaviour
{
    //Sonselectpage: 사용자가 노래를 선택하면, 다음 씬으로 전환하고 해당 씬에서 시간을 설정합니다.
    public Image overlayImage; // 희미한 이미지를 설정할 필드
    public Image songImage; // 실제 이미지 필드
    private Button button;  // 버튼 컴포넌트
    private bool isSelected = false; // 노래가 선택되었는지 여부


    void Start()
    {
        button = GetComponent<Button>();
        // 버튼의 클릭 이벤트에 SelectSong 메서드 추가
        button.onClick.AddListener(SelectSong);
        // Canvas 컴포넌트를 동적으로 추가

    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        if (!isSelected)
        {
            overlayImage.gameObject.SetActive(false); // 희미한 이미지 비활성화
        }
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        if (!isSelected)
        {
            overlayImage.gameObject.SetActive(true); // 희미한 이미지 다시 활성화
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
                Debug.Log("하드게임신으로 이동");
                break;
            case "Button2-1":
                nextSceneName = "NormalGame";
                sceneDuration = 128f;
                Debug.Log("노말게임신으로 이동");
                break;
            case "Button3-1":
                nextSceneName = "EasyGame";
                sceneDuration = 164f;
                Debug.Log("이지게임신으로 이동");
                break;
        }

        // 이전 씬 이름을 PlayerPrefs에 저장
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("SceneDuration", sceneDuration);
        PlayerPrefs.Save();

        SceneManager.LoadScene(nextSceneName);
    }
}
