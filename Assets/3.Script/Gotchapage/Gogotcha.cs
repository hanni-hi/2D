using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardGrade {SSS,S,A,B,F}

//카드의 속성을 저장하는 클래스입니다.
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
  //  // 복사 생성자 복사 생성자는 기존 객체의 필드 값을 새로운 객체로 복사하는 생성자입니다.
  //  public Gogotcha(Gogotcha original)
  //  {
  //      this.cardName = original.cardName;
  //      this.cardImage = original.cardImage;
  //      this.cardGrade = original.cardGrade;
  //      this.weight = original.weight;
  //  }
}
