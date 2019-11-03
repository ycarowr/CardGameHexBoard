﻿namespace HexCardGame.Runtime.GamePool
{
    public class Position<T> : IDataStorage<T> where T : Coverable
    {
        public bool IsLocked { get; private set; }
        public void Lock() => IsLocked = true;
        public void Unlock() => IsLocked = false;
        public bool HasData => Data != null;
        public T Data { get; private set; }
        public void SetData(T value) => Data = value;
    }
}