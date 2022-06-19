/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Your use of this SDK or tool is subject to the Oculus SDK License Agreement, available at
https://developer.oculus.com/licenses/oculussdk/

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

using UnityEngine;

namespace Oculus.Interaction.HandPosing
{
    [System.Serializable]
    public struct DistantPointDetectorFrustums
    {
        [SerializeField]
        private ConicalFrustum _selectionFrustum;
        [SerializeField, Optional]
        private ConicalFrustum _deselectionFrustum;
        [SerializeField, Optional]
        private ConicalFrustum _aidFrustum;
        [SerializeField]
        [Range(0f, 1f)]
        private float _aidBlending;

        public ConicalFrustum SelectionFrustum => _selectionFrustum;
        public ConicalFrustum DeselectionFrustum => _deselectionFrustum;
        public ConicalFrustum AidFrustum => _aidFrustum;
        public float AidBlending => _aidBlending;

        public DistantPointDetectorFrustums(ConicalFrustum selection,
            ConicalFrustum deselection, ConicalFrustum aid, float blend)
        {
            _selectionFrustum = selection;
            _deselectionFrustum = deselection;
            _aidFrustum = aid;
            _aidBlending = blend;
        }
    }

    public class DistantPointDetector
    {
        private DistantPointDetectorFrustums _frustums;

        public DistantPointDetector(DistantPointDetectorFrustums frustums)
        {
            _frustums = frustums;
        }

        public bool ComputeIsPointing(Collider[] colliders, bool isSelecting, out float bestScore, out Vector3 bestHitPoint)
        {
            ConicalFrustum _searchFrustrum = (isSelecting || _frustums.DeselectionFrustum == null) ?
                _frustums.SelectionFrustum : _frustums.DeselectionFrustum;
            bestHitPoint = Vector3.zero;
            bestScore = float.NegativeInfinity;
            bool anyHit = false;

            foreach (Collider collider in colliders)
            {
                float score = 0f;
                if (!_searchFrustrum.HitsCollider(collider, out score, out Vector3 hitPoint))
                {
                    continue;
                }

                if (_frustums.AidFrustum != null)
                {
                    if (!_frustums.AidFrustum.HitsCollider(collider, out float headScore, out Vector3 headPosition))
                    {
                        continue;
                    }
                    score = score * (1f - _frustums.AidBlending) + headScore * _frustums.AidBlending;
                }

                if (score > bestScore)
                {
                    bestHitPoint = hitPoint;
                    bestScore = score;
                    anyHit = true;
                }
            }
            return anyHit;
        }

        public bool IsPointingWithoutAid(Collider[] colliders)
        {
            if (_frustums.AidFrustum == null)
            {
                return false;
            }
            return !IsPointingAtColliders(colliders, _frustums.AidFrustum)
                && IsWithinDeselectionRange(colliders);
        }

        public bool IsWithinDeselectionRange(Collider[] colliders)
        {
            return IsPointingAtColliders(colliders, _frustums.DeselectionFrustum)
                || IsPointingAtColliders(colliders, _frustums.SelectionFrustum);
        }

        private bool IsPointingAtColliders(Collider[] colliders, ConicalFrustum frustum)
        {
            if (frustum == null)
            {
                return false;
            }

            foreach (Collider collider in colliders)
            {
                if (frustum.HitsCollider(collider, out float score, out Vector3 point))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
