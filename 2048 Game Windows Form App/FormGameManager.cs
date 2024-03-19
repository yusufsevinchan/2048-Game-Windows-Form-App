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
            string helpHeader = "2048 Oyunu";
            string helpText = "2048 Oyununa Hoş Geldiniz!\r\n\r\n" +
                "- **2048 Nedir?**\r\n2048, dört yönde kaydırma hareketleriyle oynanan bağımlılık yaratan bir bulmaca oyunudur. \r\nAynı numaralı karoları birleştirerek daha büyük ve güçlü bir karo hedefleyin.\r\n\r\n" +
                "- **2048 Nasıl Oynanır?**\r\nKaroları hareket ettirerek aynı numaralıları birleştirerek daha büyük karolar oluşturun. \r\nKarolar, boş hücreye rastlanana kadar hareket eder ve birleşir.\r\n\r\n" +
                "- **2048 Nasıl Kazanılır?**\r\nOyunda 2048 karo elde ederek kazanabilirsiniz. \r\nOyun alanı dolar ve yeni hareket yapacak karo kalmazsa oyun sona erer.\r\n\r\n" +
                "- **Puanlama Sistemi**\r\nHer hamlede puan kazanırsınız. \r\nDaha büyük ve güçlü karolar oluşturarak daha yüksek puanlar alabilirsiniz. \r\nAz hamleyle oyunu bitirirseniz daha yüksek puan elde edersiniz.\r\n\r\n" +
                "- **Stratejiler**\r\n  " +
                "  -Köşeleri Kullanın: Karoları köşelere iterek stratejik bir şekilde oynayın.\r\n  " +
                "  -Odaklanın: Yön seçimlerinizi planlayın ve oyunu kontrol altında tutun.\r\n  " +
                "  -Hızlı ve Doğru Kararlar: Hamlelerinizi iyi düşünerek yapın ve geleceği tahmin edin.\r\n\r\n" +
                "-**Sıkça Sorulan Sorular**\r\n  " +
                "  - Nasıl daha yüksek puan alabilirim?: Daha büyük karolar oluşturarak ve az hamleyle oyunu bitirerek yüksek puanlar kazanabilirsiniz\r\n  " +
                "  - Oyun neden biter?: Hareket edecek başka karo kalmadığında veya tüm alan dolarsa oyun sona erer.\r\n\r\nKeyifli Oyunlar!";

            MessageBox.Show(helpText, helpHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
