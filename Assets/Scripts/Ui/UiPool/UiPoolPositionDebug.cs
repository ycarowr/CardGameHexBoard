using System;
using System.Collections;
using System.Collections.Generic;
using Game.Ui;
using HexCardGame.Model.GamePool;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    public class UiPoolPositionDebug : MonoBehaviour
    {
        [SerializeField] Transform cardTemplate;
        Vector3 Size => cardTemplate.localScale;
        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position, Size);
        }
    }
}
