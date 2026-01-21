# Rocket Shooter Game - Setup Guide

## Overview
This game features a player that can move in 4 cardinal directions and fires rockets in a circular pattern. Power-ups scattered around the map increase the number of rockets fired (from 4 up to 8 maximum).

**⚠️ IMPORTANT:** This game uses **pure mathematics** for all movement and collision detection - **NO Unity physics components** (Rigidbody2D, Colliders, etc.) are used. Everything is calculated using vector math, distance checks, and transforms.

## Scripts Implemented

### ✅ PlayerController.cs
**Location:** `Assets/PlayerController.cs`

**Features:**
- 4-directional movement (WASD or Arrow keys)
- Automatic rocket firing every few seconds
- Rocket count starts at 4, can increase to 8
- Rockets fire in a circular pattern around the player

**Inspector Settings:**
- `Move Speed`: 5 (adjust for faster/slower movement)
- `Rocket Prefab`: Drag your rocket GameObject here
- `Fire Interval`: 2 seconds (time between rocket volleys)
- `Rocket Count`: 4 (starting rockets)
- `Max Rockets`: 8 (maximum rockets)

### ✅ Rocket.cs
**Location:** `Assets/Rocket.cs`

**Features:**
- Travels in a straight line
- Automatically rotates to face direction of travel
- Self-destructs after lifetime expires

**Inspector Settings:**
- `Speed`: 8 (rocket travel speed)
- `Lifetime`: 3 seconds (how long before auto-destroy)

### ✅ RocketPowerUp.cs
**Location:** `Assets/RocketPowerUp.cs`

**Features:**
- Detects when player is nearby
- Increases rocket count by 1 (up to max of 8)
- Destroys itself when collected

**Inspector Settings:**
- `Pickup Radius`: 0.5 (distance for auto-pickup)

### ✅ NoGoZone.cs
**Location:** `Assets/NoGoZone.cs`

**Features:**
- Creates danger zones on the map
- Visual warning (red color + shake) when player approaches
- Resets scene if player gets too close

**Inspector Settings:**
- `Player`: Drag the Player GameObject here
- `Warning Distance`: 5 (when warnings start)
- `Danger Distance`: 4 (when scene resets)
- `Shake Strength`: 0.1
- `Shake Speed`: 20

### ℹ️ ObjectMover.cs
**Location:** `Assets/ObjectMover.cs`

**Purpose:** Utility script for creating moving objects/obstacles using sine/cosine patterns.

---

## Unity Setup Instructions

### 1. Create the Player GameObject

1. **Create Player:**
   - Right-click in Hierarchy → 2D Object → Sprite → Square (or use your own sprite)
   - Rename to "Player"
   - Position at (0, 0, 0)

2. **Attach Script:**
   - Add Component → Scripts → PlayerController
   - Leave Rocket Prefab empty for now (we'll set it after creating the rocket)

### 2. Create the Rocket Prefab

1. **Create Rocket GameObject:**
   - Right-click in Hierarchy → 2D Object → Sprite → Triangle (or custom sprite)
   - Rename to "Rocket"
   - Scale to something small like (0.3, 0.3, 1)

2. **Add Components:**
   - Add Component → Scripts → Rocket
   - Set Speed: 8
   - Set Lifetime: 3

3. **Create Prefab:**
   - Create a "Prefabs" folder in Assets if it doesn't exist
   - Drag the Rocket from Hierarchy into the Prefabs folder
   - Delete the Rocket from the Hierarchy (we only need the prefab)

4. **Link to Player:**
   - Select the Player in Hierarchy
   - In PlayerController component, drag the Rocket prefab into the "Rocket Prefab" slot

### 3. Create Power-Ups

1. **Create Power-Up GameObject:**
   - Right-click in Hierarchy → 2D Object → Sprite → Circle
   - Rename to "RocketPowerUp"
   - Scale to (0.5, 0.5, 1)
   - Change color to make it stand out (e.g., yellow or green)

2. **Add Components:**
   - Add Component → Scripts → RocketPowerUp
   - Set Pickup Radius: 0.5

3. **Create Prefab (Optional):**
   - Drag into Prefabs folder to create a prefab
   - You can then duplicate this prefab around the map

4. **Place Multiple Power-Ups:**
   - Duplicate the power-up (Ctrl+D) and place around the map
   - Recommended: 5-8 power-ups scattered around

### 4. Create Danger Zones (Optional)

1. **Create NoGoZone GameObject:**
   - Right-click in Hierarchy → 2D Object → Sprite → Square
   - Rename to "NoGoZone"
   - Scale to create a danger area (e.g., 2, 2, 1)
   - Change color to something dangerous (red/orange)

2. **Add Components:**
   - Add Component → Scripts → NoGoZone
   - Drag the Player GameObject into the "Player" field
   - Adjust Warning Distance and Danger Distance as needed

3. **Place Around Map:**
   - Position these zones as obstacles/boundaries
   - Duplicate to create multiple danger zones

### 5. Camera Setup

1. **Main Camera:**
   - Select Main Camera in Hierarchy
   - Set Position: (0, 0, -10)
   - Adjust Size (Orthographic Size) to see the whole play area (try 10-15)

2. **Camera Follow (Optional):**
   - If you want the camera to follow the player, add a simple script:
   ```csharp
   public class CameraFollow : MonoBehaviour
   {
       public Transform target;
       public float smoothSpeed = 0.125f;
       public Vector3 offset = new Vector3(0, 0, -10);

       void LateUpdate()
       {
           if (target != null)
           {
               Vector3 desiredPosition = target.position + offset;
               Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
               transform.position = smoothedPosition;
           }
       }
   }
   ```

### 6. Testing

1. **Press Play** and test:
   - ✅ Player moves with WASD/Arrow keys
   - ✅ Rockets fire automatically every 2 seconds
   - ✅ 4 rockets fire in a circular pattern (45°, 135°, 225°, 315°)
   - ✅ Rockets travel in straight lines
   - ✅ Power-ups can be collected
   - ✅ Rocket count increases (check in Inspector while playing)
   - ✅ Maximum 8 rockets can be achieved

---

## Rocket Firing Pattern Explanation

The rockets fire in a perfect circle around the player:

- **4 Rockets:** 45°, 135°, 225°, 315° (diagonal cross)
- **5 Rockets:** 45°, 117°, 189°, 261°, 333°
- **6 Rockets:** 45°, 105°, 165°, 225°, 285°, 345°
- **7 Rockets:** 45°, 96.4°, 147.9°, 199.3°, 250.7°, 302.1°, 353.6°
- **8 Rockets:** 45°, 90°, 135°, 180°, 225°, 270°, 315°, 360°

The pattern always starts at 45° and distributes evenly around 360°.

---

## Customization Tips

### Adjust Movement Speed
In PlayerController, change `moveSpeed` (default: 5)

### Change Fire Rate
In PlayerController, change `fireInterval` (default: 2 seconds)

### Rocket Speed & Lifetime
In Rocket prefab, adjust `speed` (default: 8) and `lifetime` (default: 3)

### Starting Rockets
In PlayerController, change `rocketCount` (default: 4, max: 8)

### Power-Up Pickup Range
In RocketPowerUp, adjust `pickupRadius` (default: 0.5)

---

## Troubleshooting

**Rockets don't spawn:**
- Make sure Rocket Prefab is assigned in PlayerController
- Check that Rocket.cs is attached to the rocket prefab

**Power-ups don't work:**
- Ensure PlayerController is attached to the player
- Check that the player has the correct tag or the script can find it

**Player doesn't move:**
- Check that PlayerController is attached
- Verify Input settings (Edit → Project Settings → Input Manager)

**Rockets don't rotate:**
- Make sure the Rocket sprite is oriented correctly (pointing right by default)
- The script rotates based on the direction vector

---

## Next Steps

1. Add visual effects (particles) when rockets spawn
2. Add sound effects for firing and power-up collection
3. Create enemies that rockets can destroy
4. Add a score system
5. Create a UI to display current rocket count
6. Add different types of power-ups (speed boost, fire rate, etc.)

Enjoy your rocket shooter game! 🚀
