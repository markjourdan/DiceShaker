using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Coding4Fun.DiceShaker.Application.Controls
{
	public partial class Dice
	{
		private const int Time = 300;
	    private const int DefaultSides = 6;
        private static readonly Random Random = new Random();
		private Storyboard _animate = new Storyboard();
		private Storyboard _animateHighlight = new Storyboard();
		private bool _isAccent;
		private int _side;

		public Dice()
		{
            InitializeComponent();
            Sides = DefaultSides;
            Roll();

            DieBorder.BorderBrush = Resources["PhoneAccentBrush"] as Brush;

            dot00.Fill =
                dot01.Fill =
                dot02.Fill =
                dot10.Fill =
                dot11.Fill = dot12.Fill = dot20.Fill = dot21.Fill = dot22.Fill = Resources["PhoneAccentBrush"] as Brush;
		}
        
        /// <summary>
        /// The number of sides the dice has.
        /// </summary>
        public int Sides { get; set; }
        
        /// <summary>
        /// Rolls the dice.
        /// </summary>
        public void Roll()
        {
            const int minimum = 1;
            Side = Random.Next(minimum, Sides + 1);
        }

		/// <summary>
		/// The dice number.
		/// </summary>
		public int Side
		{
			get { return _side; }
			set
			{
				_side = value;
				AnimateChange();
			}
		}

		public bool IsAccent
		{
			get { return _isAccent; }
			set
			{
				_isAccent = value;
				switch (_isAccent)
				{
					case false:
						DieBorder.BorderBrush = Resources["PhoneAccentBrush"] as Brush;
						DieBorder.Background = Resources["PhoneForegroundBrush"] as Brush;
						dot00.Fill =
							dot01.Fill =
							dot02.Fill =
							dot10.Fill =
							dot11.Fill =
							dot12.Fill = dot20.Fill = dot21.Fill = dot22.Fill = Resources["PhoneAccentBrush"] as Brush;
						break;
					case true:
						DieBorder.BorderBrush = Resources["PhoneForegroundBrush"] as Brush;
						DieBorder.Background = Resources["PhoneAccentBrush"] as Brush;
						dot00.Fill =
							dot01.Fill =
							dot02.Fill =
							dot10.Fill =
							dot11.Fill =
							dot12.Fill =
							dot20.Fill = dot21.Fill = dot22.Fill = Resources["PhoneForegroundBrush"] as Brush;
						break;
				}
			}
		}

	    public bool IsHighlighted
	    {
	        set
	        {
				if (value)
				{
					Highlight.Opacity = 0;
					Highlight.Padding = new Thickness(8);
					Highlight.BorderThickness = new Thickness(5);
					Highlight.BorderBrush = new SolidColorBrush(new Color {A = 255, B = 0, G = 255, R = 127});
						//Chartreuse Color [http://en.wikipedia.org/wiki/Web_colors]

					if (_animateHighlight == null)
					{
						_animateHighlight = new Storyboard();
					}

					_animateHighlight.Stop();

					_animateHighlight.Children.Clear();
					CreateStoryboardPart(_animateHighlight, Highlight, true);
					
					_animateHighlight.Begin();
				}
	        }
	    }

		private void AnimateChange()
		{
			if (_animate == null)
			{
				_animate = new Storyboard();
			}

			_animate.Stop();

			_animate.Children.Clear();
			
			// column.row
			bool[] isVisible =
				{
					Side == 4 || Side == 5 || Side == 6,
					Side == 6,
					Side == 2 || Side == 3 || Side == 4 ||
					Side == 5 || Side == 6,
					false,
					Side == 1 || Side == 3 || Side == 5,
					false,
					Side == 2 || Side == 3 || Side == 4 ||
					Side == 5 || Side == 6,
					Side == 6,
					Side == 4 || Side == 5 || Side == 6,
				};

			CreateStoryboardPart(_animate, dot00, isVisible[0]);
			CreateStoryboardPart(_animate, dot01, isVisible[1]);
			CreateStoryboardPart(_animate, dot02, isVisible[2]);

			CreateStoryboardPart(_animate, dot10, isVisible[0 + 3]);
			CreateStoryboardPart(_animate, dot11, isVisible[1 + 3]);
			CreateStoryboardPart(_animate, dot12, isVisible[2 + 3]);

			CreateStoryboardPart(_animate, dot20, isVisible[0 + 6]);
			CreateStoryboardPart(_animate, dot21, isVisible[1 + 6]);
			CreateStoryboardPart(_animate, dot22, isVisible[2 + 6]);
			
			_animate.Begin();
		}

		private static DoubleAnimation CreateOpacityAnimation(DependencyObject obj, double from, double value,
															  double milliseconds, double beginTime,
															  EasingMode easing = EasingMode.EaseIn)
		{
			var ease = new CubicEase {EasingMode = easing};
			var animation = new DoubleAnimation();

			var propPath = new PropertyPath("(UIElement.Opacity)");

			animation.BeginTime = TimeSpan.FromMilliseconds(beginTime);
			animation.Duration = new Duration(TimeSpan.FromMilliseconds(milliseconds));
			animation.From = from;
			animation.To = Convert.ToDouble(value);
			animation.FillBehavior = FillBehavior.HoldEnd;
			animation.EasingFunction = ease;
			
			Storyboard.SetTarget(animation, obj);
			Storyboard.SetTargetProperty(animation, propPath);
			
			return animation;
		}
		
		private static void CreateStoryboardPart(Storyboard story, UIElement dot, bool isVisible)
		{
			if (dot.Opacity != isVisible.BoolToInt())
				story.Children.Add(CreateOpacityAnimation(dot, dot.Opacity, isVisible.BoolToInt(), Time, 100));
		}
	}

	internal static class BoolHelper
	{
		public static int BoolToInt(this bool value)
		{
			return value ? 1 : 0;
		}
	}
}

