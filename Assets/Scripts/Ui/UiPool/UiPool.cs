using System;
using System.Collections;
using System.Collections.Generic;
using Game.Ui;
using HexCardGame.Model.GamePool;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    public class UiPool : UiEventListener, ICreatePool
    {
        [SerializeField] UiPoolPosition[] poolCardPositions;
        [SerializeField] GameObject cardTemplate;
        IPool CurrentPool { get; set; }

        protected override void Awake()
        {
            cardTemplate.SetActive(false);
            base.Awake();
        }

        void ICreatePool.OnCreatePool(IPool pool)
        {
            CurrentPool = pool;
            Logger.Log<UiPool>("Pool View Created");
            DrawPool();
        }

        [Button]
        void DrawPool()
        {
            for (var i = 0; i < CurrentPool.Size; ++i)
            {
                var gameObj = Instantiate(cardTemplate, poolCardPositions[i].transform);
                gameObj.SetActive(true);
            }
        }
    }
}
