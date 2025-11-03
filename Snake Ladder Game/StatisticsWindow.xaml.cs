using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Snake_Ladder_Game;

public partial class StatisticsWindow : Window
{
    public StatisticsWindow(string winner, int scari, int serpi, int patratele, int zaruri)
    {
        InitializeComponent();

        txtWinner.Text = winner;
        txtScari.Text = $"🧗 Scări urcate: {scari}";
        txtSerpi.Text = $"🐍 Șerpi întâlniți: {serpi}";
        txtPatratele.Text = $"📦 Total pătrățele traversate: {patratele}";
        txtZaruri.Text = $"🎲 Aruncături cu zarul: {zaruri}";

        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(4) };
        timer.Tick += (s, e) =>
        {
            shortVideo.Visibility = Visibility.Collapsed;
            timer.Stop();
        };
        timer.Start();
    }

    private void PlayAgain_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }
}