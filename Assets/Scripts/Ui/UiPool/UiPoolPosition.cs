using System;
using System.Collections;
using System.Collections.Generic;
using Game.Ui;
using HexCardGame.Model.GamePool;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    public class UiPoolPosition : MonoBehaviour
    {
        [SerializeField] PoolPositionId id;
        GameObject StoredCard { get; set; }

        public void SetStoredCard(GameObject card) => StoredCard = card;
    }
}
