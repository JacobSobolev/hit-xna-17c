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
    public class YellowHandleDreidel : Dreidel
    {
        private new VertexPositionColor[] m_Vertices;

        public YellowHandleDreidel(Game i_Game)
            : base(i_Game)
        { }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Vertices = new VertexPositionColor[168];
            m_Vertices[0] = new VertexPositionColor(new Vector3(-4, 0, 4), Color.Yellow);
            m_Vertices[1] = new VertexPositionColor(new Vector3(-4, 8, 4), Color.Yellow);
            m_Vertices[2] = new VertexPositionColor(new Vector3(4, 0, 4), Color.Yellow);
            m_Vertices[3] = new VertexPositionColor(new Vector3(4, 8, 4), Color.Yellow);
            m_Vertices[4] = new VertexPositionColor(new Vector3(4, 0, 4), Color.Yellow);
            m_Vertices[5] = new VertexPositionColor(new Vector3(-4, 8, 4), Color.Yellow);
            m_Vertices[6] = new VertexPositionColor(new Vector3(4, 0, 4), Color.LawnGreen);
            m_Vertices[7] = new VertexPositionColor(new Vector3(4, 8, 4), Color.LawnGreen);
            m_Vertices[8] = new VertexPositionColor(new Vector3(4, 0, -4), Color.LawnGreen);
            m_Vertices[9] = new VertexPositionColor(new Vector3(4, 8, -4), Color.LawnGreen);
            m_Vertices[10] = new VertexPositionColor(new Vector3(4, 0, -4), Color.LawnGreen);
            m_Vertices[11] = new VertexPositionColor(new Vector3(4, 8, 4), Color.LawnGreen);
            m_Vertices[12] = new VertexPositionColor(new Vector3(4, 0, -4), Color.Red);
            m_Vertices[13] = new VertexPositionColor(new Vector3(4, 8, -4), Color.Red);
            m_Vertices[14] = new VertexPositionColor(new Vector3(-4, 0, -4), Color.Red);
            m_Vertices[15] = new VertexPositionColor(new Vector3(-4, 8, -4), Color.Red);
            m_Vertices[16] = new VertexPositionColor(new Vector3(-4, 0, -4), Color.Red);
            m_Vertices[17] = new VertexPositionColor(new Vector3(4, 8, -4), Color.Red);
            m_Vertices[18] = new VertexPositionColor(new Vector3(-4, 0, -4), Color.Blue);
            m_Vertices[19] = new VertexPositionColor(new Vector3(-4, 8, -4), Color.Blue);
            m_Vertices[20] = new VertexPositionColor(new Vector3(-4, 0, 4), Color.Blue);
            m_Vertices[21] = new VertexPositionColor(new Vector3(-4, 8, 4), Color.Blue);
            m_Vertices[22] = new VertexPositionColor(new Vector3(-4, 0, 4), Color.Blue);
            m_Vertices[23] = new VertexPositionColor(new Vector3(-4, 8, -4), Color.Blue);
            m_Vertices[24] = new VertexPositionColor(new Vector3(-4, 0, 4), Color.White);
            m_Vertices[25] = new VertexPositionColor(new Vector3(4, 0, 4), Color.White);
            m_Vertices[26] = new VertexPositionColor(new Vector3(0, -4, 0), Color.White);
            m_Vertices[27] = new VertexPositionColor(new Vector3(4, 0, 4), Color.White);
            m_Vertices[28] = new VertexPositionColor(new Vector3(4, 0, -4), Color.White);
            m_Vertices[29] = new VertexPositionColor(new Vector3(0, -4, 0), Color.White);
            m_Vertices[30] = new VertexPositionColor(new Vector3(4, 0, -4), Color.White);
            m_Vertices[31] = new VertexPositionColor(new Vector3(-4, 0, -4), Color.White);
            m_Vertices[32] = new VertexPositionColor(new Vector3(0, -4, 0), Color.White);
            m_Vertices[33] = new VertexPositionColor(new Vector3(-4, 0, -4), Color.White);
            m_Vertices[34] = new VertexPositionColor(new Vector3(-4, 0, 4), Color.White);
            m_Vertices[35] = new VertexPositionColor(new Vector3(0, -4, 0), Color.White);
            m_Vertices[36] = new VertexPositionColor(new Vector3(-4, 8, -4), Color.White);
            m_Vertices[37] = new VertexPositionColor(new Vector3(4, 8, -4), Color.White);
            m_Vertices[38] = new VertexPositionColor(new Vector3(4, 8, 4), Color.White);
            m_Vertices[39] = new VertexPositionColor(new Vector3(-4, 8, 4), Color.White);
            m_Vertices[40] = new VertexPositionColor(new Vector3(-4, 8, -4), Color.White);
            m_Vertices[41] = new VertexPositionColor(new Vector3(4, 8, 4), Color.White);
            m_Vertices[42] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 8, 2 * k_Quarter), Color.Yellow);
            m_Vertices[43] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 10, 2 * k_Quarter), Color.Yellow);
            m_Vertices[44] = new VertexPositionColor(new Vector3(2 * k_Quarter, 8, 2 * k_Quarter), Color.Yellow);
            m_Vertices[45] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 10, 2 * k_Quarter), Color.Yellow);
            m_Vertices[46] = new VertexPositionColor(new Vector3(2 * k_Quarter, 10, 2 * k_Quarter), Color.Yellow);
            m_Vertices[47] = new VertexPositionColor(new Vector3(2 * k_Quarter, 8, 2 * k_Quarter), Color.Yellow);
            m_Vertices[48] = new VertexPositionColor(new Vector3(2 * k_Quarter, 8, 2 * k_Quarter), Color.Yellow);
            m_Vertices[49] = new VertexPositionColor(new Vector3(2 * k_Quarter, 10, 2 * k_Quarter), Color.Yellow);
            m_Vertices[50] = new VertexPositionColor(new Vector3(2 * k_Quarter, 10, -2 * k_Quarter), Color.Yellow);
            m_Vertices[51] = new VertexPositionColor(new Vector3(2 * k_Quarter, 10, -2 * k_Quarter), Color.Yellow);
            m_Vertices[52] = new VertexPositionColor(new Vector3(2 * k_Quarter, 8, -2 * k_Quarter), Color.Yellow);
            m_Vertices[53] = new VertexPositionColor(new Vector3(2 * k_Quarter, 8, 2 * k_Quarter), Color.Yellow);
            m_Vertices[54] = new VertexPositionColor(new Vector3(2 * k_Quarter, 10, -2 * k_Quarter), Color.Yellow);
            m_Vertices[55] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 10, -2 * k_Quarter), Color.Yellow);
            m_Vertices[56] = new VertexPositionColor(new Vector3(2 * k_Quarter, 8, -2 * k_Quarter), Color.Yellow);
            m_Vertices[57] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 8, -2 * k_Quarter), Color.Yellow);
            m_Vertices[58] = new VertexPositionColor(new Vector3(2 * k_Quarter, 8, -2 * k_Quarter), Color.Yellow);
            m_Vertices[59] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 10, -2 * k_Quarter), Color.Yellow);
            m_Vertices[60] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 10, -2 * k_Quarter), Color.Yellow);
            m_Vertices[61] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 8, 2 * k_Quarter), Color.Yellow);
            m_Vertices[62] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 8, -2 * k_Quarter), Color.Yellow);
            m_Vertices[63] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 10, -2 * k_Quarter), Color.Yellow);
            m_Vertices[64] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 10, 2 * k_Quarter), Color.Yellow);
            m_Vertices[65] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 8, 2 * k_Quarter), Color.Yellow);
            m_Vertices[66] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 10, -2 * k_Quarter), Color.Yellow);
            m_Vertices[67] = new VertexPositionColor(new Vector3(2 * k_Quarter, 10, 2 * k_Quarter), Color.Yellow);
            m_Vertices[68] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 10, 2 * k_Quarter), Color.Yellow);
            m_Vertices[69] = new VertexPositionColor(new Vector3(-2 * k_Quarter, 10, -2 * k_Quarter), Color.Yellow);
            m_Vertices[70] = new VertexPositionColor(new Vector3(2 * k_Quarter, 10, -2 * k_Quarter), Color.Yellow);
            m_Vertices[71] = new VertexPositionColor(new Vector3(2 * k_Quarter, 10, 2 * k_Quarter), Color.Yellow);
            m_Vertices[72] = new VertexPositionColor(new Vector3(0, 6, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[73] = new VertexPositionColor(new Vector3(2, 5, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[74] = new VertexPositionColor(new Vector3(0, 5, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[75] = new VertexPositionColor(new Vector3(0, 6, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[76] = new VertexPositionColor(new Vector3(2, 6, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[77] = new VertexPositionColor(new Vector3(2, 5, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[78] = new VertexPositionColor(new Vector3(1, 5, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[79] = new VertexPositionColor(new Vector3(2, 5, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[80] = new VertexPositionColor(new Vector3(2, 3, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[81] = new VertexPositionColor(new Vector3(1, 5, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[82] = new VertexPositionColor(new Vector3(2, 3, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[83] = new VertexPositionColor(new Vector3(1, 3, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[84] = new VertexPositionColor(new Vector3(-1, 3, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[85] = new VertexPositionColor(new Vector3(2, 3, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[86] = new VertexPositionColor(new Vector3(2, 2, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[87] = new VertexPositionColor(new Vector3(-1, 3, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[88] = new VertexPositionColor(new Vector3(2, 2, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[89] = new VertexPositionColor(new Vector3(-1, 2, 4 + k_Quarter * k_Quarter), Color.Black);
            m_Vertices[90] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 6, 2), Color.Black);
            m_Vertices[91] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 6, -2), Color.Black);
            m_Vertices[92] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 5, -2), Color.Black);
            m_Vertices[93] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 6, 2), Color.Black);
            m_Vertices[94] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 5, -2), Color.Black);
            m_Vertices[95] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 5, 2), Color.Black);
            m_Vertices[96] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 5, -1), Color.Black);
            m_Vertices[97] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 5, -2), Color.Black);
            m_Vertices[98] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 2, -2), Color.Black);
            m_Vertices[99] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 5, -1), Color.Black);
            m_Vertices[100] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 2, -2), Color.Black);
            m_Vertices[101] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 2, -1), Color.Black);
            m_Vertices[102] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 4, 2), Color.Black);
            m_Vertices[103] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 4, -1), Color.Black);
            m_Vertices[104] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 3, -1), Color.Black);
            m_Vertices[105] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 4, 2), Color.Black);
            m_Vertices[106] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 3, -1), Color.Black);
            m_Vertices[107] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 3, 2), Color.Black);
            m_Vertices[108] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 3, 2), Color.Black);
            m_Vertices[109] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 3, 1), Color.Black);
            m_Vertices[110] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 2, 1), Color.Black);
            m_Vertices[111] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 3, 2), Color.Black);
            m_Vertices[112] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 2, 1), Color.Black);
            m_Vertices[113] = new VertexPositionColor(new Vector3(4 + k_Quarter * k_Quarter, 2, 2), Color.Black);
            m_Vertices[114] = new VertexPositionColor(new Vector3(2, 6, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[115] = new VertexPositionColor(new Vector3(-2, 6, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[116] = new VertexPositionColor(new Vector3(-2, 5, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[117] = new VertexPositionColor(new Vector3(2, 6, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[118] = new VertexPositionColor(new Vector3(-2, 5, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[119] = new VertexPositionColor(new Vector3(2, 5, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[120] = new VertexPositionColor(new Vector3(-1, 5, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[121] = new VertexPositionColor(new Vector3(-2, 5, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[122] = new VertexPositionColor(new Vector3(-2, 2, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[123] = new VertexPositionColor(new Vector3(-1, 5, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[124] = new VertexPositionColor(new Vector3(-2, 2, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[125] = new VertexPositionColor(new Vector3(-1, 2, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[126] = new VertexPositionColor(new Vector3(2, 4, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[127] = new VertexPositionColor(new Vector3(0, 4, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[128] = new VertexPositionColor(new Vector3(0, 3, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[129] = new VertexPositionColor(new Vector3(2, 4, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[130] = new VertexPositionColor(new Vector3(0, 3, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[131] = new VertexPositionColor(new Vector3(2, 3, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[132] = new VertexPositionColor(new Vector3(1, 3, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[133] = new VertexPositionColor(new Vector3(0, 3, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[134] = new VertexPositionColor(new Vector3(0, 2, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[135] = new VertexPositionColor(new Vector3(1, 3, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[136] = new VertexPositionColor(new Vector3(0, 2, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[137] = new VertexPositionColor(new Vector3(1, 2, -4 - k_Quarter * k_Quarter), Color.Black);
            m_Vertices[138] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 6, -2), Color.Black);
            m_Vertices[139] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 6, 2), Color.Black);
            m_Vertices[140] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 5, 2), Color.Black);
            m_Vertices[141] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 6, -2), Color.Black);
            m_Vertices[142] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 5, 2), Color.Black);
            m_Vertices[143] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 5, -2), Color.Black);
            m_Vertices[144] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 5, 1), Color.Black);
            m_Vertices[145] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 5, 2), Color.Black);
            m_Vertices[146] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 2, 2), Color.Black);
            m_Vertices[147] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 5, 1), Color.Black);
            m_Vertices[148] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 2, 2), Color.Black);
            m_Vertices[149] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 2, 1), Color.Black);
            m_Vertices[150] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 3, -2), Color.Black);
            m_Vertices[151] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 3, 1), Color.Black);
            m_Vertices[152] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 2, 1), Color.Black);
            m_Vertices[153] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 3, -2), Color.Black);
            m_Vertices[154] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 2, 1), Color.Black);
            m_Vertices[155] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 2, -2), Color.Black);
            m_Vertices[156] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 4, -2), Color.Black);
            m_Vertices[157] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 4, -1), Color.Black);
            m_Vertices[158] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 3, -1), Color.Black);
            m_Vertices[159] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 4, -2), Color.Black);
            m_Vertices[160] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 3, -1), Color.Black);
            m_Vertices[161] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 3, -2), Color.Black);
            m_Vertices[162] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 4, -1), Color.Black);
            m_Vertices[163] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 4, 0), Color.Black);
            m_Vertices[164] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 3 + 2 * k_Quarter, 0), Color.Black);
            m_Vertices[165] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 4, -1), Color.Black);
            m_Vertices[166] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 3 + 2 * k_Quarter, 0), Color.Black);
            m_Vertices[167] = new VertexPositionColor(new Vector3(-4 - k_Quarter * k_Quarter, 3 + 2 * k_Quarter, -1), Color.Black);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            m_BasicEffect.TextureEnabled = false;
            m_BasicEffect.VertexColorEnabled = true;
            foreach (EffectPass pass in m_BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                m_BasicEffect.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleList,
                    m_Vertices,
                    0,
                    56);
            }
        }
    }
}