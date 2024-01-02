using UnityEngine;

public interface ISlot {
    public bool Empty { get; }
    public GameObject PlacedAt { get; }
}