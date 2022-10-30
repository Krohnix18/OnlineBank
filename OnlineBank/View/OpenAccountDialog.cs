using OnlineBank.Model;
using static OnlineBank.View.UI;

namespace OnlineBank.View {
	/// <summary>Dialog which allows user to open an account.</summary>
	public class OpenAccountDialog : Dialog {
		const string TITLE = "Open account";

		/// <summary>Initialise the dialog.</summary>
		/// <param name="bank">Reference to bank where transactions will be carried out.</param>
		public OpenAccountDialog(Bank bank) : base(TITLE, bank) {
		}

		/// <summary>Implement IDisplayable; creates a new account.</summary>
		public override void Display() {
			Account acct = Bank.CreateAccount();
			WriteLine();
			WriteLine("Your account with card number {0} is now ready for use!", acct.Number);
			WriteLine("Here's your card, don't lose it...");
			WriteLine("(User downloads card from internet and stashes it safely.)");
			WriteLine();
		}
	}
}
