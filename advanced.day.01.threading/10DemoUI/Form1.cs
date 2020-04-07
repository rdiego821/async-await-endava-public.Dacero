﻿namespace _10DemoUI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using Core;

    public partial class Form1 : Form
    {
        private const string Path = "..\\..\\..\\cars.csv";
        private delegate void SafeCallDelegate(string text);

        public Form1()
        {
            this.InitializeComponent();
        }

        private void ProcessCarsThread()
        {
            this.Log("start to process file");

            Thread threadCars = new Thread(() =>
            {
                var cars = this.ProcessCarsFile(Path).ToList();

                this.DisplayCars(cars);

                this.Log($"finish to process file. {cars.Count()} cars downloaded");
            });
            threadCars.Start();

        }

        private void GetDataBtn_Click(object sender, EventArgs e)
        {
            ProcessCarsThread();
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
            if (this.logTbx.InvokeRequired)
            {
                var d = new SafeCallDelegate(Log);
                contentTxb.Invoke(d, new object[] { s });
            }
            else
            {
                this.logTbx.AppendText($"{DateTime.Now} - {s}{Environment.NewLine}");
            }
        }

        public void AppendToContent(string s)
        {
            if (this.contentTxb.InvokeRequired)
            {
                var d = new SafeCallDelegate(AppendToContent);
                contentTxb.Invoke(d, new object[] { s });
            }
            else { 

                this.contentTxb.AppendText($"{s}{Environment.NewLine}");
            }

        }
    }
}
