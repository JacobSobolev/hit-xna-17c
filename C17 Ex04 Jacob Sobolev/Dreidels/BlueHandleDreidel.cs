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
    public class BlueHandleDreidel : Dreidel
    {
        public BlueHandleDreidel(Game i_Game)
            : base(i_Game)
        { }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Texture = Game.Content.Load<Texture2D>(m_AssetName);
            m_Vertices = new VertexPositionTexture[58];
            m_Vertices[0] = new VertexPositionTexture(new Vector3(-4, 0, 4), new Vector2(0, 1));
            m_Vertices[1] = new VertexPositionTexture(new Vector3(-4, 8, 4), new Vector2(0, 0));
            m_Vertices[2] = new VertexPositionTexture(new Vector3(4, 0, 4), new Vector2(k_Quarter, 1));
            m_Vertices[3] = new VertexPositionTexture(new Vector3(4, 8, 4), new Vector2(k_Quarter, 0));
            m_Vertices[4] = new VertexPositionTexture(new Vector3(4, 0, -4), new Vector2(2 * k_Quarter, 1));
            m_Vertices[5] = new VertexPositionTexture(new Vector3(4, 8, -4), new Vector2(2 * k_Quarter, 0));
            m_Vertices[6] = new VertexPositionTexture(new Vector3(-4, 0, -4), new Vector2(3 * k_Quarter, 1));
            m_Vertices[7] = new VertexPositionTexture(new Vector3(-4, 8, -4), new Vector2(3 * k_Quarter, 0));
            m_Vertices[8] = new VertexPositionTexture(new Vector3(-4, 0, 4), new Vector2(1, 1));
            m_Vertices[9] = new VertexPositionTexture(new Vector3(-4, 8, 4), new Vector2(1, 0));
            m_Vertices[10] = new VertexPositionTexture(new Vector3(-4, 0, 4), new Vector2(0, 1));
            m_Vertices[11] = new VertexPositionTexture(new Vector3(4, 0, 4), new Vector2(k_Quarter, 1));
            m_Vertices[12] = new VertexPositionTexture(new Vector3(0, -4, 0), new Vector2(2 * k_Quarter, 2));
            m_Vertices[13] = new VertexPositionTexture(new Vector3(4, 0, 4), new Vector2(k_Quarter, 1));
            m_Vertices[14] = new VertexPositionTexture(new Vector3(4, 0, -4), new Vector2(2 * k_Quarter, 1));
            m_Vertices[15] = new VertexPositionTexture(new Vector3(0, -4, 0), new Vector2(2 * k_Quarter, 2));
            m_Vertices[16] = new VertexPositionTexture(new Vector3(4, 0, -4), new Vector2(2 * k_Quarter, 1));
            m_Vertices[17] = new VertexPositionTexture(new Vector3(-4, 0, -4), new Vector2(3 * k_Quarter, 1));
            m_Vertices[18] = new VertexPositionTexture(new Vector3(0, -4, 0), new Vector2(2 * k_Quarter, 2));
            m_Vertices[19] = new VertexPositionTexture(new Vector3(-4, 0, -4), new Vector2(3 * k_Quarter, 1));
            m_Vertices[20] = new VertexPositionTexture(new Vector3(-4, 0, 4), new Vector2(1, 1));
            m_Vertices[21] = new VertexPositionTexture(new Vector3(0, -4, 0), new Vector2(2 * k_Quarter, 2));
            m_Vertices[22] = new VertexPositionTexture(new Vector3(-4, 8, -4), new Vector2(2 * k_Quarter, -2));
            m_Vertices[23] = new VertexPositionTexture(new Vector3(4, 8, -4), new Vector2(2 * k_Quarter, 0));
            m_Vertices[24] = new VertexPositionTexture(new Vector3(4, 8, 4), new Vector2(k_Quarter, 0));
            m_Vertices[25] = new VertexPositionTexture(new Vector3(-4, 8, 4), new Vector2(0, 0));
            m_Vertices[26] = new VertexPositionTexture(new Vector3(-4, 8, -4), new Vector2(2 * k_Quarter, -2));
            m_Vertices[27] = new VertexPositionTexture(new Vector3(4, 8, 4), new Vector2(k_Quarter, 0));
            m_Vertices[28] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 8, 2 * k_Quarter), new Vector2(3 * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[29] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 10, 2 * k_Quarter), new Vector2(3 * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[30] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 8, 2 * k_Quarter), new Vector2(3 * k_Quarter + k_Quarter * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[31] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 10, 2 * k_Quarter), new Vector2(3 * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[32] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 10, 2 * k_Quarter), new Vector2(3 * k_Quarter + k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[33] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 8, 2 * k_Quarter), new Vector2(3 * k_Quarter + k_Quarter * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[34] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 8, 2 * k_Quarter), new Vector2(3 * k_Quarter + k_Quarter * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[35] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 10, 2 * k_Quarter), new Vector2(3 * k_Quarter + k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[36] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 10, -2 * k_Quarter), new Vector2(3 * k_Quarter + 2 * k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[37] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 10, -2 * k_Quarter), new Vector2(3 * k_Quarter + 2 * k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[38] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 8, -2 * k_Quarter), new Vector2(3 * k_Quarter + 2 * k_Quarter * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[39] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 8, 2 * k_Quarter), new Vector2(3 * k_Quarter + k_Quarter * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[40] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 10, -2 * k_Quarter), new Vector2(3 * k_Quarter + 2 * k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[41] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 10, -2 * k_Quarter), new Vector2(3 * k_Quarter + 3 * k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[42] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 8, -2 * k_Quarter), new Vector2(3 * k_Quarter + 2 * k_Quarter * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[43] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 8, -2 * k_Quarter), new Vector2(3 * k_Quarter + 3 * k_Quarter * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[44] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 8, -2 * k_Quarter), new Vector2(3 * k_Quarter + 2 * k_Quarter * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[45] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 10, -2 * k_Quarter), new Vector2(3 * k_Quarter + 3 * k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[46] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 10, -2 * k_Quarter), new Vector2(3 * k_Quarter + 3 * k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[47] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 8, 2 * k_Quarter), new Vector2(3 * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[48] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 8, -2 * k_Quarter), new Vector2(3 * k_Quarter + 3 * k_Quarter * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[49] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 10, -2 * k_Quarter), new Vector2(3 * k_Quarter + 3 * k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[50] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 10, 2 * k_Quarter), new Vector2(3 * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[51] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 8, 2 * k_Quarter), new Vector2(3 * k_Quarter, 2 * k_Quarter * k_Quarter));
            m_Vertices[52] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 10, -2 * k_Quarter), new Vector2(3 * k_Quarter + 3 * k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[53] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 10, 2 * k_Quarter), new Vector2(3 * k_Quarter + k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[54] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 10, 2 * k_Quarter), new Vector2(3 * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[55] = new VertexPositionTexture(new Vector3(-2 * k_Quarter, 10, -2 * k_Quarter), new Vector2(3 * k_Quarter + 3 * k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[56] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 10, -2 * k_Quarter), new Vector2(3 * k_Quarter + 2 * k_Quarter * k_Quarter, k_Quarter * k_Quarter));
            m_Vertices[57] = new VertexPositionTexture(new Vector3(2 * k_Quarter, 10, 2 * k_Quarter), new Vector2(3 * k_Quarter + k_Quarter * k_Quarter, k_Quarter * k_Quarter));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            m_BasicEffect.Texture = m_Texture;
            m_BasicEffect.VertexColorEnabled = false;
            m_BasicEffect.TextureEnabled = true;
            foreach (EffectPass pass in m_BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                m_BasicEffect.GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, m_Vertices, 0, 8);
                m_BasicEffect.GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, m_Vertices, 10, 16);
            }
        }
    }
}
