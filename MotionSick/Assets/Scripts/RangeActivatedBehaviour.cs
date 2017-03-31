using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangeActivatedBehaviour : MonoBehaviour {

    /// <summary>
    /// The number of times this behaviour can be activated.
    /// </summary>
    [Tooltip("The number of times this behaviour can be activated. Set to -1 to activate always.")]
    public int activationCount = -1;
    private int currentCount = 0;

    /// <summary>
    /// If true, requires the object to be within view of a camera. You must attach a CameraViewTrigger component to this object to use this option.
    /// </summary>
    [Tooltip("If true, requires the object to be within view of a camera. You must attach a CameraViewTrigger component to this object to use this option.")]
    public bool requireSight = false;
    private bool inSight = false;

    /// <summary>
    /// Distance at which the RangeActivatedBehaviour begins to count down, or activates.
    /// </summary>
    [Tooltip("Distance at which the RangeActivatedBehaviour begins to count down, or activates.")]
    public float distance = 1.0f;

    /// <summary>
    /// Time it takes the RangeActivatedBehaviour to activate. Set to -1 for it to never count down.
    /// </summary>
    [Tooltip("Time it takes the RangeActivatedBehaviour to activate. Set to -1 for it to never count down.")]
    public float time = 1.0f;
    private float elapsedTime = 0.0f;
    private ulong elapsedFrames = 0;

    /// <summary>
    /// Offset for the range area.
    /// </summary>
    [Tooltip("Offset for the range area.")]
    public Vector3 offset;

    /// <summary>
    /// What kind of behaviour the RangeActivatedBehaviour uses when the target exits the range.
    /// </summary>
    public enum ExitBehaviour { Pause, Reset, Activate }

    /// <summary>
    /// What kind of behaviour the RangeActivatedBehaviour uses when the target exits the range.
    /// </summary>
    [Tooltip("What kind of behaviour the RangeActivatedBehaviour uses when the target exits the range.")]
    public ExitBehaviour exitBehaviour;

    /// <summary>
    /// The specific target that triggers this behaviour.
    /// </summary>
    [Tooltip("The specific target that triggers this behaviour.")]
    public GameObject target;

    void Awake ()
    {
        if(requireSight == true && GetComponent<CameraViewTrigger>() == null)
        {
            Debug.LogError("RangeActivatedBehaviour: No CameraViewTrigger component could be found, reverting to default state.");
            requireSight = false;
        }
    }

	void Update ()
    {
        bool canActivate = (activationCount < 0) ? true : currentCount < activationCount;
        bool hasSight = (requireSight == true) ? inSight : true;

        if (target != null && canActivate && hasSight)
        {
            if ((Vector3.Distance(target.transform.position, offset + transform.position) <= distance))
            {
                if (elapsedFrames == 0 )
                {
                    gameObject.SendMessageUpwards("OnRangeEnter", null, SendMessageOptions.DontRequireReceiver);
                    elapsedFrames = 1;
                    elapsedTime = Time.deltaTime;
                }
                else
                {
                    gameObject.SendMessageUpwards("OnRangeStay", null, SendMessageOptions.DontRequireReceiver);
                    elapsedFrames++;
                    elapsedTime += Time.deltaTime;
                }
            }
            else if (elapsedFrames != 0)
            {
                gameObject.SendMessageUpwards("OnRangeExit", null, SendMessageOptions.DontRequireReceiver);
                switch (exitBehaviour)
                {                    
                    case ExitBehaviour.Reset:
                        Reset();
                        break;
                    case ExitBehaviour.Pause:
                        break;
                    case ExitBehaviour.Activate:                        
                        gameObject.SendMessageUpwards("OnRangeActivate", null, SendMessageOptions.DontRequireReceiver);
                        break;
                }
            }

            if (elapsedTime > time)
            {
                gameObject.SendMessageUpwards("OnRangeActivate", null, SendMessageOptions.DontRequireReceiver);
                Reset();
                currentCount++;
            }
        }
    }

    void Reset()
    {
        elapsedFrames = 0;
        elapsedTime = 0.0f;
        inSight = false;
    }

    void OnViewEnter(VisibleEvent rhs)
    {
        inSight = true;
    }

    void OnViewExit(VisibleEvent lhs)
    {
        inSight = false;
    }

    void OnRangeEnter()
    {
        Debug.Log("Entered!");
    }

    void OnRangeStay()
    {
        Debug.Log("Staying!");
    }

    void OnRangeExit()
    {
        Debug.Log("Exited!");
    }

    void OnRangeActivate()
    {
        Debug.Log("Activated!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(offset + transform.position, distance);
    }
}
