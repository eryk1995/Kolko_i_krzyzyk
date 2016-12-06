using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kolko_krzyzyk
{
    public partial class Form1 : Form
    {

        enum ruchGracza { Nikt, Gracz1, Gracz2 };
        ruchGracza ruch;

        enum Zwyciezca { Nikt, Gracz1, Gracz2, Remis }
        Zwyciezca zwyciezca;

        void nowaGra()
        {
            PictureBox[] wszystkieObrazki =
           {
                box0,
                box1,
                box2,
                box3,
                box4,
                box5,
                box6,
                box7,
                box8
            };
            //Wyczyść wszystkie miejsca na planszy
            foreach (var p in wszystkieObrazki)
                p.Image = null;

            ruch = ruchGracza.Gracz1;
            zwyciezca = Zwyciezca.Nikt;
            pokazRuch();

        }
        public Form1()
        {
            InitializeComponent();
        }

        void pokazRuch()
        {
            string status = "";
            string msg = "";

            switch (zwyciezca)
            {
                case Zwyciezca.Nikt:
                    if (ruch == ruchGracza.Gracz1)
                        status = "Ruch: Gracz 1";
                    else
                        status = "Ruch: Gracz 2";
                    break;

                case Zwyciezca.Gracz1:
                    msg = status = "Wygrał Gracz 1!";
                    break;

                case Zwyciezca.Gracz2:
                    msg = status = "Wygrał Gracz 2!";
                    break;

                case Zwyciezca.Remis:
                    msg = status = "Niestety, nikt nie wygrał :(!";
                    break;
            }
            if (msg != "")
            {
                MessageBox.Show(msg, "Zwycięzca!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            lblStatus.Text = status;
        }

        private void OnClick(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;

            if(p.Image != null)
                return;

            if (ruch == ruchGracza.Nikt)
                return;

            if (ruch == ruchGracza.Gracz1)
                p.Image = gracz1.Image;
            else
                p.Image = gracz2.Image;

            //Sprawdza kto wygrał
            zwyciezca = sprawdzZwyciezce();
            if (zwyciezca == Zwyciezca.Nikt)
            {
                //Zmiana ruchu
                ruch = (ruchGracza.Gracz1 == ruch) ? ruchGracza.Gracz2 : ruchGracza.Gracz1;
            }
            else
            {
                ruch = ruchGracza.Nikt;
            }

            pokazRuch();
        }

        Zwyciezca sprawdzZwyciezce()
        {
            PictureBox[] ruchyWygrane =
           {    //sprawdza kazdy rzad
                box0, box1, box2,
                box3, box4, box5,
                box6, box7, box8,
                //sprawdza kazda kolumne
                box0, box3, box6,
                box1, box4, box7,
                box2, box5, box8,
                //sprawdza po skosie
                box0, box4, box8,
                box2, box4, box6,
            };


            for (int i = 0; i < ruchyWygrane.Length; i += 3)
            {
                if (ruchyWygrane[i].Image != null)
                {
                    if (ruchyWygrane[i].Image == ruchyWygrane[i + 1].Image && ruchyWygrane[i].Image == ruchyWygrane[i + 2].Image)
                    {
                        //Zwyciezca
                        if (ruchyWygrane[i].Image == gracz1.Image)
                            return Zwyciezca.Gracz1;
                        else
                            return Zwyciezca.Gracz2;
                    }
                }
            }

            //Sprawdz czy sa puste pola
            PictureBox[] wszystkieObrazki =
            {
                box0,
                box1,
                box2,
                box3,
                box4,
                box5,
                box6,
                box7,
                box8
            };

            //Wyczyść wszystkie miejsca na planszy
            foreach (var p in wszystkieObrazki)
                if (p.Image == null)
                    return Zwyciezca.Nikt;


            //To jest remis
            return Zwyciezca.Remis;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nowaGra();
        }

        private void nowaGraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wynik = MessageBox.Show("Czy chcesz zacząć nową grę?",
                             "Nowa gra",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);

            if (wynik == DialogResult.Yes)
                nowaGra();
        }
    }
}
