# ShapeStudio

Aplicatie .NET pentru laboratorul de pattern-uri, construita pe un model de forme 2D cu ierarhie nelimitata.

## Pattern-uri implementate

- `Composite`: `Picture` este nod compus si poate contine orice `IShape`, inclusiv alte obiecte `Picture`
- `Bridge`: toate obiectele de tip `IShape` deseneaza prin `ICanvas`, fara dependenta de detalii de randare
- `Proxy`: `ReadOnlyShapeProxy` permite `Draw()` si `GetBoundingBox()`, dar blocheaza `Move()` si `Scale()`

## Structura proiect

- `ShapeStudio.sln`
- `src/ShapeStudio.Core`
  - `Interfaces`: `IShape`, `ICanvas`
  - `Shapes`: `Line`, `Circle`, `Rectangle`, `Ellipse`
  - `Composite`: `Picture`
  - `Canvas`: `ConsoleCanvas`, `SvgCanvas`
  - `Proxy`: `ReadOnlyShapeProxy`
  - `Models`: `BoundingBox`
- `src/ShapeStudio.Wpf`
  - UI desktop WPF pentru incarcare scene, transformari, demo proxy si export SVG
- `tests/ShapeStudio.Tests`
  - `CompositeTests`, `BridgeTests`, `ProxyTests`

## Exemplu scena cu niveluri multiple

```csharp
var root = new Picture();

var topLayer = new Picture();
topLayer.Add(new Line(80, 70, 260, 70));
topLayer.Add(new Rectangle(300, 40, 180, 100));

var middleLayer = new Picture();
middleLayer.Add(new Circle(200, 250, 52));
middleLayer.Add(new Ellipse(390, 250, 80, 40));

var innerLayer = new Picture();
innerLayer.Add(new Rectangle(530, 210, 140, 90));
innerLayer.Add(new Line(540, 330, 700, 380));

middleLayer.Add(innerLayer);
root.Add(topLayer);
root.Add(middleLayer);
```

## Exemplu SVG rezultat

```xml
<?xml version="1.0" encoding="UTF-8"?>
<svg xmlns="http://www.w3.org/2000/svg" version="1.1">
  <line x1="80" y1="70" x2="260" y2="70" stroke="#0f172a" fill="none" />
  <rect x="300" y="40" width="180" height="100" stroke="#0f172a" fill="none" />
  <circle cx="200" cy="250" r="52" stroke="#0f172a" fill="none" />
  <ellipse cx="390" cy="250" rx="80" ry="40" stroke="#0f172a" fill="none" />
</svg>
```

## Comenzi

- `dotnet build ShapeStudio.sln`
- `dotnet test ShapeStudio.sln`
- `dotnet run --project src/ShapeStudio.Wpf`
