using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        Graphics g;
        new Random Rand;

        private Boolean [,] mines;
        private Boolean[,] nums;
        private Boolean[,] DrNums;
        new int [,] numb;
        private Boolean[,] blanks;
        private Boolean[,] Fblanks;
        private Boolean[,] exblanks;
        private Boolean[,] exblanksF;
        private Boolean[,] exists;
        private Boolean[,] marked;
        new Rectangle[,] rect;

        new Font font;
        new Pen bPen, rPen;
        new Brush yBrush, bBrush, blBrush, gBrush, rBrush, pBrush, oBrush, brBrush, piBrush;
        int x, y, count, exblanksN, exblanksFN, mMines, Filled, wCount;

        private Image imageMarked, imageMine;
        private Boolean mineClicked, Lose, Win, Easy, Medium, Hard;

        public Form1()
        {
            InitializeComponent();

           // g = this.CreateGraphics();
            Rand = new Random();

            font = new Font("Times New Roman", 12);
            bPen = new Pen(Color.Black);
            rPen = new Pen(Color.Red);
            yBrush = new SolidBrush(Color.Yellow);
            bBrush = new SolidBrush(Color.Black);
            blBrush = new SolidBrush(Color.Blue);
            gBrush = new SolidBrush(Color.Green);
            rBrush = new SolidBrush(Color.Red);
            pBrush = new SolidBrush(Color.Purple);
            oBrush = new SolidBrush(Color.Orange);
            brBrush = new SolidBrush(Color.Brown);
            piBrush = new SolidBrush(Color.Pink);

            mines = new Boolean[21, 21];
            nums = new Boolean[21, 21];
            DrNums = new Boolean[21, 21];
            numb = new int[21, 21];
            blanks = new Boolean[21, 21];
            Fblanks = new Boolean[21, 21];
            exblanks = new Boolean[21, 21];
            exblanksF = new Boolean[21, 21];
            exists = new Boolean[21, 21];
            marked = new Boolean[21, 21];
            rect = new Rectangle[21, 21];

            mineClicked = false;

            imageMarked = new Bitmap("MarkedMine.png");
            imageMine = new Bitmap("Mine.gif");

            count = 0;
            exblanksN = 0;
            exblanksFN = 0;
            Filled = 0;
            wCount = 0;

            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    exists[i, j] = false;
                    DrNums[i, j] = false;
                    Fblanks[i, j] = false;
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    g.DrawRectangle(bPen, rect[i, j]);

                    if (DrNums[i, j] == true && exists[i, j] == true)
                    {
                        g.FillRectangle(yBrush, rect[i, j]);

                        g.DrawString(numb[i, j].ToString(), font, bBrush, (i * 20) + 2, (j * 20) + 2);

                        if (numb[i, j] == 1)
                        {
                            g.DrawString(numb[i, j].ToString(), font, blBrush, (i * 20) + 2, (j * 20) + 2);
                        }

                        else if (numb[i, j] == 2)
                        {
                            g.DrawString(numb[i, j].ToString(), font, gBrush, (i * 20) + 2, (j * 20) + 2);
                        }

                        else if (numb[i, j] == 3)
                        {
                            g.DrawString(numb[i, j].ToString(), font, rBrush, (i * 20) + 2, (j * 20) + 2);
                        }

                        else if (numb[i, j] == 4)
                        {
                            g.DrawString(numb[i, j].ToString(), font, pBrush, (i * 20) + 2, (j * 20) + 2);
                        }

                        else if (numb[i, j] == 5)
                        {
                            g.DrawString(numb[i, j].ToString(), font, oBrush, (i * 20) + 2, (j * 20) + 2);
                        }

                        else if (numb[i, j] == 6)
                        {
                            g.DrawString(numb[i, j].ToString(), font, brBrush, (i * 20) + 2, (j * 20) + 2);
                        }

                        else if (numb[i, j] == 7)
                        {
                            g.DrawString(numb[i, j].ToString(), font, piBrush, (i * 20) + 2, (j * 20) + 2);
                        }
                    }

                    else if (Fblanks[i, j] == true && exists[i, j] == true)
                    {
                        g.FillRectangle(yBrush, rect[i, j]);
                    }

                    else if (mineClicked == true && mines[i, j] == true)
                    {
                        g.DrawImage(imageMine, i * 20, j * 20);
                    }

                    else if (marked[i, j] == true && exists[i, j] == true)
                    {
                        g.DrawImage(imageMarked, i * 20, j * 20);
                    }
                }
            }

            if (Lose == true)
            {
                g.DrawString("You Lose!", new Font("Times New Roman", 20), rBrush, 100, 450);
            }

            else if (Win == true)
            {
                g.DrawString("You Win!", new Font("Times New Roman", 20), gBrush, 100, 450);
                MessageBox.Show("You Win!");
            }

            label1.Text = "Marked Mines: " + mMines.ToString();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (Lose == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        for (int j = 1; j < 21; j++)
                        {
                            if (rect[i, j].Contains(e.X, e.Y))
                            {
                                if (mines[i, j] == true)
                                {
                                    mineClicked = true;
                                    Lose = true;
                                    break;
                                }

                                else if (nums[i, j] == true)
                                {
                                    DrNums[i, j] = true;
                                    exists[i, j] = true;

                                    if (marked[i, j] == true)
                                    {
                                        marked[i, j] = false;
                                        mMines++;
                                    }
                                }

                                else if (blanks[i, j] == true)
                                {
                                    exists[i, j] = true;

                                    if (marked[i, j] == true)
                                    {
                                        marked[i, j] = false;
                                        mMines++;
                                    }

                                    if (exblanksF[i, j] == false)
                                    {
                                        Fblanks[i, j] = true;
                                        exists[i, j] = true;

                                        exblanksF[i, j] = true;
                                        exblanksFN++;

                                        if (marked[i, j] == true)
                                        {
                                            marked[i, j] = false;
                                            mMines++;
                                        }

                                        for (int a = 1; a > -2; a--)
                                        {
                                            for (int b = 1; b > -2; b--)
                                            {
                                                if (i - a < 0 || j - b < 0)
                                                {

                                                }

                                                else if (i - a > 19 || j - b > 20)
                                                {

                                                }

                                                else if (blanks[i - a, j - b] == true && exblanks[i - a, j - b] == false)
                                                {
                                                    Fblanks[i - a, j - b] = true;
                                                    exblanks[i - a, j - b] = true;
                                                    exblanksN++;
                                                    exists[i - a, j - b] = true;

                                                    if (marked[i - a, j - b] == true)
                                                    {
                                                        marked[i - a, j - b] = false;
                                                        mMines++;
                                                    }
                                                }

                                                else if (nums[i - a, j - b] == true)
                                                {
                                                    DrNums[i - a, j - b] = true;
                                                    exists[i - a, j - b] = true;

                                                    if (marked[i - a, j - b] == true)
                                                    {
                                                        marked[i - a, j - b] = false;
                                                        mMines++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    while (exblanksFN != exblanksN)
                    {
                        for (int R = 0; R < 20; R++)
                        {
                            for (int S = 1; S < 21; S++)
                            {
                                if (exblanks[R, S] == true && exblanksF[R, S] == false)
                                {
                                    exblanksFN++;
                                    exblanksF[R, S] = true;
                                    exists[R, S] = true;

                                    if (marked[R, S] == true)
                                    {
                                        marked[R, S] = false;
                                        mMines++;
                                    }

                                    for (int T = 1; T > -2; T--)
                                    {
                                        for (int U = 1; U > -2; U--)
                                        {
                                            if (R - T < 0 || S - U < 0)
                                            {

                                            }

                                            else if (R - T > 19 || S - U > 20)
                                            {

                                            }

                                            else if (blanks[R - T, S - U] == true && exblanks[R - T, S - U] == false)
                                            {
                                                Fblanks[R - T, S - U] = true;
                                                exblanks[R - T, S - U] = true;
                                                exblanksN++;
                                                exists[R - T, S - U] = true;

                                                if (marked[R - T, S - U] == true)
                                                {
                                                    marked[R - T, S - U] = false;
                                                    mMines++;
                                                }
                                            }

                                            else if (nums[R - T, S - U] == true)
                                            {
                                                DrNums[R - T, S - U] = true;
                                                exists[R - T, S - U] = true;

                                                if (marked[R - T, S - U] == true)
                                                {
                                                    marked[R - T, S - U] = false;
                                                    mMines++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (exblanksN == exblanksFN)
                        {
                            break;
                        }
                    }
                }


                else if (e.Button == MouseButtons.Right)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        for (int j = 1; j < 21; j++)
                        {
                            if (rect[i, j].Contains(e.X, e.Y) && exists[i, j] == true && marked[i, j] == false && DrNums[i, j] == false && Fblanks[i, j] == false && mMines > 0)
                            {
                                marked[i, j] = true;
                                mMines--;
                                break;
                            }

                            else if (rect[i, j].Contains(e.X, e.Y) && exists[i, j] == true && marked[i, j] == true && DrNums[i, j] == false && Fblanks[i, j] == false)
                            {
                                marked[i, j] = false;
                                mMines++;
                                break;
                            }
                        }
                    }
                }

              //  Filled = 0;


             /*   if (Filled == 85)
                {
                    Win = true;
                }  */
             
                wCount = 0;

                if (Easy == true)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 1; j < 11; j++)
                        {
                        /*    if (exists[i, j] == true || DrNums[i, j] == true || Fblanks[i, j] == true || exblanks[i, j] == true)
                            {
                                Filled++;
                            } */

                            if (mMines == 0 && mines[i, j] == true && marked[i, j] == true)
                            {
                                wCount++;
                            }
                        }
                    }

                    if (wCount == 15)
                    {
                        Win = true;
                    }
                }

                if (Medium == true)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        for (int j = 1; j < 16; j++)
                        {
                            if (mMines == 0 && mines[i, j] == true && marked[i, j] == true)
                            {
                                wCount++;
                            }
                        }
                    }

                    if (wCount == 35)
                    {
                        Win = true;
                    }
                }

                if (Hard == true)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        for (int j = 1; j < 21; j++)
                        {
                            if (mMines == 0 && mines[i, j] == true && marked[i, j] == true)
                            {
                                wCount++;
                            }
                        }
                    }

                    if (wCount == 75)
                    {
                        Win = true;
                    }
                }

                Invalidate();
            }
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Easy = true;
            Medium = false;
            Hard = false;

            mineClicked = false;

            count = 0;
            exblanksN = 0;
            exblanksFN = 0;
            Filled = 0;
            wCount = 0;

            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    mines[i, j] = false;
                    nums[i, j] = false;
                    DrNums[i, j] = false;
                    numb[i, j] = 0;
                    blanks[i, j] = false;
                    Fblanks[i, j] = false;
                    exblanks[i, j] = false;
                    exblanksF[i, j] = false;
                    exists[i, j] = false;
                    marked[i, j] = false;
                    rect[i, j] = new Rectangle(0, 0, 0, 0);

                    Lose = false;
                    Win = false;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    rect[i, j] = new Rectangle(i * 20, j * 20, 20, 20);
                }
            }

            for (int i = 0; i < 15; i++)
            {
                x = Rand.Next(0, 10);
                y = Rand.Next(1, 11);

                while (mines[x, y] == true)
                {
                    x = Rand.Next(0, 10);
                    y = Rand.Next(1, 11);
                }

                if (exists[x, y] == false)
                {
                    mines[x, y] = true;
                    exists[x, y] = true;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    count = 0;

                    if (exists[i, j] == true)
                    {

                    }

                    else if (exists[i, j] == false)
                    {
                        for (int a = 1; a > -2; a--)
                        {
                            for (int b = 1; b > -2; b--)
                            {
                                if (i - a < 0 || j - b < 0)
                                {

                                }

                                else if (mines[i - a, j - b] == true)
                                {
                                    count++;
                                }
                            }
                        }

                        if (count > 0)
                        {
                            nums[i, j] = true;
                            exists[i, j] = true;
                            numb[i, j] = count;
                        }

                        else if (count == 0)
                        {
                            blanks[i, j] = true;
                            exists[i, j] = true;
                        }
                    }
                }
            }

            mMines = 15;
            label1.Text = "Marked Mines: " + mMines.ToString();
            Invalidate();
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Easy = false;
            Medium = true;
            Hard = false;

            mineClicked = false;

            count = 0;
            exblanksN = 0;
            exblanksFN = 0;
            Filled = 0;
            wCount = 0;

            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    mines[i, j] = false;
                    nums[i, j] = false;
                    DrNums[i, j] = false;
                    numb[i, j] = 0;
                    blanks[i, j] = false;
                    Fblanks[i, j] = false;
                    exblanks[i, j] = false;
                    exblanksF[i, j] = false;
                    exists[i, j] = false;
                    marked[i, j] = false;
                    rect[i, j] = new Rectangle(0, 0, 0, 0);

                    Lose = false;
                    Win = false;
                }
            }

            for (int i = 0; i < 15; i++)
            {
                for (int j = 1; j < 16; j++)
                {
                    rect[i, j] = new Rectangle(i * 20, j * 20, 20, 20);
                }
            }

            for (int i = 0; i < 35; i++)
            {
                x = Rand.Next(0, 15);
                y = Rand.Next(1, 16);

                while (mines[x, y] == true)
                {
                    x = Rand.Next(0, 15);
                    y = Rand.Next(1, 16);
                }

                if (exists[x, y] == false)
                {
                    mines[x, y] = true;
                    exists[x, y] = true;
                }
            }

            for (int i = 0; i < 15; i++)
            {
                for (int j = 1; j < 16; j++)
                {
                    count = 0;

                    if (exists[i, j] == true)
                    {

                    }

                    else if (exists[i, j] == false)
                    {
                        for (int a = 1; a > -2; a--)
                        {
                            for (int b = 1; b > -2; b--)
                            {
                                if (i - a < 0 || j - b < 0)
                                {

                                }

                                else if (mines[i - a, j - b] == true)
                                {
                                    count++;
                                }
                            }
                        }

                        if (count > 0)
                        {
                            nums[i, j] = true;
                            exists[i, j] = true;
                            numb[i, j] = count;
                        }

                        else if (count == 0)
                        {
                            blanks[i, j] = true;
                            exists[i, j] = true;
                        }
                    }
                }
            }

            mMines = 35;
            label1.Text = "Marked Mines: " + mMines.ToString();
            Invalidate();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Easy = false;
            Medium = false;
            Hard = true;

            mineClicked = false;

            count = 0;
            exblanksN = 0;
            exblanksFN = 0;
            Filled = 0;
            wCount = 0;

            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    mines[i, j] = false;
                    nums[i, j] = false;
                    DrNums[i, j] = false;
                    numb[i, j] = 0;
                    blanks[i, j] = false;
                    Fblanks[i, j] = false;
                    exblanks[i, j] = false;
                    exblanksF[i, j] = false;
                    exists[i, j] = false;
                    marked[i, j] = false;
                    rect[i, j] = new Rectangle(0, 0, 0, 0);

                    Lose = false;
                    Win = false;
                }
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 1; j < 21; j++)
                {
                    rect[i, j] = new Rectangle(i * 20, j * 20, 20, 20);
                }
            }

            for (int i = 0; i < 75; i++)
            {
                x = Rand.Next(0, 20);
                y = Rand.Next(1, 21);

                while (mines[x, y] == true)
                {
                    x = Rand.Next(0, 20);
                    y = Rand.Next(1, 21);
                }

                if (exists[x, y] == false)
                {
                    mines[x, y] = true;
                    exists[x, y] = true;
                }
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 1; j < 21; j++)
                {
                    count = 0;

                    if (exists[i, j] == true)
                    {

                    }

                    else if (exists[i, j] == false)
                    {
                        for (int a = 1; a > -2; a--)
                        {
                            for (int b = 1; b > -2; b--)
                            {
                                if (i - a < 0 || j - b < 0)
                                {

                                }

                                else if(i - a > 19 || j - b > 20)
                                {

                                }

                                else if (mines[i - a, j - b] == true)
                                {
                                    count++;
                                }
                            }
                        }

                        if (count > 0)
                        {
                            nums[i, j] = true;
                            exists[i, j] = true;
                            numb[i, j] = count;
                        }

                        else if (count == 0)
                        {
                            blanks[i, j] = true;
                            exists[i, j] = true;
                        }
                    }
                }
            }

            mMines = 75;
            label1.Text = "Marked Mines: " + mMines.ToString();
            Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
