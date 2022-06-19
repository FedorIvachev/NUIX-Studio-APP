﻿/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Your use of this SDK or tool is subject to the Oculus SDK License Agreement, available at
https://developer.oculus.com/licenses/oculussdk/

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Oculus.Interaction
{
    /// <summary>
    /// This IDropZonePoseProvider uses a ordered list of individual Slots and will
    /// push the elements back or forth to make room for the new element.
    /// </summary>
    public class DropZoneSequentialSlotsProvider : MonoBehaviour, IDropZoneSlotsProvider
    {
        [SerializeField]
        private List<Transform> _slots;

        private int[] _slotInteractors;

        protected bool _started;

        protected virtual void Start()
        {
            this.BeginStart(ref _started);

            Assert.IsTrue(_slots != null && _slots.Count > 0);
            _slotInteractors = new int[_slots.Count];

            this.EndStart(ref _started);
        }

        public void TrackInteractor(DropZoneInteractor interactor)
        {
            int desiredIndex = FindBestSlotIndex(interactor.DropPoint.position);
            if (TryOccupySlot(desiredIndex))
            {
                _slotInteractors[desiredIndex] = interactor.Identifier;
            }
        }

        public void UntrackInteractor(DropZoneInteractor interactor)
        {
            if (TryFindIndexForInteractor(interactor, out int index))
            {
                _slotInteractors[index] = 0;
            }
        }

        public void SnapInteractor(DropZoneInteractor interactor)
        {
        }

        public void UnsnapInteractor(DropZoneInteractor interactor)
        {
        }

        public void UpdateTrackedInteractor(DropZoneInteractor interactor)
        {
            int desiredIndex = FindBestSlotIndex(interactor.DropPoint.position);
            if (TryFindIndexForInteractor(interactor, out int index))
            {
                if (desiredIndex != index)
                {
                    _slotInteractors[index] = 0;
                    if (TryOccupySlot(desiredIndex))
                    {
                        _slotInteractors[desiredIndex] = interactor.Identifier;
                    }
                }
            }
            else if (TryOccupySlot(desiredIndex))
            {
                _slotInteractors[desiredIndex] = interactor.Identifier;
            }
        }

        private bool TryFindIndexForInteractor(DropZoneInteractor interactor, out int index)
        {
            //FindIndex is not ideal, but this single line simplifies this sample SlotsProvider a lot.
            index = Array.FindIndex(_slotInteractors, i => i == interactor.Identifier);
            return index >= 0;
        }

        public bool PoseForInteractor(DropZoneInteractor interactor, out Pose pose)
        {
            if (TryFindIndexForInteractor(interactor, out int index))
            {
                pose = _slots[index].GetPose();
                return true;
            }
            pose = Pose.identity;
            return false;
        }

        private bool TryOccupySlot(int index)
        {
            if (IsSlotFree(index))
            {
                return true;
            }

            int freeSlot = FindBestSlotIndex(_slots[index].position, true);
            if (freeSlot < 0)
            {
                return false;
            }

            PushSlots(index, freeSlot);
            return true;
        }

        private bool IsSlotFree(int index)
        {
            return _slotInteractors[index] == 0;
        }

        private int FindBestSlotIndex(in Vector3 target, bool freeOnly = false)
        {
            int bestIndex = -1;
            float minDistance = float.PositiveInfinity;
            for (int i = 0; i < _slots.Count; i++)
            {
                if (freeOnly && !IsSlotFree(i))
                {
                    continue;
                }

                float distance = (target - _slots[i].position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestIndex = i;
                }

            }
            return bestIndex;
        }

        private void PushSlots(int index, int freeSlot)
        {
            bool forwardDirection = index > freeSlot;
            for (int i = freeSlot; i != index; i = Next(i))
            {
                int nextIndex = Next(i);
                SwapSlot(i, nextIndex);
            }

            int Next(int value)
            {
                return value + (forwardDirection ? 1 : -1);
            }
        }

        private void SwapSlot(int index, int freeSlot)
        {
            (_slotInteractors[index], _slotInteractors[freeSlot]) = (_slotInteractors[freeSlot], _slotInteractors[index]);
        }

        #region Inject
        public void InjectAllDropZoneSequentialSlotsProvider(List<Transform> slots)
        {
            InjectSlots(slots);
        }

        public void InjectSlots(List<Transform> slots)
        {
            _slots = slots;
        }
        #endregion
    }
}
