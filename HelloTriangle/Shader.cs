using OpenTK.Graphics.OpenGL4;

namespace HelloTriangle
{
    public class Shader : IDisposable
    {
        int Handle;

        public Shader(string vertexPath, string fragmentPath)
        {
            //Vertex shader turns co-ord into a 1 to -1 range in x y and z
            //Fragment shader turns 4 nums in range 0 to 1 into a color (RGBA)


            string VertexShaderSource = File.ReadAllText(vertexPath);  //Grab ShaderData

            string FragmentShaderSource = File.ReadAllText(fragmentPath);

            int VertexShader = GL.CreateShader(ShaderType.VertexShader); //Initialize shaders
            GL.ShaderSource(VertexShader, VertexShaderSource);

            int FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);

            GL.CompileShader(VertexShader); //Try compiling 1 at a time
            
            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int status);
            if(status == 0)
            {
                string info = GL.GetShaderInfoLog(VertexShader);
                Console.WriteLine(info);
                throw new Exception(info);
            }
            
            
            
            GL.CompileShader(FragmentShader);

            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out int _status);
            if(_status == 0)
            {
                string info = GL.GetShaderInfoLog(FragmentShader);
                Console.WriteLine(info);
                throw new Exception(info);
            }

            Handle = GL.CreateProgram();



            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(Handle);
                Console.WriteLine(infoLog);
            }
            //Cleanup code
            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);

        }

        

        public void Use()
        {
            //applies shader to gemetry
            GL.UseProgram(Handle);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
