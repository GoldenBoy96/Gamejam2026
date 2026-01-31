using Gamejam2026;
using System;
using UnityEngine;

namespace Gamejam2026
{
    [Serializable]
    public class PlayerModel
    {
        // Runtime Stats
        public float currentHealth;
        public float currentStamina;
        public Vector3 lastCheckpoint;

        // Input Flags (Controller sẽ cập nhật cái này, State sẽ đọc)
        public float inputX;
        public bool isJumpPressed;
        public bool isRollHeld;
        public bool isInflateHeld;
        public bool isAttackPressed;
        public bool isDownPressed;

        // Cooldown trackers
        public float lastRollAttackTime;

        public void Reset(PlayerConfigSO config, Vector3 startPos)
        {
            currentHealth = config.maxHealth;
            currentStamina = config.maxStamina;
            lastCheckpoint = startPos;
            lastRollAttackTime = -999f;
        }
    }
}
