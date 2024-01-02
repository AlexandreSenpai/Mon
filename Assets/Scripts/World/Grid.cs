using System;
using System.Collections.Generic;

public class Grid {
    
    private INode[,] _grid;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Grid(int width = 0, int height = 0) {
        this.Width = width;
        this.Height = height;
    }
    
    public INode[,] CreateGrid<Node>(NodeFactory<Node> nodeFactory) where Node : INode {
        this._grid = new INode[this.Width, this.Height];

        for(int x = 0; x < this.Width; x++) {
            for (int y = 0; y < this.Height; y++) {
                INode node = nodeFactory(x, y);

                this._grid[x, y] = node;
            }
        }

        return this._grid;
    }

    public INode GetNodeAtCoord(int x, int y) {
        return this._grid[x, y];
    }

    public INode GetNodeAtCoordIfExists(int x, int y) {
        try {
            return this._grid[x, y];
        } catch (IndexOutOfRangeException) {
            return null;
        }
    }

    public List<INode> GetSurroundingNodes(int x, int y) {         
    
        List<INode> surroundingNodes = new List<INode>();

        (int, int) leftNeighbor = (x - 1, y);
        (int, int) rightNeighbor = (x + 1, y);
        (int, int) upNeighbor = (x, y + 1);
        (int, int) downNeighbor = (x, y - 1);

        (int, int)[] neighbors = {upNeighbor, rightNeighbor, leftNeighbor, downNeighbor};

        foreach ((int, int) neighbor in neighbors) {

            INode node = this.GetNodeAtCoordIfExists(neighbor.Item1, neighbor.Item2);

            if (node != null) {
                surroundingNodes.Add(node);
            }

        }

        return surroundingNodes;
    }

}
