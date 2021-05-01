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
	public partial class Client : Form
	{
		MessageQueue msgQR1 = new MessageQueue(@".\private$\restaurantOne");
		MessageQueue msgQR2 = new MessageQueue(@".\private$\restaurantTwo");
		MessageQueue resQ = new MessageQueue(@".\private$\client");

		public Client()
		{
			InitializeComponent();
			// create random message queue path
			//var uid_bytes = new byte[8];
			//new Random().NextBytes(uid_bytes);
			//string uid = @".\private$\"+ Encoding.Default.GetString(uid_bytes);


			((XmlMessageFormatter)resQ.Formatter).TargetTypes = new Type[] { typeof(BookingResponse) };
			try
			{

				resQ.MessageReadPropertyFilter.CorrelationId = true;
				resQ.ReceiveCompleted += new ReceiveCompletedEventHandler(MessageEventHandler);
				IAsyncResult resQResult = resQ.BeginReceive(new TimeSpan(1, 0, 0), resQ);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

		}
		private void button1_Click(object sender, System.EventArgs e)
		{
			string bookingID = new Random().Next(0, 10000).ToString();
			if (comboBox1.SelectedItem.ToString() == "Restaurant 1")
			{
				handleRestaurant1(bookingID);
			}
			else if (comboBox1.SelectedItem.ToString() == "Restaurant 2")
			{
				handleRestaurant2(bookingID);
			}
			else
			{
				// error
			}


		}
		private void handleRestaurant1(string bookingID)
		{
			Reservation1 reservation;
			reservation.dateTime = datePicker.Text + " " + timePicker.Value.ToString("hh:mm");
			reservation.fullName = firstNameBox.Text + " " + lastNameBox.Text;
			reservation.persons = Convert.ToInt32(guestNumberBox.Text);
			reservation.Id = bookingID;
			System.Messaging.Message msg = new System.Messaging.Message();
			msg.Body = reservation;
			msg.ResponseQueue = resQ;
			if (!MessageQueue.Exists(msgQR1.Path))
				msgQR1 = MessageQueue.Create(msgQR1.Path);
			msgQR1.Send(msg);
			msgQR1.Close();
			MessageBox.Show("Order submitted!");
		}
		private void handleRestaurant2(string bookingID)
		{
			Reservation2 reservation;
			reservation.date = datePicker.Text;
			reservation.time = timePicker.Value.ToString("hh:mm");
			reservation.firstName = firstNameBox.Text;
			reservation.lastName = lastNameBox.Text;
			reservation.count = Convert.ToInt32(guestNumberBox.Text);
			reservation.Id = bookingID;
			System.Messaging.Message msg = new System.Messaging.Message();
			msg.Body = reservation;
			msg.ResponseQueue = resQ;
			if (!MessageQueue.Exists(msgQR2.Path))
				msgQR2 = MessageQueue.Create(msgQR2.Path);
			msgQR2.Send(msg);
			msgQR2.Close();
			MessageBox.Show("Order submitted!");
		}
		private void MessageEventHandler(object sender, ReceiveCompletedEventArgs e)
		{
			System.Messaging.Message msg = ((MessageQueue)e.AsyncResult.AsyncState).EndReceive(e.AsyncResult);
			BookingResponse res = (BookingResponse)msg.Body;
			displayResponse(res);
			IAsyncResult AsyncResult = ((MessageQueue)e.AsyncResult.AsyncState).BeginReceive(new TimeSpan(1, 0, 0), ((MessageQueue)e.AsyncResult.AsyncState));
		}
		private void displayResponse(BookingResponse res)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Booked by: ");
			sb.Append("\n\tFirst Name:" + res.firstName);
			sb.Append("\n\tLast Name: " + res.lastName);
			sb.Append("\n");
			sb.Append("Guests: " + res.guestNumber.ToString());
			sb.Append("\n");
			sb.Append("Time: " + res.time);
			sb.Append("\n");
			sb.Append("Date: " + res.date);
			sb.Append("\n");
			sb.Append("\n");
			sb.Append("Status: " + res.status);

			MessageBox.Show(sb.ToString(), "Booking Response Received!");
		}
		public struct Reservation1
		{
			public string fullName, dateTime, Id;
			public int persons;
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
