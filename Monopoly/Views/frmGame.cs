﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Monopoly.Models;

namespace Monopoly.Views
{
    public partial class frmGame : Form
    {
        Game game = new Game();

        public frmGame()
        {
            InitializeComponent();
            gameView.Game = game;
        }
    }
}
