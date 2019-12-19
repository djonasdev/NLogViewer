using System;
using System.Reactive.Linq;
using System.Windows;
using NLog;

namespace TestApplication
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Logger _Logger = LogManager.GetCurrentClassLogger();
        public MainWindow()
        {
            InitializeComponent();
            Random random = new Random();
            Observable.Interval(TimeSpan.FromMilliseconds(150)).Take(250).ObserveOnDispatcher().Subscribe(l =>
            {
                switch (random.Next(1,6))
                {
                    case 1:
                        _Logger.Trace("Hello everyone");
                        break;
                    case 2:
                        _Logger.Debug("Hello everyone");
                        break;
                    case 3:
                        _Logger.Info("Hello everyone");
                        break;
                    case 4:
                        _Logger.Warn("Hello everyone");
                        break;
                    case 5:
                        _Logger.Error("Hello everyone");
                        break;
                    case 6:
                        _Logger.Fatal("Hello everyone");
                        break;
                }
            });
        }
    }
}
