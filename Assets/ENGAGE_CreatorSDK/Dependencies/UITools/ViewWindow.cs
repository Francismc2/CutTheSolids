using UnityEditor;

namespace Engage.UI.Editor
{
    public interface IView
    {
        event System.Action OnClose;
        event System.Action OnViewUpdate;
        void Enable();
        void Disable();
        void Draw();
    }

    public class ViewWindow : EditorWindow
    {
        private IView view;
        public IView View
        {
            get => view;
            set
            {
                if (view != null)
                {
                    view.OnViewUpdate -= Repaint;
                    view.OnClose -= Close;
                }

                view = value;
                view.OnViewUpdate += Repaint;
                view.OnClose += Close;
            }
        }

        private void OnEnable()
        {
            View?.Enable();
        }

        private void OnDisable()
        {
            View?.Disable();
        }

        private void OnGUI()
        {
            View.Draw();
        }
    }
}