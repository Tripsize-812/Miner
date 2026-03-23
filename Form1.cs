using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Miner
{
    public partial class Form1 : Form
    {
        private Button[] buttons;  // Массив для ссылок на кнопки
        private bool[] hasMine;
        private int minesCount = 2;
        private bool gameOver = false;

        public Form1()
        {
            InitializeComponent();  // ОСТАВЛЯЕМ, он создает кнопки из дизайнера
            InitializeGame();
        }

        private void InitializeGame()
        {

            buttons = new Button[6];
            hasMine = new bool[6];

            // Заполняем массив существующими кнопками
            buttons[0] = button1;  // button1 - имя кнопки на форме
            buttons[1] = button2;
            buttons[2] = button3;
            buttons[3] = button4;
            buttons[4] = button5;
            buttons[5] = button6;



            // Размещаем мины случайно
            Random rand = new Random();
            for (int i = 0; i < minesCount; i++)
            {
                int pos;
                do
                {
                    pos = rand.Next(6);
                } while (hasMine[pos]);
                hasMine[pos] = true;
            }

            // Настраиваем каждую кнопку
            for (int i = 0; i < 6; i++)
            {
                buttons[i].Tag = i;  // Устанавливаем Tag
                buttons[i].Click += Button_Click;  // Добавляем обработчик
                buttons[i].BackColor = Color.Fuchsia;
                buttons[i].Enabled = true;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (gameOver) return;

            Button btn = sender as Button;

            // Проверка на null
            if (btn == null || btn.Tag == null)
            {
                MessageBox.Show("Ошибка: кнопка не инициализирована");
                return;
            }

            int index = (int)btn.Tag;

            if (hasMine[index])
            {
                btn.Text = "МИНА!";
                btn.BackColor = Color.Red;
                this.BackColor = Color.Red;
                gameOver = true;

                foreach (Button b in buttons)
                {
                    b.Enabled = false;
                }

                MessageBox.Show("Вы наступили на мину! Игра окончена!", "Game Over",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btn.Text = "Безопасно";
                btn.BackColor = Color.LightGreen;
                btn.Enabled = false;

                bool allSafePressed = true;
                for (int i = 0; i < 6; i++)
                {
                    if (!hasMine[i] && buttons[i].Enabled)
                    {
                        allSafePressed = false;
                        break;
                    }
                }

                if (allSafePressed)
                {
                    gameOver = true;
                    this.BackColor = Color.LightGreen;
                    MessageBox.Show("Поздравляю! Вы нашли все безопасные кнопки!", "Победа!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            gameOver = false;
            this.BackColor = Color.Fuchsia;

            Random rand = new Random();
            for (int i = 0; i < 6; i++)
            {
                hasMine[i] = false;
            }

            for (int i = 0; i < minesCount; i++)
            {
                int pos;
                do
                {
                    pos = rand.Next(6);
                } while (hasMine[pos]);
                hasMine[pos] = true;
            }

            for (int i = 0; i < 6; i++)
            {
                buttons[i].Text = "";
                buttons[i].BackColor = Color.Fuchsia;
                buttons[i].Enabled = true;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
