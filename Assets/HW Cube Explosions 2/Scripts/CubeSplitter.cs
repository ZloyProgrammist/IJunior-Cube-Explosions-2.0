using UnityEngine;

public class CubeSplitter : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private CubeClickReader _clickReader;
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeExploder _exploder;
    [SerializeField] private RandomColorProvider _colorProvider;

    [Header("Настройки разделения")]
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField] private float _spawnScaleFactor = 0.5f;
    [SerializeField] private float _spawnOffset = 0.6f;
    [SerializeField, Min(1f)] private float _chanceDecreaseDivider = 2f;
    [SerializeField] private float _splitForce = 5f;

    [Header("Настройки взрыва")]
    [SerializeField] private float _explosionRadius = 50;
    [SerializeField] private float _explosionForce = 50f;

    private void OnEnable()
    {
        if (_clickReader != null)
        {
            _clickReader.CubeClicked += OnCubeClicked;
        }
        else
        {
            Debug.LogWarning($"{nameof(CubeClickReader)}: CubeClickReader не найден!");
        }
    }

    private void OnDisable()
    {
        if (_clickReader != null)
        {
            _clickReader.CubeClicked -= OnCubeClicked;
        }
    }

    private void OnCubeClicked(SplitableCube cube)
    {
        if (Random.value <= cube.SplitChance)
        {
            Split(cube);
        }
        else
        {
            _exploder.Explode(cube, _explosionForce, _explosionRadius);
        }

        _spawner.Despawn(cube);
    }

    private void Split(SplitableCube cube)
    {
        int amountParts = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

        for (int i = 0; i < amountParts; i++)
        {
            Vector3 direction = Vector3.up;
            Vector3 spawnPosition = cube.transform.position + direction * (cube.Size * _spawnOffset);
            float newSize = cube.Size * _spawnScaleFactor;
            float newChance = cube.SplitChance / _chanceDecreaseDivider;
            Color newColor = _colorProvider.Generate();

            var newCube = _spawner.Create(spawnPosition, newSize, newChance, newColor);
            _exploder.ApplySplitForce(newCube, direction, _splitForce);
        }
    }
}