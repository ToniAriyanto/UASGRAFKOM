using System;
using System.Collections.Generic;
using System.Text;
using LearnOpenTK.Common;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace camera
{
    class Windows : GameWindow
    {

        private readonly float[] _vertices =
        {
            // Positions          Normals              Texture coords
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
        };
        private readonly Vector3 _lightPos = new Vector3(-0.1f, 0.7f, -2.2f);
        private int _vertexBufferObject;
        private int _vaoModel;
        private int _vaoLamp;
        private Shader _lampShader;
        private Shader _lightingShader;
        private Camera _camera;
        private Vector3 _objectPos;

        //------------------ COBA COBA ---------------------//

        Mesh pavementkiri = new Mesh();
        Mesh landkiri = new Mesh();
        Mesh Kandang = new Mesh();
        Mesh rumahBambu = new Mesh();
        Mesh orang = new Mesh();
        Mesh seesaw = new Mesh();
        Mesh kursi = new Mesh();
        Mesh mobil = new Mesh();
        Mesh lampuJalan1 = new Mesh();
        Mesh lampuJalan2 = new Mesh();
        Mesh lampuJalan3 = new Mesh();
        Mesh lampuJalan4 = new Mesh();
        Mesh tongSampah1 = new Mesh();
        Mesh tongSampah2 = new Mesh();


        string desertPath = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/DesertRoadPlane.obj";
        string cabinetPath = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/cabinet1.obj";
        string jalan = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/cobajalan.obj";
        string kandang = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/kandang.obj";
        string bambu = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/bamboo.obj";
        string manusia = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/Male.OBJ";
        string jungkat = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/seesaw.obj";
        string bench = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/newBench.obj";
        string ngengeng = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/mobilblender.obj";
        string lampu1 = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/lampujalan1.obj";
        string lampu2 = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/lampujalan2.obj";
        string lampu3 = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/lampujalan3.obj";
        string lampu4 = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/lampujalan4.obj";
        string trashcan1 = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/trashcan1.obj";
        string trashcan2 = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/trashcan2.obj";

        //texture

        string black = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/black.jpg";
        string desertPathjpg = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/DesertRoadDiffuse.jpg";
        string awanjpg = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/Clouds.jpg";
        string wood = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/wood-grain-texture.jpg";
        string grass = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/green-grass-texture.jpg";
        string metal = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/scratched-metal-texture.jpg";

        //texture  
        string spec1 = "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Resources/Maps/putih.png";

        private Vector2 _lastMousePosition;
        private bool _firstMove;




        public Windows(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        private Matrix4 generateArbRotationMatrix(Vector3 axis, Vector3 center, float degree)
        {
            var rads = MathHelper.DegreesToRadians(degree);

            var secretFormula = new float[4, 4] {
                { (float)Math.Cos(rads) + (float)Math.Pow(axis.X, 2) * (1 - (float)Math.Cos(rads)), axis.X* axis.Y * (1 - (float)Math.Cos(rads)) - axis.Z * (float)Math.Sin(rads),    axis.X * axis.Z * (1 - (float)Math.Cos(rads)) + axis.Y * (float)Math.Sin(rads),   0 },
                { axis.Y * axis.X * (1 - (float)Math.Cos(rads)) + axis.Z * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Y, 2) * (1 - (float)Math.Cos(rads)), axis.Y * axis.Z * (1 - (float)Math.Cos(rads)) - axis.X * (float)Math.Sin(rads),   0 },
                { axis.Z * axis.X * (1 - (float)Math.Cos(rads)) - axis.Y * (float)Math.Sin(rads),   axis.Z * axis.Y * (1 - (float)Math.Cos(rads)) + axis.X * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Z, 2) * (1 - (float)Math.Cos(rads)), 0 },
                { 0, 0, 0, 1}
            };
            var secretFormulaMatrix = new Matrix4(
                new Vector4(secretFormula[0, 0], secretFormula[0, 1], secretFormula[0, 2], secretFormula[0, 3]),
                new Vector4(secretFormula[1, 0], secretFormula[1, 1], secretFormula[1, 2], secretFormula[1, 3]),
                new Vector4(secretFormula[2, 0], secretFormula[2, 1], secretFormula[2, 2], secretFormula[2, 3]),
                new Vector4(secretFormula[3, 0], secretFormula[3, 1], secretFormula[3, 2], secretFormula[3, 3])
            );

            return secretFormulaMatrix;
        }

        protected override void OnLoad()
        {
            GL.ClearColor(0.2f, 0.3f, 0.7f, 1f);
            GL.Enable(EnableCap.DepthTest);
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
            _lightingShader = new Shader("C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Shaders/shader.vert",
               "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Shaders/lighting.frag");

            _lampShader = new Shader("C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Shaders/shader.vert",
               "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Shaders/shader_white.frag");
            _vaoModel = GL.GenVertexArray();
            GL.BindVertexArray(_vaoModel);

            var vertexLocation = _lightingShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            var normalLocation = _lightingShader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            var texCoordLocation = _lightingShader.GetAttribLocation("aTexCoords");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            _vaoLamp = GL.GenVertexArray();
            GL.BindVertexArray(_vaoLamp);
            vertexLocation = _lampShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);


            var _cameraPosInit = new Vector3(0f, 0.5f, 0);
            _camera = new Camera(_cameraPosInit, Size.X / (float)Size.Y);
            _camera.Yaw -= 90f;
            CursorGrabbed = true;



            // NGERAKIT OBJ


            pavementkiri.setupObject(jalan, black, spec1, (float)Size.X, (float)Size.Y);
            //landkiri.setupObject(path15, grass, spec1, (float)Size.X, (float)Size.Y);
            //Kandang.setupObject(kandang, wood, spec1, (float)Size.X, (float)Size.Y);
            rumahBambu.setupObject(bambu, wood, spec1, (float)Size.X, (float)Size.Y);
            //seesaw.setupObject(jungkat, wood, spec1 , (float)Size.X, (float)Size.Y);
            kursi.setupObject(bench, wood, spec1, (float)Size.X, (float)Size.Y);
            //orang.setupObject(manusia,text1,spec1, (float)Size.X, (float)Size.Y);
            //cabinet.setupObject(path21, text1, spec1, (float)Size.X, (float)Size.Y);
            mobil.setupObject(ngengeng, metal, spec1, (float)Size.X, (float)Size.Y);
            lampuJalan1.setupObject(lampu1, metal, spec1, (float)Size.X, (float)Size.Y);
            lampuJalan2.setupObject(lampu2, metal, spec1, (float)Size.X, (float)Size.Y);
            lampuJalan3.setupObject(lampu3, metal, spec1, (float)Size.X, (float)Size.Y);
            lampuJalan4.setupObject(lampu4, metal, spec1, (float)Size.X, (float)Size.Y);
            tongSampah1.setupObject(trashcan1, metal, spec1, (float)Size.X, (float)Size.Y);
            tongSampah2.setupObject(trashcan2, metal, spec1, (float)Size.X, (float)Size.Y);


            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //--------------------------------------- LOAD OBJ PATH --------------------------//

            pavementkiri.render(1, _camera, 1);
            landkiri.render(1, _camera, 2);
            //Kandang.render(1, _camera, 2);
            // cabinet.render(1, _camera, 1); 
            rumahBambu.render(1, _camera, 2);
            //orang.render(1, _camera, 1);
            //seesaw.render(1, _camera, 1);
            kursi.render(1, _camera, 2);
            mobil.render(1, _camera, 2);
            lampuJalan1.render(1, _camera, 2);
            lampuJalan2.render(1, _camera, 2);
            lampuJalan3.render(1, _camera, 2);
            lampuJalan4.render(1, _camera, 2);
            tongSampah1.render(2, _camera, 2);
            tongSampah2.render(2, _camera, 2);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);


            GL.BindVertexArray(_vaoModel);
            Matrix4 lampMatrix = Matrix4.CreateScale(0.25f);
            lampMatrix = lampMatrix * Matrix4.CreateTranslation(_lightPos);

            _lampShader.SetMatrix4("transform", lampMatrix);
            _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());


            _lampShader.Use();
            GL.BindVertexArray(_vaoLamp);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            SwapBuffers();

            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            const float cameraSpeed = 0.5f;
            // Escape
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            // Zoom in
            if (KeyboardState.IsKeyDown(Keys.I))
            {
                _camera.Fov -= 0.05f;
            }
            // Zoom out
            if (KeyboardState.IsKeyDown(Keys.O))
            {
                _camera.Fov += 0.05f;
            }

            // Rotasi X di pivot Camera
            // Lihat ke atas (T)
            if (KeyboardState.IsKeyDown(Keys.T))
            {
                _camera.Pitch += 0.05f;
            }
            // Lihat ke bawah (G)
            if (KeyboardState.IsKeyDown(Keys.G))
            {
                _camera.Pitch -= 0.05f;
            }
            // Rotasi Y di pivot Camera
            // Lihat ke kiri (F)
            if (KeyboardState.IsKeyDown(Keys.F))
            {
                _camera.Yaw -= 0.05f;
            }
            // Lihat ke kanan (H)
            if (KeyboardState.IsKeyDown(Keys.H))
            {
                _camera.Yaw += 0.05f;
            }

            // Maju (W)
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;
            }
            // Mundur (S)
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time;
            }
            // Kiri (A)
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;
            }
            // Kanan (D)
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;
            }
            // Naik (Spasi)
            if (KeyboardState.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)args.Time;
            }
            // Turun (Ctrl)
            if (KeyboardState.IsKeyDown(Keys.LeftControl))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)args.Time;
            }

            if (KeyboardState.IsKeyDown(Keys.Backslash))
            {
                mobil.translate(0.0f, 0.0f, -cameraSpeed * (float)args.Time);
            }

            if (KeyboardState.IsKeyDown(Keys.RightBracket))
            {
                mobil.translate(0.0f, 0.0f, cameraSpeed * (float)args.Time);
            }
            if (KeyboardState.IsKeyDown(Keys.Left))
            {
                mobil.translate(0.0f, 0.0f, cameraSpeed * (float)args.Time);
                _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;

            }
            if (KeyboardState.IsKeyDown(Keys.Right))
            {
                mobil.translate(0.0f, 0.0f, -cameraSpeed * (float)args.Time);
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;

            }
            if (KeyboardState.IsKeyDown(Keys.Up))
            {
                mobil.translate(-cameraSpeed * (float)args.Time, 0.0f, 0.0f);
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;

            }
            if (KeyboardState.IsKeyDown(Keys.Down))
            {
                mobil.translate(cameraSpeed * (float)args.Time, 0.0f, 0.0f);
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;

            }



            const float _rotationSpeed = 0.02f;
            // K (atas -> Rotasi sumbu x)   
            if (KeyboardState.IsKeyDown(Keys.K))
            {
                _objectPos *= 2;
                var axis = new Vector3(1, 0, 0);
                _camera.Position -= _objectPos;
                _camera.Pitch -= _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, _rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                _objectPos /= 2;
            }
            // M (bawah -> Rotasi sumbu x)
            if (KeyboardState.IsKeyDown(Keys.M))
            {
                _objectPos *= 2;
                var axis = new Vector3(1, 0, 0);
                _camera.Position -= _objectPos;
                _camera.Pitch += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                _objectPos /= 2;
            }

            // N (kiri -> Rotasi sumbu y)
            if (KeyboardState.IsKeyDown(Keys.N))
            {
                _objectPos *= 2;
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _objectPos;
                _camera.Yaw += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, _rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                _objectPos /= 2;
            }
            // , (kanan -> Rotasi sumbu y)
            if (KeyboardState.IsKeyDown(Keys.Comma))
            {
                _objectPos *= 2;
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _objectPos;
                _camera.Yaw -= _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                _objectPos /= 2;
            }

            // J (putar -> Rotasi sumbu z)
            if (KeyboardState.IsKeyDown(Keys.J))
            {
                _objectPos *= 2;
                var axis = new Vector3(0, 0, 1);
                _camera.Position -= _objectPos;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, _rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                _objectPos /= 2;
            }
            // L (putar -> Rotasi sumbu z)
            if (KeyboardState.IsKeyDown(Keys.L))
            {
                _objectPos *= 2;
                var axis = new Vector3(0, 0, 1);
                _camera.Position -= _objectPos;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                _objectPos /= 2;
            }

            if (!IsFocused)
            {
                return;
            }

            const float sensitivity = 0.02f;
            if (_firstMove)
            {
                _lastMousePosition = new Vector2(MouseState.X, MouseState.Y);
                _firstMove = false;
            }
            else
            {
                // Hitung selisih mouse position
                var deltaX = MouseState.X - _lastMousePosition.X;
                var deltaY = MouseState.Y - _lastMousePosition.Y;
                _lastMousePosition = new Vector2(MouseState.X, MouseState.Y);

                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }

            base.OnUpdateFrame(args);
        }
    }

}
