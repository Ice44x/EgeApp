using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EgeApp
{
    /// <summary>
    /// View model for the main window
    /// </summary>
    class MainWindowViewModel : BaseViewModel
    {
        #region Private properties

        /// <summary>
        /// window that controled by this vm
        /// </summary>
        private Window mWindow;

        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;

        /// <summary>
        /// min height of the windoww
        /// </summary>
        private int mMinHeight = 500;

        /// <summary>
        /// min width of the window
        /// </summary>
        private int mMinWidth = 900;

        /// <summary>
        /// max height of the window
        /// </summary>
        private int mMaxHeight = 600;

        /// <summary>
        /// max width of the window
        /// </summary>
        private int mMaxWidth = 1080;

        /// <summary>
        /// the radius of the edges of the window
        /// </summary>
        private int mWindowRadius  = 5;

        /// <summary>
        /// the margin around the window to allow a drop shadow
        /// </summary>
        private int mOuterMarginSize  = 3;

        #endregion

        #region Public properties

        public int MinHeight { get { return mMinHeight; } }
        public int MaxHeight { get { return mMaxHeight; } }
        public int MaxWidth { get { return mMaxWidth; } }
        public int MinWidth { get { return mMinWidth; } }

        public bool Borderless { get { return mWindow.WindowState == WindowState.Maximized || mDockPosition != WindowDockPosition.Undocked; } }

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder { get { return Borderless ? 0 : 6; } }

        public Thickness ResizeBorderThickness { get { return new Thickness(OuterMarginSize + ResizeBorder); } }

        /// <summary>
        /// The height of the title/ caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 25;

        /// <summary>
        /// The height of the title/ caption of the window
        /// </summary>
        public GridLength TitleHeightGridLenght { get { return new GridLength(TitleHeight + OuterMarginSize); } }

        /// <summary>
        /// The margin around the window to allow a drop shadow
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }
        /// <summary>
        /// The margin around the window to allow a drop shadow
        /// </summary>
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        /// <summary>
        /// the radius of the edges of the window
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        /// <summary>
        /// the radius of the edges of the window
        /// </summary>
        public CornerRadius CornerRadius { get { return new CornerRadius(WindowRadius); } }

        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Login;

        #endregion

        #region Commands

        /// <summary>
        /// Command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// Command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// Command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// Command to show the system menu of the window
        /// </summary>
        public ICommand SystemCommand { get; set; }

        #endregion

        #region Constructor

        public MainWindowViewModel(Window window)
        {
            mWindow = window;

            mWindow.StateChanged += (sender, e) =>
            {
                mWindowStateChanged();
            };

            CloseCommand = new RelayCommand(() => mWindow.Close());
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            SystemCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, mWindow.PointToScreen(Mouse.GetPosition(mWindow))));

            var resizer = new WindowResizer(mWindow);

            resizer.WindowDockChanged += (dock) =>
            {
                mDockPosition = dock;
                mWindowStateChanged();
            };

        }

        #endregion

        #region Private helpers

        private void mWindowStateChanged()
        {
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(CornerRadius));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
        }

        #endregion
    }
}
