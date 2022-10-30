using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineBank.Model;

namespace OnlineBank.View {
    /// <summary>Dialog specialised to offer user choice by a range of options.</summary>
    public class Menu : Dialog {
        const string Heading = "Please choose from the following list:";
        const string Prompt = "> ";

        /// <summary>Local copy of menu options.</summary>
        private IDisplayable[] options;

        /// <summary>Initialise a new Menu object.</summary>
        /// <param name="title">Title for display.</param>
        /// <param name="options">List of options offered to user.</param>
        public Menu(string title, Bank bank, params IDisplayable[] options ): base(title, bank) {
            this.options = new IDisplayable[options.Length];
            Array.Copy(options, this.options, options.Length);
        }

        /// <summary>Get an option from the user and take subsequent action.</summary>
        public override void Display() {
            while (true) {
                WriteOptions();
                IDisplayable opt;
                
                if (! GetOption( out opt) ) {
                    UI.WriteLine("Attempt to read past end of input.");
                    break;
                }

                opt.Display();

                if (opt is ITerminator) break;
            }
        }

        /// <summary>Displays the available options.</summary>
        private void WriteOptions () {
            UI.WriteLine();
            UI.WriteLine(Title);
            UI.WriteLine();
            UI.WriteLine(Heading);

            for( int i = 0; i < options.Length; i++) {
                UI.WriteLine("{0} -> {1}", i + 1, options[i].Title);
            }
        }

        /// <summary>Reads an option </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        private bool GetOption (out IDisplayable opt) {
            int optionIdx;
            bool ok = UI.Read(out optionIdx, Prompt, 1, options.Length);

            if (ok) {
                opt = options[optionIdx - 1];
            }
            else {
                opt = null;
            }

            return ok;
        }
    }
}