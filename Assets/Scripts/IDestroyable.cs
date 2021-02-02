using UnityEngine;

public interface IDestroyable {
    bool HasBeenDestroyed { get; set; }
    void RemoveFromGame();
}