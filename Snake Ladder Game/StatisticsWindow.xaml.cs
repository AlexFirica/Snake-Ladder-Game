using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Snake_Ladder_Game;

public partial class StatisticsWindow : Window
{
    int nrComponente = 5;
    string winner, scari, serpi, patratele, zaruri;
    public StatisticsWindow(string winner, int scari, int serpi, int patratele, int zaruri)
    {
        InitializeComponent();

        this.winner = winner;
        this.scari = $"🧗 Scări urcate: {scari}";
        this.serpi = $"🐍 Șerpi întâlniți: {serpi}";
        this.patratele = $"📦 Total pătrățele traversate: {patratele}";
        this.zaruri = $"🎲 Aruncături cu zarul: {zaruri}";

        updateTextStatistics(1);

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

    private void updateTextStatistics(int i)
    {
        if (i > nrComponente)
            return;

        switch (i)
        {
            case 1:
                txtWinner.Text = winner;
                break;
            case 2:
                txtScari.Text = scari;
                break;
            case 3:
                txtSerpi.Text = serpi;
                break;
            case 4:
                txtPatratele.Text = patratele;
                break;
            case 5:
                txtZaruri.Text = zaruri;
                break;
        }

        updateTextStatistics(i + 1);
    }
}