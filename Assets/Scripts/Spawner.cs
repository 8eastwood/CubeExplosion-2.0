using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

class Spawner : MonoBehaviour
{
    [SerializeField] private int _minInstantiateChance = 2;
    [SerializeField] private int _maxInstantiateChance = 6;
    [SerializeField] private int _chanceToDivide = 100;
    [SerializeField] private Color[] _colors;

    private int _randomChanceToInstantiate;
    private MeshRenderer _meshRenderer;
    private Exploder _exploder;

    private void OnMouseDown()
    {
        SpawnDividedObjects();
    }

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void SpawnDividedObjects()
    {
        int _minDivideChance = 0;
        int _maxDivideChance = 100;
        float _scaleMultiplier = 0.5f;

        int randomChanceToDivide = Random.Range(_minDivideChance, _maxDivideChance);

        if (randomChanceToDivide <= _chanceToDivide)
        {
            _randomChanceToInstantiate = Random.Range(_minInstantiateChance, _maxInstantiateChance);
            transform.localScale *= _scaleMultiplier;
            _chanceToDivide /= 2;
            Destroy(gameObject);

            for (int i = 0; i < _randomChanceToInstantiate; i++)
            {
                MeshRenderer renderer = Instantiate(_meshRenderer, transform.position, Quaternion.identity);
                SetObjectColor(renderer);
            }
        }
        else
        {
            _exploder.ExplodeObject(gameObject);
            Destroy(gameObject);
        }
    }

    private void SetObjectColor(MeshRenderer meshRenderer)
    {
        int minColorNumber = 0;
        int randomColor = Random.Range(minColorNumber, _colors.Length);
        meshRenderer.material.color = _colors[randomColor];
    }
}
