using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloTriangle
{
    public class Vec3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vec3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vec3 Normalize()
        {
            return new Vec3(Helpers.Map(X, 0, 1000, -1, 1), Helpers.Map(Y, 0, 1000, -1, 1), Helpers.Map(Z, 0, 1000, -1, 1));
        }

        public void ApplyVel(Vec3 Vel)
        {
            X += Vel.X;
            Y += Vel.Y;
            Z += Vel.Z;
        }
    }
}
