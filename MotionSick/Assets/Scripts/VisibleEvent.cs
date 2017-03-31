using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the data for a message fired by a CameraViewTrigger object.
/// </summary>
public class VisibleEvent
{
    /// <summary>
    /// Camera that viewed this object.
    /// </summary>
    public Camera camera;

    /// <summary>
    /// Time (in seconds) that this object has been viewed for.
    /// </summary>
    public float time;

    /// <summary>
    /// Number of update cycles this object has been viewed for.
    /// </summary>
    public ulong frames;

}