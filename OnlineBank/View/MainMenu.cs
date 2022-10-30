using OnlineBank.Model;

namespace OnlineBank.View {
	/// <summary>Main menu for bank transaction system.</summary>
	public class MainMenu : Menu {
		private const string TITLE = "Welcome to THE online bank!";
		private const string FAREWELL = "Thank you for banking with THE online bank!";
		private const string EXIT = "Exit";

		/// <summary>Reference to bank where transactions are conducted.</summary>
		private Bank bank;

		/// <summary>Initialise new MainMenu object.</summary>
		/// <param name="bank">Reference to bank where transactions are conducted.</param>
		public MainMenu(Bank bank) : base(TITLE, bank,
			new OpenAccountDialog(bank),
			new SwipeCardDialog(bank),
			new ExitDialog(EXIT, FAREWELL)
		) {
			this.bank = bank;
		}
	}
}