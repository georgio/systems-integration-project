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
	public partial class Restaurant1 : Form
	{
		MessageQueue msgQ = new MessageQueue();
		public Restaurant1()
		{
			InitializeComponent();
			msgQ.Path = @".\private$\restaurantOne";
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			System.Messaging.Message message;
			Reservation1 booking = new Reservation1();
			StringBuilder sb;

			try
			{
				((XmlMessageFormatter)msgQ.Formatter).TargetTypes = new Type[] { typeof(Reservation1) };
				msgQ.MessageReadPropertyFilter.CorrelationId = true;
				message = msgQ.Receive(new TimeSpan(0, 0, 3));
				booking = (Reservation1)message.Body;
				sb = new StringBuilder();
				sb.Append("Full Name: " + booking.fullName);
				sb.Append("\n");
				sb.Append("Persons: " + booking.persons.ToString());
				sb.Append("\n");
				sb.Append("Date Time: " + booking.dateTime);
				DialogResult dr = MessageBox.Show(sb.ToString(), "Booking Received!", MessageBoxButtons.YesNo);

				BookingResponse res;
				res.status = (dr == DialogResult.Yes) ? "Confirmed" : "Non Confirmed";
				res.Id = req.Id;

				System.Messaging.Message msg = new System.Messaging.Message();
				msg.Body = res;
				resQ.Send(msg);
				resQ.Close();
			}
			catch (MessageQueueException)
			{
				MessageBox.Show("No bookings!");
			}

		}
		public struct Reservation1
		{
			public string fullName, dateTime, Id;
			public int persons;
		}
		public struct BookingResponse
		{
			public string status, Id;
		}
	}
}
