namespace Engage.BuildTools
{
    public class ViewPanel<T> : IView<T> where T : ViewModel, new()
    {
        protected T viewModel;
        public T ViewModel
        {
            get
            {
                if (viewModel == null)
                {
                    viewModel = new T();
                }

                return viewModel;
            }
            protected set
            {
                if (value == null)
                    return;

                Disable();
                viewModel = value;
                Enable();
            }
        }

        public bool IsOpen { get; protected set; }
        public event System.Action OnOpen;
        public event System.Action OnClose;
        public event System.Action OnViewUpdate;

        public ViewPanel()
        {
            Initialize();
        }

        public ViewPanel(T viewModel)
        {
            ViewModel = viewModel;
            Initialize();
        }

        public virtual void Initialize() { }

        public virtual void Enable()
        {
            IsOpen = true;
            ViewModel.OnPropertyChanged += OnViewModelUpdate;
        }

        public virtual void Disable()
        {
            IsOpen = false;
            ViewModel.OnPropertyChanged -= OnViewModelUpdate;
        }

        public virtual void Close()
        {
            OnClose?.Invoke();
        }

        public virtual void Draw() { }

        protected virtual void OnViewModelUpdate(ViewModel viewModel, string property)
        {
            UpdateView();
        }

        public virtual void UpdateView()
        {
            OnViewUpdate?.Invoke();
        }
    }
}