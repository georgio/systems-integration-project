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

		List<GenericReservation> reservations = new List<GenericReservation>();

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
				MessageBox.Show("Error", e.Message);
			}

		}
		private void button1_Click(object sender, System.EventArgs e)
		{
			GenericReservation reservation;
			reservation.Id = new Random().Next(0, 1000000).ToString();

			if (comboBox1.SelectedItem.ToString() == "Restaurant 1")
			{
				reservation.restaurantID = 1;
			}
			else if (comboBox1.SelectedItem.ToString() == "Restaurant 2")
			{
				reservation.restaurantID = 2;
			}
			else
			{
				MessageBox.Show("Error", "Please select a correct restaurant");
				return;
			}

			reservation.date = datePicker.Text;
			reservation.time = timePicker.Value;

			reservation.firstName = firstNameBox.Text;
			reservation.lastName = lastNameBox.Text;

			reservation.guests = Convert.ToInt32(guestNumberBox.Text);

			reservations.Add(reservation);
			processReservation(reservation);
		}
		private void processReservation(GenericReservation reservation)
		{
			System.Messaging.Message msg = new System.Messaging.Message();
			msg.ResponseQueue = resQ;

			string time = reservation.time.ToString("hh:mm");
			if (reservation.restaurantID == 1)
			{
				Reservation1 r;
				r.dateTime = reservation.date + " " + time;
				r.fullName = reservation.firstName + " " + reservation.lastName;
				r.persons = reservation.guests;
				r.Id = reservation.Id;

				msg.Body = r;

				if (!MessageQueue.Exists(msgQR1.Path))
					msgQR1 = MessageQueue.Create(msgQR1.Path);

				msgQR1.Send(msg);
				msgQR1.Close();
			}
			else if (reservation.restaurantID == 2)
			{
				Reservation2 r;
				r.date = reservation.date;
				r.time = time;
				r.firstName = reservation.firstName;
				r.lastName = reservation.lastName;
				r.count = reservation.guests;
				r.Id = reservation.Id;

				msg.Body = r;
				if (!MessageQueue.Exists(msgQR2.Path))
					msgQR2 = MessageQueue.Create(msgQR2.Path);

				msgQR2.Send(msg);
				msgQR2.Close();
			}
			else
			{
				MessageBox.Show("Invalid Restaurant.", "Error");
			}

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
			GenericReservation r = reservations.Find(r => r.Id == res.Id);

			StringBuilder sb = new StringBuilder();

			sb.Append("\n");
			sb.Append("Restaurant: " + r.restaurantID.ToString());
			sb.Append("\n");
			sb.Append("Booked by: ");
			sb.Append("\n\tFirst Name:" + r.firstName);
			sb.Append("\n\tLast Name: " + r.lastName);
			sb.Append("\n");
			sb.Append("Guests: " + r.guests.ToString());
			sb.Append("\n");
			sb.Append("Date: " + r.date);
			sb.Append("\n");
			sb.Append("Time: " + r.time.ToString("h:mmtt"));
			sb.Append("\n");
			sb.Append("\n");
			sb.Append("Status: " + res.status);

			MessageBox.Show(sb.ToString(), "Booking Response Received!");

			reservations.Remove(r);
		}
		public struct GenericReservation
		{
			public string firstName, lastName, Id, date;
			public DateTime time;
			public int restaurantID, guests;
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
