using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod;

namespace Unity_Project.Scripts
{
    public class FishingAreaSession
    {
        public float SessionTime = 0f;

        public GameObject BobberGO;
        public BobberScript BobberScript;
        public Rigidbody BobberRB;

        public FishingAreaSession(GameObject go)
        {
            BobberGO = go;
            BobberScript = go.GetComponent<BobberScript>();
            BobberRB = go.GetComponent<Rigidbody>();
        }

        public FishingAreaSession(GameObject go, BobberScript bs, Rigidbody rb)
        {
            BobberGO = go;
            BobberScript = bs;
            BobberRB = rb;
        }
    }

    public class FishingAreaScript : MonoBehaviour
    {
        [SerializeField] private float m_SurfaceHeight;
        [SerializeField] private float m_FloorHeight;
        [SerializeField] private float m_GODepth;

        [SerializeField] private float m_FloatationEquation_A = 0.14f;
        [SerializeField] private float m_FloatationEquation_B = 1.4f;
        [SerializeField] private float m_FloatationEquation_C = -0.5f;

        public List<FishingAreaSession> ActiveFishingAreaSessions { get; private set; }
        public List<FishingAreaSession> InactiveFishingAreaSessions { get; private set; }

        private void Awake()
        {
            // Calculate Height
            m_SurfaceHeight = transform.position.y + (transform.localScale.y / 2f);
            m_FloorHeight = transform.position.y - (transform.localScale.y / 2f);
            m_GODepth = Mathf.Abs(m_SurfaceHeight - m_FloorHeight);

            // Create new Lists
            ActiveFishingAreaSessions = new List<FishingAreaSession>();
            InactiveFishingAreaSessions = new List<FishingAreaSession>();
        }

        private void FixedUpdate()
        {
            foreach (FishingAreaSession session in ActiveFishingAreaSessions)
            {
                var targetYVelocity = (session.BobberRB.mass * (-1 * Physics.gravity.y));
                var bobberYPosition = session.BobberRB.transform.position.y;
                var bobberRelativeDepth = m_SurfaceHeight - bobberYPosition; // Difference from m_SurfaceHeight to bobberYPosition
                var bobberNormalizedDepth = bobberRelativeDepth / m_GODepth;
                //Debug.Log($"Bobber's Relative Depth is {bobberRelativeDepth} ({bobberNormalizedDepth * 100f} % deep)");
                
                session.SessionTime += Time.fixedDeltaTime;
                session.BobberRB.AddForce(Vector3.up * (targetYVelocity * CalculateFloatationForce(bobberRelativeDepth)));
            }
        }

        // + + + + | Functions | + + + +  

        /// <summary>
        /// Returns the floatation force scalar to propel a Bobber in a FishingArea.
        /// </summary>
        /// <param name="normalizedDepth">The normalized depth from the surface (0 = surface height, 1 = floor height)</param>
        /// <returns></returns>
        private float CalculateFloatationForce(float normalizedDepth)
        {
            return (m_FloatationEquation_A * Mathf.Pow(normalizedDepth, 2f)) + (m_FloatationEquation_B * normalizedDepth) + m_FloatationEquation_C;
        }

        // + + + + | Collision Handling | + + + + 

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Bobber"))
            {
                //Debug.Log($"{gameObject.name} caught a bobber, {other.gameObject.name}!");

                // Existing FishingAreaSession?

                // Create new FishingAreaSession, pop it on the list!
                var newSession = new FishingAreaSession(other.gameObject);
                ActiveFishingAreaSessions.Add(newSession);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            /*
            if (other.gameObject.CompareTag("Bobber"))
            {
                // Assuming that all bobbers have Rigidbodies (they must),
                var bobberRb = other.attachedRigidbody;
                var floatForce = (bobberRb.mass * (-1 * Physics.gravity.y)) + 0.5f;

                bobberRb.AddForce(Vector3.up * floatForce, ForceMode.Force);
            }
            */
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Bobber"))
            {
                //Debug.Log($"{gameObject.name} caught a bobber leaving, {other.gameObject.name}!");

                // Find associated FishingAreaSession
                var fishingSession = ActiveFishingAreaSessions.Find(x => x.BobberGO.Equals(other.gameObject));
                if (fishingSession == null)
                {
                    //Debug.Log($"PROBLEM, active fishing session for bobber {other.gameObject} cannot be found...");
                }
                else
                {
                    Debug.Log($"Active fishing session for bobber {other.gameObject} removed successfully." +
                        $"Duration was for {fishingSession.SessionTime} seconds.");
                    ActiveFishingAreaSessions.Remove(fishingSession);
                    InactiveFishingAreaSessions.Add(fishingSession);
                }
            }
        }
    }
}
