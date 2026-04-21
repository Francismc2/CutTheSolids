using System;
using System.Linq;
using UnityEngine;

namespace Engage.AFX.v1
{
    public static class ComponentsSafeToGet
    {
        private static readonly Type[] types = 
        {
            typeof(FloatComponent),
            typeof(IntComponent),
            typeof(BoolComponent),
            typeof(StringComponent),
            typeof(QuaternionComponent),
            typeof(Vector2Component),
            typeof(Vector3Component),

            typeof(ListFloatComponent),
            typeof(ListIntComponent),
            typeof(ListBoolComponent),
            typeof(ListStringComponent),
            typeof(ListGameObjectComponent),
            typeof(ListObjectComponent),
            typeof(ListTransformComponent),
            typeof(ListMaterialComponent),

            typeof(UnityEventComponent),
        };

        public static bool IsSafeComponentType(Type type)
        {
            return types.Contains(type);
        }
    }
}