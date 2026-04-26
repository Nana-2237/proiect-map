using System.Globalization;
using System.Text;
using ShapeStudio.Core.Interfaces;

namespace ShapeStudio.Core.Canvas;

public sealed class SvgCanvas : ICanvas
{
    private readonly List<string> _svgElements = new();
    private string _strokeColor = "black";

    public void SetStrokeColor(string color)
    {
        _strokeColor = string.IsNullOrWhiteSpace(color) ? "black" : color;
    }

    public void DrawLine(double x1, double y1, double x2, double y2)
    {
        _svgElements.Add(
            $"<line x1=\"{Fmt(x1)}\" y1=\"{Fmt(y1)}\" x2=\"{Fmt(x2)}\" y2=\"{Fmt(y2)}\" stroke=\"{_strokeColor}\" fill=\"none\" />");
    }

    public void DrawCircle(double cx, double cy, double r)
    {
        _svgElements.Add(
            $"<circle cx=\"{Fmt(cx)}\" cy=\"{Fmt(cy)}\" r=\"{Fmt(r)}\" stroke=\"{_strokeColor}\" fill=\"none\" />");
    }

    public void DrawRect(double x, double y, double w, double h)
    {
        _svgElements.Add(
            $"<rect x=\"{Fmt(x)}\" y=\"{Fmt(y)}\" width=\"{Fmt(w)}\" height=\"{Fmt(h)}\" stroke=\"{_strokeColor}\" fill=\"none\" />");
    }

    public void DrawEllipse(double cx, double cy, double rx, double ry)
    {
        _svgElements.Add(
            $"<ellipse cx=\"{Fmt(cx)}\" cy=\"{Fmt(cy)}\" rx=\"{Fmt(rx)}\" ry=\"{Fmt(ry)}\" stroke=\"{_strokeColor}\" fill=\"none\" />");
    }

    public string GetSvg()
    {
        var builder = new StringBuilder();
        builder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        builder.AppendLine("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\">");

        foreach (var element in _svgElements)
        {
            builder.Append("  ");
            builder.AppendLine(element);
        }

        builder.AppendLine("</svg>");
        return builder.ToString();
    }

    private static string Fmt(double value) => value.ToString("0.###", CultureInfo.InvariantCulture);
}
