using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;
using Rehlander;

namespace Calendar
{
	public partial class Window1
	{
        DayManager manager = new DayManager("scott", "password");
        bool bigMode = true;
        double[] heightAndWidth;
        bool safeHeightChange = false;

		public Window1()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
            PopulateDays(DateTime.Now.Month, DateTime.Now.Year);
		}

        private void onDragStarted(object sender, DragStartedEventArgs e)
		{
		}
		
        private void onDragCompleted(object sender, DragCompletedEventArgs e)
        {
        }
		
        private void onDragDelta(object sender, DragDeltaEventArgs e)
        {
			if (sender == this.Window.MoveWindow)
			{
				Canvas.SetLeft(this, Canvas.GetLeft(this) + e.HorizontalChange);
				Canvas.SetTop(this, Canvas.GetTop(this) + e.VerticalChange);
			}
        }

        private void PopulateDays(int month, int year)
        {
            List<CalendarDay> days = manager.GetCalendarDays(month, year);

            // Add blank days
            DateTime firstDay = new DateTime(year, month, 1);
 
            DayOfWeek dayIterator = 0;
            while (dayIterator++ != firstDay.DayOfWeek)
            {
                Day newDay = new Day();
                newDay.DayStyleType = Day.enumdaytype.Blank;
                mainGrid.Children.Add(newDay);
            }

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                Day newDay = new Day();
                newDay.MouseDown += new System.Windows.Input.MouseButtonEventHandler(newDay_MouseDown);
                newDay.CalendarDay = days[i - 1];
                newDay.DayStyleType = Day.enumdaytype.Default;
                newDay.DayNumber = i;
                mainGrid.Children.Add(newDay);
            }
        }

        void newDay_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Day newBigDay = new Day();
            newBigDay.DayNumber = (sender as Day).DayNumber;
            newBigDay.DayStyleType = Day.enumdaytype.Big;
            newBigDay.CalendarDay = (sender as Day).CalendarDay;
            newBigDay.Margin = new Thickness(10, 10, 10, 10);
            newBigDay.OnCloseRequest += new Day.OnCloseRequestHandler(newBigDay_OnCloseRequest);
            overLayGrid.Children.Add(newBigDay);
            overLayGrid.SetValue(Grid.ZIndexProperty, 2);
            newBigDay.OnEditFinished += new Day.OnEditFinishedHander(newBigDay_OnEditFinished);
        }

        void newBigDay_OnEditFinished(CalendarDay day)
        {
            manager.UpdateDay(day);
            manager.Save();
        }

        void newBigDay_OnCloseRequest()
        {
            if (overLayGrid.Children.Count > 0)
                overLayGrid.Children.Clear();
        }

        private void Window_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (bigMode)
            {
                heightAndWidth = new double[] { ActualHeight, ActualWidth };
                safeHeightChange = true;
                Height = MinHeight;
                Width = MinWidth;
                safeHeightChange = false;
                bigMode = false;   
            }
            else
            {
                safeHeightChange = true;
                Height = heightAndWidth[0];
                Width = heightAndWidth[1];
                safeHeightChange = false;

                bigMode = true;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (safeHeightChange) return;

            bigMode = true;
            heightAndWidth = new double[] { ActualHeight, ActualWidth };
        }

    }
}