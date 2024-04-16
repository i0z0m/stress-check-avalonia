using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StressCheckAvalonia.Views
{
    public class RadarChartData
    {
        public string Label { get; set; }
        public double Value { get; set; }
        public Color Color { get; set; }
    }

    public partial class RadarChart : UserControl
    {
        public static readonly StyledProperty<IEnumerable<RadarChartData>> ItemsProperty =
            AvaloniaProperty.Register<RadarChart, IEnumerable<RadarChartData>>(nameof(Items));

        public IEnumerable<RadarChartData> Items
        {
            get => GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public RadarChart()
        {
            this.GetObservable(BoundsProperty).Subscribe(_ => InvalidateVisual());
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            if (Items == null || !Items.Any()) return;

            var center = new Point(Bounds.Width / 2, Bounds.Height / 2);
            var radius = Math.Min(Bounds.Width, Bounds.Height) / 2 * 0.8; // Use 80% of the smallest dimension

            DrawRadarChart(context, center, radius);
        }

        private void DrawRadarChart(DrawingContext context, Point center, double radius)
        {
            var angleIncrement = 360.0 / Items.Count();
            var points = new List<Point>();

            // Draw background polygons and lines
            for (int i = 5; i >= 1; i--)
            {
                var polygonRadius = radius * (i / 5.0);
                var polygonPoints = new List<Point>();

                for (int j = 0; j < Items.Count(); j++)
                {
                    var angleDegree = j * angleIncrement - 90.0;
                    var angleRadian = Math.PI * angleDegree / 180.0;
                    var point = new Point(center.X + Math.Cos(angleRadian) * polygonRadius, center.Y + Math.Sin(angleRadian) * polygonRadius);
                    polygonPoints.Add(point);

                    // Draw lines from center to the points of the largest polygon
                    if (i == 5)
                    {
                        context.DrawLine(new Pen(Brushes.LightGray, 1), center, point);
                    }
                }

                var polygonGeometry = new PolylineGeometry(polygonPoints, true);
                context.DrawGeometry(null, new Pen(Brushes.LightGray, 1), polygonGeometry);

                var typeface = new Typeface("Arial", FontStyle.Normal, FontWeight.Normal);
                var formattedText = new FormattedText(
                    i.ToString(),
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    typeface,
                    12,
                    Brushes.Gray);

                var textWidth = formattedText.Width;
                var textHeight = formattedText.Height;
                context.DrawText(formattedText, new Point(center.X - textWidth / 2, center.Y - polygonRadius - textHeight));
            }

            foreach (var item in Items)
            {
                var angleDegree = points.Count * angleIncrement - 90.0; // Start from the top (-90 degrees)
                var angleRadian = Math.PI * angleDegree / 180.0;
                var point = new Point(center.X + Math.Cos(angleRadian) * radius * (item.Value / 5), center.Y + Math.Sin(angleRadian) * radius * (item.Value / 5));
                points.Add(point);
            }

            var geometry = new StreamGeometry();
            using (var ctx = geometry.Open())
            {
                ctx.BeginFigure(points.First(), isFilled: true);
                foreach (var point in points.Skip(1))
                {
                    ctx.LineTo(point);
                }
                ctx.EndFigure(isClosed: true);
            }

            context.DrawGeometry(new SolidColorBrush(Items.First().Color), new Pen(new SolidColorBrush(Items.First().Color), 1.5), geometry);

            foreach (var point in points)
            {
                context.DrawEllipse(new SolidColorBrush(Items.First().Color), null, point, 4, 4);
            }

            for (int i = 0; i < Items.Count(); i++)
            {
                var item = Items.ElementAt(i);
                var angleDegree = i * angleIncrement - 90.0;
                var angleRadian = Math.PI * angleDegree / 180.0;
                var labelPosition = new Point(center.X + Math.Cos(angleRadian) * (radius + 20), center.Y + Math.Sin(angleRadian) * (radius + 20));

                var typeface = new Typeface("Arial", FontStyle.Normal, FontWeight.Normal);
                var formattedText = new FormattedText(
                    item.Label,
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    typeface,
                    12,
                    Brushes.Black);

                var textWidth = formattedText.Width;
                var textHeight = formattedText.Height;

                // Adjust the label position to keep it within the canvas bounds
                var adjustedLabelPosition = new Point(
                    Math.Max(textWidth / 2, Math.Min(labelPosition.X, Bounds.Width - textWidth / 2)),
                    Math.Max(textHeight / 2, Math.Min(labelPosition.Y, Bounds.Height - textHeight / 2)));

                var textPosition = new Point(adjustedLabelPosition.X - textWidth / 2, adjustedLabelPosition.Y - textHeight / 2);
                context.DrawText(formattedText, textPosition);
            }
        }
    }
}