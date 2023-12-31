using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField]
    public int blocksPerWidth;
    [SerializeField]
    public int blocksPerHeight;

    [SerializeField]
    public GameObject tile;

    private GameObject[,] gridMap;

    void Start() {
        this.GenerateGrid();
    }

    public GameObject[,] GetGrid() {
        return this.gridMap;
    }

    public List<Node> GetSurroundingNodes((int, int) coords) {
        List<Node> surroundingNodes = new List<Node>();

        int xCord = coords.Item1;
        int yCord = coords.Item2;

        (int, int) leftNeighbor = (xCord - 1, yCord);
        (int, int) rightNeighbor = (xCord + 1, yCord);
        (int, int) upNeighbor = (xCord, yCord + 1);
        (int, int) downNeighbor = (xCord, yCord - 1);

        (int, int)[] neighbors = {upNeighbor, rightNeighbor, leftNeighbor, downNeighbor};

        foreach ((int, int) neighbor in neighbors) {

                Slot slot = this.GetSlotByCoordIfExists(neighbor);

                if (slot != null && slot.Empty()) {
                    surroundingNodes.Add(slot.node);
                }

        }

        return surroundingNodes;
    }

    void GenerateGrid() {
        float containerWidth = this.transform.localScale.x;
        float containerHeight = this.transform.localScale.y;
        
        float blockWidth = containerWidth / blocksPerWidth;
        float blockHeight = containerHeight / blocksPerHeight;

        this.tile.transform.localScale = new Vector2(blockWidth, blockHeight);

        GameObject[,] _grid = new GameObject[blocksPerWidth, blocksPerHeight];

        for(int x = 0; x < blocksPerWidth; x++) {
            for (int y = 0; y < blocksPerHeight; y++) {
                GameObject spawned = Instantiate(this.tile, new Vector2(0, 0), Quaternion.identity);
                spawned.transform.parent = this.transform;
                Vector2 bottomLeftAnchor = new Vector2(-0.5f + spawned.transform.localScale.x / 2, -0.5f + spawned.transform.localScale.y / 2);
                Vector2 targetPosition = new Vector2(bottomLeftAnchor.x + spawned.transform.localScale.x * x, bottomLeftAnchor.y + spawned.transform.localScale.y * y);
                spawned.transform.localPosition = targetPosition;
                spawned.name = $"{x},{y}";

                Slot slot = spawned.GetComponent<Slot>();
                slot.node = new Node(x, y);

                TextMesh text = spawned.GetComponent<TextMesh>();
                text.text = $"{x},{y}";
                
                _grid[x, y] = spawned;
            }
        }

        this.gridMap = _grid;
    }

    public ((int, int), GameObject) GetObjectPositionInGrid(GameObject entity) {
        
        for(int x = 0; x < this.blocksPerWidth; x++) {
            for(int y = 0; y < this.blocksPerHeight; y++) {
                GameObject gridSlot = this.gridMap[x, y];
                Slot slot = gridSlot.GetComponent<Slot>();

                if(!slot.Empty() && GameObject.ReferenceEquals(slot.placedAt(), entity)) return ((x, y), gridSlot);
            }
        }

        return ((0, 0), null);

    }

    public Slot GetSlotByCoord((int, int) coord) {
        GameObject slot = this.gridMap[coord.Item1, coord.Item2];
        return slot.GetComponent<Slot>();
    }

    public Slot GetSlotByCoordIfExists((int, int) coord) {
        try {
            GameObject slot = this.gridMap[coord.Item1, coord.Item2];
            return slot.GetComponent<Slot>();
        } catch (IndexOutOfRangeException) {
            Debug.Log((coord, "Fora do range."));
            return null;
        }
    }

}
