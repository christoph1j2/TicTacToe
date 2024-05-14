using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public byte[,] xOro = new byte[3, 3];
        Random zgen = new Random();
        int player, ki;
        Button[,] but = new Button[3, 3];
        private bool r1;
        private bool r2;

        private void Fenster_Loaded(object sender, RoutedEventArgs e)
        {
            player = zgen.Next(2) + 1;

            if(player == 1)
            {
                ki = 2;
            }
            else
            {
                ki = 1;
            }

            for(byte i = 0; i < xOro.GetLength(0); ++i)
            {
                for (byte j = 0; j < xOro.GetLength(1); j++)
                {
                    xOro[i, j] = 0;
                }
            }
        }
        private void Can_Loaded(object sender, RoutedEventArgs e)
        {
            but[0, 0] = B00;
            but[0, 1] = B01;
            but[0, 2] = B02;
            but[1, 0] = B10;
            but[1, 1] = B11;
            but[1, 2] = B12;
            but[2, 0] = B20;
            but[2, 1] = B21;
            but[2, 2] = B22;

            if (player == 2)
            {
                KI();
            }
        }
        public void Change(int executor, int x, int y)
        {
            if (executor == 1)
            {
                but[x, y].Content = "X";
            }
            else
            {
                but[x, y].Content = "O";
            }

            if (Victory())
            {
                return;
            }
            if (executor==player)
            {
                KI();
            }
        }
        public enum Orientation
        {
            Column,
            Row,
            Slash,
            Backslash
        }
        public bool Victory()
        {
            if (IsEnd(Orientation.Column))
            {
                return true;
            }
            if (IsEnd(Orientation.Row))
            {
                return true;
            }
            if (IsEndSlashOrBackslash(Orientation.Backslash))
            {
                return true;
            }
            if (IsEndSlashOrBackslash(Orientation.Slash))
            {
                return true;
            }
            if (IsDraw())
            {
                return true;
            }

            return false;
        }
        public bool IsEndSlashOrBackslash(Orientation orient)
        {
            byte s = 0;
            byte k = 0;
            for(byte i = 0; i<xOro.GetLength(0); ++i)
            {
                if (xOro[i, (orient == Orientation.Backslash ? i : ((xOro.GetLength(0) - 1) - i))] == 1)
                {
                    ++s;
                }
                if(xOro[i, (orient == Orientation.Backslash ? i : ((xOro.GetLength(0) - 1) - i))] == 2)
                {
                    ++k;
                }
                if (s == xOro.GetLength(0))
                {
                    GameEnd(1);
                    return true;
                }
                if (k == xOro.GetLength(0))
                {
                    GameEnd(2);
                    return true;
                }
            }
            return false;
        }
        public bool IsDraw()
        {
            byte u = 0;
            for (int i = 0; i < xOro.GetLength(0); i++)
            {
                for (int j = 0; j < xOro.GetLength(1); j++)
                {
                    if (xOro[i, j] != 0)
                    {
                        ++u;
                    }
                }
            }
            if (u == xOro.Length)
            {
                GameEnd(0);
                return true;
            }
            return false;
        }
        public bool IsEnd(Orientation orient)
        {
            byte s = 0;
            byte k = 0;
            for (byte j = 0; j < xOro.GetLength(1); ++j)
            {
                for (byte i = 0; i < xOro.GetLength(0); ++i)
                {
                    if (xOro[orient == Orientation.Column ? j : i, (orient == Orientation.Row) ? j : i] == 1)
                    {
                        ++s;
                    }
                    if (xOro[orient == Orientation.Column ? j : i, (orient == Orientation.Row) ? j : i] == 2)
                    {
                        ++k;
                    }
                    if (s == xOro.GetLength(0))
                    {
                        GameEnd(1);
                        return true;
                    }
                    if (k==xOro.GetLength(0))
                    {
                        GameEnd(2);
                        return true;
                    }
                }
                s = 0;
                k = 0;
            }
            return false;
        }
        public void GameEnd(byte winner)
        {
            if (player == 1)
            {
                if(winner == 1)
                {
                    MessageBox.Show("Player (X) wins!");
                }
                else if (winner==2)
                {
                    MessageBox.Show("KI (O) wins!");
                }
                else
                {
                    MessageBox.Show("Draw");
                }
            }
            else if (player == 2)
            {
                if (winner == 1)
                {
                    MessageBox.Show("KI (X) wins!");
                }
                else if (winner == 2)
                {
                    MessageBox.Show("Player (O) wins!");
                }
                else
                {
                    MessageBox.Show("Draw");
                }
            }
            Restart();
        }
        public void Restart()
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Retry?", "Confirmation", System.Windows.Forms.MessageBoxButtons.YesNo);
            if(result == System.Windows.Forms.DialogResult.Yes)
            {
                Reset();
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
                Environment.Exit(0);
            }
        }
        public void Reset()
        {
            player = zgen.Next(2) + 1;
            if (player == 1)
            {
                ki = 2;
            }
            else
            {
                ki = 1;
            }

            for (int i = 0; i < xOro.GetLength(0); i++)
            {
                for (int j = 0; j < xOro.GetLength(1); j++)
                {
                    xOro[i, j] = 0;
                    but[i, j].Content = "";
                }
            }

            if (player == 2)
            {
                KI();
            }
        }
        private void B00_Click(object sender, RoutedEventArgs e)
        {
            if (xOro[0, 0] == 0)
            {
                xOro[0, 0] = (byte)player;
                Change(xOro[0, 0], 0, 0);
            }
        }
        private void B10_Click(object sender, RoutedEventArgs e)
        {
            if (xOro[1, 0] == 0)
            {
                xOro[1, 0] = (byte)player;
                Change(xOro[1, 0], 1, 0);
            }
        }
        private void B20_Click(object sender, RoutedEventArgs e)
        {
            if (xOro[2, 0] == 0)
            {
                xOro[2, 0] = (byte)player;
                Change(xOro[2, 0], 2, 0);
            }
        }
        private void B01_Click(object sender, RoutedEventArgs e)
        {
            if (xOro[0, 1] == 0)
            {
                xOro[0, 1] = (byte)player;
                Change(xOro[0, 1], 0, 1);
            }
        }
        private void B21_Click(object sender, RoutedEventArgs e)
        {
            if (xOro[2, 1] == 0)
            {
                xOro[2, 1] = (byte)player;
                Change(xOro[2, 1],2, 1);
            }
        }
        private void B02_Click(object sender, RoutedEventArgs e)
        {
            if (xOro[0, 2] == 0)
            {
                xOro[0, 2] = (byte)player;
                Change(xOro[0, 2], 0, 2);
            }
        }
        private void B12_Click(object sender, RoutedEventArgs e)
        {
            if (xOro[1, 2] == 0)
            {
                xOro[1, 2] = (byte)player;
                Change(xOro[1, 2], 1, 2);
            }
        }
        private void B22_Click(object sender, RoutedEventArgs e)
        {
            if (xOro[2, 2] == 0)
            {
                xOro[2, 2] = (byte)player;
                Change(xOro[2, 2], 2, 2);
            }
        }
        private void B11_Click(object sender, RoutedEventArgs e)
        {
            if (xOro[1, 1] == 0)
            {
                xOro[1, 1] = (byte)player;
                Change(xOro[1, 1], 1, 1);
            }
        }

        public void KI()
        {
            if (zgen.Next(20) > 18)
            {
                KI_Random();
            }
            else
            {
                CheckRows();
            }
        }
        public void KI_Random()
        {
            int i = zgen.Next(3), j = zgen.Next(3);
            if (xOro[i, j] == 0)
            {
                xOro[i, j] = (byte)ki;
                Change(ki, i, j);
            }
            else
            {
                KI_Random();
            }
        }
        public void CheckRows()
        {
            Rows[] ro = new Rows[8];

            ro[0] = new Rows(new int[3] { xOro[0, 0], 0, 0 }, new int[3] { xOro[0, 1], 0, 1 }, new int[3] { xOro[0, 2], 0, 2 });
            ro[1] = new Rows(new int[3] { xOro[1, 0], 1, 0 }, new int[3] { xOro[1, 1], 1, 1 }, new int[3] { xOro[1, 2], 1, 2 });
            ro[2] = new Rows(new int[3] { xOro[2, 0], 2, 0 }, new int[3] { xOro[2, 1], 2, 1 }, new int[3] { xOro[2, 2], 2, 2 });
            ro[3] = new Rows(new int[3] { xOro[0, 0], 0, 0 }, new int[3] { xOro[1, 1], 1, 1 }, new int[3] { xOro[2, 2], 2, 2 });
            ro[4] = new Rows(new int[3] { xOro[0, 2], 0, 2 }, new int[3] { xOro[1, 1], 1, 1 }, new int[3] { xOro[2, 0], 2, 0 });
            ro[5] = new Rows(new int[3] { xOro[0, 0], 0, 0 }, new int[3] { xOro[1, 0], 1, 0 }, new int[3] { xOro[2, 0], 2, 0 });
            ro[6] = new Rows(new int[3] { xOro[0, 1], 0, 1 }, new int[3] { xOro[1, 1], 1, 1 }, new int[3] { xOro[2, 1], 2, 1 });
            ro[7] = new Rows(new int[3] { xOro[0, 2], 0, 2 }, new int[3] { xOro[1, 2], 1, 2 }, new int[3] { xOro[2, 2], 2, 2 });

            if (ProveVictoryOrDefeatKI(ro, ki))
            {
                return;
            }
            if (ProveVictoryOrDefeatKI(ro, player))
            {
                return;
            }
            if (EdgeTrick())
            {
                return;
            }
            r1 = false;
            KI_Random();
        }

        public bool ProveVictoryOrDefeatKI(Rows[] ro,int wer)
        {
            for (int i = 0; i < ro.Length; ++i)
            {
                int[] rf = ro[i].GetData(wer);

                if (rf[0] == 2 && xOro[rf[1], rf[2]] == 0)
                {
                    xOro[rf[1], rf[2]] = (byte)ki;
                    Change(ki, rf[1], rf[2]);
                    return true;
                }
            }
            return false;
        }
        public bool EdgeTrick()
        {
            if (r1 && (xOro[0, 0] == player || xOro[0, 2] == player || xOro[2, 0] == player || xOro[2, 2] == player))
            {
                xOro[1, 1] = (byte)ki;
                Change(ki, 1, 1);
                r1 = false;
                r2 = true;
                return true;
            }
            if (r2)
            {
                RandomRand();
                return true;
            }
            return false;
        }

        public void RandomRand()
        {
            switch (zgen.Next(4))
            {
                case 0:
                    SetzeRandomRand(0, 1);
                    break;
                case 1:
                    SetzeRandomRand(2, 1);
                    break;
                case 2:
                    SetzeRandomRand(1, 0);
                    break;
                case 3:
                    SetzeRandomRand(1, 2);
                    break;
            }
        }

        public void SetzeRandomRand(int i,int j)
        {
            if (xOro[i, j] == 0)
            {
                xOro[i, j] = (byte)ki;
                Change(ki, i, j);
                r2 = false;
            }
            else
            {
                RandomRand();
            }
        }
    }
    public class Rows
    {
        int[] x1, x2, x3;

        public Rows(int[] x, int[] y, int[] z)
        {
            x1 = x;
            x2 = y;
            x3 = z;
        }
        public int[] GetData(int player)
        {
            int[] ret = new int[3];

            ret[0] = 0;

            if (x1[0] == player)
            {
                ++ret[0];
            }
            else
            {
                ret[1] = x1[1];
                ret[2] = x1[2];
            }
            if (x2[0] == player)
            {
                ++ret[0];
            }
            else
            {
                ret[1] = x2[1];
                ret[2] = x2[2];
            }
            if (x3[0] == player)
            {
                ++ret[0];
            }
            else
            {
                ret[1] = x3[1];
                ret[2] = x3[2];
            }
            return ret;
        }
    }
}
