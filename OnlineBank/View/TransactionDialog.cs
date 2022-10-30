using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineBank.Model;

namespace OnlineBank.View {
	/// <summary>Base class for dialogues which conduct transactions.</summary>
	public abstract class TransactionDialog: Dialog {
		/// <summary>Account where funds deposited.</summary>
		protected Account Account { get; }

		/// <summary>Initialise dialog.</summary>
		/// <param name="bank">Bank where transactions conducted.</param>
		/// <param name="acct">Account where funds deposited.</param>
		public TransactionDialog(string title, Bank bank, Account acct) : base(title, bank) {
			Account = acct;
		}
	}
}
