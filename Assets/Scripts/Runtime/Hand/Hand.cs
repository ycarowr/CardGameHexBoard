using System.Collections.Generic;
using Tools.GenericCollection;
using Tools.Patterns.Observer;

namespace HexCardGame.Runtime
{
    [Event]
    public interface ICreateHand
    {
        void OnCreateHand(IHand hand, PlayerId id);
    }

    public interface IHand
    {
        PlayerId Id { get; }
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
        public Hand(PlayerId id, GameParameters gameParameters, IDispatcher dispatcher)
        {
            Id = id;
            Parameters = gameParameters;
            Dispatcher = dispatcher;
            OnCreateHand();
        }

        IDispatcher Dispatcher { get; }
        GameParameters Parameters { get; }

        public PlayerId Id { get; }
        public int MaxHandSize => Parameters.Amounts.MaxHandSize;
        public int Size() => Units.Count;
        public List<CardHand> Cards => Units;
        public void Empty() => Clear();

        void OnCreateHand() => Dispatcher.Notify<ICreateHand>(i => i.OnCreateHand(this, Id));
    }
}