using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiLibraryDebug : MonoBehaviour
    {
        [SerializeField] Transform card;

        void OnDrawGizmos() => Gizmos.DrawCube(transform.position, card.localScale);
    }
}