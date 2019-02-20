﻿using System;
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
            Console.OutputEncoding = Encoding.Unicode;

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
            string[] cardFace = new string[]{ "A\u2660", "2\u2660", "3\u2660", "4\u2660", "5\u2660", "6\u2660", "7\u2660", "8\u2660", "9\u2660", "10\u2660", "J\u2660", "Q\u2660", "K\u2660",
            "A\u2663", "2\u2663", "3\u2663", "4\u2663", "5\u2663", "6\u2663", "7\u2663", "8\u2663", "9\u2663", "10\u2663", "J\u2663", "Q\u2663", "K\u2663",
            "A\u2665", "2\u2665", "3\u2665", "4\u2665", "5\u2665", "6\u2665", "7\u2665", "8\u2665", "9\u2665", "10\u2665", "J\u2665", "Q\u2665", "K\u2665",
            "A\u2666", "2\u2666", "3\u2666", "4\u2666", "5\u2666", "6\u2666", "7\u2666", "8\u2666", "9\u2666", "10\u2666", "J\u2666", "Q\u2666", "K\u2666"};
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
            Card backOfCard = new Card("XX", 0, ConsoleColor.Blue);
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
                    if (c.Value != 10) { Console.Write(" "); }
                    firstCard = false;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = c.CardColor;
                    Console.Write($"{c.CardCode}");
                    if (c.Value != 10) { Console.Write(" "); }
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
                Console.Write($"Player {iterator}. {kvp.Key}\t-- ");
                DisplayHand(kvp.Value);
                Console.WriteLine();
                iterator++;
            }
        }
    }
}
