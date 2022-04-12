using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    public class BobberScript : MonoBehaviour
    {
        public const float CASTTIME_MAXIMUM = 3f;
        public Vector3 OriginPoint;

        private IEnumerator m_FlyAlongCastPathCRT;
        public FishingRodGearContext m_FRGContext;
        public Rigidbody m_Rigidbody;

        private List<Vector3> testPointList = new List<Vector3>()
        {
            Vector3.zero, Vector3.one, Vector3.one * 2, Vector3.one * 3, Vector3.one * 4
        };

        private void Awake()
        {
            m_FRGContext = transform.parent.parent.GetComponent<FishingRodGearContext>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            OriginPoint = transform.position;
        }

        // + + + + | Functions | + + + +

        public void CastBobberNonKinematic(Vector3 playerForward)
        {
            // Cast!
            DetachFromFRGContext();
            DeactivateKinematic();
            m_Rigidbody.AddForce(playerForward.normalized * 10f, ForceMode.Impulse);

        }

        private void ActivateKinematic() => m_Rigidbody.isKinematic = true;
        private void DeactivateKinematic() => m_Rigidbody.isKinematic = false;

        private void DetachFromFRGContext()
        {
            transform.parent = null;
        }

        private void ReattachToFRGContext()
        {
            transform.parent = m_FRGContext.transform.GetChild(0);
        }

        public void HandleFlyAlongCastPath(ref List<Vector3> path)
        {
            Debug.Log("Handling FlyAlongCastPath in BobberScript!");

            foreach(Vector3 point in path)
            {
                Debug.Log(point);
            }

            // TODO: Replace final path location with the collision point
            DetachFromFRGContext();
            m_FlyAlongCastPathCRT = FlyAlongCastPathCRT(path);
            StartCoroutine(m_FlyAlongCastPathCRT);
        }

        public IEnumerator FlyAlongCastPathCRT(List<Vector3> path)
        {
            var listPathSize = path.Count;

            for(int currPathNum = 1; currPathNum < listPathSize; currPathNum++)
            {
                for (float lerpStepper = 0f; lerpStepper < 1.0f; lerpStepper += 0.05f)
                {
                    transform.position = Vector3.Lerp(path[currPathNum - 1], path[currPathNum], lerpStepper);
                    yield return new WaitForSeconds((CASTTIME_MAXIMUM / currPathNum) / 100f);
                }
            }

        }
    }
}