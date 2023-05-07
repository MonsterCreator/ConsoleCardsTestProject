using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConsoleTestProject1
{
    public class CardsManager
    {
        private static void CardsArrayIsNullError()
        {
            Console.WriteLine("You trying call the method without of cards. Cards[] is null.\nCreate cards use /createcards");
        }

        public static void CreateCards(ref Card[] cards)
        {
            Card[] newCards = new Card[64];

            int x = 0;
            int startNum = 0;

            for (int colNum = 0; colNum < 4; colNum++)
            {
                CardColor color;
                if (colNum == 0) color = CardColor.red;
                else if (colNum == 1) color = CardColor.green;
                else if (colNum == 2) color = CardColor.blue;
                else color = CardColor.yellow;
                int num = 1;
                for (int cardNum = startNum; cardNum < startNum + 16; cardNum++)
                {
                    if (x <= 1)
                    {
                        newCards[cardNum] = new Card
                        {
                            cardId = cardNum,
                            cardNum = num,
                            color = color
                        };
                        x++;
                        if (x > 1) { x = 0; num++; };
                    }
                }

                startNum += 16;
            }
            cards = newCards;
            MyConsole.SystemLog(() => Console.WriteLine("Cards was created"));
        }

        public static void PrintCards(Card[] cards)
        {
            if (cards == null)
            {
                MyConsole.ErrorLog(CardsArrayIsNullError);
                return;
            }

            foreach (var item in cards)
            {
                SetColor(() => Console.Write($"### {item.cardNum} ###\n"), item.color);
            }
        }

        public static void Shuffle(ref Card[] cards)
        {
            if (cards == null)
            {
                MyConsole.ErrorLog(CardsArrayIsNullError);
                return;
            }

            var random = new Random();

            for (int cardNum = 0; cardNum < cards.Length; cardNum++)
            {
                int num = random.Next(cardNum, cards.Length);

                Card firstCard = cards[cardNum];
                Card secondCard = cards[num];

                cards[cardNum] = secondCard;
                cards[num] = firstCard;
            }

            MyConsole.SystemLog(() => Console.WriteLine("Shuffle completed"));
        }

        public static void SortByID(ref Card[] cards)
        {
            if (cards == null)
            {
                MyConsole.ErrorLog(CardsArrayIsNullError);
                return;
            }

            List<Card> cardsList = new List<Card>();

            for (int num = 0; num < cards.Length; num++)
            {
                for (int num2 = 0; num2 < cards.Length; num2++)
                {
                    if (cards[num2].cardId == num)
                    {
                        cardsList.Add(cards[num2]);
                    }
                }
            }

            cards = cardsList.ToArray();

            MyConsole.SystemLog(() => Console.WriteLine("Sort by ID completed"));
        }

        public static void Sort(ref Card[] cards)
        {
            if (cards == null)
            {
                MyConsole.ErrorLog(CardsArrayIsNullError);
                return;
            }

            var redCards = new List<Card>();
            var greenCards = new List<Card>();
            var blueCards = new List<Card>();
            var yellowCards = new List<Card>();

            for (int cardNum = 0; cardNum < cards.Length; cardNum++)
            {
                if (cards[cardNum].color == CardColor.red) redCards.Add(cards[cardNum]);
                else if (cards[cardNum].color == CardColor.green) greenCards.Add(cards[cardNum]);
                else if (cards[cardNum].color == CardColor.blue) blueCards.Add(cards[cardNum]);
                else yellowCards.Add(cards[cardNum]);
            }

            List<Card>[] lists = getCardsByColor(ref cards);
            sortCardsByNum(lists);

            Card[] newCards = lists[0].Concat(lists[1]).Concat(lists[2]).Concat(lists[3]).ToList().ToArray();

            cards = newCards;


            MyConsole.SystemLog(() => Console.WriteLine("Sort completed"));
        }

        private static List<Card>[] getCardsByColor(ref Card[] cards)
        {
            var redCards = new List<Card>();
            var greenCards = new List<Card>();
            var blueCards = new List<Card>();
            var yellowCards = new List<Card>();

            for (int cardNum = 0; cardNum < cards.Length; cardNum++)
            {
                if (cards[cardNum].color == CardColor.red) redCards.Add(cards[cardNum]);
                else if (cards[cardNum].color == CardColor.green) greenCards.Add(cards[cardNum]);
                else if (cards[cardNum].color == CardColor.blue) blueCards.Add(cards[cardNum]);
                else yellowCards.Add(cards[cardNum]);
            }

            List<Card>[] lists = { redCards, greenCards, blueCards, yellowCards };

            return lists;
        }

        private static void sortCardsByNum(List<Card>[] lists)
        {
            lists[0] = sortCardTypeByNum(lists[0]);
            lists[1] = sortCardTypeByNum(lists[1]);
            lists[2] = sortCardTypeByNum(lists[2]);
            lists[3] = sortCardTypeByNum(lists[3]);
        }

        private static List<Card> sortCardTypeByNum(List<Card> cards)
        {
            int x = 0;
            int cardNum = 1;
            List<Card> newCards = new List<Card>();

            for (int num = 1; num <= 8; num++) //проходимся в цикле по каждому номеру от 1 до 8
            {
                for (int num2 = 0; num2 < cards.Count; num2++)
                {
                    if (cards[num2].cardNum == num)
                    {
                        newCards.Add(cards[num2]);

                        if (x >= 1)
                        {
                            cardNum++;
                            x = 0;
                            break;
                        }
                        else x++;
                    }
                }
            }
            return newCards;
        }

        private static void SetColor(Action method, CardColor color)
        {
            switch (color)
            {
                case CardColor.red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case CardColor.green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case CardColor.blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case CardColor.yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
            method.Invoke();
            Console.ResetColor();
            
        }
    }
}
