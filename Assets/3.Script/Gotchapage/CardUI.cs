using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//개별 카드의 ui를 관리하고 클릭 이벤트를 처리합니다.
// chr 필드는 카드 이미지를 표시하며, 클릭 시 애니메이션을 실행합니다. 
public class CardUI : MonoBehaviour, IPointerDownHandler
{
    public Sprite[] sprites;
    public Image[] cardImages;
    public Image cardFront;

    public Image Chr;
    private Animator animator;
    private bool isFlipped = false; // 카드가 뒤집혔는지 여부를 추적

    private Gogotcha cardData; // 현재 카드 데이터를 저장

    // Start is called before the first frame update
    private void Start()
    {
        DisplayRandomSprite();
        animator = GetComponent<Animator>();//현재 게임 오브젝트에서 Animator컴포넌트를 가져온다. 
        if (animator == null)
        {
            Debug.LogError("Animator 컴포넌트가 없습니다.");
        }

        if (Chr == null)
        {
            Debug.LogError("Image 컴포넌트가 연결되지 않았습니다.");
        }
        else
        {
            // 초기에는 카드 뒷면 이미지 설정
            Chr.sprite = Resources.Load<Sprite>("UI/back");
            Debug.Log("초기 카드 뒷면 이미지 설정 완료");
        }
    }

    void DisplayRandomSprite()
    {
        if (sprites.Length == 0)
        {
            Debug.LogError("스프라이트 배열이 비어 있습니다.");
            return;
        }

        // 랜덤으로 스프라이트 선택
        int randomIndex = Random.Range(0, sprites.Length);
        Sprite selectedSprite = sprites[randomIndex];

        // 선택된 스프라이트를 이미지 컴포넌트에 할당
        cardFront.sprite = selectedSprite;
        Debug.Log($"랜덤으로 선택된 스프라이트: {selectedSprite.name}");
    }

    //카드 정보를 초기화
    public void CardUISet(Gogotcha gogotcha)
    {
        cardData = gogotcha;
        Chr.sprite = cardData.cardImage; // 가챠 페이지가 켜질 때 이미지를 할당합니다.
        isFlipped = false; // 카드가 뒤집힌 상태를 초기화
    }

   public void OnPointerDown(PointerEventData eventData)
    {   //애니메이션 트리거 'Flip'을 활성화하여 카드 뒤집기 애니메이션을 실행

        Debug.Log("온포인터다운메서드가 실행됨");
        if (!isFlipped && animator != null)
        {
            animator.SetTrigger("Flip");
            Debug.Log("Flip 애니메이션 실행됨");
            isFlipped = true; // 카드가 뒤집힌 상태로 변경
            //StartCoroutine(FlipCardCoroutine());
        }
    }

   // private IEnumerator FlipCardCoroutine()
   // {
   //     yield return new WaitForSeconds(0.5f); // 애니메이션이 실행되는 시간만큼 대기
   //     if (cardData != null && Chr != null)
   //     {
   //         Chr.sprite = cardData.cardImage; // 카드 앞면 이미지 설정
   //         Debug.Log($"카드 이미지 설정됨: {cardData.cardName}");
   //     }
   //     else
   //     {
   //         Debug.LogError("cardData 또는 chr가 null입니다.");
   //     }
   //     isFlipped = true; // 카드가 뒤집힌 상태로 변경
   // }

}
