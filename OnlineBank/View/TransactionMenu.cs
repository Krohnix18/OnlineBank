using OnlineBank.Model;

namespace OnlineBank.View {
	internal class TransactionMenu : Menu {
		private const string TITLE = "Transaction menu";
		private const string EXIT = "Sign out";
		private const string SIGNOUT_MESSAGE = "You have successfully signed out.";

		/// <summary>Set up the transaction menu.</summary>
		/// <param name="bank">Bank where transactions conducted.</param>
		/// <param name="acct">Account where funds deposited.</param>
		public TransactionMenu(Bank bank, Account acct) : base(TITLE, bank,
			new CheckBalanceDialog(bank, acct),
			new DepositDialog(bank, acct),
			new WithdrawDialog(bank, acct),
			new TransferDialog(bank, acct),
			new ExitDialog(EXIT, SIGNOUT_MESSAGE)
		) {
			// no implementation needed.
		}
	}
}