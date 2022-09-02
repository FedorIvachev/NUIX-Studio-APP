using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NUIXRooms
{
    /// <summary>
    /// This class handles the solver components attached to this gameobject
    /// </summary>
    public class SolverHandler : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Add an additional offset of the tracked object to base the solver on. Useful for tracking something like a halo position above your head or off the side of a controller.")]
        private Vector3 additionalOffset;

        /// <summary>
        /// Add an additional offset of the tracked object to base the solver on. Useful for tracking something like a halo position above your head or off the side of a controller.
        /// </summary>
        public Vector3 AdditionalOffset
        {
            get => additionalOffset;
            set
            {
                if (additionalOffset != value)
                {
                    additionalOffset = value;
                    RefreshTrackedObject();
                }
            }
        }

        [SerializeField]
        [Tooltip("Add an additional rotation on top of the tracked object. Useful for tracking what is essentially the up or right/left vectors.")]
        private Vector3 additionalRotation;

        /// <summary>
        /// Add an additional rotation on top of the tracked object. Useful for tracking what is essentially the up or right/left vectors.
        /// </summary>
        public Vector3 AdditionalRotation
        {
            get => additionalRotation;
            set
            {
                if (additionalRotation != value)
                {
                    additionalRotation = value;
                    RefreshTrackedObject();
                }
            }
        }

        [SerializeField]
        [Tooltip("Whether or not this SolverHandler calls SolverUpdate() every frame. Only one SolverHandler should manage SolverUpdate(). This setting does not affect whether the Target Transform of this SolverHandler gets updated or not.")]
        private bool updateSolvers = true;

        /// <summary>
        /// Whether or not this SolverHandler calls SolverUpdate() every frame. Only one SolverHandler should manage SolverUpdate(). This setting does not affect whether the Target Transform of this SolverHandler gets updated or not.
        /// </summary>
        public bool UpdateSolvers
        {
            get => updateSolvers;
            set => updateSolvers = value;
        }

        protected readonly List<Solver> solvers = new List<Solver>();
        private bool updateSolversList = false;

        /// <summary>
        /// List of solvers that this handler will manage and update
        /// </summary>
        public IReadOnlyCollection<Solver> Solvers
        {
            get => solvers.AsReadOnly();
            set
            {
                if (value != null)
                {
                    solvers.Clear();
                    solvers.AddRange(value);
                }
            }
        }

        /// <summary>
        /// The position the solver is trying to move to.
        /// </summary>
        public Vector3 GoalPosition { get; set; }

        /// <summary>
        /// The rotation the solver is trying to rotate to.
        /// </summary>
        public Quaternion GoalRotation { get; set; }

        /// <summary>
        /// The scale the solver is trying to scale to.
        /// </summary>
        public Vector3 GoalScale { get; set; }


        // Hidden GameObject managed by this component and attached as a child to the tracked target type (i.e head, hand etc)
        private GameObject trackingTarget;

        /// <summary>
        /// The timestamp the solvers will use to calculate with.
        /// </summary>
        public float DeltaTime { get; set; }

        private float lastUpdateTime;


        /// <summary>
        /// Adds <paramref name="solver"/> to the list of <see cref="Solvers"/> guaranteeing inspector ordering.
        /// </summary>
        public void RegisterSolver(Solver solver)
        {
            if (!solvers.Contains(solver))
            {
                solvers.Add(solver);
                updateSolversList = true;
            }
        }

        /// <summary>
        /// Removes <paramref name="solver"/> from the list of <see cref="Solvers"/>.
        /// </summary>
        public void UnregisterSolver(Solver solver)
        {
            solvers.Remove(solver);
        }


        protected virtual void DetachFromCurrentTrackedObject()
        {
            if (trackingTarget != null)
            {
                trackingTarget.transform.parent = null;
            }
        }

        protected virtual void AttachToNewTrackedObject()
        {
            Transform target;
            if (true)
            {
                target = Camera.main.transform;
            }
            TrackTransform(target);
        }

        private void TrackTransform(Transform target)
        {
            if (trackingTarget == null)
            {
                trackingTarget = new GameObject("Tracking Target")
                {
                    hideFlags = HideFlags.HideInHierarchy
                };
            }

            if (target != null)
            {
                trackingTarget.transform.parent = target;
                trackingTarget.transform.localPosition = Vector3.Scale(AdditionalOffset, trackingTarget.transform.localScale);
                trackingTarget.transform.localRotation = Quaternion.Euler(AdditionalRotation);
            }
        }

        /// <summary>
        /// Returns true if the solver handler's transform target is not valid
        /// </summary>
        /// <returns>true if not tracking valid hands and/or target, false otherwise</returns>
        private bool IsInvalidTracking()
        {
            if (trackingTarget == null || trackingTarget.transform.parent == null)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Clears the transform target and attaches to the current
        /// </summary>
        public void RefreshTrackedObject()
        {
            DetachFromCurrentTrackedObject();
            AttachToNewTrackedObject();
        }


        private void Awake()
        {
            DeltaTime = Time.deltaTime;
            lastUpdateTime = Time.realtimeSinceStartup;
        }

        protected virtual void Start()
        {
            RefreshTrackedObject();
        }

        protected virtual void Update()
        {
            if (IsInvalidTracking())
            {
                RefreshTrackedObject();
            }

            DeltaTime = Time.realtimeSinceStartup - lastUpdateTime;
            lastUpdateTime = Time.realtimeSinceStartup;
        }
    }

}



