using System.Collections;
using System.Collections.Generic;
using HexCardGame.Runtime;
using HexCardGame.SharedData;
using UnityEngine;

public class UiBoardElement : MonoBehaviour
{
   SpriteRenderer SpriteRenderer { get; set; }
   public ICardData Data { get; private set; }
   void Awake() => SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
   void SetData(ICardData data) => Data = data;
   void UpdateData() => SpriteRenderer.sprite = Data.Artwork;
   public void SetElement(CreatureElement creatureElement)
   {
      SetData(creatureElement.Data);
      UpdateData();
   }
}
