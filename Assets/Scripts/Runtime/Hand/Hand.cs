using System.Collections.Generic;
using Tools;
using Tools.GenericCollection;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime
{
    [Event]
    public interface ICreateHand
    {
        void OnCreateHand(IHand hand);
    }

    public interface IHand
    {
        int MaxHandSize { get; }
        List<CardHand> Cards { get; }
        void Add(CardHand card);
        bool Has(CardHand card);
        bool Remove(CardHand card);
        int Size();
        void Empty();
    }

    public class Hand : Collection<CardHand>, IHand
    {
        public Hand(GameParameters gameParameters, IDispatcher dispatcher)
        {
            Parameters = gameParameters;
            Dispatcher = dispatcher;
            OnCreateHand();
        }

        IDispatcher Dispatcher { get; }
        GameParameters Parameters { get; }
        public int MaxHandSize => Parameters.Hand.MaxHandSize;
        public int Size() => Units.Count;
        public List<CardHand> Cards => Units;
        public void Empty() => Clear();

        void OnCreateHand()
        {
            Logger.Log<Hand>("Create Hands");
            Dispatcher.Notify<ICreateHand>(i => i.OnCreateHand(this));
        }
    }
}