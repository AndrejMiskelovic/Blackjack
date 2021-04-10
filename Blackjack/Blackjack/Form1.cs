using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack
{
    public partial class Form1 : Form
    {
        List<string> playerlist = new List<string>();
        List<string> dealerlist = new List<string>();
        private  Deck deck = new Deck();
        private  Player player = new Player();
        int PlayerX = 0;
        int DealerX = 0;
        double procent;
        public Form1()
        {
            InitializeComponent();
            Hit.Enabled = false;
            Stand.Enabled = false;
            Surrender.Enabled = false;
            Double.Enabled = false;
            label1.Visible = false;
            label2.Visible = false;
            listBox1.Items.Add("Current Chip Count: 500");
            listBox1.Items.Add("Minimum Bet: 10");
        }

        private void Bet_Click(object sender, EventArgs e)
        {
            if (!TakeBet())
            {
                listBox1.Items.Add("Bet not valid");
                return;
            }
            panel1.Controls.Clear();
            PlayerX = 0;
            panel3.Controls.Clear();
            DealerX = 0;
            deck.Initialize();

            player.Hand = deck.DealHand();
            Dealer.HiddenCards = deck.DealHand();
            Dealer.RevealedCards = new List<Card>();

            // If hand contains two aces, make one Hard.
            if (player.Hand[0].Face == Face.Ace && player.Hand[1].Face == Face.Ace)
            {
                player.Hand[1].Value = 1;
            }

            if (Dealer.HiddenCards[0].Face == Face.Ace && Dealer.HiddenCards[1].Face == Face.Ace)
            {
                Dealer.HiddenCards[1].Value = 1;
            }
            Hit.Enabled = true;
            Stand.Enabled = true;
            Surrender.Enabled = true;
            Double.Enabled = true;
            label1.Visible = true;
            label2.Visible = true;
            Dealer.RevealCard();
            refresh();
            if (player.GetHandValue() >= 21)
            {
                Hit.Enabled = false;
                Stand.Enabled = false;
                Surrender.Enabled = false;
                Double.Enabled = false;
                DealerTurn();
            }
            if((deck.Helpfunc(player.GetHandValue(), player.Hand)) < 40)
            {
                label3.Text = "Your chances to successfully draw a card is " + Convert.ToString(deck.Helpfunc(player.GetHandValue(), player.Hand)) + "% we recommend to Stand";
            }
            else
            {
                label3.Text = "Your chances to successfully draw a card is " + Convert.ToString(deck.Helpfunc(player.GetHandValue(), player.Hand)) + "% you can try to Hit/Double";
            }
            string name = Convert.ToString(player.Hand[0].Face) + Convert.ToString(player.Hand[0].Suit);
            PlayersCards(name);
            name = Convert.ToString(player.Hand[1].Face) + Convert.ToString(player.Hand[1].Suit);
            PlayersCards(name);
            name = Convert.ToString(Dealer.RevealedCards[0].Face) + Convert.ToString(Dealer.RevealedCards[0].Suit);
            DealerCards(name);
            DealerCards("grayback");
        }
        public void refresh()
        {
            listBox1.Items.Clear();
            playerlist = player.WriteHand();
            dealerlist = Dealer.WriteHand();
            foreach (var item in playerlist)
            {
                listBox1.Items.Add(item);
            }
            foreach (var item in dealerlist)
            {
                listBox1.Items.Add(item);
            }
            Bet.Enabled = false;
        }
        public bool TakeBet()
        {

            if (Int32.TryParse(textBox1.Text, out int bet) && bet >= Casino.MinimumBet && player.Chips >= bet)
            {
                player.AddBet(bet);
                return true;
            }
            return false;
        }
        public void DealerTurn()
        {
            Dealer.RevealCard();
            while (Dealer.GetHandValue() <= 16)
            {
                Dealer.RevealedCards.Add(deck.DrawCard());              
                if (Dealer.GetHandValue() > 21)
                {
                    foreach (Card card in Dealer.RevealedCards)
                    {
                        if (card.Value == 11) // Only a soft ace can have a value of 11
                        {
                            card.Value = 1;
                            break;
                        }
                    }
                }
            }
            refresh();
            GameEnd();
        }
        public void GameEnd()
        {
            string res = "";
            refresh();
            if(player.Hand.Count == 0)
            {
                listBox1.Items.Add("Player Surrenders " + (player.Bet / 2) + " chips.");
                player.Chips += player.Bet / 2;
                player.ClearBet();
                res = "Player Surrenders";
            }
            else if(player.GetHandValue() > 21)
            {
                listBox1.Items.Add("Player Bust.");
                res = "Player Bust";
                player.ClearBet();
            }
            else if (player.GetHandValue() > Dealer.GetHandValue())
            {
                player.Wins++;
                if (Casino.IsHandBlackjack(player.Hand))
                {
                    listBox1.Items.Add("Player Wins " + player.WinBet(true) + " chips with Blackjack.");
                }
                else
                {
                    listBox1.Items.Add("Player Wins " + player.WinBet(false) + " chips.");
                }
                res = "Player Wins";
            }
            else if (Dealer.GetHandValue() > 21)
            {
                player.Wins++;
                listBox1.Items.Add("Player Wins " + player.WinBet(false) + " chips.");
                res = "Player Wins";
            }
            else if (Dealer.GetHandValue() > player.GetHandValue())
            {
                player.ClearBet();
                listBox1.Items.Add("Dealer Wins.");
                res = "Dealer Wins";
            }
            else
            {
                player.ReturnBet();
                listBox1.Items.Add("Player and Dealer Push.");
                res = "Player and Dealer Push";
            }
            Bet.Enabled = true;
            if (player.Chips <= 0)
            {
                listBox1.Items.Add("You ran out of Chips after " + (player.HandsCompleted - 1) + " rounds.");
                listBox1.Items.Add("500 Chips will be added and your statistics have been reset.");

                player = new Player();
            }
            panel3.Controls.Clear();
            DealerX = 0;
            foreach (var item in Dealer.RevealedCards)
            {
                string name = Convert.ToString(item.Face) + Convert.ToString(item.Suit);
                DealerCards(name);
            }
            WriteToTXT(Convert.ToString(procent), res);
        }
        private void Hit_Click(object sender, EventArgs e)
        {
            procent = deck.Helpfunc(player.GetHandValue(), player.Hand);
            if (player.GetHandValue() < 21)
            {
                player.Hand.Add(deck.DrawCard());
                string name = Convert.ToString(player.Hand[player.Hand.Count - 1].Face) + Convert.ToString(player.Hand[player.Hand.Count - 1].Suit);
                PlayersCards(name);
                if (player.GetHandValue() > 21)
                {
                    foreach (Card card in player.Hand)
                    {
                        if (card.Value == 11) // Only a soft ace can have a value of 11
                        {
                            card.Value = 1;
                            break;
                        }
                    }
                }
                refresh();
            }

            if (player.GetHandValue() >= 21)
            {
                Hit.Enabled = false;
                Stand.Enabled = false;
                Surrender.Enabled = false;
                Double.Enabled = false;
                DealerTurn();
            }
            else
            {
                procent = deck.Helpfunc(player.GetHandValue(), player.Hand);
                if (procent < 40.00)
                {
                    label3.Text = "Your chances to successfully draw a card is " + Convert.ToString(procent) + "% we recommend to Stand";
                }
                else
                {
                    label3.Text = "Your chances to successfully draw a card is " + Convert.ToString(procent) + "% you can try to Hit/Double";
                }
            }

        }

        private void Stand_Click(object sender, EventArgs e)
        {
            procent = deck.Helpfunc(player.GetHandValue(), player.Hand);
            Hit.Enabled = false;
            Stand.Enabled = false;
            Surrender.Enabled = false;
            Double.Enabled = false;
            DealerTurn();
        }

        private void Surrender_Click(object sender, EventArgs e)
        {
            procent = deck.Helpfunc(player.GetHandValue(), player.Hand);
            player.Hand.Clear();
            Hit.Enabled = false;
            Stand.Enabled = false;
            Surrender.Enabled = false;
            Double.Enabled = false;
            GameEnd();
        }

        private void Double_Click(object sender, EventArgs e)
        {
            procent = deck.Helpfunc(player.GetHandValue(), player.Hand);
            if (player.Chips <= player.Bet)
            {
                player.AddBet(player.Chips);
            }
            else
            {
                player.AddBet(player.Bet);
            }
            player.Hand.Add(deck.DrawCard());

            Hit.Enabled = false;
            Stand.Enabled = false;
            Surrender.Enabled = false;
            Double.Enabled = false;
            DealerTurn();

        }
        public void PlayersCards(string name)
        {
            
            PictureBox picture = new PictureBox
            {
                Name = name,
                Size = new Size(70, 100),
                Location = new Point(PlayerX, 0),
                Image = Image.FromFile((@"cards\"+ name +".png")),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            PlayerX += 80;
            panel1.Controls.Add(picture);
        }
        public void DealerCards(string name)
        {

            PictureBox picture = new PictureBox
            {
                Name = name,
                Size = new Size(70, 100),
                Location = new Point(DealerX, 0),
                Image = Image.FromFile((@"cards\" + name + ".png")),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            DealerX += 80;
            panel3.Controls.Add(picture);
        }
        public void WriteToTXT(string help, string res)
        {
            string hint = ""; 
            if(procent < 40)
            {
                hint = "Stand";
            }
            else
            {
                hint = "Hit";
            }
            using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(@"data.txt", true))
            {
                foreach (var item in player.Hand)
                {
                    file.Write(item.Face + " " + item.Suit + ",");

                }
                file.Write(help + "," + hint);
                file.WriteLine();
                Card last = Dealer.RevealedCards.Last();
                foreach (var item in Dealer.RevealedCards)
                {
                    if (item.Equals(last)) 
                        file.Write(item.Face + " " + item.Suit);
                    else
                        file.Write(item.Face + " " + item.Suit + ",");

                }
                file.WriteLine();
                file.WriteLine(res);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
