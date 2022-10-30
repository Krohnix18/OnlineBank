using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBank.View {
	/// <summary>Dialog used to finish a menu session.</summary>
	internal class ExitDialog : IDisplayable, ITerminator {
		/// <summary>Message displayed to user when selected.</summary>
		private string message;

		/// <summary>Title displayed in parent menu.</summary>
		public string Title { get; }

		/// <summary>Initialise a new ExitDialog object.</summary>
		/// <param name="title">Title displayed in parent menu.</param>
		/// <param name="message">Message displayed to user when selected.</param>
		public ExitDialog( string title, string message) {
			Title = title;
			this.message = message;
		}

		/// <summary>Implement IDisplayable; shows the message.</summary>
		public void Display() {
			UI.WriteLine();
			UI.WriteLine(message);
			UI.WriteLine();
		}
	}
}
