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
	public partial class Restaurant2 : Form
	{
		MessageQueue msgQ = new MessageQueue(@".\private$\restaurantTwo");
		public Restaurant2()
		{
			InitializeComponent();
			try
			{

				((XmlMessageFormatter)msgQ.Formatter).TargetTypes = new Type[] { typeof(Reservation2) };
				msgQ.MessageReadPropertyFilter.CorrelationId = true;
				msgQ.ReceiveCompleted += new ReceiveCompletedEventHandler(MessageEventHandler);
				IAsyncResult msgQResult = msgQ.BeginReceive(new TimeSpan(1, 0, 0), msgQ);
			}
			catch
			{

			}
		}
		private void MessageEventHandler(object sender, ReceiveCompletedEventArgs e)
		{
			System.Messaging.Message msg = ((MessageQueue)e.AsyncResult.AsyncState).EndReceive(e.AsyncResult);
			Reservation2 req = (Reservation2)msg.Body;
			handleRequest(req, msg.ResponseQueue);
			IAsyncResult AsyncResult = ((MessageQueue)e.AsyncResult.AsyncState).BeginReceive(new TimeSpan(1, 0, 0), ((MessageQueue)e.AsyncResult.AsyncState));
		}
		private void handleRequest(Reservation2 req, MessageQueue resQ)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Booked by: ");
			sb.Append("\n\tFirst Name:" + req.firstName);
			sb.Append("\n\tLast Name: " + req.lastName);
			sb.Append("\n");
			sb.Append("Count: " + req.count.ToString());
			sb.Append("\n");
			sb.Append("Time: " + req.time);
			sb.Append("\n");
			sb.Append("Date: " + req.date);
			sb.Append("\n");
			sb.Append("\n");
			sb.Append("Approve booking request?");

			DialogResult dr = MessageBox.Show(sb.ToString(), "Booking Received!", MessageBoxButtons.YesNo);

			BookingResponse res;
			res.status = (dr == DialogResult.Yes) ? "Confirmed" : "Non Confirmed";
			res.Id = req.Id;

			System.Messaging.Message msg = new System.Messaging.Message();
			msg.Body = res;
			resQ.Send(msg);
			resQ.Close();
		}
		public struct Reservation2
		{
			public string firstName, lastName, date, time, Id;
			public int count;
		}
		public struct BookingResponse
		{
			public string status, Id;
		}

	}
}
