using SourceWeave.Controls.Utils;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;

namespace SourceWeave.Controls
{
    public partial class SWWindow
    {
        protected virtual Thickness GetDefaultMarginForDpi()
        {
            int currentDPI = SystemHelper.GetCurrentDPI();
            Thickness thickness = new Thickness(8, 8, 8, 8);
            if (currentDPI == 120)
            {
                thickness = new Thickness(7, 7, 4, 5);
            }
            else if (currentDPI == 144)
            {
                thickness = new Thickness(7, 7, 3, 1);
            }
            else if (currentDPI == 168)
            {
                thickness = new Thickness(6, 6, 2, 0);
            }
            else if (currentDPI == 192)
            {
                thickness = new Thickness(6, 6, 0, 0);
            }
            else if (currentDPI == 240)
            {
                thickness = new Thickness(6, 6, 0, 0);
            }
            return thickness;
        }

        protected virtual Thickness GetFromMinimizedMarginForDpi()
        {
            int currentDPI = SystemHelper.GetCurrentDPI();
            Thickness thickness = new Thickness(7, 7, 5, 7);
            if (currentDPI == 120)
            {
                thickness = new Thickness(6, 6, 4, 6);
            }
            else if (currentDPI == 144)
            {
                thickness = new Thickness(7, 7, 4, 4);
            }
            else if (currentDPI == 168)
            {
                thickness = new Thickness(6, 6, 2, 2);
            }
            else if (currentDPI == 192)
            {
                thickness = new Thickness(6, 6, 2, 2);
            }
            else if (currentDPI == 240)
            {
                thickness = new Thickness(6, 6, 0, 0);
            }
            return thickness;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Screen screen = Screen.FromHandle((new WindowInteropHelper(this)).Handle);
            double width = screen.WorkingArea.Width;
            Rectangle workingArea = screen.WorkingArea;
            previousScreenBounds = new System.Windows.Point(width, workingArea.Height);
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            Screen screen = Screen.FromHandle((new WindowInteropHelper(this)).Handle);
            double width = screen.WorkingArea.Width;
            Rectangle workingArea = screen.WorkingArea;
            previousScreenBounds = new System.Windows.Point(width, workingArea.Height);
            RefreshWindowState();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (base.WindowState == WindowState.Normal)
            {
                HeightBeforeMaximize = ActualHeight;
                WidthBeforeMaximize = ActualWidth;
                return;
            }
            if (WindowState == WindowState.Maximized)
            {
                Screen screen = Screen.FromHandle((new WindowInteropHelper(this)).Handle);
                if (previousScreenBounds.X != screen.WorkingArea.Width || previousScreenBounds.Y != screen.WorkingArea.Height)
                {
                    double width = screen.WorkingArea.Width;
                    Rectangle workingArea = screen.WorkingArea;
                    previousScreenBounds = new System.Windows.Point(width, workingArea.Height);
                    RefreshWindowState();
                }
            }
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            Screen screen = Screen.FromHandle((new WindowInteropHelper(this)).Handle);
            Thickness thickness = new Thickness(0);
            if (WindowState != WindowState.Maximized)
            {

                double currentDPIScaleFactor = SystemHelper.GetCurrentDPIScaleFactor();
                Rectangle workingArea = screen.WorkingArea;
                MaxHeight = (workingArea.Height + 16) / currentDPIScaleFactor;
                MaxWidth = double.PositiveInfinity;

                if (WindowState != WindowState.Maximized)
                {
                    SetMaximizeButtonsVisibility(Visibility.Visible, Visibility.Collapsed);
                }
            }
            else
            {

                thickness = GetDefaultMarginForDpi();
                if (PreviousState == WindowState.Minimized || Left == positionBeforeDrag.X && Top == positionBeforeDrag.Y)
                {
                    thickness = GetFromMinimizedMarginForDpi();
                }

                SetMaximizeButtonsVisibility(Visibility.Collapsed, Visibility.Visible);
            }

            LayoutRoot.Margin = thickness;
            PreviousState = WindowState;
        }

        private void OnMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isMouseButtonDown)
            {
                return;
            }

            double currentDPIScaleFactor = SystemHelper.GetCurrentDPIScaleFactor();
            System.Windows.Point position = e.GetPosition(this);
            System.Diagnostics.Debug.WriteLine(position);
            System.Windows.Point screen = PointToScreen(position);
            double x = mouseDownPosition.X - position.X;
            double y = mouseDownPosition.Y - position.Y;
            if (Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) > 1)
            {
                double actualWidth = mouseDownPosition.X;

                if (mouseDownPosition.X <= 0)
                {
                    actualWidth = 0;
                }
                else if (mouseDownPosition.X >= ActualWidth)
                {
                    actualWidth = WidthBeforeMaximize;
                }

                if (WindowState == WindowState.Maximized)
                {
                    ToggleWindowState();
                    Top = (screen.Y - position.Y) / currentDPIScaleFactor;
                    Left = (screen.X - actualWidth) / currentDPIScaleFactor;
                    CaptureMouse();
                }

                isManualDrag = true;

                Top = (screen.Y - mouseDownPosition.Y) / currentDPIScaleFactor;
                Left = (screen.X - actualWidth) / currentDPIScaleFactor;
            }
        }


        private void OnMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseButtonDown = false;
            isManualDrag = false;
            ReleaseMouseCapture();
        }

        private void RefreshWindowState()
        {
            if (WindowState == WindowState.Maximized)
            {
                ToggleWindowState();
                ToggleWindowState();
            }
        }

    }
}
