using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField]
    public GameObject cursor;

    private GameObject _placedAt;
    private bool _isEmpty = true;

    public Node node;

    void Start()
    {

    }

    void OnMouseEnter() {
        this.cursor.SetActive(true);
    }

    void OnMouseExit() {
        this.cursor.SetActive(false);
    }

    public bool Empty() {
        return this._isEmpty;
    }

    public GameObject placedAt() {
        return this._placedAt;
    }

    public RaycastHit2D CreateRayCast() {
        Vector2 orientation = new Vector2(0, .2f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, orientation, .2f);
        Debug.DrawRay(transform.position, orientation, Color.red);
        return hit;
    }

    void Update()
    {
        
        RaycastHit2D ray = this.CreateRayCast();

        if(ray.collider != null) {
            if(this._placedAt != null && GameObject.ReferenceEquals(ray.collider.gameObject, this._placedAt)) return;
            Debug.Log($"{ray.collider.gameObject.name} added to slot.");

            this._placedAt = ray.collider.gameObject;
            this._isEmpty = false;
        } else {
            this._placedAt = null;
            this._isEmpty = true;
        }
    }
}
