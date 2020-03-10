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
        Deck deck;
        Hand player;
        Hand dealer;

        public MainWindow()
        {
            InitializeComponent();
            player = new Hand("Player");
            dealer = new Hand("Dealer");

            deck = new Deck();
            deck.shuffleDeck();
            MessageBox.Show(deck.outputCards());

            playGame();
        }

        public void playGame()
        {
            for (int i = 0; i < 2; i++)
            {
                player.Add(deck.dealCard());
                dealer.Add(deck.dealCard());
            }
            MessageBox.Show(player.ToString());
            MessageBox.Show(dealer.ToString());
        }
    }
}
