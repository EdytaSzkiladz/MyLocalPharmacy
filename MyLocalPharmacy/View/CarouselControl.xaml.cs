using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MyLocalPharmacy.Utils;

namespace MyLocalPharmacy.CarouselControl
{
   
    public  partial class CarouselControl
    {
        // image width
        private static double IMG_WIDTH = 200;

        // image height 
        private static double IMG_HEIGHT = 120;

        // spring speed used for animation
        private static double SPRINESS = 0.4;

        // bounce speed used for animation
        private static double DECAY = 0.5;

        // scale between images
        private static double SCALE_DOWN_FACTOR = 0.5;

        // distance between images
        private static double OFFSET_FACTOR = 150;

        // opacity between images
        private static double OPACITY_DOWN_FACTOR = 0.4;

        // max scale value
        private static double MAX_SCALE = 1.75;

        private double _xCenter;
        private double _yCenter;

        // target moving position. this value shows the value of the centered image
        private double _target = 0;

        // current pos
        private double _current = 0;

        // temp used to store last moving
        private double _spring = 0;

        //save added images
        private List<Image> _images = new List<Image>();

        // fps of the on enter frame event
        private static int FPS = 22;

        // timer used to animate images
        private DispatcherTimer _timer = new DispatcherTimer();

        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        private int _imageNumber = 1;

        public CarouselControl()
        {
            InitializeComponent();
            // Find the center position
            _xCenter = carouselArea.Width / 2;
            _yCenter = carouselArea.Height / 2;
        }

        // dependency property
        public static DependencyProperty ListImagesCarouselProperty =
       DependencyProperty.Register("ListImagesCarousel", typeof(List<BitmapImage>), typeof(CarouselControl), new PropertyMetadata(null));

        public List<BitmapImage> ListImagesCarousel
        {
            get
            {
                return (List<BitmapImage>)GetValue(ListImagesCarouselProperty);
            }
            set
            {
                SetValue(ListImagesCarouselProperty, value);

                // set visibility to Visible -> this shows up all controls on UI
                gridMainCarousel.Visibility = System.Windows.Visibility.Visible;

                AddImagesToCarousel();

                // set position to center of images
                SetIndex(0);
                Start();
                StartAnimation();
            }
        }

        /// <summary>
        /// Start Dispatcher
        /// </summary>
        public void Start()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / FPS);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        public void StartAnimation()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = new TimeSpan(0,0,5);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();
        }

        void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            MoveIndex(1);
            _imageNumber++;

            if (_imageNumber == _images.Count+1)
            {
                SetIndex(0);
                _imageNumber = 1;
            }
           
        }

        // Add images to Canvas
        private void AddImagesToCarousel()
        {
            if (ListImagesCarousel != null && ListImagesCarousel.Count > 0)
            {
                for (int i = 0; i < ListImagesCarousel.Count; i++)
                {
                    // get the image url
                    BitmapImage btm = ListImagesCarousel[i];
                    Image image = new Image();
                    image.Source = btm;

                    // TAG is used as an identifier for images
                    image.Tag = i.ToString();
                    image.Width = IMG_WIDTH;
                    image.Height = IMG_HEIGHT;
                  //  image.Stretch = Stretch.UniformToFill;
                    // event Tapped to find selected image
                    image.Tap += image_Tap;

                    // add and set image position
                    LayoutRoot.Children.Add(image);

                    SetPosImage(image, i);
                    _images.Add(image);
                }
            }
            else
            {
                Image image = new Image();
                int i = 0;
                image.Source = new BitmapImage(new Uri("/Assets/Images/icon_splash.png", UriKind.Relative));
                // TAG is used as an identifier for images
                image.Tag = i.ToString();
                image.Width = IMG_WIDTH;
                image.Height = IMG_HEIGHT;
                //  image.Stretch = Stretch.UniformToFill;
                // event Tapped to find selected image
                image.Tap += image_Tap;

                // add and set image position
                LayoutRoot.Children.Add(image);

                SetPosImage(image, i);
                _images.Add(image);
            }
        }

        void image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image image = (Image)sender;
            if(image.Tag != null)
            {
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(PageURL.navigateToWebView + "?imageTag="+image.Tag, UriKind.Relative));
            }
        }

        /// <summary>
        /// Navigate Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateHandler(object sender, NavigationEventArgs e)
        {
            WebBrowser wb = sender as WebBrowser;
            if (wb != null)
            {
               // progress.Visibility = Visibility.Collapsed;
                wb.Visibility = Visibility.Visible;
            }
        }

        private void SetIndex(int value)
        {
            _target = value;
            _target = Math.Max(0, _target);
            _target = Math.Min(_images.Count - 1, _target);
        }

        private void MoveIndex(int value)
        {
            _target += value;
            _target = Math.Max(0, _target);
            _target = Math.Min(_images.Count - 1, _target);
        }

        private void SetPosImage(Image image, int index)
        {
            double diffFactor = index - _current;

            // adapt scale and position for each image based on position and scale factors
            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = MAX_SCALE - Math.Abs(diffFactor) * SCALE_DOWN_FACTOR;
            scaleTransform.ScaleY = MAX_SCALE - Math.Abs(diffFactor) * SCALE_DOWN_FACTOR;
            image.RenderTransform = scaleTransform;

            // position image
            double left = _xCenter - (IMG_WIDTH * scaleTransform.ScaleX) / 2 + diffFactor * OFFSET_FACTOR;
            double top = _yCenter - (IMG_HEIGHT * scaleTransform.ScaleY) / 2;
            image.Opacity = 1 - Math.Abs(diffFactor) * OPACITY_DOWN_FACTOR;

            image.SetValue(Canvas.LeftProperty, left);
            image.SetValue(Canvas.TopProperty, top);

            image.SetValue(Canvas.ZIndexProperty, (int)Math.Abs(scaleTransform.ScaleX * 100));
        }

        void _timer_Tick(object sender, object e)
        {
            for (int i = 0; i < _images.Count; i++)
            {
                Image image = _images[i];
                SetPosImage(image, i);
            }


            // added animation effect
            if (_target == _images.Count)
                _target = 0;
            _spring = (_target - _current) * SPRINESS + _spring * DECAY;
            _current += _spring;
        }

        private void button_right_Click(object sender, RoutedEventArgs e)
        {
            MoveIndex(1);
        }

        private void button_left_Click(object sender, RoutedEventArgs e)
        {
            MoveIndex(-1);
        }
    }
}
