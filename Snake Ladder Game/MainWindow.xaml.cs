using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

    bool resetOffset = false;
    int nrScari = 0, nrSerpi = 0, nrPatratele = 0, nrZaruri = 0;

    public MainWindow()
    {
        InitializeComponent();

        SetupGame();
    }

    private void OnClickEvent(object sender, MouseButtonEventArgs e)
    {
        if (playerOneRound == false && playerTwoRound == false)
        {
            nrZaruri++;//TODO l-am pus bine?
            position = rand.Next(1, 7);
            txtPlayer.Content = "You rolled a " + position;
            currentPosition = 0;

            if ((i + position) <= 99)
            {
                playerOneRound = true;
                gameTimer.Start();
            }
            else
            {
                if (playerTwoRound == false)
                {
                    playerTwoRound = true;
                    opponentPosition = rand.Next(1, 7);
                    txtOpponent.Content = "Opponent Rolled a " + opponentPosition;
                    opponentCurrentPosition = 0;
                    gameTimer.Start();
                }
                else
                {
                    gameTimer.Stop();
                    playerOneRound = false;
                    playerTwoRound = false;
                }
            }
        }
    }

    private void SetupGame()
    {
        int leftPos = 10;
        int topPos = 600;

        int a = 0;

        playerImage.ImageSource =  new BitmapImage(new Uri("C:\\Users\\Claudiu\\Desktop\\Snake-Ladder-Game\\Snake Ladder Game\\images\\player.gif"));
        opponentImage.ImageSource =  new BitmapImage(new Uri("C:\\Users\\Claudiu\\Desktop\\Snake-Ladder-Game\\Snake Ladder Game\\images\\opponent.gif"));
        int i = 0;
        while (i<100)
        {
            images++;
            
            ImageBrush tileImages = new ImageBrush();
            tileImages.ImageSource = new BitmapImage(new Uri("C:\\Users\\Claudiu\\Desktop\\Snake-Ladder-Game\\Snake Ladder Game\\images\\" + images + ".jpg"));
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

            i++;
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
        MovePiece(opponent, "box" + 0);
    }

    private void GameTimerEvent(object sender, EventArgs e)
    {
        if (playerOneRound == true && playerTwoRound == false)
        {
            if (i < Moves.Count)
            {
                if (currentPosition < position)
                {
                    currentPosition++;
                    i++;
                    MovePiece(player, "box" + i);
                }
                else
                {
                    playerTwoRound = true;
                    i = CheckSnakesOrLadders(i);
                    MovePiece(player, "box" + i);

                    opponentPosition = rand.Next(1, 7);
                    txtOpponent.Content = "Adversarul a dat " + opponentPosition;
                    opponentCurrentPosition = 0;
                    tempPos = i;
                    txtPlayerPosition.Content = "Jucatorul este pe pozitia @ " + (tempPos + 1);
                }
            }

            if (i == 99)
            {
                gameTimer.Stop();
                ShowStatistics("🎉 Barcelona a câștigat ca de obicei!");
            }
        }


        if (playerTwoRound == true)
        {
            if (j < Moves.Count)
            {
                if (opponentCurrentPosition < opponentPosition && (j + opponentPosition < 101))
                {
                    opponentCurrentPosition++;
                    j++;
                    MovePiece(opponent, "box" + j);
                }
                else
                {
                    playerOneRound = false;
                    playerTwoRound = false;
                    j = CheckSnakesOrLadders(j);
                    MovePiece(opponent, "box" + j);
                    tempPos = j;
                    txtOpponentPosition.Content = "Adversarul este pe pozitia @ " + (tempPos + 1);
                    gameTimer.Stop();
                }
            }

            if (j == 99)
            {
                gameTimer.Stop();
                ShowStatistics("😔 Real Madrid a câștigat!");
            }
        }
    }
    private void RestartGame()
    {
        i = -1;
        j = -1;

        MovePiece(player, "box" + 0);
        MovePiece(opponent, "box" + 0);

        //TODO pls sa incepem de pe pozitia 1
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
        int startNum = num;

        if (num == 1)
        {
            num = 37;
        }
        if (num == 6)
        {
            num = 13;
        }
        if (num == 7)
        {
            num = 30;
        }
        if (num == 14)
        {
            num = 25;
        }
        if (num == 15)
        {
            num = 5;
        }
        if (num == 20)
        {
            num = 41;
        }
        if (num == 27)
        {
            num = 83;
        }
        if (num == 35)
        {
            num = 43;
        }
        if (num == 45)
        {
            num = 24;
        }
        if (num == 48)
        {
            num = 10;
        }
        if (num == 50)
        {
            num = 66;
        }
        if (num == 61)
        {
            num = 18;
        }
        if (num == 63)
        {
            num = 59;
        }
        if (num == 70)
        {
            num = 90;
        }
        if (num == 73)
        {
            num = 52;
        }
        if (num == 77)
        {
            num = 97;
        }
        if (num == 86)
        {
            num = 93;
        }
        if (num == 88)
        {
            num = 67;
        }
        if (num == 91)
        {
            num = 87;
        }
        if (num == 94)
        {
            num = 74;
        }
        if (num == 98)
        {
            num = 79;
        }

        //num = 99;

        if (startNum != num)
            nrPatratele--;
        if (startNum > num)
            nrSerpi++;
        if (startNum < num)
            nrScari++;

        return num;
    }

    private void MovePiece(Rectangle player, string posName)
    {
        foreach (Rectangle rectangle in Moves)
        {
            if (rectangle.Name == posName)
            {
                landingRec = rectangle;
            }
        }

        //Reseteaza pozitia jucatorilor daca au fost distantati
        if (resetOffset)
        {
            Canvas.SetLeft(this.player, Canvas.GetLeft(this.player) - 10);
            Canvas.SetLeft(this.opponent, Canvas.GetLeft(this.opponent) + 10);

            resetOffset = false;
        }

        Canvas.SetLeft(player, Canvas.GetLeft(landingRec) + player.Width / 2);
        Canvas.SetTop(player, Canvas.GetTop(landingRec) + player.Height / 2);

        double playerLeft = Canvas.GetLeft(this.player);
        double playerTop = Canvas.GetTop(this.player);
        double opponentLeft = Canvas.GetLeft(this.opponent);
        double opponentTop = Canvas.GetTop(this.opponent);

        //Distanteaza jucatorii daca sunt pe aceeasi pozitie ca sa nu fie overlap
        if (playerLeft == opponentLeft && playerTop == opponentTop)
        {
            Canvas.SetLeft(this.player, playerLeft + 10);
            Canvas.SetLeft(this.opponent, opponentLeft - 10);

            resetOffset = true;
        }

        if (player.Fill is ImageBrush imageBrush)
        {
            string imagePath = imageBrush.ImageSource.ToString();

            if (imagePath.Contains("player"))
                nrPatratele++;
        }
    }

    private void ShowStatistics(string winner)
    {
        var statsWindow = new StatisticsWindow(
            winner,
            nrScari,
            nrSerpi,
            nrPatratele,
            nrZaruri
        );

        bool? result = statsWindow.ShowDialog();

        if (result == true)
        {
            RestartGame();

            nrScari = 0;
            nrSerpi = 0;
            nrPatratele = 0;
            nrZaruri = 0;
        }
    }
}