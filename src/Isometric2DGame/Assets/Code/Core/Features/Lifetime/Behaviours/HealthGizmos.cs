using System;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Core.Features.Lifetime.Behaviours
{
    public class HealthGizmos : MonoBehaviour
    {
        public EntityBehaviour EntityBehaviour;
        
        private void OnDrawGizmos()
        {
            try
            {
                DrawEntityHealthBar(
                    EntityBehaviour.Entity.WorldPosition, 
                    (int)EntityBehaviour.Entity.CurrentHp, 
                    (int)EntityBehaviour.Entity.MaxHp);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
        
         private void DrawEntityHealthBar(Vector3 entityPos, int current, int max)
        {
            var healthRatio = (float)current / max;
            var barPos = GetHealthBarPosition(entityPos);
            
            var healthColor = GetHealthColorAdvanced(healthRatio);
            
            Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
            Gizmos.DrawCube(barPos, new Vector3(2.1f, 0.35f, 0.1f));
            
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(barPos, new Vector3(2.1f, 0.35f, 0.1f));
            
            if (healthRatio > 0) 
            {
                Gizmos.color = healthColor;
                var fillSize = new Vector3(2f * healthRatio, 0.25f, 0.08f);
                var fillPos = barPos + new Vector3((2f - fillSize.x) * -0.5f, 0, 0);
                Gizmos.DrawCube(fillPos, fillSize);
            }
        }
        
        private Vector3 GetHealthBarPosition(Vector3 entityPosition)
        {
            return entityPosition + Vector3.up * 1.25f;
        }

        private Vector3 GetScaledBarSize(Vector3 worldPosition, Vector3 baseSize)
        {
            var sceneCamera = Camera.current;
            if (sceneCamera != null) 
            {
                var distance = Vector3.Distance(sceneCamera.transform.position, worldPosition);
                var scale = Mathf.Clamp(distance * 0.15f, 0.5f, 2.5f);
                return baseSize * scale;
            }
            return baseSize;
        }
        
        private Color GetHealthColorAdvanced(float healthRatio)
        {
            if (healthRatio >= 0.6f) 
            {
                var t = (healthRatio - 0.6f) / 0.4f;
                return Color.Lerp(new Color(0.8f, 1f, 0f), Color.green, t);
            }
            else if (healthRatio >= 0.3f) 
            {
                var t = (healthRatio - 0.3f) / 0.3f;
                return Color.Lerp(new Color(1f, 0.6f, 0f), Color.yellow, t);
            }
            else 
            {
                var t = healthRatio / 0.3f;
                return Color.Lerp(new Color(0.6f, 0f, 0f), Color.red, t);
            }
        }
    }
}