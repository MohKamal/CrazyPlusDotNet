using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrazyPlus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Game Game { get; set; }
        private void Form1_Load(object sender, EventArgs e)
        {
            Game = new Game(Area);
            Game.Start();
        }
    }
}
