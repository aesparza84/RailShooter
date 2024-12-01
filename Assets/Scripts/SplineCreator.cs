using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;
public class SplineCreator : MonoBehaviour
{
    /// <summary>
    /// Spline path container
    /// </summary>
    private SplineContainer splineContainer;

    [Header("Sector Anchors")]
    [SerializeField] private Transform StartPoint;
    [SerializeField] private Transform MidPoint;
    [SerializeField] private Transform EndPoint;

    [Range(0f, 50f)]
    [SerializeField] private float StartSectorRadius = 5;
    [Range(0f, 50f)]
    [SerializeField] private float MidSectorRadius = 5;
    [Range(0f, 50f)]
    [SerializeField] private float EndSectorRadius = 5;

    //Position of the knots
    private Vector3 StartPos;
    private Vector3 MidPos;
    private Vector3 EndPos;

    /// <summary>
    /// This spline container
    /// </summary>
    private Spline _spline;

    private BezierKnot StartKnot;
    private BezierKnot MidKnot;
    private BezierKnot EndKnot;

    private float pointerBendAmount = 2;

    private void Start()
    {
        splineContainer = GetComponent<SplineContainer>();
        _spline = splineContainer.Spline;

        CreateNewSpline();

    }

    /// <summary>
    /// Creates default nodes to add to spline-array
    /// </summary>
    private void CreateNewSpline()
    {       
        _spline.Add(StartKnot);
        _spline.Insert(1, MidKnot);
        _spline.Insert(2, EndKnot);
    }

    /// <summary>
    /// Configures the added splines. Sets Pos & Tanget
    /// </summary>
    public void SplineConfigure()
    {
        //TODO: 
        // - Generate 3 spheres at a 'Start', 'Mid', and 'End' sectors
        // - pick a random point in Each sector
        // - assign the corresponding knots-Positoin to the picked points
        // - Update the spline to configre the knots

        StartKnot.Position = GetSectorPoint(StartPoint, StartSectorRadius);
        MidKnot.Position =  GetSectorPoint(MidPoint, MidSectorRadius);
        EndKnot.Position = GetSectorPoint(EndPoint, EndSectorRadius);

        StartKnot.TangentOut = new Unity.Mathematics.float3(0, pointerBendAmount, 1);

        _spline.SetKnot(0, StartKnot);
        _spline.SetKnot(1, MidKnot);
        _spline.SetKnot(2, EndKnot);

        _spline.SetTangentMode(0, TangentMode.Mirrored, BezierTangent.Out);

        _spline.SetTangentMode(1, TangentMode.AutoSmooth, BezierTangent.In);

        _spline.SetTangentMode(2, TangentMode.Mirrored, BezierTangent.In);
    }

    /// <summary>
    /// Generates a point within the cirlce around a given secot point
    /// </summary>
    /// <param name="sectorTransform"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    private Vector3 GetSectorPoint(Transform sectorTransform, float radius)
    {
        Vector3 sectorPos = Random.insideUnitSphere * radius;
        sectorPos += sectorTransform.localPosition;
        sectorPos.z = sectorTransform.localPosition.z;   

        return sectorPos;
    }

    /// <summary>
    /// Returns the splin container used by the path generator
    /// </summary>
    /// <returns></returns>
    public SplineContainer GetSplineContainer()
    {
        return splineContainer;
    }

    /// <summary>
    /// Returns the current generated spline
    /// </summary>
    /// <returns></returns>
    public Spline GetCurrentSplinePath()
    {
        return _spline;
    }


    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(StartPoint.position, StartPoint.forward, StartSectorRadius);

        Handles.color = Color.yellow;
        Handles.DrawWireDisc(MidPoint.position, MidPoint.forward, MidSectorRadius);

        Handles.color = Color.red;
        Handles.DrawWireDisc(EndPoint.position, EndPoint.forward, EndSectorRadius);

    }
}
