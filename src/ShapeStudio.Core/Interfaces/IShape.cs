using ShapeStudio.Core.Models;

namespace ShapeStudio.Core.Interfaces;

public interface IShape
{
    void Draw(ICanvas canvas);
    void Move(double dx, double dy);
    void Scale(double factor);
    BoundingBox GetBoundingBox();
}
