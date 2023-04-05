using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace HelloTriangle
{
    public class GLCode : GameWindow
    {

        public Ball ball = new(new Vec3(500,500,0), new Vec3(0,0,0), 1, 100);

        public int fps = 0;
        
        public int VertexBufferObject; //handle for the buffer

        public int VertexArrayObject; //handle for the array

        public Shader shader;

        public Stopwatch timer = new();


        public GLCode(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings
        {
            Size = new OpenTK.Mathematics.Vector2i(width, height),
            Title = title
        })
        {

        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            ball.Update();
            ball.InitVertexes();
            GL.Clear(ClearBufferMask.ColorBufferBit);




            //FPS COUNTER
            timer.Stop();
            double elapsed = timer.Elapsed.TotalSeconds;
            timer.Restart();

            fps = (int)(1.0 / elapsed);

            double frameTime = 1.0 / 60.0;

            if (elapsed < frameTime)
            {
                int sleepTime = (int)((frameTime - elapsed) * 1000);
                Thread.Sleep(sleepTime);
            }

            Title = $"HelloTriangle FPS: {fps}";
            float[] vertices = ball.OutputVertexes();

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Lines, 0, vertices.Length/3);


            KeyboardState input = KeyboardState;
            if(input.IsKeyDown(Keys.Escape))
            {
                Close();
            }



            SwapBuffers();
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            timer.Start();
            ball.InitVertexes();
            GL.ClearColor(0.0f,0.0f,0.0f,1.0f); //Background color


            VertexArrayObject = GL.GenVertexArray();

            float[] vertices = ball.OutputVertexes();


            // ..:: Initialization code (done once (unless your object frequently changes)) :: ..
            // 1. bind Vertex Array Object
            GL.BindVertexArray(VertexArrayObject);
            // 2. copy our vertices array in a buffer for OpenGL to use
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            // 3. then set our vertex attributes pointers
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //handle for the buffer
            VertexBufferObject = GL.GenBuffer();
            //binds the current buffer to that handle
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            //Put triangle vertecies into the buffer
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            shader = new("C:\\Users\\Will\\source\\repos\\HelloTriangle\\HelloTriangle\\shader.vert", "C:\\Users\\Will\\source\\repos\\HelloTriangle\\HelloTriangle\\shader.frag");
        }


        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0,0,e.Width,e.Height);
        }
        protected override void OnClosed()
        {
            base.OnClosed();

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            shader.Dispose();
        }

    }


}