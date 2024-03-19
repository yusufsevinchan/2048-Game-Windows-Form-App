using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048_Game_Windows_Form_App
{
    public partial class FormGameManager : Form
    {
        public const int boardSize = 4;
        public static readonly Color backColorDefault = Color.CadetBlue;
        public const string GameOverText = "Game Over";
        public const string GameRestartText = "Restart";
        public bool keepPlaying = true;

        public static List<Button> buttonList = new List<Button>();
        public static List<int> emptyButtonIndexes = new List<int>();
        static Random random = new Random();
        public FormGameManager()
        {
            InitializeComponent();
        }

        private void FormGameManager_Load(object sender, EventArgs e)
        {
            ComponentCreator componentCreator = new ComponentCreator(boardSize, backColorDefault);
            buttonList = componentCreator.GetButtonList();
            Controls.AddRange(buttonList.ToArray());

            var randomEmptyButtonIndexes = GetRandomEmptyButtonIndexes(2);
            FillButtons("2", randomEmptyButtonIndexes);
        }
        public static int[] GetRandomEmptyButtonIndexes(int count)
        {
            FillEmptyButtonIndexes();
            if (!emptyButtonIndexes.Any() || count <= 0) return Array.Empty<int>();

            var randomIndexes = new List<int>();

            while (randomIndexes.Count < count && emptyButtonIndexes.Any())
            {
                int index = random.Next(0, emptyButtonIndexes.Count);
                randomIndexes.Add(emptyButtonIndexes[index]);
                emptyButtonIndexes.RemoveAt(index);
            }

            return randomIndexes.ToArray();
        }
        public static void FillButtons(string buttonText, params int[] indexes)
        {
            foreach (var index in indexes)
            {
                var button = buttonList.FirstOrDefault(btn => btn.TabIndex == index);
                if (button != null)
                {
                    button.Text = buttonText;
                    ButtonColorManager.SetButtonBackColor(button);
                }
            }
        }

        private void GameManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (keepPlaying)
            {
                HandleKeyPress(e.KeyCode);
                GameOverCheck();
            }
        }
        private void HandleKeyPress(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    TileMovementManager.MoveUp();
                    break;
                case Keys.Down:
                    TileMovementManager.MoveDown();
                    break;
                case Keys.Right:
                    TileMovementManager.MoveRight();
                    break;
                case Keys.Left:
                    TileMovementManager.MoveLeft();
                    break;
            }

            ButtonEvents.AddRandomDefaultButton();

            FillEmptyButtonIndexes();
        }
        public static int GetNumAndIndex(int index, ref int storedIndex)
        {
            string firstStr = buttonList[index].Text;
            if (firstStr != "")
            {
                storedIndex = index;
                return int.Parse(firstStr);
            }

            return 0;
        }
        private static void FillEmptyButtonIndexes()
        {
            emptyButtonIndexes.Clear();

            //get empty button indexes
            emptyButtonIndexes.AddRange(buttonList.Where(button => button.Text == "").Select(button => button.TabIndex));
        }

        private void GameOverCheck()
        {
            if (emptyButtonIndexes.Count == 0)
            {
                if (IsHorizontalGameOver() && IsVerticalGameOver())
                {
                    GameOver();
                    CreateRestartButton();
                }
            }
        }

        private bool IsHorizontalGameOver()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize - 1; j++)
                {
                    int currentIndex = boardSize * i + j;
                    int nextIndex = boardSize * i + j + 1;

                    if (buttonList[currentIndex].Text == buttonList[nextIndex].Text)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsVerticalGameOver()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize - 1; j++)
                {
                    int currentIndex = boardSize * j + i;
                    int nextIndex = boardSize * (j + 1) + i;

                    if (buttonList[currentIndex].Text == buttonList[nextIndex].Text)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        private void GameOver()
        {
            keepPlaying = false;
            MessageBox.Show(GameOverText, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void CreateRestartButton()
        {
            string btnName = "btn" + GameRestartText;

            Button btn = new Button();
            btn.BackColor = Color.AliceBlue;
            btn.Font = new Font("Forte", 20f, FontStyle.Bold);
            btn.Height = 80;
            btn.Width = 180;
            btn.Location = new Point(Left, Top);
            btn.Left = (ClientSize.Width - btn.Width) / 2;
            btn.Top = (ClientSize.Height - btn.Height) / 2;
            btn.Name = btnName;
            btn.Text = GameRestartText;
            btn.Click += new EventHandler(ButtonRestart_Click);

            Controls.Add(btn);
            foreach (Control control in Controls)
            {
                if (control.Name == btnName)
                {
                    (control as Button).BringToFront();
                }
            }
        }
        private void ButtonRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void FormGameManager_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            string helpHeader, helpText;

            try
            {
                string helpFileName = "HelpText.txt";
                string path = Path.Combine(Application.StartupPath, helpFileName);
                string[] textFileds = File.ReadAllText(path).Split(';');

                helpHeader = textFileds[0];
                helpText = textFileds[1];
            }
            catch
            {
                MessageBox.Show("An error occured while retrieving help texts!");
                return;
            }

            MessageBox.Show(helpText, helpHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
