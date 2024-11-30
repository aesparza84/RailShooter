using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// Path generator to place enemies on
    /// </summary>
    private SplineCreator _splineCreator;

    [SerializeField] private GameObject EnemyPrefab;

    private void Start()
    {
        _splineCreator = GetComponent<SplineCreator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnEnemyOnPath();
        }
    }
    private void SpawnEnemyOnPath()
    {
        _splineCreator.SplineConfigure();

        Spline path = _splineCreator.GetCurrentSplinePath();
        SplineContainer splineContainer = _splineCreator.GetSplineContainer();

        GameObject instEnemy = Instantiate(EnemyPrefab, transform.position, transform.rotation);
        instEnemy.GetComponent<SplineAnimate>().Container = splineContainer;
        instEnemy.GetComponent<SplineAnimate>().Container.Spline = path;
        instEnemy.GetComponent<SplineAnimate>().Play();
    }

}
