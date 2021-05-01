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
        MessageQueue msgQR1 = new MessageQueue(@".\private$\restaurant1");
        MessageQueue msgQR2 = new MessageQueue(@".\private$\restaurant2");
        MessageQueue resQ = new MessageQueue(@".\private$\c1");
        public Form1()
        {
            // create random message queue path
            //var uid_bytes = new byte[8];
            //new Random().NextBytes(uid_bytes);
            //string uid = @".\private$\"+ Encoding.Default.GetString(uid_bytes);

            InitializeComponent();

            Console.WriteLine(resQ.Path);

            ((XmlMessageFormatter)resQ.Formatter).TargetTypes = new Type[] { typeof(BookingResponse) };
            try
            {

            resQ.ReceiveCompleted += new ReceiveCompletedEventHandler(MessageEventHandler);
            IAsyncResult resQResult = resQ.BeginReceive(new TimeSpan(1, 0, 0), resQ);
            } catch {
            //    MessageBox.Show("no msg");
            }

        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            var bytes = new byte[8];
            new Random().NextBytes(bytes);
            string bookingID = Encoding.Default.GetString(bytes);
            Console.WriteLine(comboBox1.SelectedText);
            if (comboBox1.SelectedText == "Restaurant 1") {
                handleRestaurant1(bookingID);
            } else if (comboBox1.SelectedText == "Restaurant 2") {
                handleRestaurant2(bookingID);
            } else
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
            System.Messaging.Message msg = new System.Messaging.Message();
            msg.Body = reservation;
            msg.CorrelationId = bookingID;
            msg.ResponseQueue = resQ;
            if (!MessageQueue.Exists(msgQR1.Path))
                msgQR1 = MessageQueue.Create(msgQR1.Path);
            msgQR1.Send(msg);
            msgQR1.Close();

        }
        private void handleRestaurant2(string bookingID)
        {
            Reservation2 reservation;
            reservation.date = datePicker.Text;
            reservation.time = timePicker.Value.ToString("hh:mm");
            reservation.firstName = firstNameBox.Text;
            reservation.lastName=lastNameBox.Text;
            reservation.count = Convert.ToInt32(guestNumberBox.Text);
            Console.WriteLine("here");
            Console.WriteLine(reservation);
            System.Messaging.Message msg = new System.Messaging.Message();
            msg.Body = reservation;
            msg.ResponseQueue = resQ;
            msg.CorrelationId = bookingID;
            if (!MessageQueue.Exists(msgQR2.Path))
                msgQR2 = MessageQueue.Create(msgQR2.Path);
            msgQR2.Send(msg);
            msgQR2.Close();
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
            public string fullName, dateTime;
            public int persons;
        }
        public struct Reservation2
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
