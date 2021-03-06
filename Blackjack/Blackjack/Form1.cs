using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public Form1()
        {
            InitializeComponent();
            Hit.Enabled = false;
            Stand.Enabled = false;
            Surrender.Enabled = false;
            Double.Enabled = false;
            Help.Enabled = false;
            //Console.Write("Current Chip Count: ");
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine(player.Chips);
            //Casino.ResetColor();

            //Console.Write("Minimum Bet: ");
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine(Casino.MinimumBet);
            //Casino.ResetColor();

            //Console.Write("Enter bet to begin hand " + player.HandsCompleted + ": ");
            //Console.ForegroundColor = ConsoleColor.Magenta;
            //string s = Console.ReadLine();
            //Casino.ResetColor();
        }

        private void Bet_Click(object sender, EventArgs e)
        {
            if (!TakeBet())
            {
                listBox1.Items.Add("bet not valid");
                return;
            }
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
            Help.Text = Convert.ToString(deck.Helpfunc(player.GetHandValue(), player.Hand)); 
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
            refresh();
            if(player.Hand.Count == 0)
            {
                listBox1.Items.Add("Player Surrenders " + (player.Bet / 2) + " chips");
                player.Chips += player.Bet / 2;
                player.ClearBet();
            }
            else if(player.GetHandValue() > 21)
            {
                listBox1.Items.Add("Player Bust");
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
                    listBox1.Items.Add("Player Wins " + player.WinBet(false) + " chips");
                }
            }
            else if (Dealer.GetHandValue() > 21)
            {
                player.Wins++;
                listBox1.Items.Add("Player Wins " + player.WinBet(false) + " chips");
            }
            else if (Dealer.GetHandValue() > player.GetHandValue())
            {
                player.ClearBet();
                listBox1.Items.Add("Dealer Wins.");
            }
            else
            {
                player.ReturnBet();
                listBox1.Items.Add("Player and Dealer Push.");
            }
            Bet.Enabled = true;
            if (player.Chips <= 0)
            {
                listBox1.Items.Add("You ran out of Chips after " + (player.HandsCompleted - 1) + " rounds.");
                listBox1.Items.Add("500 Chips will be added and your statistics have been reset.");

                player = new Player();
            }

        }
        private void Hit_Click(object sender, EventArgs e)
        {
            if(player.GetHandValue() < 21)
            {
                player.Hand.Add(deck.DrawCard());
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
            if(player.GetHandValue() >= 21)
            {
                Hit.Enabled = false;
                Stand.Enabled = false;
                Surrender.Enabled = false;
                Double.Enabled = false;
                Help.Text = "";
                DealerTurn();
            }
            else
            {
                Help.Text = Convert.ToString(deck.Helpfunc(player.GetHandValue(), player.Hand));
            }
            
        }

        private void Stand_Click(object sender, EventArgs e)
        {

            Hit.Enabled = false;
            Stand.Enabled = false;
            Surrender.Enabled = false;
            Double.Enabled = false;
            Help.Text = "";
            DealerTurn();
        }

        private void Surrender_Click(object sender, EventArgs e)
        {
            player.Hand.Clear();
            Hit.Enabled = false;
            Stand.Enabled = false;
            Surrender.Enabled = false;
            Double.Enabled = false;
            Help.Text = "";
            GameEnd();
        }

        private void Double_Click(object sender, EventArgs e)
        {
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
            Help.Text = "";
            DealerTurn();

        }
    }
}
