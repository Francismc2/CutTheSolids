namespace Engage.BuildTools
{
    public interface IView<T>
    {
        T ViewModel { get; }

        event System.Action OnOpen;
        event System.Action OnClose;
        event System.Action OnViewUpdate;

        void Enable();
        void Disable();
        void Draw();
        void UpdateView();
    }
}