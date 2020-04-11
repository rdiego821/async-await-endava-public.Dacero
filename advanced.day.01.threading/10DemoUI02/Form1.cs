using _10DemoUI.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10DemoUI02
{
    public partial class Form1 : Form
    {
        private const string Path = "..\\..\\..\\cars.csv";
        private delegate void SafeCallDelegate(string text);

        public Form1()
        {
            this.InitializeComponent();
        }

        private void GetDataBtn_Click(object sender, EventArgs e)
        {
            this.Log("start to process file");

            Task task1 = Task.Factory.StartNew(() =>
            {
                return this.ProcessCarsFile(Path).ToList();
                
            }).ContinueWith(ant =>
            {
                if (ant.Exception != null)
                {
                    Console.WriteLine(ant.Exception.Message);
                }
                else
                {
                    this.DisplayCars(ant.Result);
                    this.Log($"finish to process file. {ant.Result.Count()} cars downloaded");
                }
            });

            task1.Wait();

        }

        private void DisplayCars(List<Car> cars)
        {
            foreach (var car in cars)
            {
                this.AppendToContent(car.ToString());
            }
        }

        private IEnumerable<Car> ProcessCarsFile(string filePath)
        {
            var cars = new List<Car>(600);
            var lines = File.ReadAllLines(filePath).Skip(2);

            foreach (var line in lines)
            {
                cars.Add(Car.Parse(line));
            }

            Thread.Sleep(TimeSpan.FromSeconds(3)); // simulate some work

            return cars;
        }

        public void Log(string s)
        {
            this.logTbx.AppendText($"{DateTime.Now} - {s}{Environment.NewLine}");
        }

        public void AppendToContent(string s)
        {
            if (this.contentTxb.InvokeRequired)
            {
                var d = new SafeCallDelegate(AppendToContent);
                contentTxb.Invoke(d, new object[] { s });
            }
            else
            {

                this.contentTxb.AppendText($"{s}{Environment.NewLine}");
            }
            
        }
    }
}
