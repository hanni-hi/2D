using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardGrade {SSS,S,A,B,F}

//ī���� �Ӽ��� �����ϴ� Ŭ�����Դϴ�.
[System.Serializable]
public class Gogotcha
{
    public string cardName;
    public Sprite cardImage;
    public CardGrade cardGrade;
    public int weight;

    public Gogotcha(Gogotcha gogotcha)
    {
        this.cardName = gogotcha.cardName;
        this.cardImage = gogotcha.cardImage;
        this.cardGrade = gogotcha.cardGrade;
        this.weight = gogotcha.weight;
    }
  //  // ���� ������ ���� �����ڴ� ���� ��ü�� �ʵ� ���� ���ο� ��ü�� �����ϴ� �������Դϴ�.
  //  public Gogotcha(Gogotcha original)
  //  {
  //      this.cardName = original.cardName;
  //      this.cardImage = original.cardImage;
  //      this.cardGrade = original.cardGrade;
  //      this.weight = original.weight;
  //  }
}
