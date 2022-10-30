namespace OnlineBank.Model{
	/// <summary>Minimum viable model of a savings or loan account.</summary>
	public class Account {
		/// <summary>Separator used in string representation.</summary>
		private const char Separator = ':';

		/// <summary>Account number.</summary>
		public AccountNumber Number { get; }

		/// <summary>Current account balance</summary>
		public int Balance { get; private set; }

		/// <summary>Returns true if and only if it is logically possible to deposit funds.</summary>
		public bool CanDeposit {
			get {
				return Balance < int.MaxValue;
			}
		}

		/// <summary>Returns true if and only if it is logically possible to deposit funds.</summary>
		public bool CanWithdraw {
			get {
				return Balance >= 1;
			}
		}

		/// <summary>Maximum logically consistent deposit amount.</summary>
		public int MaxDeposit {
			get {
				return int.MaxValue - Balance;
			}
		}

		// TODO: balance should be computed from transactions, not stored directly.

		/// <summary>Initialise anew account.</summary>
		/// <param name="accountNumber">The account number.</param>
		/// <param name="balance">The initial balance.</param>
		/// <exception cref="ArgumentException">
		/// Thrown if supplied arguments are invalid.
		/// </exception>
		public Account(AccountNumber accountNumber, int balance = 0) {
			if (IsValid(accountNumber, balance)) {
				Number = accountNumber;
				Balance = balance;
			}
			else {
				throw new ArgumentException();
			}
		}

		/// <summary>Determines if the supplied arguments are valid.</summary>
		/// <param name="accountNumber">Account number.</param>
		/// <param name="balance">Initial account balance.</param>
		/// <returns>True if and only if the account number is not null.</returns>
		public static bool IsValid(AccountNumber accountNumber, int balance = 0) {
			return accountNumber != null;
		}

		/// <summary>Convert string to account.</summary>
		/// <param name="s"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool TryParse(string s, out Account value) {
			value = default;

			if (!string.IsNullOrWhiteSpace(s)) {
				string[] parts = s.Split(Separator);

				if (parts.Length == 2
				&& AccountNumber.TryParse(parts[0], out AccountNumber acct)
				&& int.TryParse(parts[1], out int bal)
				&& IsValid(acct, bal)
				) {
					value = new Account(acct, bal);
					return true;
				}
			}

			return false;
		}

		/// <summary>Determines if this account has a particular account number.</summary>
		/// <param name="acctNum">The query account number.</param>
		/// <returns>True if and only if this account has a particular account number.</returns>
		public bool Matches(AccountNumber acctNum) {
			return Number.Equals(acctNum);
		}

		/// <summary>Gets a string representing the Account.</summary>
		/// <returns>Returns a string representing the Account.</returns>
		public override string ToString() {
			return $"{Number}{Separator}{Balance}";
		}

		/// <summary>Deposit funds into account.</summary>
		/// <param name="depositAmount">Amount to deposit.</param>
		/// <exception cref="ArgumentException">If deposit amount is less than 1 dollar.</exception>
		public void Deposit(int depositAmount) {
			if (depositAmount < 1) throw new ArgumentException();
			Balance += depositAmount;
		}

		/// <summary>Deposit funds into account.</summary>
		/// <param name="withdrawAmount">Amount to withdraw.</param>
		/// <exception cref="ArgumentException">
		/// If withdraw amount is less than 1 dollar or greater than the 
		/// account balance.
		/// </exception>
		public void Withdraw(int withdrawAmount) {
			if (withdrawAmount < 1 || withdrawAmount > Balance) throw new ArgumentException();
			Balance -= withdrawAmount;
		}

		/// <summary>Transfer funds between accounts.</summary>
		/// <param name="transferAmount">Amount to transfer.</param>
		/// <param name="toAccount">Destination account for funds.</param>
		internal void Transfer(int transferAmount, Account toAccount) {
			Withdraw(transferAmount);
			toAccount.Deposit(transferAmount);
		}
	}
}
