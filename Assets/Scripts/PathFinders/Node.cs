using System;
using UnityEngine;

public class Node : INode {

    public int X { get; private set; }
    public int Y { get; private set; }
    public int F { get; private set; }
    public int G { get; private set; }
    public int H { get; private set; }
    public INode Parent { get; private set; }
    public bool CanPassThrough { get; private set; }
    public bool IsOnClosedList { get; private set; }
    public bool IsOnOpenList { get; private set; }

    public bool Empty { get; private set; }
    public GameObject Slot { get; private set; }

    public Node(int x = 0, int y = 0) {
        this.X = x;
        this.Y = y;

        this.F = 0;
        this.G = 0;
        this.H = 0;

        this.Parent = null;
        this.CanPassThrough = true;
        this.IsOnClosedList = false;
        this.IsOnOpenList = false;

        this.Empty = true;
        this.Slot = null;
    }

    public void AddSlot(GameObject slot) {
        this.Empty = false;
        this.Slot = slot;
    }

    public void RemoveSlot() {
        this.Empty = true;
        this.Slot = null;
    }

    public void SetCanPassThrough(bool canPassThrough) {
        this.CanPassThrough = canPassThrough;
    }
    public void AddedToList(ListType type) {
        switch(type) {
            case ListType.CLOSED:
                this.IsOnClosedList = true;
                this.IsOnOpenList = false;
                break;
            case ListType.OPEN:
                this.IsOnOpenList = true;
                this.IsOnClosedList = false;
                break;
        }
    }

    private int Manhattan(INode start, INode end) {
        return Math.Abs(end.X - start.X) + Math.Abs(end.Y - start.Y);
    }

    private void CalculateF() {
        this.F = this.G + this.H;
    }

    public int Heuristic(HeuristicType type, INode start, INode end)
    {
        return type switch {
            HeuristicType.MANHATTAN => this.Manhattan(start, end),
            _ => -1,
        };
    }

    public void SetG(int g)
    {
        this.G = g;
        this.CalculateF();
    }

    public void SetH(int h)
    {
        this.H = h;
        this.CalculateF();
    }

    public void SetParent(INode node)
    {
        this.Parent = node;
    }
}
