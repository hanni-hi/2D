using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//���� ī���� ui�� �����ϰ� Ŭ�� �̺�Ʈ�� ó���մϴ�.
// chr �ʵ�� ī�� �̹����� ǥ���ϸ�, Ŭ�� �� �ִϸ��̼��� �����մϴ�. 
public class CardUI : MonoBehaviour, IPointerDownHandler
{
    public Sprite[] sprites;
    public Image[] cardImages;
    public Image cardFront;

    public Image Chr;
    private Animator animator;
    private bool isFlipped = false; // ī�尡 ���������� ���θ� ����

    private Gogotcha cardData; // ���� ī�� �����͸� ����

    // Start is called before the first frame update
    private void Start()
    {
        DisplayRandomSprite();
        animator = GetComponent<Animator>();//���� ���� ������Ʈ���� Animator������Ʈ�� �����´�. 
        if (animator == null)
        {
            Debug.LogError("Animator ������Ʈ�� �����ϴ�.");
        }

        if (Chr == null)
        {
            Debug.LogError("Image ������Ʈ�� ������� �ʾҽ��ϴ�.");
        }
        else
        {
            // �ʱ⿡�� ī�� �޸� �̹��� ����
            Chr.sprite = Resources.Load<Sprite>("UI/back");
            Debug.Log("�ʱ� ī�� �޸� �̹��� ���� �Ϸ�");
        }
    }

    void DisplayRandomSprite()
    {
        if (sprites.Length == 0)
        {
            Debug.LogError("��������Ʈ �迭�� ��� �ֽ��ϴ�.");
            return;
        }

        // �������� ��������Ʈ ����
        int randomIndex = Random.Range(0, sprites.Length);
        Sprite selectedSprite = sprites[randomIndex];

        // ���õ� ��������Ʈ�� �̹��� ������Ʈ�� �Ҵ�
        cardFront.sprite = selectedSprite;
        Debug.Log($"�������� ���õ� ��������Ʈ: {selectedSprite.name}");
    }

    //ī�� ������ �ʱ�ȭ
    public void CardUISet(Gogotcha gogotcha)
    {
        cardData = gogotcha;
        Chr.sprite = cardData.cardImage; // ��í �������� ���� �� �̹����� �Ҵ��մϴ�.
        isFlipped = false; // ī�尡 ������ ���¸� �ʱ�ȭ
    }

   public void OnPointerDown(PointerEventData eventData)
    {   //�ִϸ��̼� Ʈ���� 'Flip'�� Ȱ��ȭ�Ͽ� ī�� ������ �ִϸ��̼��� ����

        Debug.Log("�������ʹٿ�޼��尡 �����");
        if (!isFlipped && animator != null)
        {
            animator.SetTrigger("Flip");
            Debug.Log("Flip �ִϸ��̼� �����");
            isFlipped = true; // ī�尡 ������ ���·� ����
            //StartCoroutine(FlipCardCoroutine());
        }
    }

   // private IEnumerator FlipCardCoroutine()
   // {
   //     yield return new WaitForSeconds(0.5f); // �ִϸ��̼��� ����Ǵ� �ð���ŭ ���
   //     if (cardData != null && Chr != null)
   //     {
   //         Chr.sprite = cardData.cardImage; // ī�� �ո� �̹��� ����
   //         Debug.Log($"ī�� �̹��� ������: {cardData.cardName}");
   //     }
   //     else
   //     {
   //         Debug.LogError("cardData �Ǵ� chr�� null�Դϴ�.");
   //     }
   //     isFlipped = true; // ī�尡 ������ ���·� ����
   // }

}
