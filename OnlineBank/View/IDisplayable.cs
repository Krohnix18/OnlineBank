using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineBank.View {
    /// <summary>All objects displayable in the view will implement this interface.</summary>
	public interface IDisplayable {
        /// <summary> The displayable object's title</summary>
        public string Title { get; }

        /// <summary>Display and execute the logic of the displayable object.</summary>
        public void Display();
    }
}