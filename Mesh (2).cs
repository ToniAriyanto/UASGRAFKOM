using LearnOpenTK.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace camera
{

    class Mesh
    {

        protected List<Vector3> vertices = new List<Vector3>();
        protected List<Vector2> texture = new List<Vector2>();
        protected List<Vector3> normals = new List<Vector3>();
        protected List<Vector3> vertices_res = new List<Vector3>();
        protected List<uint> vertexIndices = new List<uint>();
        protected List<uint> textureIndices = new List<uint>();
        protected List<uint> normalIndices = new List<uint>();
        protected bool _lightSource = false;


        protected int _VBO;
        protected int _VAO;
        protected int _EBO;
        protected int _NBO;
        protected int _TBO;

        protected Shader _shader;
        protected Matrix4 _transform;
        protected Matrix4 _transform_tmp;

        //lighting
        //private readonly Vector3 _lightPos = new Vector3(0f, 0f, 0f);
        //private int _vaoLamp;
        //private Shader _lampShader;
        private Shader _lightingShader;
        private Texture _diffuseMap;
        private Texture _specularMap;

        protected Matrix4 _projection;
        protected Matrix4 _view;



        public Vector3 scaleratio = new Vector3(0.1f);

        List<float> vertex = new List<float>();


        public List<Mesh> child = new List<Mesh>();
        public Mesh(bool lightSource = false)
        {
            _lightSource = lightSource;
        }

        private List<Vector3> GenerateCircleVertices(float radius, int segments)
        {
            List<Vector3> vertices = new List<Vector3>();

            float angleIncrement = (2 * MathHelper.Pi) / segments;
            for (int i = 0; i < segments; i++)
            {
                float angle = i * angleIncrement;
                float x = radius * (float)Math.Cos(angle);
                float y = radius * (float)Math.Sin(angle);
                vertices.Add(new Vector3(x, y, 0f));
            }

            return vertices;
        }


        public void setupObject(string p1, string diffuse, string specular, float Sizex, float Sizey)
        {
            _transform = Matrix4.Identity;
            LoadObjFile(p1);
            for (int i = 0; i < vertexIndices.Count; i++)
            {
                int index = (int)vertexIndices[i];

                vertex.Add(vertices[index].X);
                vertex.Add(vertices[index].Y);
                vertex.Add(vertices[index].Z);

                index = (int)normalIndices[i];

                vertex.Add(normals[index].X);
                vertex.Add(normals[index].Y);
                vertex.Add(normals[index].Z);


                index = (int)textureIndices[i];

                vertex.Add(texture[index].X);
                vertex.Add(texture[index].Y);
            }

            _VAO = GL.GenVertexArray();
            GL.BindVertexArray(_VAO);

            _VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertex.Count * sizeof(float), vertex.ToArray(), BufferUsageHint.StaticDraw);
            _lightingShader = new Shader("C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Shaders/shader.vert",
                             "C:/Users/tonia/Downloads/Camera_revisi-20230621T085641Z-001/Camera_revisi/Camera/Shaders/lighting.frag");
            //_lampShader = new Shader("C:/Users/kupan/source/repos/Camera/Camera/Shaders/shader.vert",
            //                 "C:/Users/kupan/source/repos/Camera/Camera/Shaders/shader.frag");

            var vertexLocation = _lightingShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            var normalLocation = _lightingShader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            var texCoordLocation = _lightingShader.GetAttribLocation("aTexCoords");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));



            _diffuseMap = Texture.LoadFromFile(diffuse);
            _specularMap = Texture.LoadFromFile(specular);
            _transform *= Matrix4.CreateScale(scaleratio);


        }

       
        public void render(int mode, Camera _camera,int dir)
        {
            //lighting
            GL.BindVertexArray(_VAO);

            _diffuseMap.Use(TextureUnit.Texture0);
            _specularMap.Use(TextureUnit.Texture1);

            if (dir==1)
            {
                _lightingShader.SetMatrix4("transform", _transform);
                _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
                _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
                _lightingShader.SetVector3("viewPos", _camera.Position);
                _lightingShader.SetInt("material.diffuse", 0);
                _lightingShader.SetInt("material.specular", 1);
                _lightingShader.SetFloat("material.shininess", 64.0f);
                _lightingShader.SetVector3("light.direction", new Vector3(-0.1f, 0.7f, -2.2f));
                _lightingShader.SetVector3("light.ambient", new Vector3(0.7f));
                _lightingShader.SetVector3("light.diffuse", new Vector3(0.5f));
                _lightingShader.SetVector3("light.specular", new Vector3(0.5f, 0.5f, 0.5f));
                _lightingShader.Use();

            }
            else
            {
                _lightingShader.SetMatrix4("transform", _transform);
                _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
                _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
                _lightingShader.SetVector3("viewPos", _camera.Position);
                _lightingShader.SetInt("material.diffuse", 0);
                _lightingShader.SetInt("material.specular", 1);
                _lightingShader.SetFloat("material.shininess", 64.0f);
                _lightingShader.SetVector3("light.direction", new Vector3(0.1f, -0.7f, 2.2f));
                _lightingShader.SetVector3("light.ambient", new Vector3(0.7f));
                _lightingShader.SetVector3("light.diffuse", new Vector3(0.5f));
                _lightingShader.SetVector3("light.specular", new Vector3(0.5f, 0.5f, 0.5f));
                _lightingShader.Use();
            }
            

            switch (mode)
            {
                case 1:
                    GL.DrawArrays(PrimitiveType.Triangles, 0, vertex.Count / 8);
                    break;
                case 2:
                    GL.DrawArrays(PrimitiveType.TriangleFan, 0, vertex.Count / 8);
                    break;
                case 3:
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, vertex.Count / 8);
                    break;
            }
        }

      

        public void save()
        {
            _transform_tmp = _transform;
        }
        public void reset()
        {
            _transform = _transform_tmp;
        }

        public void rotate(float dr)
        {
            _transform = _transform * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(dr));

        }
        public void scale(float r)
        {
            _transform = _transform * Matrix4.CreateScale(r);
        }

        public void translate(float dx, float dy, float dz)
        {
            _transform = _transform * Matrix4.CreateTranslation(dx, dy, dz) ;

        }

        public Vector3 getPos()
        {
            return _transform.ExtractTranslation();
        }



        public void LoadObjFile(string path)
        {

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to open \"" + path + "\", does not exist.");
            }

            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    List<string> words = new List<string>(streamReader.ReadLine().ToLower().Split(' '));
                    words.RemoveAll(s => s == string.Empty);
                    if (words.Count == 0)
                        continue;
                    string type = words[0];
                    words.RemoveAt(0);


                    switch (type)
                    {
                        case "v":
                            vertices.Add(new Vector3(float.Parse(words[0]) / 10, float.Parse(words[1]) / 10, float.Parse(words[2]) / 10));
                            break;

                        case "vt":
                            texture.Add(new Vector2(float.Parse(words[0]), float.Parse(words[1])));
                            break;

                        case "vn":
                            normals.Add(new Vector3(float.Parse(words[0]), float.Parse(words[1]), float.Parse(words[2])));
                            break;
                        // face
                        case "f":
                            foreach (string w in words)
                            {
                                if (w.Length == 0)
                                    continue;

                                string[] comps = w.Split('/');

                                // subtract 1: indices start from 1, not 0
                                vertexIndices.Add(uint.Parse(comps[0]) - 1);
                                textureIndices.Add(uint.Parse(comps[1]) - 1);
                                normalIndices.Add(uint.Parse(comps[2]) - 1);
                            }
                            break;

                        default:
                            break;
                    }
                }

            }
        }
    }
}