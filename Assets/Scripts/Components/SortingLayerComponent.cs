using UnityEngine;

public class SortingLayerComponent : MonoBehaviour
{
    [SerializeField] private string sortingLayerName;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.sortingLayerName = sortingLayerName;
        //_renderer.sortingOrder = 10;
    }
}
