using System;
using Unity.VisualScripting;
using UnityEngine;

public class Slot : MonoBehaviour, ISlot
{
    [SerializeField]
    public Sprite sprite;

    public GameObject PlacedAt { get; private set; }
    public bool Empty { get; private set; } = true;

    public RaycastHit2D CreateRayCast() {
        Vector2 orientation = new Vector2(0, .2f);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, orientation, .2f);
        Debug.DrawRay(transform.position, orientation, Color.red);
        return hit;
    }

    void Update()
    {
        RaycastHit2D ray = this.CreateRayCast();

        if(ray.collider != null) {
            if(this.PlacedAt != null && GameObject.ReferenceEquals(ray.collider.gameObject, this.PlacedAt)) return;
            Debug.Log($"{ray.collider.gameObject.name} added to slot.");

            this.PlacedAt = ray.collider.gameObject;
            this.Empty = false;
        } else {
            this.PlacedAt = null;
            this.Empty = true;
        }
    }

}
