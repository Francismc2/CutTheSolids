using System;
using UnityEditor;
using UnityEngine;

namespace Engage.BuildTools
{
    public class View<T> : EditorWindow, IView<T> where T : ViewModel, new()
    {
        protected T viewModel;
        public T ViewModel
        {
            get
            {
                if (viewModel == null)
                {
                    viewModel = new T();
                    viewModel.AddListener(this, OnViewModelUpdate);
                }

                return viewModel;
            }
            protected set
            {
                if (value == null)
                    return;

                viewModel = value;
                viewModel.AddListener(this, OnViewModelUpdate);
            }
        }

        public bool IsOpen { get; protected set; }
        public event Action OnOpen;
        public event Action OnClose;
        public event Action OnViewUpdate;

        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void OnEnable()
        {
            Enable();
        }

        protected virtual void OnDisable()
        {
            Disable();
        }

        protected virtual void OnGUI()
        {
            Draw();
        }

        protected virtual void Open()
        {
            ViewModel.Refresh();
            Show();
        }

        protected virtual void Initialize() { }

        protected virtual void OnViewModelUpdate(ViewModel viewModel, string property)
        {
            UpdateView();
        }

        public virtual void Enable()
        {
            IsOpen = true;
            ViewModel.Enable();
            OnOpen?.Invoke();
        }

        public virtual void Disable()
        {
            ViewModel.Disable();
            IsOpen = false;
            OnClose?.Invoke();
        }

        public virtual void Draw() { }

        public virtual void UpdateView()
        {
            OnViewUpdate?.Invoke();
            Repaint();
        }

        public void Center()
        {
            var position = this.position;
            position.center = new Rect(0f, 0f, Screen.currentResolution.width, Screen.currentResolution.height).center;
            this.position = position;
        }
    }
}