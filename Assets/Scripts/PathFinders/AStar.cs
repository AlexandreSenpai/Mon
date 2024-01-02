using System.Collections.Generic;
using UnityEngine;

public class AStar : IPathFinder
{
    public void CalculateDistanceFromTargetToEachNode(Grid grid, INode target) {
        for(int x = 0; x < grid.Width; x++) {
            for(int y = 0; y < grid.Height; y++) {
                INode node = grid.GetNodeAtCoordIfExists(x, y);
                ISlot slot = node.Slot.GetComponent<ISlot>();
                if(!slot.Empty) continue;
                int h = node.Heuristic(HeuristicType.MANHATTAN, node, target);
                node.SetH(h);
            }
        }
    }

    private INode FindTheLowestF(List<INode> open) {
        INode lowest = null;

        foreach(INode node in open) {
            if(lowest == null) {
                lowest = node;
                continue;
            }
            if(node.F > lowest.F) continue;
            lowest = node;
        }

        return lowest;
    }

    private List<INode> GetPath(INode target) {
        INode node = target;
        List<INode> path = new List<INode>();

        while(node.Parent != null) {
            path.Add(node);
            node.Slot.GetComponent<TextMesh>().color = Color.red;
            node = node.Parent;
        }

        path.Reverse();

        return path;
    }

    private void DebugAStarParameters(Grid grid) {
        for(int x = 0; x < grid.Width; x++) {
            for(int y = 0; y < grid.Height; y++) {
                INode node = grid.GetNodeAtCoordIfExists(x, y);
                TextMesh slot = node.Slot.GetComponent<TextMesh>();
                slot.GetComponent<TextMesh>().text = $"F={node.F}\nG={node.G}\nH={node.H}";
            }
        }
    }

    public List<INode> Find(Grid grid, INode start, INode target) {
        List<INode> openList = new List<INode>();
        List<INode> closedList = new List<INode>();

        openList.Add(start);

        this.CalculateDistanceFromTargetToEachNode(grid, target);

        while(openList.Count > 0) {
            INode currentNode = this.FindTheLowestF(openList);
            
            GameObject currentSlotObj = currentNode.Slot;
            TextMesh text = currentSlotObj.GetComponent<TextMesh>();
            text.color = Color.red;

            openList.Remove(currentNode);
            
            currentNode.AddedToList(ListType.CLOSED);
            closedList.Add(currentNode);

            if(currentNode.X == target.X && currentNode.Y == target.Y) {
                this.DebugAStarParameters(grid);
                return this.GetPath(target);
            }

            List<INode> neighbors = grid.GetSurroundingNodes(currentNode.X, currentNode.Y);
            Debug.Log(neighbors.Count);

            foreach(INode neighbor in neighbors) {
                neighbor.Slot.GetComponent<TextMesh>().color = Color.yellow;
                if(neighbor.IsOnClosedList) continue;

                ISlot neighSlot = neighbor.Slot.GetComponent<ISlot>();

                if(!neighSlot.Empty) {
                    neighbor.AddedToList(ListType.CLOSED);
                    neighbor.SetCanPassThrough(false);
                    closedList.Add(neighbor);
                    neighbor.Slot.GetComponent<TextMesh>().color = Color.magenta;
                    continue;
                }

                int nextG = currentNode.G + 1;
                neighbor.SetG(nextG);
                neighbor.SetParent(currentNode);

                if(!neighbor.IsOnOpenList) {
                    neighbor.AddedToList(ListType.OPEN);
                    openList.Add(neighbor);
                }
            }
        }

        return new List<INode>();
    }
}
