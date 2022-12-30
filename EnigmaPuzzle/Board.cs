using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Resources;

namespace EnigmaPuzzle
{
    /// <summary>
    /// Class for the board of the puzzle
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The disk positions
        /// </summary>
        public enum eDisc
        {
            eUpperDisc,
            eLowerDisc
        }

        /// <summary>
        /// The disc rotation directions
        /// </summary>
        public enum eDirection
        {
            eLeft,
            eRight
        }


        /// <summary>
        /// Struct to hold a move
        /// </summary>
        private struct Move
        {
            public eDisc m_disc;
            public eDirection m_dir;

            public Move(eDisc disc, eDirection dir)
            {
                m_disc = disc;
                m_dir = dir;
            }
        }

        /// <summary>
        /// List of all moves
        /// </summary>
        private List<Move> m_moves = new List<Move>();

        /// <summary>
        /// State variable for the board
        /// </summary>
        private int m_lev = 0;
        private int m_rotationDelay;
        private int m_rotationSteps;
        private int m_swingSteps;
        private bool m_bSwing;
        private int m_numTurns;
        private bool m_bShowTurns;
        private bool m_bFullScreen;
        private float m_offsetX = 0.0f;

        /// <summary>
        /// Size of the board
        /// </summary>
        private int m_h = 300;
        private int m_w = 230;

        /// <summary>
        /// Current position of the stones and bones
        /// </summary>
        private int[] m_upperBones = new int[6];
        private int[] m_lowerBones = new int[6];
        private int[] m_upperStones = new int[6];
        private int[] m_lowerStones = new int[6];
        
        /// <summary>
        /// Figures in the board (stones and bones)
        /// </summary>
        private Figure[] m_stones = new Figure[10];
        private Figure[] m_bones = new Figure[11];
        private Figure[] m_frames = new Figure[18];

        /// <summary>
        /// Coordinates
        /// </summary>
        private PointF m_upperCenter = new PointF(116.60254F, 100);
        private PointF m_lowerCenter = new PointF(116.60254F, 200);

        /// <summary>
        /// Bitmaps for the board
        /// </summary>
        private Bitmap m_upperDisk = null;
        private Bitmap m_lowerDisk = null;
        private Bitmap m_background = null;

        /// <summary>
        /// The last disk that has been rotated
        /// </summary>
        private eDisc m_rotDisc;

        /// <summary>
        /// Pause the timer or start it again
        /// </summary>
        public bool Pause { get; set; }


        /// <summary>
        /// Gets true if a game is running
        /// </summary>
        public bool GameActive { get; set; }

        /// <summary>
        /// Starttime for a game to calc the elapsed time
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Number of the moves in the current game
        /// </summary>
        public int GameMoves { get; set; }


        /// <summary>
        /// The moves of the puzzler
        /// </summary>
        public int Moves
        {
            get
            {
                return m_moves.Count - GameMoves;
            }
        }



        /// <summary>
        /// Gets the current level
        /// </summary>
        public int Level
        {
            get
            {
                return m_lev;
            }
        }


        /// <summary>
        /// Gets the X-part of the middlepoint of the board
        /// </summary>
        private float MiddleX
        {
            get
            {
                return (m_upperCenter.X + m_lowerCenter.X) / 2.0f;
            }
        }

        /// <summary>
        /// Gets the Y-part of the middlepoint of the board
        /// </summary>
        private float MiddleY
        {
            get
            {
                return (m_upperCenter.Y + m_lowerCenter.Y) / 2.0f;
            }
        }



        /// <summary>
        /// Background Image
        /// </summary>
        public Bitmap Background
        {
            get
            {
                return m_background;
            }
        }


        /// <summary>
        /// Uppder disk Bitmap
        /// </summary>
        public Bitmap UpperDisk
        {
            get
            {
                return m_upperDisk;
            }
        }


        /// <summary>
        /// Lowerdisk bitmap
        /// </summary>
        public Bitmap LowerDisk
        {
            get
            {
                return m_lowerDisk;
            }
        }



        /// <summary>
        /// The disc currently rotating
        /// </summary>
        public eDisc RotDisk
        {
            get
            {
                return m_rotDisc;
            }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public Board()
        {
            InitBoard(Settings.GetInt(Settings.C_Key_Level));
        }


        /// <summary>
        /// Reset the board
        /// </summary>
        /// <param name="form"></param>
        public void Reset(Form form)
        {
            InitBoard(m_lev);
            OnResize(form.ClientSize.Width, form.ClientSize.Height);
        }


        /// <summary>
        /// Init the board for a given level
        /// </summary>
        /// <param name="level"></param>
        public void InitBoard(int level)
        {
            // Get settings
            m_lev = level;
            m_rotationDelay = Settings.GetInt(Settings.C_Key_RotationDelay);
            m_rotationSteps = Settings.GetInt(Settings.C_Key_RotationSteps);
            m_swingSteps = Settings.GetInt(Settings.C_Key_SwingSteps);
            m_bSwing = Settings.GetBool(Settings.C_Key_Swing);
            m_numTurns = Settings.GetInt(Settings.C_Key_NumTurns);
            m_bShowTurns = Settings.GetBool(Settings.C_Key_ShowTurns);
            m_bFullScreen = Settings.GetBool(Settings.C_Key_FullScreen);
            m_upperCenter = new PointF(116.60254F, 100);
            m_lowerCenter = new PointF(116.60254F, 200);

            // Save current Level
            Settings.SetInt(Settings.C_Key_Level, level);
            Settings.Write();

            // Reset active game
            GameActive = false;
            m_moves = new List<Move>();
            GameMoves = 0;
            Pause = false;

            // Init the stones and bones
            m_stones = new Figure[10];
            m_bones = new Figure[11];
            m_frames = new Figure[18];

            // Build the blocks and color them
            Block.Init(level);

            // Build the stones and bones with the blocks
            m_bones[0] = new Figure();
            m_bones[0].AddBlock(28);
            m_bones[0].AddBlock(29);

            for (int i = 1; i < 6; i++)
            {
                m_bones[i] = new Figure();
                m_bones[i].AddBlock(5 * (i - 1) + 3);
                m_bones[i].AddBlock(5 * (i - 1) + 4);
            }
            for (int i = 0; i < 6; i++)
            {
                m_stones[i] = new Figure();
                m_stones[i].AddBlock(5 * i);
                m_stones[i].AddBlock(5 * i + 1);
                m_stones[i].AddBlock(5 * i + 2);
            }
            
            for (int i = 6; i < 11; i++)
            {
                m_bones[i] = new Figure();
                m_bones[i].AddBlock(5 * i);
                m_bones[i].AddBlock(5 * i + 1);

            }
            for (int i = 6; i < 10; i++)
            {
                m_stones[i] = new Figure();
                m_stones[i].AddBlock(5 * i + 2);
                m_stones[i].AddBlock(5 * i + 3);
                m_stones[i].AddBlock(5 * i + 4);
            }

            // Create the frames
            for (int i = 0; i < 18; i++)
            {
                m_frames[i] = new Figure();
                m_frames[i].AddBlock(i + 52);
            }
            
            // Build the default array of the stones and bones-numbers of
            // the upper and lower disc
            for (int i = 0; i < 6; i++)
            {
                m_upperBones[i] = i;
                m_lowerBones[i] = i + 5;
                m_upperStones[i] = i;
                m_lowerStones[i] = i + 4;
            }

            m_w = 230;
            m_h = 300;
        }



        /// <summary>
        /// Paints a disk depending on the current position
        /// </summary>
        /// <param name="g"></param>
        /// <param name="disc">upper or lower</param>
        private void PaintDisk(Graphics g, eDisc disc)
        {
            if (disc == eDisc.eUpperDisc)
            {
                for (int i = 0; i < 6; i++)
                {
                    m_bones[m_upperBones[i]].Paint(g);
                    m_stones[m_upperStones[i]].Paint(g);
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    m_bones[m_lowerBones[i]].Paint(g);
                    m_stones[m_lowerStones[i]].Paint(g);
                }
            }
        }


        /// <summary>
        /// Paints the background
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bPrint">If true, then the buttons will not be painted</param>
        private void PaintBackground(Graphics g,bool bPrint = false)
        {
            // Show all frames
            for (int i = 0; i < 18; i++)
            {
                if (bPrint && i >= 10)
                {
                    continue;
                }
                m_frames[i].Paint(g);
            }
        }


        /// <summary>
        /// Get the colorstring for all blocks in the board.
        /// The colorstring can be used to check if the board
        /// is equal to the original board - puzzle solved!
        /// </summary>
        /// <returns></returns>
        public string GetColorString()
        {
            StringBuilder sb = new StringBuilder();

            // First the stones and bones of the upper disc
            for (int i = 0; i < 5; i++)
            {
                sb.Append(m_stones[m_upperStones[i]].GetColorString());
                sb.Append(m_bones[m_upperBones[i + 1]].GetColorString());
            }
            sb.Append(m_stones[m_upperStones[5]].GetColorString());
            sb.Append(m_bones[m_upperBones[0]].GetColorString());

            // .. and now the lower disc
            sb.Append(m_bones[m_lowerBones[1]].GetColorString());
            for (int i = 2; i < 6; i++)
            {
                sb.Append(m_stones[m_lowerStones[i]].GetColorString());
                sb.Append(m_bones[m_lowerBones[i]].GetColorString());
            }
           
            return sb.ToString();
        }



        /// <summary>
        /// Resize the graphics
        /// </summary>
        /// <param name="newW"></param>
        /// <param name="newH"></param>
        public void OnResize(int newW, int newH)
        {
            // Calculate the transform matrix
            float dx = 1.0f * newW / m_w;
            float dy = 1.0f * newH / m_h;
            Matrix m = new Matrix();
            m.Scale(dy, dy);

            // Scale all blocks
            Block.TransformAll(m);

            RectangleF rfu1 = Block.Blocks[52].GP.GetBounds();
            float xu1 = rfu1.X + (rfu1.Width / 2.0f);

            // In fullscreen mode the board is placed in the center of the screen
            // else on the left side
            if (m_bFullScreen)
            {
                // Move the blocks to the center 
                // of the screen
                float dd = newW / 2.0f - xu1;
                m.Reset();
                m.Translate(dd, 0f);

                // Translate all blocks
                Block.TransformAll(m);

                // save the offset for when the user changes to sizeable screen
                m_offsetX *= dy;
                m_offsetX += dd;
            }
            else
            {
                // Move the blocks to the left for when the user comes from fullscreen mode
                m.Reset();
                m.Translate(-m_offsetX, 0f);

                // Translate all blocks
                Block.TransformAll(m);

                m_offsetX = 0;
            }

            // adjust the centers of the discs with the center blocks 
            // of both discs
            RectangleF rfu = Block.Blocks[52].GP.GetBounds();
            RectangleF rfl = Block.Blocks[53].GP.GetBounds();

            m_upperCenter.X = rfu.X + (rfu.Width / 2.0f);
            m_upperCenter.Y = rfu.Y + (rfu.Height / 2.0f);
            m_lowerCenter.X = rfl.X + (rfl.Width / 2.0f);
            m_lowerCenter.Y = rfl.Y + (rfl.Height / 2.0f);

            // save the current size
            m_h = newH;
            m_w = newW;

            // Create all the bitmaps newly
            CreateBackground();
            CreateUpperDisk();
            CreateLowerDisk();
        }


        /// <summary>
        /// Create Bitmap for upper disc
        /// </summary>
        private void CreateUpperDisk()
        {
            // Create a bitmap
            if (m_upperDisk != null)
            {
                m_upperDisk.Dispose();
            }
            m_upperDisk = new Bitmap(m_w, m_h);
            Graphics g = Graphics.FromImage(m_upperDisk);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Paint the disk
            PaintDisk(g, eDisc.eUpperDisc);

            // Clean up
            g.Dispose();
        }



        /// <summary>
        /// Create bitmap for lower disc
        /// </summary>
        private void CreateLowerDisk()
        {
            // Create a bitmap
            if (m_lowerDisk != null)
            {
                m_lowerDisk.Dispose();
            }
            m_lowerDisk = new Bitmap(m_w, m_h);

            Graphics g = Graphics.FromImage(m_lowerDisk);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Paint the disk
            PaintDisk(g, eDisc.eLowerDisc);

            // Clean up
            g.Dispose();
        }


        /// <summary>
        /// Create bitmap for the background
        /// </summary>
        /// <param name="bPrint"></param>
        public void CreateBackground(bool bPrint = false)
        {
            // Create a bitmap
            if (m_background != null)
            {
                m_background.Dispose();
            }
            m_background = new Bitmap(m_w, m_h);
            Graphics g = Graphics.FromImage(m_background);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Fill the rect with the background color
            if (!bPrint)
            {
                g.FillRectangle(Block.Colors[8], 0, 0, m_w, m_h);
            }

            // Paint the board
            PaintBackground(g, bPrint);

            // Clean up
            g.Dispose();
        }



        /// <summary>
        /// React on MouseDown for the graphic-buttons to turn the discs
        /// </summary>
        /// <param name="point">The click position</param>
        /// <param name="form">the form</param>
        /// <returns>true if the click has been handled</returns>
        public bool MouseDown(Point point, EnigmaPuzzleDlg form)
        {
            // Buttons
            if (Block.Blocks[62].GP.IsVisible(point))
            {
                // Upper disc left
                Rotate(Board.eDisc.eUpperDisc, Board.eDirection.eRight, true, form);
                return true;
            }
            else if (Block.Blocks[63].GP.IsVisible(point))
            {
                // Upper disc right
                Rotate(Board.eDisc.eUpperDisc, Board.eDirection.eLeft, true, form);
                return true;
            }
            else if (Block.Blocks[64].GP.IsVisible(point))
            {
                // Lower disc left
                Rotate(Board.eDisc.eLowerDisc, Board.eDirection.eLeft, true, form);
                return true;
            }
            else if (Block.Blocks[65].GP.IsVisible(point))
            {
                // Lower disc right
                Rotate(Board.eDisc.eLowerDisc, Board.eDirection.eRight, true, form);
                return true;
            }

            // Click not handled
            return false;
        }


        /// <summary>
        /// Start settings dialog
        /// </summary>
        /// <param name="form"></param>
        public void Setup(EnigmaPuzzleDlg form)
        {
            // Show setup dialog
            SetupDlg dlg = new SetupDlg();
            dlg.ShowDialog();

            // Get settings
            m_rotationDelay = Settings.GetInt(Settings.C_Key_RotationDelay);
            m_rotationSteps = Settings.GetInt(Settings.C_Key_RotationSteps);
            m_swingSteps = Settings.GetInt(Settings.C_Key_SwingSteps);
            m_bSwing = Settings.GetBool(Settings.C_Key_Swing);
            m_numTurns = Settings.GetInt(Settings.C_Key_NumTurns);
            m_bShowTurns = Settings.GetBool(Settings.C_Key_ShowTurns);
            m_bFullScreen = Settings.GetBool(Settings.C_Key_FullScreen);


            if (m_bFullScreen)
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;
            }
            else
            {
                form.FormBorderStyle = FormBorderStyle.Sizable;
                form.WindowState = FormWindowState.Normal;
            }


            form.ShowLevel();
        }



        /// <summary>
        /// Increment the gamelevel if possible
        /// </summary>
        /// <param name="form"></param>
        public void LevelUp(EnigmaPuzzleDlg form)
        {
            // Increment the level
            if (m_lev == 10)
            {
                return;
            }

            m_lev++;
            GameActive = false;
            InitBoard(m_lev);
            OnResize(form.ClientSize.Width, form.ClientSize.Height);
        }


        /// <summary>
        /// Decrement the gamelevel if possible
        /// </summary>
        /// <param name="form"></param>
        public void LevelDown(EnigmaPuzzleDlg form)
        {
            if (m_lev == 0)
            {
                return;
            }
            m_lev--;
            GameActive = false;
            InitBoard(m_lev);
            OnResize(form.ClientSize.Width, form.ClientSize.Height);
        }


        /// <summary>
        /// Start a new game by random rotation of the discs
        /// </summary>
        /// <param name="form"></param>
        public void NewGame(EnigmaPuzzleDlg form)
        {
            InitBoard(m_lev);
            OnResize(form.ClientSize.Width, form.ClientSize.Height);

            // Get the settings for new games
            m_numTurns = Settings.GetInt(Settings.C_Key_NumTurns);
            m_bShowTurns = Settings.GetBool(Settings.C_Key_ShowTurns);
            GameActive = false;
            m_moves = new List<Move>();
            GameMoves = 0;
            StartTime = DateTime.Now;
            Pause = false;

            Random r = new Random();
            for (int i = 0; i < m_numTurns; i++)
            {
                eDirection dir = eDirection.eRight;
                eDisc disk = eDisc.eUpperDisc;
                if (i % 2 == 0)
                {
                    disk = eDisc.eLowerDisc;
                }

                int anz = r.Next(1, 6);
                if (anz > 3)
                {
                    dir = eDirection.eLeft;
                    anz = anz - 3;
                }

                GameMoves += anz;

                for (int j = 0; j < anz; j++)
                {
                    Rotate(disk, dir, m_bShowTurns, form);
                    if (m_bShowTurns)
                    {
                        System.Threading.Thread.Sleep(3 * m_rotationDelay);
                    }
                }
            }

            // Check if the board is solved yet by accident
            string s = GetColorString();
            string s1 = Block.GetColorString();
            if (s == s1)
            {
                // Do some rotations
                Rotate(eDisc.eLowerDisc, eDirection.eRight, false, form);
                Rotate(eDisc.eLowerDisc, eDirection.eRight, false, form);
                Rotate(eDisc.eLowerDisc, eDirection.eRight, false, form);
                Rotate(eDisc.eUpperDisc, eDirection.eLeft, false, form);
                Rotate(eDisc.eUpperDisc, eDirection.eLeft, false, form);
                Rotate(eDisc.eUpperDisc, eDirection.eLeft, false, form);

                GameMoves += 6;
            }

            // If the turns have not been shown, we have to rebuild the
            // bitmaps here
            if (!m_bShowTurns)
            {
                CreateUpperDisk();
                CreateLowerDisk();
                form.Refresh();
            }

            GameActive = true;
        }


        /// <summary>
        /// Rotate a disc graphically with the possibility of overswing
        /// </summary>
        /// <param name="bones">Array with the numbers of bones</param>
        /// <param name="stones">Array with the numbers of stones</param>
        /// <param name="c">Rotation center</param>
        /// <param name="deg">Rotation degrees per step</param>
        /// <param name="steps">Rotation steps</param>
        /// <param name="zi">Swing</param>
        /// <param name="de">Delta for swing</param>
        /// <param name="bShow">If true the rotation will be made visible</param>
        /// <param name="form">Form to refresh</param>
        private void RotateDisk(int[] bones, int[] stones, PointF c, float deg, int steps, float zi, float de, bool bShow, eDisc disc, Form form)
        {
            // Create the necessary matrices
            Matrix mat4 = new Matrix();

            mat4.RotateAt(deg, c);

            // Do the rotation of all bones and stones
            for (int s = 0; s < steps; s++)
            {
                for (int i = 0; i < 6; i++)
                {
                    m_bones[bones[i]].Transform(mat4);
                    m_stones[stones[i]].Transform(mat4);
                }

                if (bShow)
                {
                    if (s < steps - 1)
                    {
                        System.Threading.Thread.Sleep(m_rotationDelay);
                    }
                    if (disc == eDisc.eUpperDisc)
                    {
                        CreateUpperDisk();
                    }
                    else
                    {
                        CreateLowerDisk();
                    }
                    form.Refresh();
                }
            }

            // Show the swing
            if (m_bSwing)
            {
                Matrix mat5 = new Matrix();
                Matrix mat6 = new Matrix();

                float s1 = 0 + zi;
                float s2 = 0 - (zi + de);

                mat5.RotateAt(s1 - s2, c);
                mat6.RotateAt(-s1, c);

                while (zi != 0)
                {

                    for (int i = 0; i < 6; i++)
                    {
                        m_bones[bones[i]].Transform(mat6);
                        m_stones[stones[i]].Transform(mat6);
                    }
                    if (bShow)
                    {
                        if (disc == eDisc.eUpperDisc)
                        {
                            CreateUpperDisk();
                        }
                        else
                        {
                            CreateLowerDisk();
                        }
                        form.Refresh();
                        System.Threading.Thread.Sleep((int)(5 * -zi * de * m_rotationDelay));
                    }

                    for (int i = 0; i < 6; i++)
                    {
                        m_bones[bones[i]].Transform(mat5);
                        m_stones[stones[i]].Transform(mat5);
                    }
                    if (bShow)
                    {
                        if (disc == eDisc.eUpperDisc)
                        {
                            CreateUpperDisk();
                        }
                        else
                        {
                            CreateLowerDisk();
                        }
                        form.Refresh();
                        System.Threading.Thread.Sleep((int)(5 * -zi * de * m_rotationDelay));
                    }

                    zi += de;

                    s1 = 0 + zi;
                    s2 = 0 - (zi + de);

                    mat6.Reset();
                    mat6.RotateAt(-s1 - zi, c);
                    mat5.Reset();
                    mat5.RotateAt(s1 - s2, c);

                }
            }
        }


        /// <summary>
        /// Prepare params for the graphic disc rotation
        /// </summary>
        /// <param name="deg">Angle of rotation</param>
        /// <param name="disc">Which disc</param>
        /// <param name="dir">Direction</param>
        /// <param name="bShow">Show the rotation or not</param>
        /// <param name="form">Form to refresh</param>
        private void RotateDisk(float deg, eDisc disc, eDirection dir, bool bShow, Form form)
        {
            float z;
            float d;
            int steps = 1;

            // When show the rotation calculate
            // how many degrees per step
            if (bShow)
            {
                steps = m_rotationSteps;
                deg = deg / steps;
            }

            // Calc the parameter for the real rotation
            if (disc == eDisc.eUpperDisc)
            {
                if (dir == eDirection.eLeft)
                {
                    deg = -deg;
                    z = m_swingSteps;
                    d = -1;
                }
                else
                {
                    z = -m_swingSteps;
                    d = 1;
                }
                RotateDisk(m_upperBones, m_upperStones, m_upperCenter, deg, steps, z, d, bShow, disc, form);
            }
            else
            {
                if (dir == eDirection.eLeft)
                {
                    deg = -deg;
                    z = m_swingSteps;
                    d = -1;
                }
                else
                {
                    z = -m_swingSteps;
                    d = 1;
                }
                RotateDisk(m_lowerBones, m_lowerStones, m_lowerCenter, deg, steps, z, d, bShow, disc, form);
            }

        }


        /// <summary>
        /// Rotate a disc. Grphically an logically.
        /// </summary>
        /// <param name="disc">Which disc</param>
        /// <param name="dir">The direction</param>
        /// <param name="bShow">Show the rotation or not</param>
        /// <param name="form">Form to refresh</param>
        public bool Rotate(eDisc disc, eDirection dir, bool bShow, EnigmaPuzzleDlg form)
        {
            // Local vars for the new figure positions
            int[] newD = new int[6];
            int[] newD2 = new int[6];

            // Remember the rotated disk
            m_rotDisc = disc;

            // Add the move
            m_moves.Add(new Move(disc, dir));
            if (GameActive && m_moves.Count - GameMoves > 0)
            {
                form.ShowGameStatus();
            }

            // Graphically rotation
            RotateDisk(60.0f, disc, dir, bShow, form);

            // Logically rotation - adjust the stones and bones on the disks and take incount
            // that there are two stone and one bone that overlapp
            if (disc == eDisc.eUpperDisc)
            {
                if (dir == eDirection.eLeft)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (m_upperStones[i] >= 6)
                        {
                            m_stones[m_upperStones[i]].IncOrient();
                        }

                        int idx = i + 5;
                        if (idx > 5)
                        {
                            idx -= 6;
                        }
                        newD[idx] = m_upperBones[i];
                        newD2[idx] = m_upperStones[i];
                    }
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (m_upperStones[i] >= 6)
                        {
                            m_stones[m_upperStones[i]].DecOrient();
                        }

                        int idx = i - 5;
                        if (idx < 0)
                        {
                            idx += 6;
                        }
                        newD[idx] = m_upperBones[i];
                        newD2[idx] = m_upperStones[i];
                    }
                }

                for (int i = 0; i < 6; i++)
                {
                    m_upperBones[i] = newD[i];
                    m_upperStones[i] = newD2[i];
                }

                m_lowerBones[0] = m_upperBones[5];
                m_lowerStones[0] = m_upperStones[4];
                m_lowerStones[1] = m_upperStones[5];

            }
            else
            {
                if (dir == eDirection.eRight)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (m_lowerStones[i] < 6)
                        {
                            m_stones[m_lowerStones[i]].IncOrient();
                        }

                        int idx = i + 5;
                        if (idx > 5)
                        {
                            idx -= 6;
                        }
                        newD[idx] = m_lowerBones[i];
                        newD2[idx] = m_lowerStones[i];
                    }
                }
                else
                {

                    for (int i = 0; i < 6; i++)
                    {
                        if (m_lowerStones[i] < 6)
                        {
                            m_stones[m_lowerStones[i]].DecOrient();
                        }

                        int idx = i - 5;
                        if (idx < 0)
                        {
                            idx += 6;
                        }
                        newD[idx] = m_lowerBones[i];
                        newD2[idx] = m_lowerStones[i];
                    }
                }

                for (int i = 0; i < 6; i++)
                {
                    m_lowerBones[i] = newD[i];
                    m_lowerStones[i] = newD2[i];
                }

                m_upperBones[5] = m_lowerBones[0];
                m_upperStones[4] = m_lowerStones[0];
                m_upperStones[5] = m_lowerStones[1];

            }

            // Create the bitmaps newly and show them 
            if (bShow)
            {
                CreateUpperDisk();
                CreateLowerDisk();
                form.Refresh();
            }

            // Check if the game has been solved
            if (GameActive)
            {
                TimeSpan sp = (DateTime.Now - StartTime);
                int hours = sp.Hours;
                int mins = sp.Minutes;
                int secs = sp.Seconds;

                string s = GetColorString();
                string s1 = Block.GetColorString();

                // If the puzzle has been solved show the dialog with the results (time and moves)
                if (s == s1)
                {
                    form.ShowGameStatus(true);
                    
                    ColorStones(form);
                    string m = String.Format(Properties.Resources.PuzzleSolved, (m_moves.Count - GameMoves).ToString());
                    m += "\r\n\r\n";
                    m += String.Format(Properties.Resources.PuzzleSolvedTime);

                    m += " ";

                    if (hours > 0)
                    {
                        if (hours == 1)
                        {
                            m += hours.ToString() + " " + Properties.Resources.Hour;
                        }
                        else
                        {
                            m += hours.ToString() + " " + Properties.Resources.Hours;
                        }
                    }

                    m += " ";

                    if (mins == 1)
                    {
                        m += mins.ToString() + " " + Properties.Resources.Minute;
                    }
                    else
                    {
                        m += mins.ToString() + " " + Properties.Resources.Minutes;
                    }

                    m += " ";

                    if (secs == 1)
                    {
                        m += secs.ToString() + " " + Properties.Resources.Second;
                    }
                    else
                    {
                        m += secs.ToString() + " " + Properties.Resources.Seconds;
                    }

                    MessageDlg dlg = new MessageDlg(m);
                    dlg.ShowDialog();

                    InitBoard(m_lev);
                    OnResize(form.ClientSize.Width, form.ClientSize.Height);
                    form.Refresh();
                }
                return true;
            }

            return false;
        }


        /// <summary>
        /// Handle the MouseMove-Event when dragging a disk. Dragging will be translated
        /// into a rotation of the disc. 
        /// The two points define a vector which defines the rotation direction
        /// </summary>
        /// <param name="x1">X of startpoint</param>
        /// <param name="y1">Y of startpoint</param>
        /// <param name="x2">X of endpoint</param>
        /// <param name="y2">Y of endpoint</param>
        /// <param name="form">the calling form</param>
        /// <returns>false if the angle-diff is less than 5 degrees, true otherwise</returns>
        public bool TurnDisk(float x1, float y1, float x2, float y2, EnigmaPuzzleDlg form)
        {
            eDisc disc = eDisc.eUpperDisc;
            eDirection dir = eDirection.eLeft;
            float cy;

            // Determin the disk - just look at the hor. middle of the board
            if (y1 < MiddleY)
            {
                disc = eDisc.eUpperDisc;
                cy = m_upperCenter.Y;
            }
            else
            {
                disc = eDisc.eLowerDisc;
                cy = m_lowerCenter.Y;
            }
                       
            // Move the coordinates so that the y-axle is in the middle of the board
            x1 -= MiddleX;
            x2 -= MiddleX;

            // Because 0/0 is upper left corner, we have to inverse the y-coordinate
            y1 = -(y1 - cy);
            y2 = -(y2 - cy);

            // If the drag is too short (length of the turning vector) - do nothing
            // Get the turning vector
            float vx = x2 - x1;
            float vy = y2 - y1;
            if (Math.Sqrt(vx * vx + vy * vy) < 20)
            {
                return false;
            }

            // Calc vector product to get the orientation
            double orient = x1 * y2 - y1 * x2;
            if (orient > 0)
            {
                dir = eDirection.eLeft;
            }
            else
            {
                dir = eDirection.eRight;
            }

            // Do the rotations
            Rotate(disc, dir, true, form);

            return true;
        }



        /// <summary>
        /// Set the board to a saved position with the given rotations-string
        /// </summary>
        /// <param name="rots"></param>
        /// <param name="form"></param>
        public void SetBoard(string rots, EnigmaPuzzleDlg form)
        {
            for (int i = 0; i < rots.Length; i += 2)
            {
                eDisc disc = eDisc.eUpperDisc;
                eDirection dir = eDirection.eRight;
                if (rots[i] == '1')
                {
                    disc = eDisc.eLowerDisc;
                }
                if (rots[i + 1] == '1')
                {
                    dir = eDirection.eLeft;
                }

                // Rotate the disc but don't show it
                Rotate(disc, dir, false, form);
            }

            // Create all the bitmaps newly
            CreateBackground();
            CreateUpperDisk();
            CreateLowerDisk();
        }


        /// <summary>
        /// Load a board from a file.
        /// Start the board with the level on the first line in the file.
        /// Then do all the moves from the second line. The characters define
        /// which disc has to be rotated to which direction. Allways two 
        /// characters define one rotation.
        /// </summary>
        /// <param name="fName">The filename with the path</param>
        /// <param name="form">the calling form</param>
        public void LoadBoard(string fName, EnigmaPuzzleDlg form)
        {
            String line;
            if (File.Exists(fName))
            {
                StreamReader sr = new StreamReader(fName);
                try
                {
                    GameActive = false;
                    m_moves = new List<Move>();
                    GameMoves = 0;

                    int lev = 1;
                    StartTime = DateTime.Now;
                    int gameMoves = 0;

                    line = sr.ReadLine();
                    if (line != null)
                    {
                        lev = Int32.Parse(line);
                    }
                    else
                    {
                        throw new Exception(Properties.Resources.LineMissing);
                    }

                    line = sr.ReadLine();
                    if (line != null)
                    {
                        gameMoves = Int32.Parse(line);
                    }
                    else
                    {
                        throw new Exception(Properties.Resources.LineMissing);
                    }

                    line = sr.ReadLine();
                    if (line != null)
                    {
                        long ticks = Int64.Parse(line);
                        StartTime = DateTime.Now.AddTicks(-ticks);
                    }
                    else
                    {
                        throw new Exception(Properties.Resources.LineMissing);
                    }

                    line = sr.ReadLine();
                    if (line == null)
                    {
                        throw new Exception(Properties.Resources.LineMissing);
                    }

                    if ((line.Length % 2) != 0) {
                        throw new Exception(Properties.Resources.MoveLineWrong);
                    }

                    InitBoard(lev);
                    OnResize(form.ClientSize.Width, form.ClientSize.Height);

                    SetBoard(line, form);
                    GameMoves = gameMoves;
                    GameActive = true;
                    Pause = false;
                    form.ShowGameStatus();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    // Close the file
                    if (sr != null)
                    {
                        sr.Close();
                    }
                }
            }
        }


        /// <summary>
        /// Returns the moves to store the current game
        /// </summary>
        /// <returns></returns>
        public string GetMoves()
        {
            // Write the moves
            string sRow = "";
            foreach (Move m in m_moves)
            {
                if (m.m_disc == eDisc.eUpperDisc)
                {
                    sRow += "0";
                }
                else
                {
                    sRow += "1";
                }
                if (m.m_dir == eDirection.eRight)
                {
                    sRow += "0";
                }
                else
                {
                    sRow += "1";
                }
            }
            return sRow;
        }



        /// <summary>
        /// A Bitmap of the Board in original size
        /// </summary>
        /// <returns></returns>
        public Bitmap GetBoard()
        {
            int w = m_w;
            int h = m_h;

            OnResize(230, 300);

            Bitmap bmp = new Bitmap(UpperDisk);
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.CompositingMode = CompositingMode.SourceOver;
                CreateBackground(true);
                gfx.DrawImage(LowerDisk, new Point());
                gfx.DrawImage(Background, new Point());
            }

            OnResize(w, h);
            return bmp;
        }


        /// <summary>
        /// Save a board into a file either as text to restore the game or as a jpeg-picture.
        /// On the first line we save the level of the game and on the second 
        /// line all moves from the startpoint to the current state of the board.
        /// </summary>
        /// <param name="fName">The filename with the path</param>
        /// <param name="fTyp">Selected Filetype 1=enigma, 2=jpeg</param>
        public void SaveBoard(string fName, int fTyp)
        {
            if (fTyp == 2)
            {
                Bitmap bmp = new Bitmap(UpperDisk);
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    gfx.CompositingMode = CompositingMode.SourceOver;
                    CreateBackground(true);
                    gfx.DrawImage(LowerDisk, new Point());
                    gfx.DrawImage(Background, new Point());
                }

                bmp.Save(fName);
                CreateBackground(false);
                return;
            }

            try
            {
                // Open the file for writing
                StreamWriter sw = new StreamWriter(fName);

                // Write the level
                string sRow = "";
                sRow += m_lev.ToString();
                sw.WriteLine(sRow);

                // Game moves
                sRow = GameMoves.ToString();
                sw.WriteLine(sRow);

                // Time
                sRow = (DateTime.Now - StartTime).Ticks.ToString();
                sw.WriteLine(sRow);

                sRow = GetMoves();
                sw.WriteLine(sRow);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }


        /// <summary>
        /// Show sparkling colors on the board
        /// </summary>
        /// <param name="form"></param>
        public void ColorStones(Form form)
        {
            Random r = new Random();

            // Do the rotation of all bones and stones
            for (int s = 0; s < 20; s++)
            {
                for (int i = 0; i < 70; i++)
                {
                    Block.Blocks[i].Col = r.Next(0,7);
                }

                CreateBackground();
                CreateUpperDisk();
                CreateLowerDisk();
                form.Refresh();

                System.Threading.Thread.Sleep(1);
            }
        }

    }

}
