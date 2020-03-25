using HexCardGame.Runtime;
using HexCardGame.SharedData;
using UnityEngine;

public class UiBoardElement : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer { get; set; }
    public ICardData Data { get; private set; }

    private void Awake()
    {
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void SetData(ICardData data)
    {
        Data = data;
    }

    private void UpdateData()
    {
        SpriteRenderer.sprite = Data.Artwork;
    }

    public void SetElement(CreatureElement creatureElement)
    {
        SetData(creatureElement.Data);
        UpdateData();
    }
}