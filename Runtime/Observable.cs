using System;
using UnityEngine.Events;

namespace Edgegap.Gen2SDK
{
    public class Observable<T>
    {
        public T Current { get; private set; }
        public T Previous { get; private set; }
        private UnityEvent<Observable<T>, ObservableActionType, string> UpdateEvent =
            new UnityEvent<Observable<T>, ObservableActionType, string>();

        public Observable() { }

        ~Observable()
        {
            UpdateEvent.RemoveAllListeners();
        }

        public void Subscribe(UnityAction<Observable<T>, ObservableActionType, string> subscriber)
        {
            UpdateEvent.AddListener(subscriber);
        }

        public void Unsubscribe(UnityAction<Observable<T>, ObservableActionType, string> subscriber)
        {
            UpdateEvent.RemoveListener(subscriber);
        }

        public void _Notify(string message, ObservableActionType type = ObservableActionType.Log)
        {
            UpdateEvent.Invoke(this, type, message);
        }

#nullable enable
        public void _Update(T? value, string message)
        {
            Previous = Current;
            Current = value;
            UpdateEvent.Invoke(this, ObservableActionType.Update, message);
        }

        public void _Update(object value, string message)
        {
            throw new NotImplementedException();
        }
#nullable disable
    }

    public enum ObservableActionType
    {
        Log = 0,
        Update = 100,
        Warn = 200,
        Error = 300,
    }
}
