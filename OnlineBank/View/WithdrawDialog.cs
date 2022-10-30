using OnlineBank.Model;

namespace OnlineBank.View {
	/// <summary>Enable user to withdraw funds.</summary>
	public class WithdrawDialog : TransactionDialog {
		const string TITLE = "Withdraw funds";
		private const string PROMPT = "Please enter the withdrawal amount in whole dollars:\n> ";
		private const string SUCCESS_FORMAT = "Successfully withdrew {0:C} from your account";
		private const string INSUFFICIENT = "Insufficient funds!";

		/// <summary>Initialise dialog.</summary>
		/// <param name="bank">Bank where transactions conducted.</param>
		/// <param name="acct">Account where funds deposited.</param>
		public WithdrawDialog(Bank bank, Account acct) : base(TITLE, bank, acct) {
		}

		/// <summary>Implement IDisplayable, allowing the user to withdraw funds.</summary>
		public override void Display() {
			UI.WriteLine();
			int dollars;

			if (Account.CanWithdraw) {
				if (UI.Read(out dollars, PROMPT, 1, (int)Account.Balance)) {
					Account.Withdraw(dollars);
					UI.WriteLine(SUCCESS_FORMAT, dollars);
					UI.WriteLine();
				}
				else {
					Console.Error.WriteLine("Attempt to read past end of input.");
				}
			}
			else {
				UI.WriteLine();
				UI.WriteLine(INSUFFICIENT);
				UI.WriteLine();
			}
		}

	}
}