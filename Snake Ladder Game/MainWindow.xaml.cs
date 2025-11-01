using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;


namespace Snake_Ladder_Game;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Rectangle landingRec;
    Rectangle player;
    Rectangle opponent;

    List<Rectangle> Moves = new List<Rectangle>();

    DispatcherTimer gameTimer = new DispatcherTimer();

    ImageBrush playerImage = new ImageBrush();
    ImageBrush opponentImage = new ImageBrush();

    int i = -1;
    int j = -1;

    int position;
    int currentPosition;

    int opponentPosition;
    int opponentCurrentPosition;

    int images = -1;

    Random rand = new Random();

    bool playerOneRound, playerTwoRound;

    int tempPos;


    public MainWindow()
    {
        InitializeComponent();

        SetupGame();
    }

    private void OnClickEvent(object sender, MouseButtonEventArgs e)
    {

    }

    private void SetupGame()
    {
        int leftPos = 10;
        int topPos = 600;

        int a = 0;

        playerImage.ImageSource =  new BitmapImage(new Uri("pack://application:,,,/images/player.gif"));
        opponentImage.ImageSource =  new BitmapImage(new Uri("pack://application:,,,/images/opponent.gif"));


    }

    private void RestartGame()
    {

    }

    private int CheckSnakesOrLadders(int num)
    {

        return num;
    }

    private void MovePiece(Rectangle player, string posName )
    {

    }



}