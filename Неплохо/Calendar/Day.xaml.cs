using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Rehlander;
using System.Windows.Input;

namespace Calendar
{
	public partial class Day
	{
        public delegate void OnCloseRequestHandler();
        public event OnCloseRequestHandler OnCloseRequest;

        public delegate void OnEditFinishedHander(CalendarDay day);
        public event OnEditFinishedHander OnEditFinished;

        public enum enumwdwe
        {
            Weekday,
            Weekend
        }

        public enum enumdaytype
        {
            Blank,
            Default,
            Big
        }

        private int daynumber;
        public int DayNumber
        {
            get
            {
                return daynumber;
            }
            set
            {
                daynumber = value;
                Number.Text = daynumber.ToString();
            }
        }

        private enumwdwe wdwe;
        public enumwdwe WdWe
        {
            get
            {
                return wdwe;
            }
            set
            {
                wdwe = value;
                if (wdwe == enumwdwe.Weekend)
                {
                    DayBackground.Fill = (LinearGradientBrush)FindResource("WeekendFill");
                    DayBackground.Stroke = (LinearGradientBrush)FindResource("WeekendStroke");
                }
            }
        }

        private enumdaytype daystyletype;
        public enumdaytype DayStyleType
        {
            get
            {
                return daystyletype;
            }
            set
            {
                daystyletype = value;
                if (daystyletype == enumdaytype.Blank)
                {
                    LayoutRoot.Visibility = System.Windows.Visibility.Hidden;
                }
                else if (daystyletype == enumdaytype.Big)
                {
                    tbEdit.Visibility = Visibility.Visible;
                    tbBack.Visibility = Visibility.Visible;
                    mouseLeaveTrigger.Actions.Clear();
                }
            }
        }

        CalendarDay calenderDay;
        public CalendarDay CalendarDay
        {
            get { return calenderDay; }
            set
            {
                calenderDay = value;

                Binding b2 = new Binding("Data");
                b2.Source = value;
                tbData.SetBinding(TextBlock.TextProperty, b2);
            }
        }

		public Day()
		{
			this.InitializeComponent();

			// Insert code required on object creation below this point.
		}

        private void tbBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (OnCloseRequest != null)
                OnCloseRequest();
        }

        private void tbEdit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (tbEdit.Text.Equals("Done"))
            {
                for(int i = 0; i < LayoutRoot.Children.Count; i++)
                    if (LayoutRoot.Children[i] is TextBox)
                    {
                        LayoutRoot.Children.Remove(LayoutRoot.Children[i]);
                        // Exit the loop
                        i = LayoutRoot.Children.Count + 1;
                    }

                if (OnEditFinished != null)
                    OnEditFinished(calenderDay);

                tbEdit.Text = "Edit";
                tbData.Visibility = Visibility.Visible;
            }
            else
            {
                TextBox tb = new TextBox();
                tb.TextChanged += new TextChangedEventHandler(tb_TextChanged);
                Binding b = new Binding("Data");
                b.Mode = BindingMode.TwoWay;
                b.Source = calenderDay;
                tb.SetBinding(TextBox.TextProperty, b);

                tb.SetValue(Grid.RowProperty, 2);
                tb.SetValue(Grid.ColumnProperty, 1);
                tb.Foreground = Brushes.White;
                tb.Background = Brushes.Transparent;
                tb.BorderThickness = new Thickness(0);
                tb.Margin = new Thickness(5, 5, 5, 25);
                tb.TextWrapping = TextWrapping.Wrap;
                tb.AcceptsReturn = true;
                tbData.Visibility = Visibility.Hidden;

                LayoutRoot.Children.Add(tb);

                tb.SelectAll();
                tb.Focus();

                tbEdit.Text = "Done";
            }

        }

        void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            calenderDay.Data = (sender as TextBox).Text;
        }

	}
}