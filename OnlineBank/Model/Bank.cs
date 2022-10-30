namespace OnlineBank.Model {
	public class Bank {
		private const string BadFileData = "There seems to be something wrong with the saved account info.";
		private const string UnexpectedInputFormat = "Skipping unexpected input {0}";

		/// <summary>Name of save-data file</summary>
		string acctFileName;

		/// <summary>Random number generator for account generation.</summary>
		Random rand;

		/// <summary>List of accounts.</summary>
		List<Account> accounts = new List<Account>();

		public Bank(string acctFileName) {
			this.acctFileName = acctFileName;
			rand = new Random(GetType().Name.GetHashCode());
			Load();
		}

		/// <summary>Generate a unique account number.</summary>
		/// <returns>A new bank account with zero balance and a distinct id number.</returns>
		public Account CreateAccount() {
			AccountNumber acctNum;

			while (true) {
				acctNum = AccountNumber.Generate(rand);

				if (!HasAccount(acctNum)) break;
			}

			Account acct = new Account(acctNum);
			accounts.Add(acct);
			return acct;
		}

		/// <summary>Determines whether an account with the supplied number
		/// is present in the bank.</summary>
		/// <param name="acctNum">The account number to find.</param>
		/// <returns>True if and only if the bank has an account
		/// with matching account number.</returns>
		public bool HasAccount(AccountNumber acctNum) {
			return GetAccount(acctNum) != null;
		}

		/// <summary>Tries to locate an account having the supplied account 
		/// number.</summary>
		/// <param name="acctNum">The account number to find.</param>
		/// <returns>A non-null Account if and only if the bank has an account
		/// with matching account number.</returns>
		public Account GetAccount(AccountNumber acctNum) {
			foreach (Account acct in accounts) {
				if (acct.Matches(acctNum)) return acct;
			}

			return null;
		}

		/// <summary>Loads account info from the save data file.</summary>
		private void Load() {
			if (File.Exists(acctFileName)) {
				using (StreamReader r = new StreamReader(acctFileName)) {
					while (true) {
						string typename = r.ReadLine();

						if (typename == null) break;
						else if (typename == typeof(Account).Name) {
							LoadAccount(r);
						}
						else {
							Console.Error.WriteLine(UnexpectedInputFormat, typename);
						}
					}
				}
			}

		}

		/// <summary>Attempts to parse an account from the supplied text reader.</summary>
		/// <param name="r">A TextReader.</param>
		private void LoadAccount(TextReader r) {
			string acctNumber_ = r.ReadLine();
			string balance_ = r.ReadLine();

			AccountNumber acctNum;
			int balance;

			if (AccountNumber.TryParse(acctNumber_, out acctNum)
				&& int.TryParse(balance_, out balance)
				&& Account.IsValid(acctNum, balance)
				&& !HasAccount(acctNum)
			) {
				accounts.Add(new Account(acctNum, balance));
			}
			else {
				Console.Error.WriteLine(BadFileData);
			}
		}

		/// <summary>Saves data to a text file, able to be parsed by Load.</summary>
		public void Save() {
			using (StreamWriter w = new StreamWriter(acctFileName)) {
				foreach (Account acct in accounts) {
					w.WriteLine("Account");
					w.WriteLine(acct.Number.ToString());
					w.WriteLine(acct.Balance);
				}

				w.Close();
			}
		}
	}
}