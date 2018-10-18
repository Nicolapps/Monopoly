﻿using Monopoly.Models;
using Monopoly.Models.Cases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Monopoly.Views
{
    public partial class frmCheat : Form
    {
        Game game;
        Player selectedPlayer;
        public frmGame FrmGame { get; set; }

        public frmCheat(frmGame frmGame)
        {
            FrmGame = frmGame;
            game = FrmGame.game;
            InitializeComponent();
            foreach (Player p in game.Players)
            {
                lbxArgentJoueurs.Items.Add(p.Name);
            }
            lbxArgentJoueurs.SelectedIndex = 0;
            lbxArgentJoueurs_DoubleClick(lbxArgentJoueurs, new EventArgs());
        }
        

        private void btnValiderDes_Click(object sender, EventArgs e)
        {
            game.ResultFirstDice = Convert.ToInt32(tbxResultFirstDice.Text);
            game.ResultSecDice = Convert.ToInt32(tbxResultSecDice.Text);
            game.PlayDice(game.ResultFirstDice, game.ResultSecDice);
            FrmGame.UpdateTabs();

            game.LastFirstDiceResult = game.ResultFirstDice;
            game.LastSecDiceResult = game.ResultSecDice;

            tbxResultFirstDice.Text = "0";
            tbxResultSecDice.Text = "0";
        }

        private void lbxArgentJoueurs_DoubleClick(object sender, EventArgs e)
        {
            selectedPlayer = game.Players
                .Find(player => player.Name == lbxArgentJoueurs.SelectedItem.ToString());
            lblNomJoueurArgent.Text = selectedPlayer.Name;
            lblArgentActuelJoueur.Text = selectedPlayer.Wealth.ToString();

            lblNomJoueurPropriete.Text = selectedPlayer.Name;
            lbxProprietesJoueur.Items.AddRange(selectedPlayer.GetProperties(game).ToArray());
            lbxProprietesDispo.Items.AddRange(game.GetFreeProperties().ToArray());

        }

        private void btnValiderArgent_Click(object sender, EventArgs e)
        {
            if (rbtnAjouter.Checked)
            {
                selectedPlayer.Wealth += (int)nudArgentJoueur.Value;
            }
            else
            {
                selectedPlayer.Wealth -= (int)nudArgentJoueur.Value;
                // @TODO:: Tester si le player est en banqueroute
            }

            lblArgentActuelJoueur.Text = selectedPlayer.Wealth.ToString();
            nudArgentJoueur.Value = 0;
            
        }

        private void btnAjouterPropriete_Click(object sender, EventArgs e)
        {
            PropertyCase[] selectedPropertiesDispo = new PropertyCase[lbxProprietesDispo.SelectedItems.Count];
            lbxProprietesDispo.SelectedItems.CopyTo(selectedPropertiesDispo, 0);

            foreach (PropertyCase p in selectedPropertiesDispo)
            {
                p.Owner = selectedPlayer;
                p.Invalidate();
                lbxProprietesJoueur.Items.Add(p);
                lbxProprietesDispo.Items.Remove(p);
            }

            
        }

        private void btnEnleverPropriete_Click(object sender, EventArgs e)
        {
            PropertyCase[] selectedPropertiesJoueur = new PropertyCase[lbxProprietesJoueur.SelectedItems.Count];
            lbxProprietesJoueur.SelectedItems.CopyTo(selectedPropertiesJoueur, 0);

            foreach (PropertyCase p in selectedPropertiesJoueur)
            {
                p.Owner = null;
                p.Invalidate();
                lbxProprietesJoueur.Items.Remove(p);
                lbxProprietesDispo.Items.Add(p);
            }
        }

        //@TODO:: fonction tri des listbox
    }
}
