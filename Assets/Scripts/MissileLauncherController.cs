using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherController : MonoBehaviour, IWeapon
{
    [Header("Crosshair Reader")]
    [SerializeField] private GameObject crosshairObject;
    private ICrosshairReader _crosshairReader;

    //Missile Launcher
    private MissileLauncher _missileLauncher;

    //Targets to shoot at
    private Queue<GameObject> _queuedTargets;

    private void Start()
    {
        if (_crosshairReader == null)
            _crosshairReader = crosshairObject.GetComponent<CrosshairTargetFinder>();

        _crosshairReader.OnTargetOverlapped += OnTargetOverlapped;

        _missileLauncher = GetComponent<MissileLauncher>();

        _queuedTargets = new Queue<GameObject>();
    }
    private void OnDisable()
    {
        _crosshairReader.OnTargetOverlapped -= OnTargetOverlapped;
    }

    private void OnTargetOverlapped(object sender, RaycastHit e)
    {
        CheckAddNewTargetToQueue(e.transform.gameObject);   
    }
    private void CheckAddNewTargetToQueue(GameObject target)
    {
        if (_queuedTargets == null)
            _queuedTargets = new Queue<GameObject>();

        if (target.transform.GetComponent<IDamageable>() == null)
            return;

        if (_queuedTargets.Contains(target.transform.gameObject))
            return;

        Debug.Log("Added new target to queue");
        _queuedTargets.Enqueue(target.transform.gameObject);
    }
    public void Shoot()
    {
        int n = _queuedTargets.Count;

        for (int i = 0; i < n; i++)
        {
            GameObject selectedTarget = _queuedTargets.Dequeue();

            if (selectedTarget == null)
                continue;

            _missileLauncher.UpdateTarget(selectedTarget.transform);
            _missileLauncher.Shoot();
        }
    }

    public void CooldownWeapon()
    {
        //nothing for this
    }
}
