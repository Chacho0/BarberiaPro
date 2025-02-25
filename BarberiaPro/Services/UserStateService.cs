namespace BarberiaPro.Services
{
    public class UserStateService
    {
        public int? UserId { get; private set; }

        public event Action OnChange;

        public void SetUserId(int userId)
        {
            UserId = userId;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
