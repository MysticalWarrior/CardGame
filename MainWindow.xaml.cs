using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _205704CardGame2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImageBrush sprite;
        TranslateTransform translate;
        Deck deck;
        Hand player;
        Hand dealer;
        int[] playerHand;
        int[] dealerhand;
        int LocationInDeck;
        TranslateTransform[] playerTranslateTransform;
        TranslateTransform[] dealerTranslateTransform;
        Rectangle[] rectanglePlayer;
        Rectangle[] rectangleDealer;
        ImageBrush[] playerSprite;
        ImageBrush[] dealerSprite;

        public MainWindow()
        {
            InitializeComponent();
            player = new Hand("Player");
            dealer = new Hand("Dealer");

            deck = new Deck();
            deck.shuffleDeck();
            //MessageBox.Show(deck.outputCards());

            playerSprite = new ImageBrush[4];
            dealerSprite = new ImageBrush[4];
            LocationInDeck = 0;
            rectanglePlayer = new Rectangle[4];
            rectangleDealer = new Rectangle[4];
            playerHand = new int[] { -1, -1, -1, -1 };
            dealerhand = new int[] { -1, -1, -1, -1 };
            playerTranslateTransform = new TranslateTransform[4];
            dealerTranslateTransform = new TranslateTransform[4];

            playGame();
        }

        public void playGame()
        {/*
            for (int i = 0; i < 2; i++)
            {
                player.Add(deck.dealCard());
                dealer.Add(deck.dealCard());
            }
            createSprite();
            displayCard(player[0].Value);
            foreach(Card c in player) { displayCard(c.Value); }
            MessageBox.Show(player.ToString());
            //MessageBox.Show(dealer.ToString());
            */
            //mctavish code
            for (int i = 0; i < 2; i++)
            {
                playerHand[i] = deck.dealCard().Value;
                dealerhand[i] = deck.dealCard().Value;
            }

            string playerhandOutput = "Player cards: ";
            string dealerhandOutput = "Dealer cards: ";
            for (int i = 0; i < playerHand.Length; i++)
            {
                if (playerHand[i] >= 0)
                {
                    playerhandOutput += playerHand[i].ToString() + ", ";
                }
                if (dealerhand[i] >= 0)
                {
                    dealerhandOutput += dealerhand[i].ToString() + ", ";
                }
            }
            displayCards();
            MessageBox.Show(playerhandOutput + Environment.NewLine
                + dealerhandOutput);
        }

        /*public void displayCard(int c)
        {
            //suits = 0(spades) 1(hearts), etc
            sprite.Viewport = new Rect(0, 0, 71, 96);
            translate = new TranslateTransform(-(c%13)*(71), -(c/13)*(96));
            sprite.Transform = translate;

            Rectangle card1 = new Rectangle();
            card1.Height = 96; card1.Width = 71;
            card1.Fill = sprite;
            canvas.Children.Add(card1);
            Canvas.SetLeft(card1, (c%13));
            Canvas.SetTop(card1, 0);
        }

        public void createSprite()
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("cards.png", UriKind.Relative));
            sprite = new ImageBrush(bitmapImage);
            sprite.Stretch = Stretch.None;
            sprite.AlignmentX = AlignmentX.Left;
            sprite.AlignmentY = AlignmentY.Top;
        }*/

        public void displayCards()
        {
            canvas.Children.Clear();
            BitmapImage bitmapImage = new BitmapImage(new Uri("cards.png", UriKind.Relative));


            for (int i = 0; i < playerHand.Length; i++)
            {
                playerSprite[i] = new ImageBrush(bitmapImage);
                dealerSprite[i] = new ImageBrush(bitmapImage);
                playerTranslateTransform[i] = new TranslateTransform(0, 0);
                dealerTranslateTransform[i] = new TranslateTransform(0, 0);

                playerSprite[i].Stretch = Stretch.None;
                playerSprite[i].AlignmentX = AlignmentX.Left;
                playerSprite[i].AlignmentY = AlignmentY.Top;
                dealerSprite[i].Stretch = Stretch.None;
                dealerSprite[i].AlignmentX = AlignmentX.Left;
                dealerSprite[i].AlignmentY = AlignmentY.Top;

                playerSprite[i].Viewport = new Rect(0, 0, bitmapImage.Width / 13, bitmapImage.Height / 4);
                dealerSprite[i].Viewport = new Rect(0, 0, bitmapImage.Width / 13, bitmapImage.Height / 4);

                //set based on card
                playerTranslateTransform[i] =
                    new TranslateTransform(-(playerHand[i] % 13) * (bitmapImage.Width / 13),
                    -(playerHand[i] / 13) * (bitmapImage.Height / 4));
                playerSprite[i].Transform = playerTranslateTransform[i];
                dealerTranslateTransform[i] =
                    new TranslateTransform(-(dealerhand[i] % 13) * (bitmapImage.Width / 13),
                    -(dealerhand[i] / 13) * (bitmapImage.Height / 4));
                dealerSprite[i].Transform = dealerTranslateTransform[i];

                //make array for player and dealer
                rectanglePlayer[i] = new Rectangle();
                rectanglePlayer[i].Fill = playerSprite[i];
                rectanglePlayer[i].Width = bitmapImage.Width / 13;
                rectanglePlayer[i].Height = bitmapImage.Height / 4;
                //make array for player and dealer
                rectangleDealer[i] = new Rectangle();
                rectangleDealer[i].Fill = dealerSprite[i];
                rectangleDealer[i].Width = bitmapImage.Width / 13;
                rectangleDealer[i].Height = bitmapImage.Height / 4;

                canvas.Children.Add(rectanglePlayer[i]);
                //set based on position of hand
                Canvas.SetTop(rectanglePlayer[i], 10);
                Canvas.SetLeft(rectanglePlayer[i], 10 * i + (bitmapImage.Width / 13) * i);

                canvas.Children.Add(rectangleDealer[i]);
                //set based on position of hand
                Canvas.SetTop(rectangleDealer[i], 110);
                Canvas.SetLeft(rectangleDealer[i], 10 * i + (bitmapImage.Width / 13) * i);

            }//end for loop
        }

        private void btnStay_Click(object sender, RoutedEventArgs e)
        {
            checkIfBust();
            updateDealer();
        }

        private void updateDealer()
        {
            int handTotal = 0;
            int currentCard;
            for (int i = 0; i < playerHand[i]; i++)
            {
                currentCard = dealerhand[i] % 13 + 1;
                if (currentCard > 10)
                {
                    currentCard = 10;
                }
                if (currentCard == 1)
                {
                    currentCard = 11;
                }
                else
                handTotal += currentCard;
            }//end loop
            if (handTotal == 21) gameOver(); //dealer wins if 21
            else if (handTotal > 16) checkWhoWon(); //check who won (higher score) if dealer is over 16
            else hitDealer(); //adds a card if dealer is under 16
        }

        private void hitDealer()
        {
            for (int i = 0; i < 4; i++)
            {
                if (dealerhand[i] < 0)
                {
                    dealerhand[i] = deck.dealCard().Value;
                    break;
                }
                //call display cards to display all the cards
            }
            displayCards();
            checkIfBust();
        }

        private void checkWhoWon()
        {
            MessageBox.Show("checkwhowon");
        }

        private void gameOver()
        {
            MessageBox.Show("gameover Dealer won");
        }

        private void btnHit_Click(object sender, RoutedEventArgs e)
        {
            //loop through array until the value is -1; set element at that index to -dealcard-; end loop
            for (int i = 0; i < 4; i++)
            {
                if (playerHand[i] < 0)
                {
                    playerHand[i] = deck.dealCard().Value;
                    break;
                }
            //call display cards to display all the cards
            }
            displayCards();
            checkIfBust();
        }

        private void checkIfBust()
        {
            MessageBox.Show("checkIfBust");
        }

        private void btnConsole_Click(object sender, RoutedEventArgs e)
        {
            //shows cards output
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //shows cards output
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (UIElement obj in canvas.Children)
            {
                if (obj.IsMouseOver)
                {
                    canvas.Children.Remove(obj);
                    break;
                }
            }
        }
    }
}
