namespace Blazor.Client.Services
{
    public enum ToastLevel
    {
        Success,
        Error,
        Warning,
        Info
    }

    public class ToastService
    {
        public event Action<ToastLevel, string>? OnShow;

        public void ShowSuccess(string mensaje) => Show(ToastLevel.Success, mensaje);
        public void ShowError(string mensaje) => Show(ToastLevel.Error, mensaje);
        public void ShowWarning(string mensaje) => Show(ToastLevel.Warning, mensaje);
        public void ShowInfo(string mensaje) => Show(ToastLevel.Info, mensaje);

        private void Show(ToastLevel level, string mensaje)
        {
            OnShow?.Invoke(level, mensaje);
        }
    }
}
