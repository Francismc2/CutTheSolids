using System;
using System.Collections.Generic;
using System.Linq;

namespace Engage.BuildTools
{
    public abstract class ViewModel : IDisposable
    {
        protected virtual bool IsEnabled { get; set; }

        public event Action<ViewModel> OnEnable;
        public event Action<ViewModel> OnDisable;
        public event Action<ViewModel, string> OnPropertyChanged;

        protected virtual Dictionary<object, Action<ViewModel, string>> Listeners { get; } = new Dictionary<object, Action<ViewModel, string>>();

        public ViewModel()
        {
            Initialize();
        }

        protected virtual void Initialize() { }
        public virtual void Enable()
        {
            IsEnabled = true;
            OnEnable?.Invoke(this);
        }
        public virtual void Disable()
        {
            IsEnabled = false;
            OnDisable?.Invoke(this);
            RemoveAllListeners();
        }
        public virtual void Refresh() { }

        public void NotifyPropertyChange(string property = "")
        {
            OnPropertyChanged?.Invoke(this, property);
        }

        public virtual void Dispose()
        {
            if (IsEnabled)
            {
                Disable();
            }

            OnPropertyChanged = null;
        }

        public virtual void AddListener(object view, Action<ViewModel, string> onChanged)
        {
            if (!Listeners.ContainsKey(view))
            {
                OnPropertyChanged += onChanged;
            }
        }

        public virtual void RemoveListener(object view)
        {
            if (Listeners.ContainsKey(view))
            {
                OnPropertyChanged -= Listeners[view];
                Listeners.Remove(view);
            }
        }

        public virtual void RemoveAllListeners()
        {
            var listeners = Listeners.ToArray();

            foreach (var listener in listeners)
            {
                RemoveListener(listener.Key);
            }
        }
    }
}
