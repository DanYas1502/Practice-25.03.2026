using PathFinder.MapGeneration;

namespace PathFinder;

public class BreadthFirstSearch : IPathFinder
{
    private static readonly Point[] Directions =
    {
        new Point(-1, 0), // up
        new Point(1, 0),  // down
        new Point(0, -1), // left
        new Point(0, 1),  // right
    };

    public (List<Point>, int) FindPath(string[,] map, Point start, Point destination)
    {
        var queue = new Queue<Point>();
        var visited = new HashSet<Point>();
        var origins = new Dictionary<Point, Point>();

        int visitedVertices = 0;

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            Point current = queue.Dequeue();
            visitedVertices++;

            if (current.Equals(destination))
            {
                List<Point> path = RestorePath(origins, start, destination);
                return (path, visitedVertices);
            }

            foreach (Point next in GetNeighbors(map, current))
            {
                if (visited.Contains(next))
                    continue;

                visited.Add(next);
                origins[next] = current;
                queue.Enqueue(next);
            }
        }

        return (new List<Point>(), visitedVertices);
    }

    private static List<Point> RestorePath(
        Dictionary<Point, Point> origins,
        Point start,
        Point destination)
    {
        var path = new List<Point>();

        if (!start.Equals(destination) && !origins.ContainsKey(destination))
            return path;

        Point current = destination;
        path.Add(current);

        while (!current.Equals(start))
        {
            current = origins[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }

    private static IEnumerable<Point> GetNeighbors(string[,] map, Point point)
    {
        foreach (var direction in Directions)
        {
            Point next = new Point(
                point.Row + direction.Row,
                point.Column + direction.Column
            );

            if (!IsInside(map, next))
                continue;

            if (IsWall(map, next))
                continue;

            yield return next;
        }
    }

    private static bool IsInside(string[,] map, Point point)
    {
        return point.Row >= 0 &&
               point.Row < map.GetLength(0) &&
               point.Column >= 0 &&
               point.Column < map.GetLength(1);
    }

    private static bool IsWall(string[,] map, Point point)
    {
        return map[point.Row, point.Column] == "█";
    }
}
