using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour
{
    [SerializeField]
    public GameObject cursor;

    void Start()
    {
        
    }

    void Move((int, int) coord, GridGenerator gridManager) {
        Slot slot = gridManager.GetSlotByCoord(coord);
        if(!slot.Empty()) {
            Debug.Log($"Você não pode se mover para esta direção pois {slot.placedAt().name} está bloqueando o caminho!");
            return;
        }
        AStar star = this.GetComponent<AStar>();
        star.Find();
        // Vector2 position = new Vector2(slot.transform.position.x, slot.transform.position.y + .2f);
        // this.transform.position = position;
    }

    void FlipModel(int side) {
        if (side == 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (side == 1)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    
    void Update()
    {
        GameObject grid = GameObject.FindGameObjectWithTag("Grid");

        if(grid == null) return;

        GridGenerator gridManager = grid.GetComponent<GridGenerator>();

        ((int, int), GameObject) entityPosition = gridManager.GetObjectPositionInGrid(this.gameObject);

        if(entityPosition.Item2 == null) return;

        (int, int) coords = entityPosition.Item1;
        int xCord = coords.Item1;
        int yCord = coords.Item2;

        if(Input.GetKeyDown(KeyCode.RightArrow) && xCord >= 0 && xCord < gridManager.blocksPerWidth) {
            this.Move((xCord + 1, yCord), gridManager);
            this.FlipModel(1);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && xCord > 0) {
            this.Move((xCord - 1, yCord), gridManager);
            this.FlipModel(0);
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && yCord >= 0 && yCord < gridManager.blocksPerHeight) {
            this.Move((xCord, yCord + 1), gridManager);
        } else if (Input.GetKeyDown(KeyCode.DownArrow) && yCord > 0) {
            this.Move((xCord, yCord - 1), gridManager);
        }

    }
}
