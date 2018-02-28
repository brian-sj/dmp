using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DMP.Controls
{
    /// <summary>
    /// MMProgressBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MMProgressBar : UserControl
    {
        public static readonly DependencyProperty MMaxProperty =
           DependencyProperty.Register("MMax", typeof(int), typeof(MMProgressBar)
               , new UIPropertyMetadata(2200));

        public static readonly DependencyProperty MMinProperty =
            DependencyProperty.Register("MMin", typeof(int), typeof(MMProgressBar)
                , new UIPropertyMetadata(100));

        public static readonly DependencyProperty ThumbWidthProperty =
   DependencyProperty.Register("ThumbWidth", typeof(int), typeof(MMProgressBar)
       , new UIPropertyMetadata(2));



        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(MMProgressBar)
        , new UIPropertyMetadata(2200));

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(int), typeof(MMProgressBar)
                , new UIPropertyMetadata(100));

        public static readonly DependencyProperty DraggableProperty =
    DependencyProperty.Register("Draggable", typeof(bool), typeof(MMProgressBar)
        , new UIPropertyMetadata(true));

        public int ThumbWidth
        {
            get { return (int)GetValue(ThumbWidthProperty); }
            set { SetValue(ThumbWidthProperty, value); }
        }
        public int MMin
        {
            get { return (int)GetValue(MMinProperty); }
            set { SetValue(MMinProperty, value); }
        }
        public int MMax
        {
            get { return (int)GetValue(MMaxProperty); }
            set { SetValue(MMaxProperty, value); }
        }
        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MMinProperty, value); }
        }
        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MMaxProperty, value); }
        }
        public bool Draggable
        {
            get { return (bool)GetValue(DraggableProperty); }
            set { SetValue(DraggableProperty, value); }
        }
/*
        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider s = (sender as Slider);

            Console.WriteLine(String.Format("S:{0} E:{1} Value:{2}", s.SelectionStart, s.SelectionEnd, e.NewValue));
            if (s.SelectionEnd != s.SelectionStart)
            {
                if (e.NewValue < MMin || e.NewValue > MMax)
                {
                    return;
                }

                if (e.NewValue > s.SelectionEnd)
                {
                    s.SelectionEnd = e.NewValue;
                    //EndValueChanged();
                }
                else if (e.NewValue < s.SelectionStart)
                {
                    s.SelectionStart = e.NewValue;
                    //StartValueChanged();
                }
            }
        }
*/
    }
}
