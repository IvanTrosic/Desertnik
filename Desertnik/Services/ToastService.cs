using Desertnik.Data;

namespace Desertnik.Services
{
	public class ToastService
	{
		AppDbContext _context;
		public ToastService(AppDbContext context)
		{
			_context = context;
		}
		public event Action<string, string, int>? OnShow;
		public event Action? OnHide;

		public void ShowToast(string message, string type = "info", int dismissAfter = 3)
		{
			OnShow?.Invoke(message, type, dismissAfter);
		}

		public void HideToast()
		{
			OnHide?.Invoke();
		}
	}
}