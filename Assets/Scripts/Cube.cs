using UnityEngine;

[RequireComponent(typeof(Spawner))]

public class Cube : MonoBehaviour
{
    [SerializeField] private GameObject _cube;

    private Spawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>(); 
    }

    private void OnMouseDown()
    {
        _spawner.SpawnDividedObjects();
    }
}
