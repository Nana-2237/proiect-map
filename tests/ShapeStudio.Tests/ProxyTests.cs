using ShapeStudio.Core.Interfaces;
using ShapeStudio.Core.Proxy;
using ShapeStudio.Core.Shapes;

namespace ShapeStudio.Tests;

public class ProxyTests
{
    [Test]
    public void Move_OnLockedShapeProxy_ThrowsWithExpectedMessage()
    {
        var proxy = new ReadOnlyShapeProxy(new Circle(10, 10, 5));

        var ex = Assert.Throws<InvalidOperationException>(() => proxy.Move(2, 3));
        Assert.That(ex!.Message, Is.EqualTo("Shape is locked"));
    }

    [Test]
    public void Draw_OnLockedShapeProxy_DelegatesToInnerShape()
    {
        var proxy = new ReadOnlyShapeProxy(new Circle(10, 10, 5));
        var canvas = new RecordingCanvas();

        Assert.DoesNotThrow(() => proxy.Draw(canvas));
        Assert.That(canvas.CirclesDrawn, Is.EqualTo(1));
        Assert.That(canvas.TotalCalls, Is.EqualTo(1));
    }

    private sealed class RecordingCanvas : ICanvas
    {
        public int CirclesDrawn { get; private set; }
        public int TotalCalls { get; private set; }

        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            TotalCalls++;
        }

        public void DrawCircle(double cx, double cy, double r)
        {
            CirclesDrawn++;
            TotalCalls++;
        }

        public void DrawRect(double x, double y, double w, double h)
        {
            TotalCalls++;
        }

        public void DrawEllipse(double cx, double cy, double rx, double ry)
        {
            TotalCalls++;
        }
    }
}
