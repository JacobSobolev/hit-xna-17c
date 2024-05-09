using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Infrastructure.ServiceInterfaces;
using Infrastructure.Managers;
using Infrastructure.ObjectModel;

namespace Dreidels
{
    public class RedHandleDreidel : Dreidel
    {
        private VertexBuffer m_VertexBuffer;
        private IndexBuffer m_IndexBuffer;
        private short[] m_Indices;

        public RedHandleDreidel(Game i_Game)
            : base(i_Game)
        { }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Texture = Game.Content.Load<Texture2D>(m_AssetName);
            m_Vertices = new VertexPositionTexture[20];
            m_Vertices[0] = new VertexPositionTexture(new Vector3(-4, 0, 4), new Vector2(0, 1));
            m_Vertices[1] = new VertexPositionTexture(new Vector3(-4, 8, 4), new Vector2(0, 0));
            m_Vertices[2] = new VertexPositionTexture(new Vector3(4, 0, 4), new Vector2(k_Quarter, 1));
            m_Vertices[3] = new VertexPositionTexture(new Vector3(4, 8, 4), new Vector2(k_Quarter, 0));
            m_Vertices[4] = new VertexPositionTexture(new Vector3(4, 8, -4), new Vector2(2 * k_Quarter, 0));
            m_Vertices[5] = new VertexPositionTexture(new Vector3(4, 0, -4), new Vector2(2 * k_Quarter, 1));
            m_Vertices[6] = new VertexPositionTexture(new Vector3(-4, 0, -4), new Vector2(3 * k_Quarter, 1));
            m_Vertices[7] = new VertexPositionTexture(new Vector3(-4, 8, -4), new Vector2(3 * k_Quarter, 0));
            m_Vertices[8] = new VertexPositionTexture(new Vector3(-4, 0, 4), new Vector2(1, 1));
            m_Vertices[9] = new VertexPositionTexture(new Vector3(-4, 8, 4), new Vector2(1, 0));
            m_Vertices[10] = new VertexPositionTexture(new Vector3(0, -4, 0), new Vector2(2 * k_Quarter, 2));
            m_Vertices[11] = new VertexPositionTexture(new Vector3(-4, 8, -4), new Vector2(2 * k_Quarter, -2));
            m_Vertices[12] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 10, 2 * k_Quarter), new Vector2(2 * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[13] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 8, 2 * k_Quarter), new Vector2(2 * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[14] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 8, 2 * k_Quarter), new Vector2(2 * k_Quarter + k_Quarter * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[15] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 10, 2 * k_Quarter), new Vector2(2 * k_Quarter + k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[16] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 10, -2 * k_Quarter), new Vector2(2 * k_Quarter + 2 * k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[17] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 8, -2 * k_Quarter), new Vector2(2 * k_Quarter + 2 * k_Quarter * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[18] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 10, -2 * k_Quarter), new Vector2(2 * k_Quarter + 3 * k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[19] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 8, -2 * k_Quarter), new Vector2(2 * k_Quarter + 3 * k_Quarter * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Indices = new short[72];

            m_VertexBuffer = new VertexBuffer(
                this.GraphicsDevice,
                typeof(VertexPositionTexture),
                m_Vertices.Length,
                BufferUsage.WriteOnly);

            m_VertexBuffer.SetData(m_Vertices, 0, m_Vertices.Length);
            ////middle
            m_Indices[0] = 0;
            m_Indices[1] = 1;
            m_Indices[2] = 2;
            m_Indices[3] = 1;
            m_Indices[4] = 3;
            m_Indices[5] = 2;
            m_Indices[6] = 3;
            m_Indices[7] = 4;
            m_Indices[8] = 2;
            m_Indices[9] = 2;
            m_Indices[10] = 4;
            m_Indices[11] = 5;
            m_Indices[12] = 5;
            m_Indices[13] = 4;
            m_Indices[14] = 6;
            m_Indices[15] = 6;
            m_Indices[16] = 4;
            m_Indices[17] = 7;
            m_Indices[18] = 7;
            m_Indices[19] = 9;
            m_Indices[20] = 6;
            m_Indices[21] = 6;
            m_Indices[22] = 9;
            m_Indices[23] = 8;
            ////bottom
            m_Indices[24] = 0;
            m_Indices[25] = 2;
            m_Indices[26] = 10;
            m_Indices[27] = 2;
            m_Indices[28] = 5;
            m_Indices[29] = 10;
            m_Indices[30] = 5;
            m_Indices[31] = 6;
            m_Indices[32] = 10;
            m_Indices[33] = 6;
            m_Indices[34] = 8;
            m_Indices[35] = 10;
            m_Indices[36] = 11;
            m_Indices[37] = 4;
            m_Indices[38] = 3;
            m_Indices[39] = 1;
            m_Indices[40] = 11;
            m_Indices[41] = 3;
            ////handle
            m_Indices[42] = 13;
            m_Indices[43] = 12;
            m_Indices[44] = 14;
            m_Indices[45] = 12;
            m_Indices[46] = 15;
            m_Indices[47] = 14;
            m_Indices[48] = 14;
            m_Indices[49] = 15;
            m_Indices[50] = 16;
            m_Indices[51] = 16;
            m_Indices[52] = 17;
            m_Indices[53] = 14;
            m_Indices[54] = 16;
            m_Indices[55] = 18;
            m_Indices[56] = 17;
            m_Indices[57] = 19;
            m_Indices[58] = 17;
            m_Indices[59] = 18;
            m_Indices[60] = 18;
            m_Indices[61] = 13;
            m_Indices[62] = 19;
            m_Indices[63] = 18;
            m_Indices[64] = 12;
            m_Indices[65] = 13;
            m_Indices[66] = 18;
            m_Indices[67] = 15;
            m_Indices[68] = 12;
            m_Indices[69] = 18;
            m_Indices[70] = 16;
            m_Indices[71] = 15;
            m_IndexBuffer = new IndexBuffer(this.GraphicsDevice, typeof(short), m_Indices.Length, BufferUsage.WriteOnly);
            m_IndexBuffer.SetData(m_Indices);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            m_BasicEffect.Texture = m_Texture;
            m_BasicEffect.VertexColorEnabled = false;
            m_BasicEffect.TextureEnabled = true;
            m_BasicEffect.GraphicsDevice.Indices = m_IndexBuffer;
            m_BasicEffect.GraphicsDevice.SetVertexBuffer(m_VertexBuffer);
            foreach (EffectPass pass in m_BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                m_BasicEffect.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, m_VertexBuffer.VertexCount, 0, 24);
            }
        }
    }
}
