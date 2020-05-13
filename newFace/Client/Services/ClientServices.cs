using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newFace.Client
{
    public interface IMyService
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }

    public class MyService : IMyService
    {
        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }

    public class WebAlert
    {
        public bool Show { get; private set; }
        public string Message { get; private set; }
        public string Style { get; private set; }

        public event Action OnChange;

        public void alert(string message, string style= "success", bool show=false)
        {
            Message = message;
            Style = style;
            Show = show;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();



    }

    public class LayoutLoading
    {
        public bool Show { get; private set; }
        

        public event Action OnChange;

        public void showLoding(bool show = false)
        {
            Show = show;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();



    }
}
