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
        public List<FishingAreaSession> ActiveFishingAreaSessions { get; private set; }
        public List<FishingAreaSession> InactiveFishingAreaSessions { get; private set; }

        private void Awake()
        {
            ActiveFishingAreaSessions = new List<FishingAreaSession>();
            InactiveFishingAreaSessions = new List<FishingAreaSession>();
        }

        private void FixedUpdate()
        {
            foreach (FishingAreaSession session in ActiveFishingAreaSessions)
            {
                var targetYVelocity = (session.BobberRB.mass * (-1 * Physics.gravity.y)) + 0.125f;
                var bobberYVelocity = session.BobberRB.velocity.y;
                session.SessionTime += Time.fixedDeltaTime;

                session.BobberRB.AddForce(Vector3.up * Mathf.Lerp(30f, targetYVelocity, Mathf.Clamp01(session.SessionTime / 1.5f)));
                //session.BobberRB.AddForce(Vector3.up * targetYVelocity);
            }
        }

        // + + + + | Functions | + + + +  

        // + + + + | Collision Handling | + + + + 

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Bobber"))
            {
                Debug.Log($"{gameObject.name} caught a bobber, {other.gameObject.name}!");

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
                Debug.Log($"{gameObject.name} caught a bobber leaving, {other.gameObject.name}!");

                // Find associated FishingAreaSession
                var fishingSession = ActiveFishingAreaSessions.Find(x => x.BobberGO.Equals(other.gameObject));
                if (fishingSession == null)
                {
                    Debug.Log($"PROBLEM, active fishing session for bobber {other.gameObject} cannot be found...");
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
