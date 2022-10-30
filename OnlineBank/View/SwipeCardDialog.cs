using OnlineBank.Model;

namespace OnlineBank.View {
	internal class SwipeCardDialog : Dialog {
		private const string TITLE = "Swipe card to do business";
		private const string PROMPT = "Please enter the card number in the form AAA-DDDDDDD\n> ";

		/// <summary>Initialise dialog.</summary>
		/// <param name="bank">Reference to bank where transactions are conducted.</param>
		public SwipeCardDialog(Bank bank) : base(TITLE, bank) {}

		/// <summary>Implement IDisplayable: gets account number and displays next menu.</summary>
		public override void Display() {
			UI.WriteLine();
			AccountNumber acctNum;

			if (Read(out acctNum, PROMPT)) {
				Account acct = Bank.GetAccount(acctNum);

				if (acct == null) {
					UI.WriteLine();
					UI.WriteLine("Card not registered. Please check the cord or open an account.");
					UI.WriteLine();
				}
				else {
					Menu transactionMenu = new TransactionMenu(Bank, acct);
					transactionMenu.Display();
				}
			}
		}

		/// <summary>Gets an account number from the user.</summary>
		/// <param name="acctNum">Reference to a variable where the result will be saved.</param>
		/// <returns>False if and only if we attempted to read past the end of input.</returns>
		public static bool Read(out AccountNumber acctNum, string prompt ) {
			bool ok = true;
			acctNum = null;

			while(true) {
				string userInput;
				ok = UI.Read(out userInput, prompt);

				if (!ok) break;

				if (AccountNumber.TryParse(userInput, out acctNum)) break;

				UI.WriteLine("{0} is not a valid card number.", userInput);
			}

			return ok;
		}
	}
}