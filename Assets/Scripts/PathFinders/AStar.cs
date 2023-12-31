using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private GridGenerator gridManager;
    [SerializeField]
    public int targetX = 0;
    public int targetY = 0;
    void Start()
    {
        GameObject grid = GameObject.FindGameObjectWithTag("Grid");
        this.gridManager = grid.GetComponent<GridGenerator>();
    }

    public void Find() {
        Debug.Log("Initializing Searching.");
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        Slot startSlot = this.gridManager.GetSlotByCoord((0, 0));
        Slot targetSlot = this.gridManager.GetSlotByCoord((targetX, targetY));
        
        Node start = startSlot.node;
        Node end = targetSlot.node;

        if(!targetSlot.Empty()) {
            return;
        }

        openList.Add(start);

        for(int x = 0; x < gridManager.blocksPerWidth; x++) {
            for(int y = 0; y < gridManager.blocksPerHeight; y++) {
                Slot slot = this.gridManager.GetSlotByCoord((x, y));

                if(!slot.Empty()) {
                    Node node = new Node(x, y);
                    node.isOnClosedList = true;
                    closedList.Add(node);
                } else {
                    Node node = new Node(x, y);
                    node.canPassThrough = true;
                    int h = Node.Heuristic(start, end);
                    node.SetHValue(h);
                }

            }
        }

        while(openList.Count > 0) {
            Node currentNode = openList.OrderBy(node => node.GetFValue()).FirstOrDefault();
            openList.Remove(currentNode);

            closedList.Add(currentNode);

            if(currentNode.GetX() == end.GetX() && currentNode.GetY() == end.GetY()) {
                Debug.Log("Found!");

                Node point = end;

                while(point.GetParent() != null) {
                    GameObject slot = gridManager.GetGrid()[point.GetX(), point.GetY()];
                    TextMesh text = slot.GetComponent<TextMesh>();
                    text.color = Color.red;
                    Debug.Log($"Walk to ({point.GetX()},{point.GetY()})");
                    point = point.GetParent();
                }

                return;
            }

            List<Node> neighbors = this.gridManager.GetSurroundingNodes((currentNode.GetX(), currentNode.GetY()));
            foreach(Node neighbor in neighbors) {
                if(neighbor.isOnClosedList) {
                    continue;
                }

                int nextGValue = currentNode.GetGValue();

                if(!neighbor.isOnOpenList || nextGValue < neighbor.GetGValue()) {
                    neighbor.SetGValue(nextGValue);
                    neighbor.SetParentNode(currentNode);

                    if(!neighbor.isOnOpenList) {
                        neighbor.isOnOpenList = true;
                        openList.Add(neighbor);
                    } else {
                        neighbor.SetParentNode(currentNode);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
