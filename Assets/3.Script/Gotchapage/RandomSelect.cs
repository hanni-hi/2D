using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//������ �������� ī�带 �����ϰ�
// ���õ� ī�带 ui�� ǥ���մϴ�. 
// �� ī���� ����ġ�� �������� Ȯ�������� ī�带 �����մϴ�. 
public class RandomSelect : MonoBehaviour
{
    public List<Gogotcha> deck = new List<Gogotcha>(); // ī�嵦
    public int total = 0; //�� ����ġ
    public List<Gogotcha> result = new List<Gogotcha>();//�����ϰ� ���õ� ī�带 ���� ����Ʈ

    public Transform parent; //ī���� �θ� �� Transform 
    public GameObject cardprefab; //ī�� ������

    public Image[] cardFronts; // Canvas�� CardFront �̹��� �迭

    private List<GameObject> createdCards = new List<GameObject>();

    // �������� ī�带 �����Ͽ� ��� ����Ʈ�� �߰��ϰ�, ui�� ǥ���ϴ� �޼���
    public void ResultSelect()
    {
         //������ ������ ī�带 ��� ����
          foreach (GameObject card in createdCards)
          {
              Destroy(card);
          }
          createdCards.Clear();
        result.Clear(); // result ����Ʈ �ʱ�ȭ

        for (int i = 0; i < 10; i++)
        {
            Gogotcha selectedCard = Randomcard();
            if (selectedCard == null)
            {
                continue;
            }
            result.Add(selectedCard);

            GameObject cardObj = Instantiate(cardprefab, parent);
            createdCards.Add(cardObj); // ������ ī�带 ����Ʈ�� �߰�
            CardUI cardUI = cardObj.GetComponent<CardUI>();
            if (cardUI != null)
            {
                cardUI.CardUISet(selectedCard);
            }

            // ī���� RectTransform �ʱ�ȭ
            RectTransform rt = cardObj.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.localScale = Vector3.one; // ũ�� �ʱ�ȭ
                rt.anchoredPosition3D = Vector3.zero; // ��ġ �ʱ�ȭ
                rt.localPosition = Vector3.zero; // ���� ��ġ �ʱ�ȭ
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
            if (randomWeight < cumulativeWeight) //����ġ�� ���� ī�� ���õ�
            {
                return card;
            }
        }
        return null;  //deck[Random.Range(0, deck.Count)];
    }

    // Start is called before the first frame update
    void Start()
    {
        //��ũ��Ʈ�� Ȱ��ȭ �Ǹ� ī�� ���� ��� ī���� �� ����ġ�� �����ݴϴ�.
        foreach (Gogotcha card in deck)
        {
            total += card.weight; //total �� ��� ī�� ����ġ �� 
        }
        ResultSelect();
    }

    void UpdateCardFronts()
    {

        for (int i = 0; i < result.Count; i++)
        {
            if (result[i] != null)
            {
                // "Card" ������Ʈ�� ã�Ƽ� �ڳ� "CardImage"�� ������
                GameObject card = GameObject.Find($"Canvas/Card ({i + 1})/Front/CardImage");
                if (card != null)
                {
                    Image cardImage = card.GetComponent<Image>();
                    if (cardImage != null)
                    {
                        cardImage.sprite = result[i].cardImage;
                        Debug.Log($"CardImage {i}�� ��������Ʈ {result[i].cardImage.name}�� �Ҵ�Ǿ����ϴ�.");
                    }
                    else
                    {
                        Debug.LogError($"Card ({i + 1})�� CardImage�� Image ������Ʈ�� �����ϴ�.");
                    }
                }
                else
                {
                    Debug.LogError($"Card ({i + 1})�� CardImage�� ã�� �� �����ϴ�.");
                }
            }
            else
            {
                Debug.LogError($"CardImage {i}�� ��������Ʈ�� �Ҵ����� ���߽��ϴ�.");
            }
        }
    }
    // {
    //     if (result[i] != null && cardFronts[i] != null)
    //     {
    //         cardFronts[i].sprite = result[i].cardImage;
    //         Debug.Log($"CardFront {i}�� ��������Ʈ {result[i].cardImage.name}�� �Ҵ�Ǿ����ϴ�.");
    //     }
    //     else
    //     {
    //         Debug.LogError($"CardFront {i}�� ��������Ʈ�� �Ҵ����� ���߽��ϴ�.");
    //     }
    // }

}
