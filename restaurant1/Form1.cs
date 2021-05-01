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
			msgQ.Path = @".\private$\restaurant1";
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			System.Messaging.Message message;

			try
			{
				((XmlMessageFormatter)msgQ.Formatter).TargetTypes = new Type[] { typeof(Booking) };
				msg = msgQ.Receive(new TimeSpan(0, 0, 3));
				Booking req = (Booking)message.Body;
				handleRequest(req, msg.CorrelationId, msg.ResponseQueue);
			}
			catch (MessageQueueException)
			{
				MessageBox.Show("No bookings!");
			}

		}
		private void handleRequest(Booking req, string bookingID, MessageQueue resQ)
		{
			sb = new StringBuilder();
			sb.Append("Full Name: " + booking.fullName);
			sb.Append("\n");
			sb.Append("Persons: " + booking.persons.ToString());
			sb.Append("\n");
			sb.Append("Date Time: " + booking.dateTime);
			MessageBox.Show(sb.ToString(), "Message Received!");
			sb.Append("\n");
			sb.Append("\n");
			sb.Append("Approve booking request?");

			DialogResult dr = MessageBox.Show(sb.ToString(), "Booking Response Received!", MessageBoxButtons.YesNo);

			BookingResponse res;
			res.firstName = req.firstName;
			res.lastName = req.lastName;
			res.date = req.date;
			res.time = req.time;
			res.guestNumber = req.count;
			res.status = (dr == DialogResult.Yes) ? "Confirmed" : "Non Confirmed";

			System.Messaging.Message msg = new System.Messaging.Message();
			msg.Body = res;
			msg.CorrelationId = bookingID;
			resQ.Send(msg);
			resQ.Close();
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
