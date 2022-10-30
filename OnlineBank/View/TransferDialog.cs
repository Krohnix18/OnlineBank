using OnlineBank.Model;

namespace OnlineBank.View {
	/// <summary>Enable user to transfer funds.</summary>
	internal class TransferDialog : TransactionDialog {
		private const string TITLE = "Transfer funds";
		private const string PROMPT = "Please enter the amount to transfer between {0} and {1}\n> ";
		private const string ACCOUNTS_SAME = "Destination account must not be the same as source account. Transaction cancelled.";
		private const string TFER_ACCT_FORMAT = "Please enter a card number other than {0}:\n> ";
		private const string MISSING_DESTINATION = "Destination account does not exist. Transaction cancelled.";
		private const string ERROR_EOF = "Attempt to read past end of input.";
		private const string SUCCESS_FORMAT = "Successfully transferred {0} from {1} to {2}.";

		/// <summary>Initialise dialog.</summary>
		/// <param name="bank">Bank where transactions conducted.</param>
		/// <param name="acct">Account where funds deposited.</param>
		public TransferDialog(Bank bank, Account acct) : base(TITLE, bank, acct ) {
		}

		/// <summary>Implement IDisplayable, allowing the user to withdraw funds.</summary>
		public override void Display() {
			AccountNumber toAcctNum;
			string prompt = String.Format(TFER_ACCT_FORMAT, Account.Number);

			if (SwipeCardDialog.Read(out toAcctNum, prompt)) {
				Account toAccount = Bank.GetAccount(toAcctNum);

				if (toAccount != null) {
					if (toAccount.Equals(Account)) {
						UI.WriteLine(ACCOUNTS_SAME);
					}
					else {
						Transfer(toAccount);
					}
				}
				else {
					UI.WriteLine(MISSING_DESTINATION);
				}
			}
			else {
				Console.Error.WriteLine(ERROR_EOF);
			}
		}

		private void Transfer(Account toAccount) {
			if (Account.CanWithdraw && toAccount.CanDeposit) {
				int maxTransfer = Math.Min(Account.Balance, toAccount.MaxDeposit);
				string prompt = String.Format(PROMPT, 1, maxTransfer);

				int transferAmount;

				if (UI.Read(out transferAmount, prompt, 1, maxTransfer)) {
					Account.Transfer(transferAmount, toAccount);
					UI.WriteLine(SUCCESS_FORMAT, transferAmount, Account.Number, toAccount.Number);
					UI.WriteLine();
				}
				else {
					Console.Error.WriteLine(ERROR_EOF);
				}
			}

		}
	}
}