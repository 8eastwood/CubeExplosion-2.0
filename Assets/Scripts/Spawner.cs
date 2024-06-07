using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

class Spawner : MonoBehaviour
{
    [SerializeField] private int _minInstantiateChance = 2;
    [SerializeField] private int _maxInstantiateChance = 6;
    [SerializeField] private int _chanceToDivide = 100;
    [SerializeField] private Color[] _colors;

    private int _minDivideChance = 0;
    private int _maxDivideChance = 100;
    private int _randomChanceToDivide;
    private int _randomChanceToInstantiate;
    private float _scaleMultiplier = 0.5f;
    private MeshRenderer _meshRenderer;
    private int _randomColor;
    private int _minColorNumber = 0;
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
        _randomChanceToDivide = Random.Range(_minDivideChance, _maxDivideChance);

        if (_randomChanceToDivide <= _chanceToDivide)
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
            _exploder.GetExplode(gameObject);
            Destroy(gameObject);
        }
    }

    private void SetObjectColor(MeshRenderer meshRenderer)
    {
        _randomColor = Random.Range(_minColorNumber, _colors.Length);
        meshRenderer.material.color = _colors[_randomColor];
    }
}
