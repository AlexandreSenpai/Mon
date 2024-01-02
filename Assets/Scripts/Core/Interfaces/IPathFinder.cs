using System.Collections.Generic;

public interface IPathFinder {
    List<INode> Find(Grid grid, INode start, INode target);
}