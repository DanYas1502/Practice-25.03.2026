using PathFinder;
using PathFinder.MapGeneration;

var optionsToGenerate = new MapGeneratorOptions()
{
    Height = 10,
    Width = 30,
    Seed = 123
};

var generator = new MapGenerator(optionsToGenerate);
string[,]? map = generator.Generate();

if (map == null)
{
    Console.WriteLine("Map was not generated.");
    return;
}

var start = new Point(1, 1);
var destination = new Point(8, 28);

var pathFinder = new BreadthFirstSearch();
var (path, visitedVertices) = pathFinder.FindPath(map, start, destination);

Console.WriteLine("Generated map:");
new MapPrinter().Print(map);

Console.WriteLine();
Console.WriteLine($"Visited vertices: {visitedVertices}");
Console.WriteLine($"Path length: {path.Count}");

Console.WriteLine();
Console.WriteLine("Map with path:");
new MapPrinter().Print(map, path);
