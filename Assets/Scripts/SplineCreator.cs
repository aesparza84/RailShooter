using UnityEngine;
using UnityEngine.Splines;
public class SplineCreator : MonoBehaviour
{
    /// <summary>
    /// Spline path container
    /// </summary>
    private SplineContainer splineContainer;

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

        SplineConfigure();
    }

    /// <summary>
    /// Creates default nodes to add to spline-array
    /// </summary>
    private void CreateNewSpline()
    {       
        _spline.Add(StartKnot);
        _spline.Insert(0, MidKnot);
        _spline.Insert(1, EndKnot);
    }

    /// <summary>
    /// Configures the added splines. Sets Pos & Tanget
    /// </summary>
    private void SplineConfigure()
    {

        //TODO: 
        // - Generate 3 spheres at a 'Start', 'Mid', and 'End' sectors
        // - pick a random point in Each sector
        // - assign the corresponding knots-Positoin to the picked points
        // - Update the spline to configre the knots

        StartKnot.TangentOut = new Unity.Mathematics.float3(0, pointerBendAmount, 1);
        EndKnot.TangentOut = new Unity.Mathematics.float3(0, pointerBendAmount, -1);

        _spline.SetKnot(0, StartKnot);
        _spline.SetKnot(1, EndKnot);

        _spline.SetTangentMode(0, TangentMode.Mirrored, BezierTangent.Out);
        _spline.SetTangentMode(0, TangentMode.Mirrored, BezierTangent.In);
    }

}
