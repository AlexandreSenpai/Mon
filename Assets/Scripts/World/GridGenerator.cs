using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField]
    public int blocksPerWidth;
    [SerializeField]
    public int blocksPerHeight;

    [SerializeField]
    public GameObject slot;

    private Grid _grid = null;

    void Start() {
        this._grid = new Grid(this.blocksPerWidth, this.blocksPerHeight);
        this.InstantiateGrid();
    }

    void Update() {
        AStar finder = new AStar();

        INode start = this._grid.GetNodeAtCoord(0, 0);
        INode end = this._grid.GetNodeAtCoord(16, 5);

        List<INode> path = finder.Find(this._grid, start, end);

        foreach(INode node in path) {
            Debug.Log($"Caminhe para: {node.X} {node.Y}");
        }
    }

    void InstantiateGrid() {
        this._grid.CreateGrid((int x, int y) => new Node(x, y));

        float containerWidth = this.transform.localScale.x;
        float containerHeight = this.transform.localScale.y;
        
        float blockWidth = containerWidth / blocksPerWidth;
        float blockHeight = containerHeight / blocksPerHeight;

        this.slot.transform.localScale = new Vector2(blockWidth, blockHeight);

        for(int x = 0; x < this._grid.Width; x++) {
            for (int y = 0; y < this._grid.Height; y++) {

                INode node = this._grid.GetNodeAtCoord(x, y);

                GameObject spawned = Instantiate(this.slot, new Vector2(0, 0), Quaternion.identity);
                spawned.transform.SetParent(this.transform);

                node.AddSlot(spawned);

                Vector2 bottomLeftAnchor = new Vector2(
                    -0.5f + spawned.transform.localScale.x / 2, 
                    -0.5f + spawned.transform.localScale.y / 2
                );
                Vector2 targetPosition = new Vector2(
                    bottomLeftAnchor.x + spawned.transform.localScale.x * x, 
                    bottomLeftAnchor.y + spawned.transform.localScale.y * y
                );

                spawned.transform.localPosition = targetPosition;
                spawned.name = $"{x},{y}";

                Slot slot = spawned.GetComponent<Slot>();

                TextMesh text = spawned.GetComponent<TextMesh>();
                text.text = $"{x},{y}";

            }
        }
    }

}
