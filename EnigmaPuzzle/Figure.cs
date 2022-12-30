using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EnigmaPuzzle
{
    /// <summary>
    /// Class representing a game figure i.e. a stone or a bone in the board
    /// </summary>
    public class Figure
    {
        /// <summary>
        /// The blocks that build up the stone or bone
        /// </summary>
        private List<Block> m_blocks;

        /// <summary>
        /// The current orientation. 0 means in the original orientation.
        /// 1 = 60 deg
        /// 2 = 120 deg
        /// </summary>
        private int m_orient;


        /// <summary>
        /// Constructor
        /// </summary>
        public Figure()
        {
            m_blocks = new List<Block>();
            m_orient = 0;
        }


        /// <summary>
        /// Increment the rotation
        /// </summary>
        public void IncOrient()
        {
            m_orient++;
            m_orient %= 3;
        }


        /// <summary>
        /// Decrement orientation
        /// </summary>
        public void DecOrient()
        {
            m_orient--;
            if (m_orient < 0)
            {
                m_orient += 3;
            }
        }
        

        /// <summary>
        /// Add a part to the figure
        /// </summary>
        /// <param name="nr"></param>
        public void AddBlock(int nr)
        {
            m_blocks.Add(Block.Blocks[nr]);
        }


        /// <summary>
        /// Paint the figure
        /// </summary>
        /// <param name="g"></param>
        public void Paint(Graphics g)
        {
            foreach (Block block in m_blocks)
            {
                block.Paint(g);
            }
        }


        /// <summary>
        /// Gets the colorstring of the blockparts based on the 
        /// current orientation.
        /// </summary>
        /// <returns></returns>
        public string GetColorString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < m_blocks.Count; i++)
            {
                // Add the current orientation to the block position number
                // and make sure, the the index is in the range of the 
                // number of blocks in the figure.
                int idx = (i + m_orient) % 3;
                sb.Append(m_blocks[idx].Col.ToString());
            }

            return sb.ToString();
        }


        /// <summary>
        /// Transform the graphics path of all blockparts of the figure 
        /// Used during turning a disk.
        /// </summary>
        /// <param name="mat"></param>
        public void Transform(Matrix mat)
        {
            foreach (Block block in m_blocks)
            {
                block.GP.Transform(mat);
            }
        }

    }

}
