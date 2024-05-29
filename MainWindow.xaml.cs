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

namespace CalendarV3
{
    public partial class MainWindow : Window
    {
        private DateTime currentDate;

        public MainWindow()
        {
            InitializeComponent();
            currentDate = DateTime.Now;
            RenderCalendar(currentDate);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddMonths(1);
            RenderCalendar(currentDate);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddMonths(-1);
            RenderCalendar(currentDate);
        }

        private void RenderCalendar(DateTime date)
        {

            calendarGrid.Children.Clear();


            var headers = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            for (int col = 0; col < 7; col++)
            {
                TextBlock header = new TextBlock
                {
                    Text = headers[col],
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetRow(header, 1);
                Grid.SetColumn(header, col);
                calendarGrid.Children.Add(header);
            }


            Button nextButton = new Button
            {
                Content = "Next",
                FontSize = 15,
                Height = 50,
                Width = 50,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            nextButton.Click += Next_Click;
            Grid.SetColumn(nextButton, 4);
            calendarGrid.Children.Add(nextButton);

            Button backButton = new Button
            {
                Content = "Back",
                FontSize = 15,
                Height = 50,
                Width = 50,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            backButton.Click += Back_Click;
            Grid.SetColumn(backButton, 2);
            calendarGrid.Children.Add(backButton);

            TextBlock monthLabel = new TextBlock
            {
                Text = date.ToString("MMMM yyyy"),
                FontSize = 30,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Grid.SetColumn(monthLabel, 2);
            Grid.SetColumnSpan(monthLabel, 3);
            calendarGrid.Children.Add(monthLabel);

            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime firstDayOfDisplayWeek = firstDayOfMonth.AddDays(-(int)firstDayOfMonth.DayOfWeek + (int)DayOfWeek.Monday);

            for (int row = 2; row < 7; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    TextBlock dayText = new TextBlock
                    {
                        Text = firstDayOfDisplayWeek.Day.ToString(),
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    if (firstDayOfDisplayWeek.Month != date.Month)
                    {
                        dayText.Foreground = Brushes.Gray;
                    }
                    Grid.SetRow(dayText, row);
                    Grid.SetColumn(dayText, col);
                    calendarGrid.Children.Add(dayText);

                    firstDayOfDisplayWeek = firstDayOfDisplayWeek.AddDays(1);
                }
            }
        }
    }
}