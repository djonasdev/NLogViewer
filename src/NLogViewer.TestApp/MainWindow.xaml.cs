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
        public const string LOREM_IPSUM = @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        
        private readonly Logger _Logger = LogManager.GetCurrentClassLogger();
        public MainWindow()
        {
            Title = $"Testing v{AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName}";
            InitializeComponent();
            Random random = new Random();
            Observable.Interval(TimeSpan.FromMilliseconds(250)).ObserveOnDispatcher().Subscribe(l =>
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
                        try
                        {
                            int a = 0;
                            int b = 1;
                            var c = b / a;
                        }
                        catch (Exception ex)
                        {
                            _Logger.Error(ex, "There was an error on divison :/");
                        }
                        break;
                    case 6:
                        _Logger.Fatal(LOREM_IPSUM);
                        break;
                }
            });
        }
    }
}
