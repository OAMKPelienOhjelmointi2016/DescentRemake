using UnityEngine;

static class UnityExtensions
{
    public static bool IsNan( this Vector3 self )
    {
        return float.IsNaN(self.x) || float.IsNaN(self.y) || float.IsNaN(self.z);
    }
}

