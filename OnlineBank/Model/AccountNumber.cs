namespace OnlineBank.Model {
	/// <summary>Class which encapsulates account number functionality. An
	/// account number has the form AAA-NNNNNNN. AAA is the Branch Id, and 
	/// NNNNNNN is numeric account identifier.</summary>
	public class AccountNumber {
		/// <summary>Number of symbols in branch id.</summary>
		private const int BranchLen = 3;

		/// <summary>Minimum legal account id.</summary>
		private const int MinAccountId = 1000000;

		/// <summary>Exclusive upper bound for legal account id.</summary>
		private const int MaxAccountId = 10000000;

		/// <summary>Exclusive upper bound for legal account id.</summary>
		private const char Separator = '-';

		/// <summary>Branch id (read-only).</summary>
		public string BranchId { get; }

		/// <summary>Account id (read-only).</summary>
		public int AccountId { get; }

		/// <summary>Initialise a new account number.</summary>
		/// <param name="branchId">Branch id code.</param>
		/// <param name="accountId">Account id code.</param>
		/// <exception cref="ArgumentException">Thrown if parameters are invalid.</exception>
		public AccountNumber(string branchId, int accountId) {
			if (IsValid(branchId, accountId)) {
				BranchId = branchId;
				AccountId = accountId;
			}
			else {
				throw new ArgumentException();
			}
		}

		/// <summary>Determines if a parameter combination is valid.</summary>
		/// <param name="branchId">Branch id code.</param>
		/// <param name="accountId">Account id code.</param>
		/// <returns>True if and only if both arguments are valid.</returns>
		public static bool IsValid(string branchId, int accountId) {
			return IsValidBranchId(branchId) && IsValidAccountId(accountId);
		}

		/// <summary>Determines if a branch id is valid.</summary>
		/// <param name="branchId">Branch id code.</param>
		/// <returns>
		/// True if and only if the argument is BranchLen upper-case numeric 
		/// digits.
		/// </returns>
		private static bool IsValidBranchId(string branchId) {
			if (branchId == null) return false;
			int numLetters = 0;

			foreach (char c in branchId) if (char.IsUpper(c)) numLetters++;

			return numLetters == BranchLen && branchId.Length == BranchLen;
		}

		/// <summary>Determines if the argument is a legal account id.</summary>
		/// <param name="accountId">Branch id code.</param>
		/// <returns>
		/// True if and only if MinAccountId &lt;= accountId &lt; MaxAccountId.
		/// </returns>
		private static bool IsValidAccountId(int accountId) {
			return accountId >= MinAccountId && accountId < MaxAccountId;
		}

		/// <summary>Try to extract an account number from a string.</summary>
		/// <param name="s">A string to convert.</param>
		/// <param name="value">Reference to a variable where the result will be saved.</param>
		/// <returns>True if and only if the conversion is successful.</returns>
		public static bool TryParse(string s, out AccountNumber value) {
			value = default;

			if (!string.IsNullOrWhiteSpace(s)) {
				string[] parts = s.Split(Separator);

				if (parts.Length == 2 
					&& int.TryParse(parts[1], out int accountId)
					&& IsValid(parts[0], accountId) 
				){
					value = new AccountNumber(parts[0], accountId);
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Determines if this account number matches another object.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		/// True if and only if the two objects have the same account id and 
		/// branch id.
		/// </returns>
		public override bool Equals(object obj) {
			if (obj is AccountNumber) {
				AccountNumber other = (AccountNumber)obj;
				return BranchId.Equals(other.BranchId)
					&& AccountId.Equals(other.AccountId);
			}
			else {
				return false;
			};
		}

		/// <summary>
		/// Return a hash code suitable for use as a key in a dictionary.
		/// </summary>
		/// <returns>HashCode.Combine(BranchId, AccountId)</returns>
		public override int GetHashCode() {
			return HashCode.Combine(BranchId, AccountId);
		}

		/// <summary>Gets a string representing the account number.</summary>
		/// <returns>A string representing the account number.</returns>
		public override string ToString() {
			return $"{BranchId}-{AccountId}";
		}

		/// <summary>Gets a randomly generated account number.</summary>
		/// <param name="rand">A random number generator.</param>
		/// <returns>A randomly generated account number.</returns>
		public static AccountNumber Generate(Random rand) {
			string branchId = "";

			for (int i = 0; i < BranchLen; i++) branchId += (char)rand.Next('A', 'Z' + 1);

			int accountId = rand.Next(MinAccountId, MaxAccountId);

			return new AccountNumber(branchId, accountId);
		}
	}
}
