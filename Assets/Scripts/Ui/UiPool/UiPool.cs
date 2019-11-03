using Game.Ui;
using HexCardGame.Runtime;
using HexCardGame.Runtime.GamePool;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    public class UiPool : UiEventListener, ICreatePool<CardPool>
    {
        [SerializeField] GameObject cardTemplate;
        [SerializeField] UiPoolPosition[] poolCardPositions;
        IPool<CardPool> CurrentPool { get; set; }

        void ICreatePool<CardPool>.OnCreatePool(IPool<CardPool> pool)
        {
            CurrentPool = pool;
            Logger.Log<UiPool>("Pool View Created");
            DrawPool();
        }

        protected override void Awake()
        {
            cardTemplate.SetActive(false);
            base.Awake();
        }

        [Button]
        void DrawPool()
        {
            var size = CurrentPool.Size();
            for (var i = 0; i < size; ++i)
            {
                Debug.Log("Create card pool");
                var gameObj = Instantiate(cardTemplate, poolCardPositions[i].transform);
                gameObj.SetActive(true);
            }
        }
    }
}