using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using LotteryBook.Model;

namespace LotteryBook.Program.Views.Draw
{
    public partial class LotteryTicketUC : UserControl
    {
        public static readonly DependencyProperty AnimationScaleProperty =
            DependencyProperty.Register("AnimationScale", typeof(double), typeof(LotteryTicketUC), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty LetterProperty =
            DependencyProperty.Register("Letter", typeof(char), typeof(LotteryTicketUC), new PropertyMetadata(default(char), LetterChanged));

        public static readonly DependencyProperty ColorBrushProperty =
            DependencyProperty.Register("ColorBrush", typeof(Brush), typeof(LotteryTicketUC), new PropertyMetadata(default(Brush), ColorChanged));

        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register("Number", typeof(int), typeof(LotteryTicketUC), new PropertyMetadata(default(int), NumberChanged));

        private DoubleAnimation m_FadeInAnimation;
        private DoubleAnimation m_FadeOutAnimation;
        private ScaleTransform m_ScaleTransform;
        private DoubleAnimation m_ScaleOutAnimation;
        private FadeInPhase m_FadeInPhase;

        public event EventHandler DrawFinished;

        public LotteryTicketUC()
        {
            InitializeComponent();

            LotteryManager = LotteryManager.GetInstance();

            if (LotteryManager.LastTicketDrawn == null)
            {
                ticketViewbox.Opacity = 0.0;
            }

            m_ScaleTransform = new ScaleTransform();
            ticketViewbox.RenderTransform = m_ScaleTransform;
            ticketViewbox.RenderTransformOrigin = new Point(0.5, 0.5);

            m_ScaleOutAnimation = new DoubleAnimation(0.0, 1.0, new Duration(new TimeSpan(0, 0, 0, 3)));

            m_FadeInAnimation = new DoubleAnimation(0.0, 1.0, new Duration(new TimeSpan(0, 0, 0, 1, 200)));
            m_FadeInAnimation.Completed += FadeInAnimationOnCompleted;

            m_FadeOutAnimation = new DoubleAnimation(1.0, 0.0, new Duration(new TimeSpan(0, 0, 0, 1, 500)));
            m_FadeOutAnimation.Completed += FadeOutAnimationOnCompleted;
        }

        public int Number
        {
            get
            {
                return (int)GetValue(NumberProperty);
            }

            set
            {
                SetValue(NumberProperty, value);
            }
        }

        public Brush ColorBrush
        {
            get
            {
                return (Brush)GetValue(ColorBrushProperty);
            }

            set
            {
                SetValue(ColorBrushProperty, value);
            }
        }

        public char Letter
        {
            get
            {
                return (char)GetValue(LetterProperty);
            }

            set
            {
                SetValue(LetterProperty, value);
            }
        }

        public LotteryManager LotteryManager { get; private set; }

        public double AnimationScale
        {
            get
            {
                return (double)GetValue(AnimationScaleProperty);
            }

            set
            {
                SetValue(AnimationScaleProperty, value);
            }
        }

        public void DrawTicket()
        {
            if (LotteryManager.LastTicketDrawn != null)
            {
                DrawTicketPhase1();
            }
            else
            {
                DrawTicketPhase2();
            }
        }

        public virtual void OnDrawFinished()
        {
            EventHandler handler = DrawFinished;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private static void NumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LotteryTicketUC).numberTextBlock.Text = e.NewValue.ToString();
        }

        private static void ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LotteryTicketUC).Background = e.NewValue as Brush;
        }

        private static void LetterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LotteryTicketUC).letterTextBlock.Text = e.NewValue.ToString();
        }

        private void FadeOutAnimationOnCompleted(object sender, EventArgs eventArgs)
        {
            DrawTicketPhase2();
        }

        private void DrawTicketPhase1()
        {
            if (LotteryManager.Settings.AnimateDrawing)
            {
                ticketViewbox.BeginAnimation(OpacityProperty, m_FadeOutAnimation);                
            }
            else
            {
                DrawTicketPhase2();                
            }
        }

        private void DrawTicketPhase2()
        {
            ResetAnimation();
            LotteryManager.DrawLotteryTicket();
            
            if (LotteryManager.Settings.AnimateDrawing)
            {
                FadeInTicket();
            }
            else
            {
                ShowTicket();
            }
        }

        private void ResetAnimation()
        {
            ticketViewbox.BeginAnimation(OpacityProperty, null);
            letterTextBlock.BeginAnimation(OpacityProperty, null);
            numberTextBlock.BeginAnimation(OpacityProperty, null);

            ticketViewbox.Opacity = 0.0;
            letterTextBlock.Opacity = 0.0;
            numberTextBlock.Opacity = 0.0;
        }

        private void FadeInTicket()
        {
            m_FadeInPhase = FadeInPhase.Ticket;
            FadeIn();
        }

        private void ShowTicket()
        {
            ticketViewbox.Opacity = 1.0;
            letterTextBlock.Opacity = 1.0;
            numberTextBlock.Opacity = 1.0;
            OnDrawFinished();
        }

        private void FadeIn()
        {
            switch (m_FadeInPhase)
            {
                case FadeInPhase.Ticket:
                    var beginTime = new TimeSpan(0, 0, 0, 0);
                    m_FadeInAnimation.BeginTime = beginTime;
                    m_ScaleOutAnimation.BeginTime = beginTime;
                    ticketViewbox.BeginAnimation(OpacityProperty, m_FadeInAnimation);
                    m_ScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, m_ScaleOutAnimation);
                    m_ScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, m_ScaleOutAnimation);
                    break;

                case FadeInPhase.Letter:
                    m_FadeInAnimation.BeginTime = new TimeSpan(0, 0, 0, 1, 200);
                    letterTextBlock.BeginAnimation(OpacityProperty, m_FadeInAnimation);
                    break;

                case FadeInPhase.Number:
                    m_FadeInAnimation.BeginTime = new TimeSpan(0, 0, 0, 1, 200);
                    numberTextBlock.BeginAnimation(OpacityProperty, m_FadeInAnimation);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FadeInAnimationOnCompleted(object sender, EventArgs eventArgs)
        {
            if (m_FadeInPhase == FadeInPhase.Ticket)
            {
                m_FadeInPhase = FadeInPhase.Letter;
                FadeIn();
            }
            else if (m_FadeInPhase == FadeInPhase.Letter)
            {
                m_FadeInPhase = FadeInPhase.Number;
                FadeIn();
            }
            else if (m_FadeInPhase == FadeInPhase.Number)
            {
                OnDrawFinished();
            }
        }
    }
}