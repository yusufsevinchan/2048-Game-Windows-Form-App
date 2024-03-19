using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048_Game_Windows_Form_App
{
    public class TileMovementManager
    {
        static int _boardSize = FormGameManager.boardSize;
        static List<Button> _buttonList = FormGameManager.buttonList;

        public static void MoveUp()
        {
            for (int column = 0; column < _boardSize; column++)
            {
                int num = 0, index = 0;

                for (int x = 0; x < _boardSize; x++)
                {
                    int indexOfButton = (x * _boardSize) + column;

                    if (num == 0)
                    {
                        num = FormGameManager.GetNumAndIndex(indexOfButton, ref index);
                        continue;
                    }

                    int nextNum = FormGameManager.GetNumAndIndex(indexOfButton, ref index);

                    if (num == nextNum)
                    {
                        ButtonEvents.CombineButtons(index, index - _boardSize);
                        num = 0;
                    }
                    else
                    {
                        num = nextNum;
                    }
                }

                int emptyIndex = column;
                int fullIndex = column + _boardSize;

                while (fullIndex < _boardSize * _boardSize)
                {
                    if (_buttonList[emptyIndex].Text != "")
                    {
                        emptyIndex += _boardSize;
                        fullIndex += _boardSize;
                        continue;
                    }
                    else if (_buttonList[fullIndex].Text == "")
                    {
                        fullIndex += _boardSize;
                        continue;
                    }
                    else
                    {
                        ButtonEvents.SwapButtons(emptyIndex, fullIndex);
                    }
                }
            }
        }

        public static void MoveDown()
        {
            for (int column = 0; column < _boardSize; column++)
            {
                int num = 0, index = 0;

                for (int y = _boardSize - 1; y >= 0; y--)
                {
                    int indexOfButton = column + (y * _boardSize);

                    if (num == 0)
                    {
                        num = FormGameManager.GetNumAndIndex(indexOfButton, ref index);
                        continue;
                    }

                    int nextNum = FormGameManager.GetNumAndIndex(indexOfButton, ref index);

                    if (num == nextNum)
                    {
                        ButtonEvents.CombineButtons(index, index + _boardSize);
                        num = 0;
                    }
                    else
                    {
                        num = nextNum;
                    }
                }

                int emptyIndex = (_boardSize * (_boardSize - 1)) + column;
                int fullIndex = emptyIndex - _boardSize;

                while (fullIndex >= 0)
                {
                    if (_buttonList[emptyIndex].Text != "")
                    {
                        emptyIndex -= _boardSize;
                        fullIndex -= _boardSize;
                        continue;
                    }
                    else if (_buttonList[fullIndex].Text == "")
                    {
                        fullIndex -= _boardSize;
                        continue;
                    }
                    else
                    {
                        ButtonEvents.SwapButtons(emptyIndex, fullIndex);
                    }
                }
            }
        }

        public static void MoveLeft()
        {
            for (int row = 0; row < _boardSize; row++)
            {
                int num = 0, index = 0;

                for (int y = 0; y < _boardSize; y++)
                {
                    int indexOfButton = row * _boardSize + y;

                    if (num == 0)
                    {
                        num = FormGameManager.GetNumAndIndex(indexOfButton, ref index);
                        continue;
                    }

                    int nextNum = FormGameManager.GetNumAndIndex(indexOfButton, ref index);

                    if (num == nextNum)
                    {
                        ButtonEvents.CombineButtons(index, index - 1);
                        num = 0;
                    }
                    else
                    {
                        num = nextNum;
                    }
                }

                int emptyIndex = row * _boardSize;
                int fullIndex = emptyIndex + 1;

                while (fullIndex <= (row + 1) * _boardSize - 1)
                {
                    if (_buttonList[emptyIndex].Text != "")
                    {
                        emptyIndex += 1;
                        fullIndex += 1;
                        continue;
                    }
                    else if (_buttonList[fullIndex].Text == "")
                    {
                        fullIndex += 1;
                        continue;
                    }
                    else
                    {
                        ButtonEvents.SwapButtons(emptyIndex, fullIndex);
                    }
                }
            }
        }

        public static void MoveRight()
        {
            for (int row = 0; row < _boardSize; row++)
            {
                int num = 0, index = 0;

                for (int y = _boardSize - 1; y >= 0; y--)
                {
                    int indexOfButton = row * _boardSize + y;

                    if (num == 0)
                    {
                        num = FormGameManager.GetNumAndIndex(indexOfButton, ref index);
                        continue;
                    }

                    int nextNum = FormGameManager.GetNumAndIndex(indexOfButton, ref index);

                    if (num == nextNum)
                    {
                        ButtonEvents.CombineButtons(index, index + 1);
                        num = 0;
                    }
                    else
                    {
                        num = nextNum;
                    }
                }

                int emptyIndex = (row + 1) * _boardSize - 1;
                int fullIndex = emptyIndex - 1;

                while (fullIndex >= row * _boardSize)
                {
                    if (_buttonList[emptyIndex].Text != "")
                    {
                        emptyIndex -= 1;
                        fullIndex -= 1;
                        continue;
                    }
                    else if (_buttonList[fullIndex].Text == "")
                    {
                        fullIndex -= 1;
                        continue;
                    }
                    else
                    {
                        ButtonEvents.SwapButtons(emptyIndex, fullIndex);
                    }
                }
            }
        }
    }

}
