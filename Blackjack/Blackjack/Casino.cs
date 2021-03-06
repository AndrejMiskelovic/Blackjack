using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Casino
    {
        private static string versionCode = "1.0";

        public static int MinimumBet { get; } = 10;
        public static string GetVersionCode() { return versionCode; }

        /// <param name="hand">The hand to check</param>
        /// <returns>Returns true if the hand is blackjack</returns>
        public static bool IsHandBlackjack(List<Card> hand)
        {
            if (hand.Count == 2)
            {
                if (hand[0].Face == Face.Ace && hand[1].Value == 10) return true;
                else if (hand[1].Face == Face.Ace && hand[0].Value == 10) return true;
            }
            return false;
        }
    }

    public class Player
    {
        public int Chips { get; set; } = 500;
        public int Bet { get; set; }
        public int Wins { get; set; }
        public int HandsCompleted { get; set; } = 1;

        public List<Card> Hand { get; set; }

        /// <summary>
        /// Add Player's chips to their bet.
        /// </summary>
        /// <param name="bet">The number of Chips to bet</param>
        public void AddBet(int bet)
        {
            Bet += bet;
            Chips -= bet;
        }

        /// <summary>
        /// Set Bet to 0
        /// </summary>
        public void ClearBet()
        {
            Bet = 0;
        }

        /// <summary>
        /// Cancel player's bet. They will neither lose nor gain any chips.
        /// </summary>
        public void ReturnBet()
        {
            Chips += Bet;
            ClearBet();
        }

        /// <summary>
        /// Give player chips that they won from their bet.
        /// </summary>
        /// <param name="blackjack">If player won with blackjack, player wins 1.5 times their bet</param>
        public int WinBet(bool blackjack)
        {
            int chipsWon;
            if (blackjack)
            {
                chipsWon = (int)Math.Floor(Bet * 1.5);
            }
            else
            {
                chipsWon = Bet * 2;
            }

            Chips += chipsWon;
            ClearBet();
            return chipsWon;
        }

        /// <returns>
        /// Value of all cards in Hand
        /// </returns>
        public int GetHandValue()
        {
            int value = 0;
            foreach (Card card in Hand)
            {
                value += card.Value;
            }
            return value;
        }

        /// <summary>
        /// Write player's hand to console.
        /// </summary>
        public List<string> WriteHand()
        {
            List<string> message = new List<string>();
            // Write Bet, Chip, Win, Amount with color, and write Round #
            message.Add("Bet: ");
            //Console.ForegroundColor = ConsoleColor.Magenta;
            message.Add(Bet + "  ");
            //Casino.ResetColor();
            message.Add("Chips: ");
            //Console.ForegroundColor = ConsoleColor.Green;
            message.Add(Chips + "  ");
            //Casino.ResetColor();
            message.Add("Wins: ");
            //Console.ForegroundColor = ConsoleColor.Yellow;
            message.Add(Convert.ToString(Wins));
            //Casino.ResetColor();
            message.Add("Round #" + HandsCompleted);

            message.Add(" ");
            message.Add("Your Hand (" + GetHandValue() + "):");
            foreach (Card card in Hand)
            {
                message.Add(card.WriteDescription());
            }
            //Console.WriteLine();

            return message;
        }
    }

    public class Dealer
    {
        public static List<Card> HiddenCards { get; set; } = new List<Card>();
        public static List<Card> RevealedCards { get; set; } = new List<Card>();

        /// <summary>
        /// Take the top card from HiddenCards, remove it, and add it to RevealedCards.
        /// </summary> 
        public static void RevealCard()
        {
            RevealedCards.Add(HiddenCards[0]);
            HiddenCards.RemoveAt(0);
        }

        /// <returns>
        /// Value of all cards in RevealedCards
        /// </returns>
        public static int GetHandValue()
        {
            int value = 0;
            foreach (Card card in RevealedCards)
            {
                value += card.Value;
            }
            return value;
        }

        /// <summary>
        /// Write Dealer's RevealedCards to Console.
        /// </summary>
        public static List<string> WriteHand()
        {
            List<string> description = new List<string>();
            description.Add("Dealer's Hand (" + GetHandValue() + "):");
            foreach (Card card in RevealedCards)
            {
                description.Add(card.WriteDescription());
            }
            for (int i = 0; i < HiddenCards.Count; i++)
            {
                //Console.ForegroundColor = ConsoleColor.DarkRed;
                description.Add("<hidden>");
                //Casino.ResetColor();
            }
            description.Add(" ");
            return description;
        }
    }
}