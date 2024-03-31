using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace stress_check_avalonia
{
    public partial class RadarChart : UserControl
    {
        public static readonly StyledProperty<IEnumerable<RadarChartData>> ItemsProperty =
            AvaloniaProperty.Register<RadarChart, IEnumerable<RadarChartData>>(nameof(Items));

        public IEnumerable<RadarChartData> Items
        {
            get { return GetValue(ItemsProperty); }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        public RadarChart()
        {
            InitializeComponent();
            this.GetObservable(BoundsProperty).Subscribe(_ => this.InvalidateVisual());
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            if (Items == null || !Items.Any())
                return;

            if (Bounds.Width < 20 || Bounds.Height < 20)
            {
                return;
            }

            var center = new Point(Bounds.Width / 2, Bounds.Height / 2);
            var radius = Math.Min(Bounds.Width, Bounds.Height) / 2 - 10;
            if (radius <= 0) return;

            var angleIncrement = 2 * Math.PI / Items.Count();

            DrawAxes(context, center, radius, angleIncrement);
            DrawPoints(context, center, radius, angleIncrement);
        }

        private void DrawAxes(DrawingContext context, Point center, double radius, double angleIncrement)
        {
            for (int i = 0; i < Items.Count(); i++)
            {
                var angle = i * angleIncrement;
                var lineEnd = new Point(
                    center.X + radius * Math.Sin(angle),
                    center.Y - radius * Math.Cos(angle));

                context.DrawLine(new Pen(Brushes.Black), center, lineEnd);
            }
        }

        private void DrawPoints(DrawingContext context, Point center, double radius, double angleIncrement)
        {
            var points = Items.Select((item, index) =>
            {
                var angle = index * angleIncrement;
                var normalizedValue = item.Value / 5.0; // Valueを0から1の範囲に正規化
                var distance = normalizedValue * radius;

                Console.WriteLine($"Index: {item.Index}, Value: {item.Value}");

                return new Point(
                    center.X + distance * Math.Sin(angle),
                    center.Y - distance * Math.Cos(angle));
            }).ToList();

            var pointBrush = new SolidColorBrush(Colors.Red);
            var pointSize = new Size(4, 4);

            foreach (var point in points)
            {
                context.DrawEllipse(pointBrush, null, point, pointSize.Width / 2, pointSize.Height / 2);
            }
        }
    }

    public class RadarChartData
    {
        public int Index { get; set; }
        public double Value { get; set; }
    }
}