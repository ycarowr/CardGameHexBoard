using System.Collections;
using System.Collections.Generic;
using Game.Ui;
using HexCardGame.Model;
using UnityEngine;
using Logger = Tools.Logger;

namespace HexCardGame.UI
{
    public class UiLibrary : UiEventListener, ICreateLibrary
    {

        void ICreateLibrary.OnCreateLibrary(Library lib)
        {
            Logger.Log<UiLibrary>("Library View Created");
        }
    }
}