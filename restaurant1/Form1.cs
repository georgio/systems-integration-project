using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Messaging;

namespace booking
{
	public partial class Form1 : Form
	{
		MessageQueue msgQ = new MessageQueue();
		public Form1()
		{
			InitializeComponent();
			msgQ.Path = @".\private$\restaurantOne
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			System.Messaging.Message message;
			Booking booking = new Booking();
			StringBuilder sb;

			try
			{
				((XmlMessageFormatter)msgQ.Formatter).TargetTypes = new Type[] { typeof(Booking) };
				message = msgQ.Receive(new TimeSpan(0, 0, 3));
				booking = (Booking)message.Body;

				sb = new StringBuilder();
				sb.Append("Full Name: " + booking.fullName);
				sb.Append("\n");
				sb.Append("Persons: " + booking.persons.ToString());
				sb.Append("\n");
				sb.Append("Date Time: " + booking.dateTime);
				MessageBox.Show(sb.ToString(), "Message Received!");
			}
			catch (MessageQueueException)
			{
				MessageBox.Show("No bookings!");
			}

		}
		public struct Booking
		{
			public string fullName, dateTime;
			public int persons;
		}
		public struct BookingResponse
		{
			public string firstName, lastName, date, time, status;
			public int guestNumber;
		}
	}
}
