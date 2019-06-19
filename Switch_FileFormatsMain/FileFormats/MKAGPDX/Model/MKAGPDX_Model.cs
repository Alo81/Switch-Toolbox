﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switch_Toolbox;
using System.Windows.Forms;
using Switch_Toolbox.Library;
using Switch_Toolbox.Library.IO;
using OpenTK;

namespace FirstPlugin
{
    public class MKAGPDX_Model : IFileFormat
    {
        public FileType FileType { get; set; } = FileType.Model;

        public bool CanSave { get; set; }
        public string[] Description { get; set; } = new string[] { "Mario Kart Arcade GP DX" };
        public string[] Extension { get; set; } = new string[] { "*.bin" };
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public IFileInfo IFileInfo { get; set; }

        public bool Identify(System.IO.Stream stream)
        {
            using (var reader = new Switch_Toolbox.Library.IO.FileReader(stream, true))
            {
                return reader.CheckSignature(4, "BIKE");
            }
        }

        public Type[] Types
        {
            get
            {
                List<Type> types = new List<Type>();
                return types.ToArray();
            }
        }

        public void Load(System.IO.Stream stream)
        {

        }
        public void Unload()
        {

        }
        public byte[] Save()
        {
            return null;
        }

        public class Header
        {
            public uint Version { get; set; }
            public uint Alignment { get; set; }
            public uint HeaderSize { get; set; }

            public void Read(FileReader reader)
            {
                reader.ReadSignature(4, "BIKE");
                Version = reader.ReadUInt32();
                Alignment = reader.ReadUInt32();
                uint Padding = reader.ReadUInt32();
                uint MaterialCount = reader.ReadUInt32();
                HeaderSize = reader.ReadUInt32();
                uint TextureMapsCount = reader.ReadUInt32();
                uint TextureMapsOffset = reader.ReadUInt32();
                uint UpperLevelNodeCount = reader.ReadUInt32();
                uint UpperLevelNodeOffset = reader.ReadUInt32();
                uint MiddleLevelNodeCount = reader.ReadUInt32();
                uint MiddleLevelNodeOffset = reader.ReadUInt32();
                uint LowerLevelNodeCount = reader.ReadUInt32();
                uint LowerLevelNodeOffset = reader.ReadUInt32();
                uint Padding2 = reader.ReadUInt32();
                uint[] Unknowns = reader.ReadUInt32s(10);

            }
        }

        public class Material
        {
            public Vector4 Ambient;
            public Vector4 Diffuse;
            public Vector4 Specular;
            public float Shiny;
            public Vector4 Transparency;
            public float TransGlossy;
            public float TransparencySamples;
            public Vector4 Reflectivity;
            public float ReflectGlossy;
            public float ReflectSample;
            public float IndexRefreaction;
            public float Translucency;
            public float Unknown;
            public ushort[] TextureIndices;
            public uint[] Unknowns;

            public Material()
            {
                Ambient = new Vector4(0.3f, 0.3f, 0.3f,1.0f);
                Diffuse = new Vector4(0.7f, 0.7f, 0.7f, 1.0f);
                Specular = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                Shiny = 50;
                Transparency = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
                TextureIndices = new ushort[10];
            }

            public void Read(FileReader reader)
            {
                Ambient = reader.ReadVec4();
                Diffuse = reader.ReadVec4();
                Specular = reader.ReadVec4();
                Shiny = reader.ReadSingle();
                Transparency = reader.ReadVec4();
                TransGlossy = reader.ReadSingle();
                TransparencySamples = reader.ReadSingle();
                Reflectivity = reader.ReadVec4();
                ReflectGlossy = reader.ReadSingle();
                ReflectSample = reader.ReadSingle();
                IndexRefreaction = reader.ReadSingle();
                Translucency = reader.ReadSingle();
                Unknown = reader.ReadSingle();
                TextureIndices = reader.ReadInt16s(10);
                Unknowns = reader.ReadUInt32s(10);
            }
        }
    }
}
