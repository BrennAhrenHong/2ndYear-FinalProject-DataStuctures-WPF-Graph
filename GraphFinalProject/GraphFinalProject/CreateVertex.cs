using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GraphFinalProject
{
    public class CreateVertex
    {
        public char LabelLetter { get; protected set; }

        public Label CreateLabel(char prevLetter, int labelNumber)
        {
            Label newLabel = new Label();

            if (prevLetter == 'Z')
                LabelLetter = 'A';
            else
            {
                //LabelLetter = prevLetter;
                //LabelLetter++;
                //LabelLetter = LabelLetter + labelNumber;
            }

            newLabel.FontSize = 15;
            newLabel.Content = LabelLetter + labelNumber;

            return newLabel;
        }
    }
}
