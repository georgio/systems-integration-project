﻿using System;
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
		MessageQueue msgQ = new MessageQueue(@".\private$\restaurant2");
		public Form1()
		{
			InitializeComponent();
			try
			{

				((XmlMessageFormatter)msgQ.Formatter).TargetTypes = new Type[] { typeof(Booking) };
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
			Booking req = (Booking)msg.Body;
			handleRequest(req, msg.CorrelationId, msg.ResponseQueue);
			IAsyncResult AsyncResult = ((MessageQueue)e.AsyncResult.AsyncState).BeginReceive(new TimeSpan(1, 0, 0), ((MessageQueue)e.AsyncResult.AsyncState));
		}
		private void handleRequest(Booking req, string bookingID, MessageQueue resQ)
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
			public string firstName, lastName, date, time;
			public int count;
		}
		public struct BookingResponse
		{
			public string firstName, lastName, date, time, status;
			public int guestNumber;
		}

	}
}
