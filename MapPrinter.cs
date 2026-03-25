using PathFinder.MapGeneration;

namespace PathFinder;

public class MapPrinter
{
    public void Print(string[,] maze)
    {
        for (int row = 0; row < maze.GetLength(0); row++)
        {
            for (int col = 0; col < maze.GetLength(1); col++)
            {
                Console.Write(maze[row, col]);
            }

            Console.WriteLine();
        }
    }

    public void Print(string[,] maze, List<Point> path)
    {
        var pathSet = new HashSet<Point>(path);

        Point? start = path.Count > 0 ? path[0] : null;
        Point? end = path.Count > 0 ? path[^1] : null;

        for (int row = 0; row < maze.GetLength(0); row++)
        {
            for (int col = 0; col < maze.GetLength(1); col++)
            {
                Point current = new Point(row, col);

                if (start.HasValue && current.Equals(start.Value))
                {
                    Console.Write("A");
                }
                else if (end.HasValue && current.Equals(end.Value))
                {
                    Console.Write("B");
                }
                else if (pathSet.Contains(current))
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write(maze[row, col]);
                }
            }

            Console.WriteLine();
        }
    }
}
