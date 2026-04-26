using System;
using System.Windows;
using ShapeStudio.Core.Canvas;
using ShapeStudio.Core.Composite;
using ShapeStudio.Core.Proxy;
using ShapeStudio.Core.Shapes;
using ShapeStudio.Wpf.Rendering;

namespace ShapeStudio.Wpf;

public partial class MainWindow : Window
{
    private Picture _scene = new();
    private int _step;

    public MainWindow()
    {
        InitializeComponent();
        LoadLayeredScene();
    }

    private void LoadLayeredSceneClick(object sender, RoutedEventArgs e)
    {
        LoadLayeredScene();
    }

    private void LoadSymmetrySceneClick(object sender, RoutedEventArgs e)
    {
        _scene = BuildSymmetryScene();
        DrawScene();
        Log("symmetry scene loaded");
    }

    private void MoveSceneClick(object sender, RoutedEventArgs e)
    {
        _scene.Move(30, 15);
        DrawScene();
        Log("scene moved by (+30, +15)");
    }

    private void ScaleSceneClick(object sender, RoutedEventArgs e)
    {
        _scene.Scale(1.15);
        DrawScene();
        Log("scene scaled by 1.15");
    }

    private void ProxyDemoClick(object sender, RoutedEventArgs e)
    {
        var lockedCircle = new ReadOnlyShapeProxy(new Circle(140, 90, 32));
        try
        {
            lockedCircle.Move(10, 5);
        }
        catch (InvalidOperationException ex)
        {
            Log($"proxy check: {ex.Message}");
        }
    }

    private void ExportSvgClick(object sender, RoutedEventArgs e)
    {
        var svgCanvas = new SvgCanvas();
        svgCanvas.SetStrokeColor("#0f172a");
        _scene.Draw(svgCanvas);
        SvgTextBox.Text = svgCanvas.GetSvg();
        Log("svg snapshot exported");
    }

    private void LoadLayeredScene()
    {
        _scene = BuildLayeredScene();
        DrawScene();
        Log("layered scene loaded");
    }

    private void DrawScene()
    {
        var renderCanvas = new WpfCanvas(ViewportCanvas);
        _scene.Draw(renderCanvas);
        var bounds = _scene.GetBoundingBox();
        StatusText.Text = $"Scene bounds: ({bounds.MinX:0.#}, {bounds.MinY:0.#}) -> ({bounds.MaxX:0.#}, {bounds.MaxY:0.#})";
    }

    private void Log(string text)
    {
        _step++;
        OperationsList.Items.Insert(0, $"#{_step:00} {text}");
    }

    private static Picture BuildLayeredScene()
    {
        var root = new Picture();
        var top = new Picture();
        top.Add(new Line(80, 70, 260, 70));
        top.Add(new Rectangle(300, 40, 180, 100));

        var middle = new Picture();
        middle.Add(new Circle(200, 250, 52));
        middle.Add(new Ellipse(390, 250, 80, 40));

        var inner = new Picture();
        inner.Add(new Rectangle(530, 210, 140, 90));
        inner.Add(new Line(540, 330, 700, 380));

        middle.Add(inner);
        root.Add(top);
        root.Add(middle);
        return root;
    }

    private static Picture BuildSymmetryScene()
    {
        var root = new Picture();
        var left = new Picture();
        left.Add(new Rectangle(120, 120, 100, 80));
        left.Add(new Circle(170, 260, 38));
        left.Add(new Line(80, 340, 220, 390));

        var right = new Picture();
        right.Add(new Rectangle(520, 120, 100, 80));
        right.Add(new Circle(570, 260, 38));
        right.Add(new Line(520, 390, 660, 340));

        var bridge = new Picture();
        bridge.Add(new Ellipse(370, 220, 90, 45));
        bridge.Add(new Line(260, 220, 480, 220));

        root.Add(left);
        root.Add(right);
        root.Add(bridge);
        return root;
    }
}
