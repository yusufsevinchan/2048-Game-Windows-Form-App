using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048_Game_Windows_Form_App
{
    internal class ComponentCreator
    {
        private readonly int _boardSize;
        private readonly int _buttonSize = 125;
        private readonly int _buttonMargin = 10;
        private readonly int _buttonSpacing = 131;
        private readonly Color _defaultColor;

        private List<Button> _buttonList;

        public ComponentCreator(int boardSize, Color defaultColor)
        {
            _boardSize = boardSize;
            _defaultColor = defaultColor;
        }

        public List<Button> GetButtonList()
        {
            CreateAndInitializeButtons();
            return _buttonList;
        }

        private void CreateAndInitializeButtons()
        {
            _buttonList = new List<Button>();

            int heightOfButton = _buttonMargin;

            for (int i = 0; i < _boardSize; i++)
            {
                int widthOfButton = _buttonMargin;

                for (int j = 0; j < _boardSize; j++)
                {
                    int index = (i * _boardSize) + j;
                    Button button = InitializeButton(index, widthOfButton, heightOfButton, _defaultColor);
                    _buttonList.Add(button);

                    widthOfButton += _buttonSpacing;
                }
                heightOfButton += _buttonSpacing;
            }
        }

        private Button InitializeButton(int index, int widthOfButton, int heightOfButton, Color color)
        {
            Button button = new Button
            {
                Enabled = false,
                Font = new Font("Verdana", 20F, FontStyle.Bold, GraphicsUnit.Point),
                Location = new Point(widthOfButton, heightOfButton),
                Name = "button" + index,
                Size = new Size(_buttonSize, _buttonSize),
                TabIndex = index,
                UseVisualStyleBackColor = true,
                BackColor = color
            };
            return button;
        }
    }
}
