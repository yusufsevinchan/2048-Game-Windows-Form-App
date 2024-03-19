using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048_Game_Windows_Form_App
{
    internal class ButtonEvents
    {
        private static List<Button> _buttonList = FormGameManager.buttonList;
        public static void CombineButtons(int firstIndexAndSum, int SecondIndex)//firstIndex will be sum
        {
            int numFirst = int.Parse(_buttonList[firstIndexAndSum].Text);
            int numSecond = int.Parse(_buttonList[SecondIndex].Text);

            _buttonList[firstIndexAndSum].Text = (numFirst + numSecond).ToString();
            _buttonList[SecondIndex].Text = "";

            SetButtonBackColor(_buttonList[firstIndexAndSum], _buttonList[SecondIndex]);
        }

        public static void SwapButtons(int targetIndex, int sourceIndex)
        {
            _buttonList[targetIndex].Text = _buttonList[sourceIndex].Text;
            _buttonList[sourceIndex].Text = "";

            SetButtonBackColor(_buttonList[sourceIndex], _buttonList[targetIndex]);
        }

        private static void SetButtonBackColor(Button button1, Button button2)
        {
            ButtonColorManager.SetButtonBackColor(button1);
            ButtonColorManager.SetButtonBackColor(button2);
        }


        public static void AddRandomDefaultButton()
        {
            FormGameManager.FillButtons("2", FormGameManager.GetRandomEmptyButtonIndexes(1));
        }
    }
}
