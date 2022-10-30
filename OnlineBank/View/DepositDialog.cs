using OnlineBank.Model;

namespace OnlineBank.View {
	/// <summary>Allow user to deposit funds.</summary>
	internal class DepositDialog : TransactionDialog {
		private const string TITLE = "Deposit funds";
		private const string PROMPT = "Please enter the deposit amount in whole dollars:\n> ";
		private const string SUCCESS_FORMAT = "Successfully deposited {0:C} to your account";
		private const string ACCOUNT_FULL = "Account has reached maximum capacity.";

		/// <summary>Initialise dialog.</summary>
		/// <param name="bank">Bank where transactions conducted.</param>
		/// <param name="acct">Account where funds deposited.</param>
		public DepositDialog(Bank bank, Account acct) : base(TITLE, bank, acct) {
		}

		/// <summary>Implement IDisplayable, allowing the user to deposit funds.</summary>
		public override void Display() {
			UI.WriteLine();
			int dollars;

			if (Account.CanDeposit) {
				if (UI.Read(out dollars, PROMPT, 1, int.MaxValue - (int)Account.Balance)) {
					Account.Deposit(dollars);
					UI.WriteLine(SUCCESS_FORMAT, dollars);
				}
			}
			else {
				UI.WriteLine(ACCOUNT_FULL);
			}
		}
	}
}