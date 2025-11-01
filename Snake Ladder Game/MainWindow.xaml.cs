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

        for (int i=0; i<100; i++)
        {
            images++;
            
            ImageBrush tileImages = new ImageBrush();
            tileImages.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + images + ".jpg"));
            Rectangle box = new Rectangle
            {
                Height = 60,
                Width = 60,
                Fill = tileImages,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                
            };
            box.Name = "box" + i.ToString();
            this.RegisterName(box.Name, box);
            
            Moves.Add(box);

            if (a == 10)
            {
                topPos -= 60;
                a = 30;
            }
            if (a==20)
            {
                topPos -= 60;
                a = 0;
            }
            if(a>20)
            {
                a--;
                Canvas.SetLeft(box, leftPos);
                leftPos -= 60;
            }
            if(a<10)
            {   a++; 
                Canvas.SetLeft(box, leftPos);
                leftPos += 60;
                Canvas.SetLeft(box, leftPos);
            }
            Canvas.SetTop(box, topPos);

            MyCanvas.Children.Add(box);

        }

        gameTimer.Tick += GameTimerEvent;
        gameTimer.Interval = TimeSpan.FromSeconds(.2);

        player = new Rectangle
        {
            Height = 30,
            Width = 30,
            Fill = playerImage,
            StrokeThickness = 2
        };

        opponent = new Rectangle
        {
            Height = 30,
            Width = 30,
            Fill = opponentImage,
            StrokeThickness = 2
        };

        MyCanvas.Children.Add(player);
        MyCanvas.Children.Add(opponent);

        MovePiece(player, "box" + 0);
    }

    private void RestartGame()
    {
        i = -1;
        j = -1;

        MovePiece(player, "box" + 0);
        MovePiece(opponent, "box" + 0);


        position = 0;
        currentPosition = 0;


        opponentPosition = 0;
        opponentCurrentPosition = 0;


        playerOneRound = false;
        playerTwoRound = false;


        txtPlayer.Content = "You Rolled a" + position;
        txtPlayerPosition.Content = "Player is @ 1";

        txtOpponent.Content = "Opponent Rolled a " + opponentPosition;
        txtOpponentPosition.Content = "Opponent is @ 1";

        gameTimer.Stop();



    }

    private int CheckSnakesOrLadders(int num)
    {

        return num;
    }

    private void MovePiece(Rectangle player, string posName )
    {
        foreach (Rectangle rectangle in Moves)
        {
            if (rectangle.Name == posName)
            {
                landingRec = rectangle;
            }
        }


        Canvas.SetLeft(player, Canvas.GetLeft(landingRec) + player.Width / 2);

        Canvas.SetTop(player, Canvas.GetTop(landingRec) + player.Height / 2);
    }



}