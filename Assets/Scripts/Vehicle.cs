using UnityEngine;

namespace TrafficLights
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Vehicle : MonoBehaviour
    {
        public delegate void SendVehicle(Vehicle vehicle);
        public event SendVehicle VehicleFinish;
        public bool IsActive { get; set; }
        
        [SerializeField] private float speed = 10f;

        private Rigidbody2D vehicleRigidbody;

        private void Start()
        {
            vehicleRigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            vehicleRigidbody.velocity = IsActive ? transform.up * speed : Vector3.zero;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var vehicle = other.gameObject.GetComponent<Vehicle>();
            if (vehicle)
            {
                GlobalEvents.Instance.SendVehicleCrush();
            }
            else
            {
                VehicleFinish?.Invoke(this);
            }
        }
    }
}
