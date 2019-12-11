using Microsoft.Win32;
using SourceWeave.Controls.Utils;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;

namespace SourceWeave.Controls
{
    public partial class SWWindow : Window
    {
        private HwndSource _hwndSource;

        private bool isMouseButtonDown;
        private bool isManualDrag;
        private System.Windows.Point mouseDownPosition;
        private System.Windows.Point positionBeforeDrag;
        private System.Windows.Point previousScreenBounds;

        public Grid WindowRoot { get; private set; }
        public Grid LayoutRoot { get; private set; }
        public System.Windows.Controls.Button MinimizeButton { get; private set; }
        public System.Windows.Controls.Button MaximizeButton { get; private set; }
        public System.Windows.Controls.Button RestoreButton { get; private set; }
        public System.Windows.Controls.Button CloseButton { get; private set; }
        public Grid HeaderBar { get; private set; }
        public double HeightBeforeMaximize { get; private set; }
        public double WidthBeforeMaximize { get; private set; }
        public WindowState PreviousState { get; private set; }

        static SWWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SWWindow),
                new FrameworkPropertyMetadata(typeof(SWWindow)));
        }

        public SWWindow()
        {
            double currentDPIScaleFactor = SystemHelper.GetCurrentDPIScaleFactor();
            Screen screen = Screen.FromHandle((new WindowInteropHelper(this)).Handle);
            SizeChanged += new SizeChangedEventHandler(OnSizeChanged);
            StateChanged += new EventHandler(OnStateChanged);
            Loaded += new RoutedEventHandler(OnLoaded);
            Rectangle workingArea = screen.WorkingArea;
            MaxHeight = (workingArea.Height + 16) / currentDPIScaleFactor;
            SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
            AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(OnMouseButtonUp), true);
            AddHandler(MouseMoveEvent, new System.Windows.Input.MouseEventHandler(OnMouseMove));
        }

        public T GetRequiredTemplateChild<T>(string childName) where T : DependencyObject
        {
            return (T)GetTemplateChild(childName);
        }

        public override void OnApplyTemplate()
        {
            WindowRoot = GetRequiredTemplateChild<Grid>("WindowRoot");
            LayoutRoot = GetRequiredTemplateChild<Grid>("LayoutRoot");
            MinimizeButton = GetRequiredTemplateChild<System.Windows.Controls.Button>("MinimizeButton");
            MaximizeButton = GetRequiredTemplateChild<System.Windows.Controls.Button>("MaximizeButton");
            RestoreButton = GetRequiredTemplateChild<System.Windows.Controls.Button>("RestoreButton");
            CloseButton = GetRequiredTemplateChild<System.Windows.Controls.Button>("CloseButton");
            HeaderBar = GetRequiredTemplateChild<Grid>("PART_HeaderBar");

            if (LayoutRoot != null && WindowState == WindowState.Maximized)
            {
                LayoutRoot.Margin = GetDefaultMarginForDpi();
            }

            if (CloseButton != null)
            {
                CloseButton.Click += CloseButton_Click;
            }

            if (MinimizeButton != null)
            {
                MinimizeButton.Click += MinimizeButton_Click;
            }

            if (RestoreButton != null)
            {
                RestoreButton.Click += RestoreButton_Click;
            }

            if (MaximizeButton != null)
            {
                MaximizeButton.Click += MaximizeButton_Click;
            }

            if (HeaderBar != null)
            {
                HeaderBar.AddHandler(Grid.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnHeaderBarMouseLeftButtonDown));
            }

            base.OnApplyTemplate();
        }

        protected override void OnInitialized(EventArgs e)
        {
            SourceInitialized += OnSourceInitialized;
            base.OnInitialized(e);
        }

        protected virtual void OnHeaderBarMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isManualDrag)
            {
                return;
            }

            System.Windows.Point position = e.GetPosition(this);
            int headerBarHeight = 36;
            int leftmostClickableOffset = 50;

            if (position.X - LayoutRoot.Margin.Left <= leftmostClickableOffset && position.Y <= headerBarHeight)
            {
                if (e.ClickCount != 2)
                {
                    OpenSystemContextMenu(e);
                }
                else
                {
                    Close();
                }
                e.Handled = true;
                return;
            }

            if (e.ClickCount == 2 && ResizeMode == ResizeMode.CanResize)
            {
                ToggleWindowState();
                return;
            }

            if (WindowState == WindowState.Maximized)
            {
                isMouseButtonDown = true;
                mouseDownPosition = position;
            }
            else
            {
                positionBeforeDrag = new System.Windows.Point(Left, Top);
                DragMove();
            }
        }

        protected void ToggleWindowState()
        {
            if (WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleWindowState();
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleWindowState();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            _hwndSource = (HwndSource)PresentationSource.FromVisual(this);
        }

        private void SetMaximizeButtonsVisibility(Visibility maximizeButtonVisibility, Visibility reverseMaximizeButtonVisiility)
        {
            if (MaximizeButton != null)
            {
                MaximizeButton.Visibility = maximizeButtonVisibility;
            }
            if (RestoreButton != null)
            {
                RestoreButton.Visibility = reverseMaximizeButtonVisiility;
            }
        }

        private void OpenSystemContextMenu(MouseButtonEventArgs e)
        {
            System.Windows.Point position = e.GetPosition(this);
            System.Windows.Point screen = PointToScreen(position);
            int num = 36;
            if (position.Y < num)
            {
                IntPtr handle = (new WindowInteropHelper(this)).Handle;
                IntPtr systemMenu = NativeUtils.GetSystemMenu(handle, false);
                if (WindowState != WindowState.Maximized)
                {
                    NativeUtils.EnableMenuItem(systemMenu, 61488, 0);
                }
                else
                {
                    NativeUtils.EnableMenuItem(systemMenu, 61488, 1);
                }
                int num1 = NativeUtils.TrackPopupMenuEx(systemMenu, NativeUtils.TPM_LEFTALIGN | NativeUtils.TPM_RETURNCMD, Convert.ToInt32(screen.X + 2), Convert.ToInt32(screen.Y + 2), handle, IntPtr.Zero);
                if (num1 == 0)
                {
                    return;
                }

                NativeUtils.PostMessage(handle, 274, new IntPtr(num1), IntPtr.Zero);
            }
        }
    }
}
