using OnlineBank.Model;

namespace OnlineBank.View {
	/// <summary>Base class for most displayable objects.</summary>
	public abstract class Dialog : IDisplayable {
		/// <summary>Implement Title property of IDisplayable</summary>
		public string Title { get; }

		/// <summary>Reference to bank where transactions are conducted.</summary>
		protected Bank Bank { get; }

		/// <summary>Initialise the Dialog.</summary>
		/// <param name="title">String containing title text.</param>
		/// <param name="bank">Reference to bank where transactions are conducted.</param>
		public Dialog( string title, Bank bank ) {
			Title = title;
			Bank = bank;
		}

		/// <summary>Abstract implementation of Display method of IDisplayable.</summary>
		public abstract void Display();
	}
}
