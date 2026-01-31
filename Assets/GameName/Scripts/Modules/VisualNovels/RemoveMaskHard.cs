using System.Collections.Generic;
using UnityEngine;

namespace Gamejam2026
{
    public class RemoveMaskHard : MonoBehaviour
    {
        public List<GameObject> masks = new List<GameObject>();
        public float duration = 1f;
        // No parent zoom/shake — only masks animate
        private List<Vector3> originalPositions = new List<Vector3>();

        void Start()
        {
            StoreOriginalPositions();
            RemoveMasks();
        }

        private void StoreOriginalPositions()
        {
            originalPositions.Clear();
            foreach (GameObject mask in masks)
            {
                originalPositions.Add(mask.transform.position);
            }
        }
        public void RemoveMasks()
        {
            StartCoroutine(RemoveMasksSequentially());
        }

        public void ResetMasks()
        {
            for (int i = 0; i < masks.Count; i++)
            {
                GameObject mask = masks[i];
                if (mask == null) continue;

                // Reset position
                if (i < originalPositions.Count)
                {
                    mask.transform.position = originalPositions[i];
                }

                // Reset opacity to full
                if (mask.TryGetComponent<CanvasGroup>(out var cg))
                {
                    cg.alpha = 1f;
                }
                else if (mask.TryGetComponent<Renderer>(out var rend))
                {
                    Color c = rend.material.color;
                    c.a = 1f;
                    rend.material.color = c;
                }
            }
        }

        private System.Collections.IEnumerator RemoveMasksSequentially()
        {
            Vector2 previousDirection = Vector2.zero;
            int count = Mathf.Max(1, masks.Count);
            float perMaskDuration = duration / count;

            foreach (GameObject mask in masks)
            {
                yield return StartCoroutine(AnimateMask(mask, previousDirection, (direction) => previousDirection = direction, perMaskDuration));
            }
        }

        private System.Collections.IEnumerator AnimateMask(GameObject mask, Vector2 previousDirection, System.Action<Vector2> onComplete, float maskDuration)
        {
            Vector3 originalPosition = mask.transform.position;
            Color originalColor = mask.GetComponent<CanvasGroup>() != null && mask.GetComponent<CanvasGroup>().alpha > 0 ? 
                Color.white : (mask.GetComponent<Renderer>() != null ? mask.GetComponent<Renderer>().material.color : Color.white);

            Vector2 randomDirection = GetRandomDirectionAvoidingSimilar(previousDirection);

            // Run mask animation for this mask's allocated time
            yield return StartCoroutine(MaskAnimationCoroutine(mask, originalPosition, randomDirection, originalColor, maskDuration));

            // // Reset mask position and opacity
            // mask.transform.position = originalPosition;
            // if (mask.TryGetComponent<CanvasGroup>(out var cg))
            // {
            //     cg.alpha = 1f;
            // }
            // else if (mask.TryGetComponent<Renderer>(out var rend))
            // {
            //     Color c = rend.material.color;
            //     c.a = 1f;
            //     rend.material.color = c;
            // }

            onComplete(randomDirection);
        }

        private System.Collections.IEnumerator MaskAnimationCoroutine(GameObject mask, Vector3 originalPosition, Vector2 randomDirection, Color originalColor, float maskDuration)
        {
            float elapsed = 0f;
            while (elapsed < maskDuration)
            {
                elapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsed / maskDuration);

                // Move away from origin in random direction
                Vector3 moveDirection = new Vector3(randomDirection.x, randomDirection.y, 0f);
                Vector3 newPosition = originalPosition + moveDirection * progress * 200f;
                mask.transform.position = newPosition;

                // Fade out by reducing opacity
                if (mask.TryGetComponent<CanvasGroup>(out var canvasGroup))
                {
                    canvasGroup.alpha = Mathf.Lerp(1f, 0f, progress);
                }
                else if (mask.TryGetComponent<Renderer>(out var renderer))
                {
                    Color color = renderer.material.color;
                    color.a = Mathf.Lerp(1f, 0f, progress);
                    renderer.material.color = color;
                }

                yield return null;
            }
        }

        

        private Vector2 GetRandomDirectionAvoidingSimilar(Vector2 previousDirection)
        {
            Vector2 newDirection;
            float minAngle = 60f; // Minimum angle in degrees
            float minDotProduct = Mathf.Cos(minAngle * Mathf.Deg2Rad);

            do
            {
                newDirection = Random.insideUnitCircle.normalized;
            } while (previousDirection != Vector2.zero && Vector2.Dot(newDirection, previousDirection) > minDotProduct);

            return newDirection;
        }
    }

}