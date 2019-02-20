using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise6
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Shaun Wehe
             * February 19, 2019
             * Project and Portfolio 2
             * Exercise 6
             */

            string inputLine, introduction = "This game deals out 13 cards to four players.\n" +
                "The cards are assigned values, 2-10 is the face value of the card, K-J is 12 points, and Aces are 15 points.\n" +
                "The hand values are tallied and the player with the highest value hand wins.";
            bool programRunning = true;
            List<Card> deck = new List<Card>();
            Dictionary<string, List<Card>> player = new Dictionary<string, List<Card>>();
            deck = BuildDeck();
            List<Card> hand;

            Console.WriteLine(introduction);
            Utility.KeyToProceed();

            //Set up players
            for (int i = 0; i < 4; i++)
            {
                Console.Clear();
                Console.Write($"Please enter the name of player {i+1}: ");
                inputLine = Console.ReadLine();
                while (player.TryGetValue(inputLine, out hand))
                {
                    Console.Write($"Players must have unique names.\nPlease enter a unique name for player {i+1}: ");
                    inputLine = Console.ReadLine();
                    Console.Clear();
                }
                hand = BuildBlankHand();
                player.Add(inputLine, hand);
            }

            while (programRunning)
            {
                //Seven shuffles is commonly held as the least amount of shuffles needed to thoroughly randomize the deck
                for (int i = 0; i < 7; i++)
                {
                    deck.Shuffle();
                }
                //This readies the deck for use in the program
                Queue<Card> shuffledDeck = new Queue<Card>(deck);

                DisplayAllHands(player);
                Utility.KeyToProceed();
                
                //This code block "deals" the cards to the players, it has about a third of a second delay to simulate the dealing speed
                //It displays the updated "hands" of the players after each card is dealt
                for (int i = 0; i < 13; i++)
                {
                    foreach (KeyValuePair<string,List<Card>> kvp in player)
                    {
                        kvp.Value[i] = shuffledDeck.Dequeue();
                        Console.Clear();
                        DisplayAllHands(player);
                        System.Threading.Thread.Sleep(300);
                    }
                }

                Utility.KeyToProceed();
                programRunning = false;
            }
        }

        public static List<Card> BuildDeck()
        {
            List<Card> deck = new List<Card>();
            char[] cardFace = new char[]{ '\uf0a1', '\uf0a2', '\uf0a3', '\uf0a4', '\uf0a5', '\uf0a6', '\uf0a7', '\uf0a8', '\uf0a9', '\uf0aa', '\uf0ab', '\uf0ad', '\uf0ae',
            '\uf0d1', '\uf0d2', '\uf0d3', '\uf0d4', '\uf0d5', '\uf0d6', '\uf0d7', '\uf0d8', '\uf0d9', '\uf0da', '\uf0db', '\uf0dd', '\uf0de',
            '\uf0b1', '\uf0b2', '\uf0b3', '\uf0b4', '\uf0b5', '\uf0b6', '\uf0b7', '\uf0b8', '\uf0b9', '\uf0ba', '\uf0bb', '\uf0bd', '\uf0be',
            '\uf0c1', '\uf0c2', '\uf0c3', '\uf0c4', '\uf0c5', '\uf0c6', '\uf0c7', '\uf0c8', '\uf0c9', '\uf0ca', '\uf0cb', '\uf0cd', '\uf0ce'};
            int[] value = new int[52];
            ConsoleColor[] cardColor = new ConsoleColor[52];

            //Sets up the value array
            for (int i = 0; i < 40; i += 13)
            {
                for (int j = 0; j < 13; j++)
                {
                    switch (j)
                    {
                        case 0:
                            value[i + j] = 15;
                            break;
                        case 1:
                            value[i + j] = 2;
                            break;
                        case 2:
                            value[i + j] = 3;
                            break;
                        case 3:
                            value[i + j] = 4;
                            break;
                        case 4:
                            value[i + j] = 5;
                            break;
                        case 5:
                            value[i + j] = 6;
                            break;
                        case 6:
                            value[i + j] = 7;
                            break;
                        case 7:
                            value[i + j] = 8;
                            break;
                        case 8:
                            value[i + j] = 9;
                            break;
                        case 9:
                            value[i + j] = 10;
                            break;
                        case 10:
                        case 11:
                        case 12:
                            value[i + j] = 12;
                            break;
                        default:
                            value[i + j] = 0;
                            break;
                    }
                }
            }

            //Sets up cardColor array
            for (int i = 0; i < 27; i += 26)
            {
                for (int j = 0; j < 26; j++)
                {
                    if (i == 0) { cardColor[i + j] = ConsoleColor.Black; }
                    else if (i == 26) { cardColor[i + j] = ConsoleColor.Red; }
                    else { cardColor[i + j] = ConsoleColor.Blue; }
                }
            }

            for (int i = 0; i < 52; i++)
            {
                Card temp = new Card(cardFace[i], value[i], cardColor[i]);
                deck.Add(temp);
            }

            return deck;
        }

        public static List<Card> BuildBlankHand()
        {
            //This method returns a hand consisting of the backs of cards with no value
            List<Card> hand = new List<Card>();
            Card backOfCard = new Card('\uf0a0', 0, ConsoleColor.Blue);
            for (int j = 0; j < 13; j++)
            {
                hand.Add(backOfCard);
            }
            return hand;
        }

        public static void DisplayHand(List<Card> hand)
        {
            bool firstCard = true;
            foreach (Card c in hand)
            {
                if (firstCard)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = c.CardColor;
                    Console.Write($"{c.CardCode}");
                    firstCard = false;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = c.CardColor;
                    Console.Write($"{c.CardCode}");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DisplayAllHands (Dictionary<string, List<Card>> players)
        {
            //This displays all the hand of the current players
            int iterator = 1;
            foreach (KeyValuePair<string, List<Card>> kvp in players)
            {
                Console.Write($"Player {iterator}. {kvp.Key} -- ");
                DisplayHand(kvp.Value);
                Console.WriteLine();
                iterator++;
            }
        }
    }
}
