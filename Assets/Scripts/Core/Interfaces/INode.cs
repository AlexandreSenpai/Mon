using UnityEngine;

public enum ListType {
    OPEN = 1,
    CLOSED = 0
}

public enum HeuristicType {
    MANHATTAN = 1
}
public interface INode {

    int X { get; }
    int Y { get; }
    int F { get; }
    int G { get; }
    int H { get; }
    INode Parent { get; }
    bool CanPassThrough { get; }
    bool IsOnClosedList { get; }
    bool IsOnOpenList { get; }
    bool Empty { get; }
    GameObject Slot { get; }

    public int Heuristic(HeuristicType type, INode start, INode end);
    public void SetParent(INode node);
    public void SetG(int g);
    public void SetH(int h);
    public void SetCanPassThrough(bool canPassThrough); 
    public void AddedToList(ListType type);
    public void AddSlot(GameObject slot);
    public void RemoveSlot();
}