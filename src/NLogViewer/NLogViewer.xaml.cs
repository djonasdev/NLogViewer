using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using DJ.Targets;
using NLog;

namespace DJ
{
    /// <summary>
    /// Interaktionslogik für NLogViewer.xaml
    /// </summary>
    public partial class NLogViewer : UserControl
    {
        // ##############################################################################################################################
        // Dependency Properties
        // ##############################################################################################################################

        #region Dependency Properties

        /// <summary>
        /// MyComment
        /// </summary>
        [Category("NLogViewer")]
        public ICollectionView LogEvents
        {
            get => (ICollectionView) GetValue(LogEventsProperty);
            private set => SetValue(LogEventsProperty, value);
        }

        /// <summary>
        /// The <see cref="LogEvents"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty LogEventsProperty = DependencyProperty.Register("LogEvents",
            typeof(ICollectionView), typeof(NLogViewer), new PropertyMetadata(null));

        /// <summary>
        /// The background for the trace output
        /// </summary>
        [Category("NLogViewer")]
        public Brush TraceBackground
        {
            get => (Brush) GetValue(TraceBackgroundProperty);
            set => SetValue(TraceBackgroundProperty, value);
        }

        /// <summary>
        /// The <see cref="TraceBackground"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty TraceBackgroundProperty =
            DependencyProperty.Register("TraceBackground", typeof(Brush), typeof(NLogViewer),
                new PropertyMetadata((Brush) (new BrushConverter().ConvertFrom("#D3D3D3"))));

        /// <summary>
        /// The foreground for the trace output
        /// </summary>
        [Category("NLogViewer")]
        public Brush TraceForeground
        {
            get => (Brush) GetValue(TraceForegroundProperty);
            set => SetValue(TraceForegroundProperty, value);
        }

        /// <summary>
        /// The <see cref="TraceForeground"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty TraceForegroundProperty =
            DependencyProperty.Register("TraceForeground", typeof(Brush), typeof(NLogViewer),
                new PropertyMetadata((Brush) (new BrushConverter().ConvertFrom("#042271"))));

        /// <summary>
        /// The background for the debug output
        /// </summary>
        [Category("NLogViewer")]
        public Brush DebugBackground
        {
            get => (Brush) GetValue(DebugBackgroundProperty);
            set => SetValue(DebugBackgroundProperty, value);
        }

        /// <summary>
        /// The <see cref="DebugBackground"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty DebugBackgroundProperty =
            DependencyProperty.Register("DebugBackground", typeof(Brush), typeof(NLogViewer),
                new PropertyMetadata((Brush) (new BrushConverter().ConvertFrom("#90EE90"))));

        /// <summary>
        /// The foreground for the debug output
        /// </summary>
        [Category("NLogViewer")]
        public Brush DebugForeground
        {
            get => (Brush) GetValue(DebugForegroundProperty);
            set => SetValue(DebugForegroundProperty, value);
        }

        /// <summary>
        /// The <see cref="DebugForeground"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty DebugForegroundProperty =
            DependencyProperty.Register("DebugForeground", typeof(Brush), typeof(NLogViewer),
                new PropertyMetadata((Brush) (new BrushConverter().ConvertFrom("#042271"))));

        /// <summary>
        /// The background for the info output
        /// </summary>
        [Category("NLogViewer")]
        public Brush InfoBackground
        {
            get => (Brush) GetValue(InfoBackgroundProperty);
            set => SetValue(InfoBackgroundProperty, value);
        }

        /// <summary>
        /// The <see cref="InfoBackground"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty InfoBackgroundProperty = DependencyProperty.Register("InfoBackground",
            typeof(Brush), typeof(NLogViewer),
            new PropertyMetadata((Brush) (new BrushConverter().ConvertFrom("#0000FF"))));

        /// <summary>
        /// The foreground for the info output
        /// </summary>
        [Category("NLogViewer")]
        public Brush InfoForeground
        {
            get => (Brush) GetValue(InfoForegroundProperty);
            set => SetValue(InfoForegroundProperty, value);
        }

        /// <summary>
        /// The <see cref="InfoForeground"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty InfoForegroundProperty = DependencyProperty.Register("InfoForeground",
            typeof(Brush), typeof(NLogViewer), new PropertyMetadata(Brushes.White));

        /// <summary>
        /// The background for the warn output
        /// </summary>
        [Category("NLogViewer")]
        public Brush WarnBackground
        {
            get => (Brush) GetValue(WarnBackgroundProperty);
            set => SetValue(WarnBackgroundProperty, value);
        }

        /// <summary>
        /// The <see cref="WarnBackground"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty WarnBackgroundProperty = DependencyProperty.Register("WarnBackground",
            typeof(Brush), typeof(NLogViewer),
            new PropertyMetadata((Brush) (new BrushConverter().ConvertFrom("#FFFF00"))));

        /// <summary>
        /// The foreground for the warn output
        /// </summary>
        [Category("NLogViewer")]
        public Brush WarnForeground
        {
            get => (Brush) GetValue(WarnForegroundProperty);
            set => SetValue(WarnForegroundProperty, value);
        }

        /// <summary>
        /// The <see cref="WarnForeground"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty WarnForegroundProperty = DependencyProperty.Register("WarnForeground",
            typeof(Brush), typeof(NLogViewer),
            new PropertyMetadata((Brush) (new BrushConverter().ConvertFrom("#324B5C"))));

        /// <summary>
        /// The background for the error output
        /// </summary>
        [Category("NLogViewer")]
        public Brush ErrorBackground
        {
            get => (Brush) GetValue(ErrorBackgroundProperty);
            set => SetValue(ErrorBackgroundProperty, value);
        }

        /// <summary>
        /// The <see cref="ErrorBackground"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty ErrorBackgroundProperty =
            DependencyProperty.Register("ErrorBackground", typeof(Brush), typeof(NLogViewer),
                new PropertyMetadata(Brushes.Red));

        /// <summary>
        /// The foreground for the error output
        /// </summary>
        [Category("NLogViewer")]
        public Brush ErrorForeground
        {
            get => (Brush) GetValue(ErrorForegroundProperty);
            set => SetValue(ErrorForegroundProperty, value);
        }

        /// <summary>
        /// The <see cref="ErrorForeground"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty ErrorForegroundProperty =
            DependencyProperty.Register("ErrorForeground", typeof(Brush), typeof(NLogViewer),
                new PropertyMetadata(Brushes.White));

        /// <summary>
        /// The background for the fatal output
        /// </summary>
        [Category("NLogViewer")]
        public Brush FatalBackground
        {
            get => (Brush) GetValue(FatalBackgroundProperty);
            set => SetValue(FatalBackgroundProperty, value);
        }

        /// <summary>
        /// The <see cref="FatalBackground"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty FatalBackgroundProperty =
            DependencyProperty.Register("FatalBackground", typeof(Brush), typeof(NLogViewer),
                new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// The foreground for the fatal output
        /// </summary>
        [Category("NLogViewer")]
        public Brush FatalForeground
        {
            get => (Brush) GetValue(FatalForegroundProperty);
            set => SetValue(FatalForegroundProperty, value);
        }

        /// <summary>
        /// The <see cref="FatalForeground"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty FatalForegroundProperty =
            DependencyProperty.Register("FatalForeground", typeof(Brush), typeof(NLogViewer),
                new PropertyMetadata(Brushes.Yellow));
        
        /// <summary>
        /// Automatically scroll to the newest entry
        /// </summary>
        [Category("NLogViewer")]
        public bool AutoScroll
        {
            get => (bool)GetValue(AutoScrollProperty);
            set => SetValue(AutoScrollProperty, value);
        }

        /// <summary>
        /// The <see cref="AutoScroll"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty AutoScrollProperty = DependencyProperty.Register("AutoScroll", typeof(bool), typeof(NLogViewer), new PropertyMetadata(true, AutoScrollChangedCallback));

        private static void AutoScrollChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is NLogViewer instance)
            {
                instance.OnAutoScrollChanged();
            }
        }

        protected virtual void OnAutoScrollChanged()
        {
            if (AutoScroll)
                DataGrid?.ScrollToEnd();
        }
        
        /// <summary>
        /// Delelte all entries
        /// </summary>
        [Category("NLogViewer")]
        public ICommand ClearCommand
        {
            get => (ICommand) GetValue(ClearCommandProperty);
            set => SetValue(ClearCommandProperty, value);
        }

        /// <summary>
        /// The <see cref="ClearCommand"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty ClearCommandProperty = DependencyProperty.Register("ClearCommand",
            typeof(ICommand), typeof(NLogViewer), new PropertyMetadata(null));
        
        /// <summary>
        /// Stop logging
        /// </summary>
        [Category("NLogViewer")]
        public bool Pause
        {
            get => (bool)GetValue(PauseProperty);
            set => SetValue(PauseProperty, value);
        }

        /// <summary>
        /// The <see cref="Pause"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty PauseProperty = DependencyProperty.Register("Pause", typeof(bool), typeof(NLogViewer), new PropertyMetadata(false));
        
        /// <summary>
        /// The maximum number of entries before automatic cleaning is performed. There is a hysteresis of 100 entries which must be exceeded.
        /// Example: <see cref="MaxCount"/> is '1000'. Then after '1100' entries, everything until '1000' is deleted.
        /// If set to '0' or less, it is deactivated
        /// </summary>
        public int MaxCount
        {
            get => (int)GetValue(MaxCountProperty);
            set => SetValue(MaxCountProperty, value);
        }

        /// <summary>
        /// The <see cref="MaxCount"/> DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty MaxCountProperty = DependencyProperty.Register("MaxCount", typeof(int), typeof(NLogViewer), new PropertyMetadata(5000));
        
        #endregion

        // ##############################################################################################################################
        // Properties
        // ##############################################################################################################################

        #region Properties

        // ##########################################################################################
        // Public Properties
        // ##########################################################################################



        // ##########################################################################################
        // Private Properties
        // ##########################################################################################

        private ObservableCollection<LogEventInfo> _LogEventInfos { get; } = new ObservableCollection<LogEventInfo>();

        #endregion

        // ##############################################################################################################################
        // Constructor
        // ##############################################################################################################################

        #region Constructor

        public NLogViewer()
        {
            InitializeComponent();
            DataContext = this;

            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            LogEvents = CollectionViewSource.GetDefaultView(_LogEventInfos);
            Loaded += _OnLoaded;
            ClearCommand = new ActionCommand(_LogEventInfos.Clear);

            var target = CacheTarget.GetInstance(1000);
            
            target.Cache.SubscribeOn(Scheduler.Default).Buffer(TimeSpan.FromMilliseconds(100)).Where (x => x.Any()).ObserveOnDispatcher(DispatcherPriority.Background).Subscribe(infos =>
            {
                if (Pause) return;
                foreach (LogEventInfo info in infos)
                {
                    _LogEventInfos.Add(info);
                }
                if (AutoScroll)
                {
                    DataGrid?.ScrollToEnd();
                }
            });
        }

        private void _OnLoaded(object sender, RoutedEventArgs e)
        {
            DataGrid.ScrollToEnd();
        }

        #endregion
    }
}
