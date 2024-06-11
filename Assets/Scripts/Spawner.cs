using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent (typeof(Exploder))]

class Spawner : MonoBehaviour
{
    [SerializeField] private int _minInstantiateChance = 2;
    [SerializeField] private int _maxInstantiateChance = 6;
    [SerializeField] private int _chanceToDivide = 100;
    [SerializeField] private Color[] _colors;

    private int _randomChanceToInstantiate;
    private MeshRenderer _meshRenderer;
    private Exploder _exploder;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        SpawnDividedObjects();
    }

    private void SpawnDividedObjects()
    {
        int minDivideChance = 0;
        int maxDivideChance = 100;
        float scaleMultiplier = 0.5f;

        int randomChanceToDivide = Random.Range(minDivideChance, maxDivideChance);

        if (randomChanceToDivide <= _chanceToDivide)
        {
            _randomChanceToInstantiate = Random.Range(_minInstantiateChance, _maxInstantiateChance + 1);
            transform.localScale *= scaleMultiplier;
            _chanceToDivide /= 2;

            for (int i = 0; i < _randomChanceToInstantiate; i++)
            {
                MeshRenderer renderer = Instantiate(_meshRenderer, transform.position, Quaternion.identity);
                SetRandomColor(renderer);
            }
        }
        else
        {
            _exploder.Explode();
        }

        Destroy(gameObject);
    }

    private void SetRandomColor(MeshRenderer meshRenderer)
    {
        int minColorNumber = 0;
        int randomColor = Random.Range(minColorNumber, _colors.Length);
        meshRenderer.material.color = _colors[randomColor];
    }
}
