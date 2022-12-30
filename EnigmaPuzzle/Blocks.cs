using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace EnigmaPuzzle
{
    /// <summary>
    /// Class representing one part of a stone or a bone in the board.
    /// </summary>
    public class Block
    {

        /// <summary>
        /// Gets/Sets the graphics path of the block
        /// </summary>
        public GraphicsPath GP { get; set; }

        /// <summary>
        /// Gets/Sets the color number
        /// </summary>
        public int Col { get; set; }

        /// <summary>
        /// Gets/Sets the pen number
        /// </summary>
        public int Edge { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nr"></param>
        public Block()
        {
            GP = new GraphicsPath();
            Col = -1;
            Edge = -1;
        }


        /// <summary>
        /// Paints the block
        /// </summary>
        /// <param name="g"></param>
        public void Paint(Graphics g)
        {
            if (Col >= 0 && Col < m_colors.Count())
            {
                g.FillPath(m_colors[Col], GP);
            }
            if (Edge >= 0 && Edge < m_pens.Count())
            {
                g.DrawPath(m_pens[Edge], GP);
            }
        }


        /// <summary>
        /// The colors of the blocks for the different game levels
        /// </summary>
        private static int[,] m_games = new int[11, 62]  { 
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 2, 2, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 1, 0, 2, 1, 2, 2, 0, 1},
            {3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 3, 2, 3},
            {1, 1, 1, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 4, 2, 2, 2, 2, 2, 2, 2, 2, 4, 4, 3, 3, 3, 3, 3, 4, 4, 4, 0, 4, 1, 3, 4, 2, 4},
            {0, 0, 0, 0, 0, 0, 0, 0, 6, 6, 1, 1, 1, 1, 1, 1, 1, 1, 6, 6, 2, 2, 2, 2, 2, 2, 2, 2, 6, 6, 6, 6, 3, 3, 3, 3, 3, 3, 3, 3, 6, 6, 4, 4, 4, 4, 4, 4, 4, 4, 6, 6, 6, 6, 0, 6, 1, 6, 6, 4, 6, 3},
            {2, 0, 5, 2, 0, 3, 0, 2, 3, 0, 2, 0, 3, 2, 0, 5, 0, 2, 5, 0, 1, 0, 5, 1, 0, 5, 0, 1, 5, 0, 1, 5, 6, 5, 1, 1, 6, 3, 6, 1, 1, 3, 6, 3, 1, 1, 6, 5, 6, 1, 1, 5, 0, 1, 2, 3, 2, 5, 5, 6, 3, 6},
            {2, 0, 5, 2, 0, 3, 0, 2, 3, 0, 4, 0, 3, 4, 0, 5, 0, 4, 5, 0, 1, 0, 5, 1, 0, 5, 0, 1, 5, 0, 1, 5, 4, 5, 1, 1, 4, 3, 4, 1, 1, 3, 2, 3, 1, 1, 2, 5, 2, 1, 1, 5, 0, 1, 2, 3, 4, 5, 5, 2, 3, 4},
            {2, 0, 5, 2, 0, 3, 0, 2, 3, 0, 4, 0, 3, 4, 0, 6, 0, 4, 6, 0, 1, 0, 6, 1, 0, 5, 0, 1, 5, 0, 1, 5, 4, 5, 1, 1, 4, 3, 4, 1, 1, 3, 2, 3, 1, 1, 2, 6, 2, 1, 1, 6, 0, 1, 2, 3, 4, 5, 6, 2, 3, 4}
        };


        /// <summary>
        /// Alle the defined blocks
        /// </summary>
        private static Block[] m_blocks = null;

        /// <summary>
        /// All the defined colors
        /// </summary>
        private static Brush[] m_colors = null;

        /// <summary>
        /// The defined pens
        /// </summary>
        private static Pen[] m_pens = null;


        /// <summary>
        /// Gets the blocks with the graphics pathes
        /// </summary>
        public static Block[] Blocks
        {
            get
            {
                return m_blocks;
            }
        }


        /// <summary>
        /// Gets the array of solid brushes
        /// </summary>
        public static Brush[] Colors
        {
            get
            {
                return m_colors;
            }
        }

        
        /// <summary>
        /// Init all blocks and colors etc. for a given game level
        /// Blocks 0..51 are the blocks for the game-board
        /// Blocks 52/53 are the centers of the two disks
        /// Blocks 54..61 build the frame of the board
        /// Blocks 62..69 are the buttons and arrows to turn the disks
        /// </summary>
        public static void Init(int level)
        {
            // Create all the static blocks or reset them
            if (m_blocks == null)
            {
                m_blocks = new Block[70];
                for (int i = 0; i < 70; i++)
                {
                    m_blocks[i] = new Block();
                }
            }
            else
            {
                // Reset the blocks if they already exist
                for (int i = 0; i < 70; i++)
                {
                    m_blocks[i].GP.Reset();
                }
            }

            // Init the colors from the settings
            if (m_colors == null)
            {
                m_colors = Settings.GetBrushes(Settings.C_Key_Colors);
            }

            // Create the pens
            if (m_pens == null)
            {
                m_pens = new Pen[2];
                m_pens[0] = new Pen(Brushes.White, 2);
                m_pens[1] = new Pen(Brushes.Gold, 2);
            }

            // Create the first sub-part of the first stone
            m_blocks[0].GP.AddArc(new RectangleF(6.60254F, 20, 160, 160), 180, 21.31781F);
            m_blocks[0].GP.AddArc(new RectangleF(-80, 70, 160, 160), 278.68219F, 21.31781F);
            m_blocks[0].GP.AddLine(new PointF(40.00000F, 80.71797F), new PointF(28.86751F, 100));
            m_blocks[0].GP.AddLine(new PointF(28.86751F, 100), new PointF(6.60254F, 100F));

            Matrix mat120 = new Matrix();
            mat120.RotateAt(120.0F, new PointF(28.86751F, 100));

            // The second sub-part of the first stone (rotate the first by 120 degrees)
            m_blocks[1].GP.AddPath(m_blocks[0].GP, false);
            m_blocks[1].GP.Transform(mat120);

            // The third sub-part of the first stone (rotate the second part by 120 degrees)
            m_blocks[2].GP.AddPath(m_blocks[1].GP, false);
            m_blocks[2].GP.Transform(mat120);

            // The first sub-part of the first bone
            m_blocks[3].GP.AddArc(new RectangleF(6.60254F, 20, 160, 160), 218.68218F, -17.36437F);
            m_blocks[3].GP.AddArc(new RectangleF(-80, 70, 160, 160), 278.68219F, 21.31781F);
            m_blocks[3].GP.AddLine(new PointF(40.00000F, 80.71797F), new PointF(46.60254F, 69.28203F));
            m_blocks[3].GP.AddArc(new RectangleF(6.60254F, -80, 160, 160), 120, 21.31781F);

            Matrix mat180 = new Matrix();
            mat180.RotateAt(180.0F, new PointF(43.30127F, 75F));

            // The second sub-part of the first stone (rotate the first part by 180 degrees)
            m_blocks[4].GP.AddPath(m_blocks[3].GP, false);
            m_blocks[4].GP.Transform(mat180);

            Matrix mat60 = new Matrix();
            mat60.RotateAt(60.0F, new PointF(86.60254F, 100));

            // Create all the stones and bones of the upper disk based on the first one
            // by rotating them six times by 60 degrees.
            for (int i = 5; i < 30; i++)
            {
                m_blocks[i].GP.AddPath(m_blocks[i - 5].GP, false);
                m_blocks[i].GP.Transform(mat60);
            }

            Matrix matMin60 = new Matrix();
            matMin60.RotateAt(-60.0F, new PointF(86.60254F, 200));

            // Create all the stones of the intersection
            for (int i = 30; i < 35; i++)
            {
                m_blocks[i].GP.AddPath(m_blocks[i - 7].GP, false);
                m_blocks[i].GP.Transform(matMin60);
            }

            // Create all the stones of the lower disk
            for (int i = 35; i < 52; i++)
            {
                m_blocks[i].GP.AddPath(m_blocks[i - 5].GP, false);
                m_blocks[i].GP.Transform(matMin60);
            }

            // Crate the center of the upper disk
            m_blocks[52].GP.AddArc(new RectangleF(6.60254F, 20, 160, 160), 201.31781F, 17.36437F);
            m_blocks[52].GP.Transform(mat180);

            GraphicsPath gp = new GraphicsPath();
            gp.AddPath(m_blocks[52].GP, true);
            gp.Transform(mat60);
            m_blocks[52].GP.AddPath(gp, true);
            gp.Transform(mat60);
            m_blocks[52].GP.AddPath(gp, true);
            gp.Transform(mat60);
            m_blocks[52].GP.AddPath(gp, true);
            gp.Transform(mat60);
            m_blocks[52].GP.AddPath(gp, true);
            gp.Transform(mat60);
            m_blocks[52].GP.AddPath(gp, true);

            // Create the center of the lower disk (by translating the upper one)
            Matrix mat100 = new Matrix();
            mat100.Translate(0, 100);
            m_blocks[53].GP.AddPath(m_blocks[52].GP, false);
            m_blocks[53].GP.Transform(mat100);

            // Create the border by createing the necessary polygons
            m_blocks[54].GP.AddLine(86.60254F - 100, 100, 86.60254F - 50, 100 - 86.60254F);
            m_blocks[54].GP.AddArc(new RectangleF(6.60254F, 20, 160, 160), 240, -60);
            m_blocks[54].GP.AddLine(6.60254F, 100, 86.60254F - 100, 100);

            m_blocks[55].GP.AddPath(m_blocks[54].GP, false);
            m_blocks[55].GP.Transform(mat60);

            m_blocks[56].GP.AddPath(m_blocks[55].GP, false);
            m_blocks[56].GP.Transform(mat60);

            m_blocks[57].GP.AddLine(-13.39746F, 100, 6.60254F, 100);
            m_blocks[57].GP.AddArc(new RectangleF(6.60254F, 20, 160, 160), 180, -38.68218F);
            m_blocks[57].GP.AddArc(new RectangleF(6.60254F, 120, 160, 160), 218.68218F, -38.68218F);
            m_blocks[57].GP.AddLine(-13.39746F, 200, 6.60254F, 200);
            m_blocks[57].GP.AddLine(-13.39746F, 200, 14.60254F, 150);
            m_blocks[57].GP.AddLine(14.60254F, 150, -13.39746F, 100);

            mat180 = new Matrix();
            mat180.RotateAt(180.0F, new PointF(86.60254F, 150F));

            m_blocks[58].GP.AddPath(m_blocks[57].GP, false);
            m_blocks[58].GP.Transform(mat180);

            m_blocks[59].GP.AddPath(m_blocks[54].GP, false);
            m_blocks[59].GP.Transform(mat180);

            m_blocks[60].GP.AddPath(m_blocks[55].GP, false);
            m_blocks[60].GP.Transform(mat180);

            m_blocks[61].GP.AddPath(m_blocks[56].GP, false);
            m_blocks[61].GP.Transform(mat180);

            // Create the clickable button-form (UL, UR etc.)
            m_blocks[62].GP.AddEllipse(new RectangleF(-27, 120, 30, 30));
            m_blocks[63].GP.AddEllipse(new RectangleF(170, 120, 30, 30));
            m_blocks[64].GP.AddEllipse(new RectangleF(-27, 150, 30, 30));
            m_blocks[65].GP.AddEllipse(new RectangleF(170, 150, 30, 30));

            // Create the arrow in the first button
            m_blocks[66].GP.AddLine(new Point(-23, 124), new Point(-16, 124));
            m_blocks[66].GP.AddLine(new Point(-16, 124), new Point(-16, 125));
            m_blocks[66].GP.AddLine(new Point(-16, 125), new Point(-21, 125));
            m_blocks[66].GP.AddLine(new Point(-21, 125), new Point(-13, 133));
            m_blocks[66].GP.AddLine(new Point(-13, 133), new Point(-14, 134));
            m_blocks[66].GP.AddLine(new Point(-14, 134), new Point(-22, 126));
            m_blocks[66].GP.AddLine(new Point(-22, 126), new Point(-22, 131));
            m_blocks[66].GP.AddLine(new Point(-22, 131), new Point(-23, 131));
            m_blocks[66].GP.AddLine(new Point(-23, 131), new Point(-23, 124));

            // Copy the arrow to the other buttons
            Matrix matTransRot = new Matrix();
            matTransRot.RotateAt(270.0F, new PointF(-12F, 165F));
            matTransRot.Translate(0, 30F);
            m_blocks[67].GP.AddPath(m_blocks[66].GP, false);
            m_blocks[67].GP.Transform(matTransRot);

            m_blocks[69].GP.AddPath(m_blocks[66].GP, false);
            m_blocks[69].GP.Transform(mat180);

            m_blocks[68].GP.AddPath(m_blocks[67].GP, false);
            m_blocks[68].GP.Transform(mat180);

            // Move everything a little bit to the right, so that the border on 
            // the left side has positive coordinates
            Matrix m = new Matrix();
            m.Translate(30, 0);
            for (int i = 0; i < 70; i++)
            {
                m_blocks[i].GP.Transform(m);
            }

            // Default borders
            for (int i = 0; i < 62; i++)
            {
                m_blocks[i].Edge = 0;
            }

            // No border for the centers of the disks
            Block.Blocks[52].Edge = -1;
            Block.Blocks[53].Edge = -1;

            // Game init with the given level
            for (int i = 0; i < 62; i++)
            {
                m_blocks[i].Col = m_games[level, i];
            }

            // Buttons
            for (int i = 62; i < 66; i++)
            {
                // Button form
                m_blocks[i].Col = 8;
                m_blocks[i].Edge = -1;

                // Text on buttons
                m_blocks[i + 4].Col = 8;
                m_blocks[i + 4].Edge = 0;
            }
        }


        /// <summary>
        /// Transforms all the blocks with the given matrix
        /// </summary>
        /// <param name="m">the transformation matrix</param>
        public static void TransformAll(Matrix m)
        {
            for (int i = 0; i < 70; i++)
            {
                m_blocks[i].GP.Transform(m);
            }
        }

        
        /// <summary>
        /// Gets the color-string for the original-setting of the blocks
        /// Used to check if the puzzle has been solved
        /// </summary>
        /// <returns>a string with the color-numbers of all the relevant parts in the board</returns>
        public static string GetColorString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 52; i++)
            {
                sb.Append(m_blocks[i].Col.ToString());
            }
            return sb.ToString();
        }
        

    }

}
