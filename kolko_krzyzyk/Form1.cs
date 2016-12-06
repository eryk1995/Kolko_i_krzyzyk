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

        void nowaGra()
        {
            ruch = ruchGracza.Gracz1;
            pokazRuch();

        }
        public Form1()
        {
            InitializeComponent();
        }

        void pokazRuch()
        {
            string status = "";


            if (ruch == ruchGracza.Gracz1)
                status = "Ruch: Gracz 1";
            else
                status = "Ruch: Gracz 2";

            lblStatus.Text = status;
        }

        private void OnClick(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;

            if(p.Image != null)
                return;






            if (ruch == ruchGracza.Gracz1)
                p.Image = gracz1.Image;
            else
                p.Image = gracz2.Image;

            //Zmiana ruchu
            ruch = (ruchGracza.Gracz1 == ruch) ? ruchGracza.Gracz2 : ruchGracza.Gracz1;

            pokazRuch();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nowaGra();
        }
    }
}
