using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this script to an object with a trigger collider to make it follow any GameObject containing a RigidBody with the tag defined in the variable "m_tag".
// Movement will start when a new target enters the trigger and will always finish
// If m_tag is left empty, it will bypass the verification and accept any GameObject
// The movement will take m_animationTime seconds and follow the animation curve m_curve.

public class MagnetizedMovement : MonoBehaviour
{
    [SerializeField] private float m_animationTime;
    [SerializeField] private AnimationCurve m_curve;
    [SerializeField] private string m_tag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_tag) || m_tag == "")
        {
            StartCoroutine(MoveTo(other.gameObject));
        }
    }

    private IEnumerator MoveTo(GameObject target)
    {
        float startTime = Time.time;
        Vector3 initialPosition = transform.position;

        while (Time.time < startTime + m_animationTime)
        {
            float delta = m_curve.Evaluate((Time.time - startTime) / m_animationTime);

            transform.position = Vector3.Lerp(initialPosition, target.transform.position, delta);

            Debug.Log(delta);

            yield return null;
        }

        CompleteMovement(target);
    }

    // Use this function to do anything needed when the movement is completed.
    private void CompleteMovement(GameObject target)
    {
        // Destroys the GameObject the script is attached to
        Destroy(gameObject);
    }
}
