using ShapeStudio.Core.Composite;
using ShapeStudio.Core.Shapes;

namespace ShapeStudio.Tests;

public class CompositeTests
{
    [Test]
    public void Scale_WhenAppliedOnNestedComposite_UpdatesEveryPrimitive()
    {
        var line = new Line(2, 3, 8, 9);
        var rectangle = new Rectangle(4, 5, 10, 6);
        var circle = new Circle(7, 8, 4);
        var root = new Picture();
        var nested = new Picture();
        nested.Add(rectangle);
        nested.Add(circle);
        root.Add(line);
        root.Add(nested);

        root.Scale(2.0);

        Assert.Multiple(() =>
        {
            Assert.That(line.X1, Is.EqualTo(4));
            Assert.That(line.Y1, Is.EqualTo(6));
            Assert.That(line.X2, Is.EqualTo(16));
            Assert.That(line.Y2, Is.EqualTo(18));
            Assert.That(rectangle.X, Is.EqualTo(8));
            Assert.That(rectangle.Y, Is.EqualTo(10));
            Assert.That(rectangle.Width, Is.EqualTo(20));
            Assert.That(rectangle.Height, Is.EqualTo(12));
            Assert.That(circle.CenterX, Is.EqualTo(14));
            Assert.That(circle.CenterY, Is.EqualTo(16));
            Assert.That(circle.Radius, Is.EqualTo(8));
        });
    }

    [Test]
    public void GetBoundingBox_ForKnownScene_ReturnsMergedCoordinates()
    {
        var picture = new Picture();
        picture.Add(new Circle(12, 20, 4));
        picture.Add(new Rectangle(30, 6, 8, 5));
        picture.Add(new Line(5, 28, 9, 24));

        var box = picture.GetBoundingBox();

        Assert.Multiple(() =>
        {
            Assert.That(box.MinX, Is.EqualTo(5));
            Assert.That(box.MinY, Is.EqualTo(6));
            Assert.That(box.MaxX, Is.EqualTo(38));
            Assert.That(box.MaxY, Is.EqualTo(28));
            Assert.That(box.Width, Is.EqualTo(33));
            Assert.That(box.Height, Is.EqualTo(22));
        });
    }
}
