using OnlineBank.Model;

namespace OnlineBank.View {
	/// <summary>Dialog which allows user to deposit funds.</summary>
	internal class CheckBalanceDialog: TransactionDialog {
		private const string TITLE = "Check balance";

		/// <summary>Initialise dialog.</summary>
		/// <param name="bank">Bank where transactions conducted.</param>
		/// <param name="acct">Account where funds deposited.</param>
		public CheckBalanceDialog(Bank bank, Account acct) : base(TITLE, bank, acct) {
		}

		/// <summary>Implement IDisplayable, allowing student to display balance.</summary>
		public override void Display() {
			UI.WriteLine();
			UI.WriteLine("Your account balance is {0:C}", Account.Balance);
			UI.WriteLine();
		}
	}
}