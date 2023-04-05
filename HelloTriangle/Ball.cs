using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloTriangle
{
    public class Ball
    {

        public Vec3 Pos { get; set; }
        public Vec3 Vel { get; set; }

        public long Gravity { get; set; }

        public long Radius { get; set; }

        public Vec3[] Vertexes { get; set; }

        public Ball(Vec3 pos, Vec3 vel, long gravity, long radius)
        {
            Pos = pos;
            Vel = vel;
            Gravity = gravity;
            Radius = radius;
        }

        
        public void InitVertexes()
        {
            //generating a circle as an 360-gon

            Vertexes = new Vec3[360];
            for (int i = 0; i < 360; i++)
            {
                var degToRad = (Math.PI / 180) * i;
                float x = Pos.X + (long)(Radius * Math.Cos(degToRad));
                float y = Pos.Y + (long)(Radius * Math.Sin(degToRad));
                Vertexes[i] = new Vec3(x, y, 0);
            }
        }





        public void Update()
        {
            Vel.Y -= Gravity;
            Pos.ApplyVel(Vel);

            //Collision
            if(Pos.Y >= 1000 - Radius)
            {
                Pos.Y = 1000 - Radius;
                Vel.Y = -Vel.Y;
            }
            else if(Pos.Y - Radius <= 0)
            {
                Pos.Y = Radius;
                Vel.Y = -Vel.Y;
            }

        }

        public float[] OutputVertexes()
        {
            float[] t = new float[Vertexes.Length*3];

            for (int i = 0; i < Vertexes.Length; i++)
            {
                var vertices = Vertexes[i].Normalize();
                t[i * 3] = vertices.X;
                t[(i * 3) + 1] = vertices.Y;
                t[(i * 3) + 2] = vertices.Z;
            }
            return t;
        }


    }
}
