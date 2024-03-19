using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048_Game_Windows_Form_App
{
    public class ButtonColorManager
    {
        public static Color ColorDefault = FormGameManager.backColorDefault;
        private static Color Color2 = Color.FromArgb(142, 198, 63);
        private static Color Color4 = Color.FromArgb(59, 182, 75);
        private static Color Color8 = Color.FromArgb(0, 166, 80);
        private static Color Color16 = Color.FromArgb(0, 166, 116);
        private static Color Color32 = Color.FromArgb(1, 168, 158);
        private static Color Color64 = Color.FromArgb(1, 172, 198);
        private static Color Color128 = Color.FromArgb(0, 174, 237);
        private static Color Color256 = Color.FromArgb(1, 142, 213);
        private static Color Color512 = Color.FromArgb(0, 113, 186);
        private static Color Color1024 = Color.FromArgb(0, 84, 165);
        private static Color Color2048 = Color.FromArgb(46, 48, 148);

        public static readonly Dictionary<string, Color> buttonColors = new Dictionary<string, Color>
        {
            {"", ColorDefault },
            { "2", Color2 },
            { "4", Color4 },
            { "8", Color8 },
            { "16", Color16 },
            { "32", Color32 },
            { "64", Color64 },
            { "128", Color128 },
            { "256", Color256 },
            { "512", Color512 },
            { "1024", Color1024 },
            { "2048", Color2048 }
        };

        public static void SetButtonBackColor(Button btn)
        {
            if (buttonColors.TryGetValue(btn.Text, out Color color))
            {
                btn.BackColor = color;
            }
        }
    }
}
