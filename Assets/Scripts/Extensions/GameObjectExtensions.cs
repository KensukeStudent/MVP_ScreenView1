using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    public static void SetSafeActive(this GameObject self, bool isActive)
    {
        if (self != null && (self.activeInHierarchy != isActive || self.activeSelf != isActive))
        {
            self.SetActive(isActive);
        }
    }
}
