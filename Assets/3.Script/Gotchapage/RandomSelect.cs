using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//덱에서 랜덤으로 카드를 선택하고
// 선택된 카드를 ui에 표시합니다. 
// 각 카드의 가중치를 기준으로 확률적으로 카드를 선택합니다. 
public class RandomSelect : MonoBehaviour
{
    public List<Gogotcha> deck = new List<Gogotcha>(); // 카드덱
    public int total = 0; //총 가중치
    public List<Gogotcha> result = new List<Gogotcha>();//랜덤하게 선택된 카드를 담을 리스트

    public Transform parent; //카드의 부모가 될 Transform 
    public GameObject cardprefab; //카드 프리팹

    public Image[] cardFronts; // Canvas의 CardFront 이미지 배열

    private List<GameObject> createdCards = new List<GameObject>();

    // 랜덤으로 카드를 선택하여 결과 리스트에 추가하고, ui에 표시하는 메서드
    public void ResultSelect()
    {
         //기존에 생성된 카드를 모두 제거
          foreach (GameObject card in createdCards)
          {
              Destroy(card);
          }
          createdCards.Clear();
        result.Clear(); // result 리스트 초기화

        for (int i = 0; i < 10; i++)
        {
            Gogotcha selectedCard = Randomcard();
            if (selectedCard == null)
            {
                continue;
            }
            result.Add(selectedCard);

            GameObject cardObj = Instantiate(cardprefab, parent);
            createdCards.Add(cardObj); // 생성된 카드를 리스트에 추가
            CardUI cardUI = cardObj.GetComponent<CardUI>();
            if (cardUI != null)
            {
                cardUI.CardUISet(selectedCard);
            }

            // 카드의 RectTransform 초기화
            RectTransform rt = cardObj.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.localScale = Vector3.one; // 크기 초기화
                rt.anchoredPosition3D = Vector3.zero; // 위치 초기화
                rt.localPosition = Vector3.zero; // 로컬 위치 초기화
            }
        }
        UpdateCardFronts();
    }

    public Gogotcha Randomcard()
    {
        int randomWeight = Random.Range(0, total);
        int cumulativeWeight = 0;

        foreach (Gogotcha card in deck)
        {
            cumulativeWeight += card.weight;
            if (randomWeight < cumulativeWeight) //가중치에 따라 카드 선택됨
            {
                return card;
            }
        }
        return null;  //deck[Random.Range(0, deck.Count)];
    }

    // Start is called before the first frame update
    void Start()
    {
        //스크립트가 활성화 되면 카드 덱의 모든 카드의 총 가중치를 구해줍니다.
        foreach (Gogotcha card in deck)
        {
            total += card.weight; //total 에 모든 카드 가중치 합 
        }
        ResultSelect();
    }

    void UpdateCardFronts()
    {

        for (int i = 0; i < result.Count; i++)
        {
            if (result[i] != null)
            {
                // "Card" 오브젝트를 찾아서 자녀 "CardImage"를 가져옴
                GameObject card = GameObject.Find($"Canvas/Card ({i + 1})/Front/CardImage");
                if (card != null)
                {
                    Image cardImage = card.GetComponent<Image>();
                    if (cardImage != null)
                    {
                        cardImage.sprite = result[i].cardImage;
                        Debug.Log($"CardImage {i}에 스프라이트 {result[i].cardImage.name}가 할당되었습니다.");
                    }
                    else
                    {
                        Debug.LogError($"Card ({i + 1})의 CardImage에 Image 컴포넌트가 없습니다.");
                    }
                }
                else
                {
                    Debug.LogError($"Card ({i + 1})의 CardImage를 찾을 수 없습니다.");
                }
            }
            else
            {
                Debug.LogError($"CardImage {i}에 스프라이트를 할당하지 못했습니다.");
            }
        }
    }
    // {
    //     if (result[i] != null && cardFronts[i] != null)
    //     {
    //         cardFronts[i].sprite = result[i].cardImage;
    //         Debug.Log($"CardFront {i}에 스프라이트 {result[i].cardImage.name}가 할당되었습니다.");
    //     }
    //     else
    //     {
    //         Debug.LogError($"CardFront {i}에 스프라이트를 할당하지 못했습니다.");
    //     }
    // }

}
