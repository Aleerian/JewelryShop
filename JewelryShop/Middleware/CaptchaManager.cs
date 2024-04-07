using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Label = System.Windows.Controls.Label;

namespace JewelryShop.Middleware
{
    public class CaptchaManager
    {
        private const string SYMBOLS = "QWERTYUIOPLKJHGFDSAZXCVBNMqwertyuioplkjhgfdsazxcvbnm1234567890";
        private readonly Random random;
        public CaptchaManager() 
        {
            random = new Random();
        }

        public string GenerateCaptcha(Canvas canvas)
        {
            canvas.Children.Clear();
            var captchaCode = GetCaptchaCode();
            for(int i = 0; i < captchaCode.Length; ++i)
            {
                var label = CreateLabel(captchaCode[i], canvas);
                canvas.Children.Add(label);

                Canvas.SetLeft(label, 120 + (i * 40));
                Canvas.SetTop(label, random.Next(0, 10));
            }
            GenerateNoice(canvas);
            return captchaCode;
        }

        private string GetCaptchaCode()
        {
            var captchaCode = "";

            for (int i = 0; i < 4; i++) 
            {
                captchaCode += SYMBOLS[random.Next(SYMBOLS.Length)];
            }

            return captchaCode;
        }

        private Label CreateLabel(char ch, Canvas canvas) 
        {
            Label label = new Label();
            label.FontSize = random.Next(20, 30);
            label.Width = 50;
            label.Height = 50;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalContentAlignment = VerticalAlignment.Center;
            label.RenderTransformOrigin = new Point(0.5, 0.5);
            label.RenderTransform = new RotateTransform(random.Next(-20, 20));
            label.Content = ch.ToString();
            label.FontFamily = new FontFamily("ManaSpase");
            label.FontWeight = FontWeights.Bold;
            label.Foreground = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)));

            return label;
        }

        private void GenerateNoice(Canvas canvas)
        {
            int ellipseCount = random.Next(50, 150);
            for (int i = 1; i < ellipseCount; i++)
            {
                double x = random.NextDouble() * 400;
                double y = random.NextDouble() * 50;

                int radius = random.Next(2, 6);
                Ellipse ellipse = new Ellipse
                {
                    Width = radius,
                    Height = radius,
                    Fill = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256))),
                    Stroke = Brushes.Transparent,
                };
                canvas.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, x);
                Canvas.SetTop(ellipse, y);
            }

            int lineCount = random.Next(1, 4);
            for (int i = 0; i < lineCount; i++) 
            {
                Line line = new Line
                {
                    X1 = random.Next(80, 150),
                    Y1 = random.Next(10, 70),
                    X2 = random.Next(240, 280),
                    Y2 = random.Next(10, 70),
                    Stroke = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256))),
                    StrokeThickness = 1,
                };

                canvas.Children.Add(line);
            }
        } 
    }
}
