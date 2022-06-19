/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Your use of this SDK or tool is subject to the Oculus SDK License Agreement, available at
https://developer.oculus.com/licenses/oculussdk/

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Oculus.Interaction.Throw;

namespace Oculus.Interaction
{
    public class DistanceGrabInteractor : PointerInteractor<DistanceGrabInteractor, DistanceGrabInteractable>,
        IDistanceInteractor
    {
        [SerializeField, Interface(typeof(ISelector))]
        private MonoBehaviour _selector;

        [SerializeField]
        private ConicalFrustum _selectionFrustum;

        [SerializeField, Optional]
        private Transform _grabCenter;

        [SerializeField, Optional]
        private Transform _grabTarget;

        private IMovement _movement;

        public ConicalFrustum PointerFrustum => _selectionFrustum;

        public float BestInteractableWeight { get; private set; } = float.MaxValue;

        [SerializeField, Interface(typeof(IVelocityCalculator)), Optional]
        private MonoBehaviour _velocityCalculator;
        public IVelocityCalculator VelocityCalculator { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Selector = _selector as ISelector;
            VelocityCalculator = _velocityCalculator as IVelocityCalculator;
        }

        protected override void Start()
        {
            this.BeginStart(ref _started, base.Start);
            Assert.IsNotNull(Selector);
            Assert.IsNotNull(_selectionFrustum);

            if (_grabCenter == null)
            {
                _grabCenter = transform;
            }

            if (_grabTarget == null)
            {
                _grabTarget = _grabCenter;
            }

            if (_velocityCalculator != null)
            {
                Assert.IsNotNull(VelocityCalculator);
            }
            this.EndStart(ref _started);
        }

        protected override void DoPreprocess()
        {
            transform.position = _grabCenter.position;
            transform.rotation = _grabCenter.rotation;
        }

        protected override DistanceGrabInteractable ComputeCandidate()
        {
            DistanceGrabInteractable closestInteractable = null;
            float bestScore = float.NegativeInfinity;

            IEnumerable<DistanceGrabInteractable> interactables = DistanceGrabInteractable.Registry.List(this);
            foreach (DistanceGrabInteractable interactable in interactables)
            {
                Collider[] colliders = interactable.Colliders;
                foreach (Collider collider in colliders)
                {
                    if (_selectionFrustum.HitsCollider(collider, out float score, out Vector3 hitPoint)
                        && score > bestScore)
                    {
                        bestScore = score;
                        closestInteractable = interactable;
                    }
                }
            }

            BestInteractableWeight = bestScore;
            return closestInteractable;
        }

        protected override void InteractableSelected(DistanceGrabInteractable interactable)
        {
            Pose target = _grabTarget.GetPose();
            _movement = interactable.GenerateAligner(_grabTarget.GetPose());
            base.InteractableSelected(interactable);
            interactable.WhenPointerEventRaised += HandleOtherPointerEventRaised;
        }

        protected override void InteractableUnselected(DistanceGrabInteractable interactable)
        {
            interactable.WhenPointerEventRaised -= HandleOtherPointerEventRaised;
            _movement?.StopAndSetPose(_movement.Pose);
            base.InteractableUnselected(interactable);
            _movement = null;

            ReleaseVelocityInformation throwVelocity = VelocityCalculator != null ?
                VelocityCalculator.CalculateThrowVelocity(interactable.transform) :
                new ReleaseVelocityInformation(Vector3.zero, Vector3.zero, Vector3.zero);
            interactable.ApplyVelocities(throwVelocity.LinearVelocity, throwVelocity.AngularVelocity);
        }

        private void HandleOtherPointerEventRaised(PointerArgs args)
        {
            if (SelectedInteractable == null)
            {
                return;
            }

            if (args.PointerEvent == PointerEvent.Select || args.PointerEvent == PointerEvent.Unselect)
            {
                Pose toPose = _grabTarget.GetPose();
                if (SelectedInteractable.ResetGrabOnGrabsUpdated)
                {
                    _movement = SelectedInteractable.GenerateAligner(toPose);
                    SelectedInteractable.PointableElement.ProcessPointerEvent(
                        new PointerArgs(Identifier, PointerEvent.Move, _movement.Pose));
                }
            }

            if (args.Identifier == Identifier && args.PointerEvent == PointerEvent.Cancel)
            {
                SelectedInteractable.WhenPointerEventRaised -= HandleOtherPointerEventRaised;
            }
        }

        protected override Pose ComputePointerPose()
        {
            if (SelectedInteractable != null)
            {
                return _movement.Pose;
            }
            return _grabTarget.GetPose();
        }

        protected override void DoSelectUpdate()
        {
            DistanceGrabInteractable interactable = _selectedInteractable;
            if (interactable == null)
            {
                return;
            }

            _movement.UpdateTarget(_grabTarget.GetPose());
            _movement.Tick();
        }

        #region Inject
        public void InjectAllGrabInteractor(ISelector selector, ConicalFrustum selectionFrustum)
        {
            InjectSelector(selector);
            InjectSelectionFrustum(selectionFrustum);
        }

        public void InjectSelector(ISelector selector)
        {
            _selector = selector as MonoBehaviour;
            Selector = selector;
        }

        public void InjectSelectionFrustum(ConicalFrustum selectionFrustum)
        {
            _selectionFrustum = selectionFrustum;
        }

        public void InjectOptionalGrabCenter(Transform grabCenter)
        {
            _grabCenter = grabCenter;
        }

        public void InjectOptionalGrabTarget(Transform grabTarget)
        {
            _grabTarget = grabTarget;
        }

        public void InjectOptionalVelocityCalculator(IVelocityCalculator velocityCalculator)
        {
            _velocityCalculator = velocityCalculator as MonoBehaviour;
            VelocityCalculator = velocityCalculator;
        }

        #endregion
    }
}
