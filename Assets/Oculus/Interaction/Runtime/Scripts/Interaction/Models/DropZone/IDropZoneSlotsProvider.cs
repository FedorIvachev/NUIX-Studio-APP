﻿/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Your use of this SDK or tool is subject to the Oculus SDK License Agreement, available at
https://developer.oculus.com/licenses/oculussdk/

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

using UnityEngine;

namespace Oculus.Interaction
{
    /// <summary>
    /// This interface is used to specify how a series of slots (e.g. an inventory)
    /// will be laid out and how the objects will snap to them within a DropZone.
    /// </summary>
    public interface IDropZoneSlotsProvider
    {
        /// <summary>
        /// Indicates that a new element is hovering the slots.
        /// </summary>
        /// <param name="interactor">The element nearby</param>
        void TrackInteractor(DropZoneInteractor interactor);
        /// <summary>
        /// Indicates that an element is no longer part of the drop zone
        /// </summary>
        /// <param name="interactor">The element that exited</param>
        void UntrackInteractor(DropZoneInteractor interactor);
        /// <summary>
        /// Indicates that the tracked element has been snapped by
        /// the dropzone.
        /// </summary>
        /// <param name="interactor">The selected element</param>
        void SnapInteractor(DropZoneInteractor interactor);
        /// <summary>
        /// Indicates that the element is not snapping anymore
        /// to the dropzone.
        /// </summary>
        /// <param name="interactor">the unselected element</param>
        void UnsnapInteractor(DropZoneInteractor interactor);
        /// <summary>
        /// Called frequently when a non-placed element moves near the slots.
        /// Use this callback to reorganize the placed elements.
        /// </summary>
        /// <param name="interactor">The element nearby</param>
        void UpdateTrackedInteractor(DropZoneInteractor interactor);
        /// <summary>
        /// This method returns the desired Pose for a queried element
        /// within the drop zone.
        /// </summary>
        /// <param name="interactor">Queried element</param>
        /// <param name="pose">The desired pose in the drop zone</param>
        /// <returns>True if the element has a valid pose in the zone</returns>
        bool PoseForInteractor(DropZoneInteractor interactor, out Pose pose);
    }
}
