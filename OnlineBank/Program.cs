using OnlineBank.Model;
using OnlineBank.View;
using static System.Console;

namespace OnlineBank
{
	/// <summary>Program entry point for online bank simulator.</summary>
	class Program {
		/// <summary>Main entry point.</summary>
		/// <param name="args">Command line arguments: if supplied, args[0] 
		/// is the name of a text file containing account information.</param>
		public static void Main(string[] args) {
			string acctFileName = "accts.txt";

			if (args.Length > 0) {
				if (args[0].Contains("-h")) {
					WriteLine("Usage: OnlineBank [acct_file_name]");
					WriteLine("If acct_file_name is omitted, then data will be saved in the present working directory in file 'accts.txt'.");
					return;
				}
				else {
					acctFileName = args[0];
				}
			}

			Bank bank = new Bank(acctFileName);
			MainMenu menu = new MainMenu(bank);
			menu.Display();
			bank.Save();
		}
	}
}