using System;
using System.Collections.Generic;
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

namespace Calendar
{
	/// <summary>
	/// Interaction logic for DayName.xaml
	/// </summary>
	public partial class DayName : UserControl
	{
		public DayName()
		{
			this.InitializeComponent();
		}
		
		public enum daysoftheweek
		{
  			Sunday,
  			Monday,
  			Tuesday,
			Wednesday,
			Thursday,
			Friday,
			Saturday
		}
		
		private daysoftheweek day;
		
		public daysoftheweek Day
		{
  			get
			{
				return day;
			}
   			set
			{
				day = value;
				this.NameOfTheDay.Text = Day.ToString();
			}
		}
	}
}