using ShapeStudio.Core.Canvas;
using ShapeStudio.Core.Shapes;

namespace ShapeStudio.Tests;

public class BridgeTests
{
    [Test]
    public void SvgCanvas_WhenRenderingCircle_ContainsCircleElementAndCoordinates()
    {
        var canvas = new SvgCanvas();
        canvas.SetStrokeColor("#334155");
        var circle = new Circle(40, 55, 12);

        circle.Draw(canvas);
        var svg = canvas.GetSvg();

        Assert.Multiple(() =>
        {
            Assert.That(svg, Does.Contain("<circle"));
            Assert.That(svg, Does.Contain("cx=\"40\""));
            Assert.That(svg, Does.Contain("cy=\"55\""));
            Assert.That(svg, Does.Contain("r=\"12\""));
        });
    }
}
