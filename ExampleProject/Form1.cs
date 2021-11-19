using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var c = new Core();
            c.Notify += DisplayMessage;
            c.ExampleMethod();
        }

        private void DisplayMessage(object sender, LogEventArgs e)
        {
            richTextBox1.AppendText("\r\n" + e.Date.ToString("dd/MM/yyyy HH:mm:ss "), Color.SlateGray);
            richTextBox1.AppendText(e.Message, Color.Blue);
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
            
        }
    }

    class Core
    {
        public delegate void LogHandler(object sender, LogEventArgs e);
        public event LogHandler Notify;


        public void ExampleMethod()
        {
            var rand = new Random();

            for (int i = 1; i < 10000; i++)
            {   var pause = rand.Next(50, 2000);
                Thread.Sleep(pause);
                Notify?.Invoke(this, new LogEventArgs($"logged {i} pause in miliseconds {pause}", DateTime.Now, MessageType.Notice));
            }
        }
    }
        class LogEventArgs
    {

        public string Message { get; }

        public DateTime Date { get; }
        public MessageType MessageType { get; }


        public LogEventArgs(string mes, DateTime date, MessageType messageType)
        {
            Message = mes;
            Date = date;
            MessageType = messageType;
        }
    }

    enum MessageType
    {
        Notice,
        Warning,
        Error,
    }
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }


}
