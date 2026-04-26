using ShapeStudio.Core.Interfaces;
using ShapeStudio.Core.Models;

namespace ShapeStudio.Core.Composite;

public sealed class Picture : IShape
{
    private readonly List<IShape> _shapes = new();

    public IReadOnlyCollection<IShape> Shapes => _shapes;

    public void Add(IShape shape)
    {
        ArgumentNullException.ThrowIfNull(shape);
        _shapes.Add(shape);
    }

    public bool Remove(IShape shape) => _shapes.Remove(shape);

    public void Draw(ICanvas canvas)
    {
        ArgumentNullException.ThrowIfNull(canvas);
        ForEachShape(shape => shape.Draw(canvas));
    }

    public void Move(double dx, double dy) => ForEachShape(shape => shape.Move(dx, dy));

    public void Scale(double factor) => ForEachShape(shape => shape.Scale(factor));

    public BoundingBox GetBoundingBox()
    {
        if (_shapes.Count == 0)
        {
            return new BoundingBox(0, 0, 0, 0);
        }

        return _shapes
            .Select(shape => shape.GetBoundingBox())
            .Aggregate(BoundingBox.Union);
    }

    private void ForEachShape(Action<IShape> action)
    {
        foreach (var shape in _shapes)
        {
            action(shape);
        }
    }
}
