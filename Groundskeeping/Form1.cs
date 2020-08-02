using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Groundskeeping
{
    public partial class GroundsKeeping : Form
    {
        /*=====================================
         *           DEFINITIONS
         *====================================*/

        //positions of the black squares (notice 3 is not there, as the third block is white)
        // the numbers go from left to right, then top to bottom
        int[] WallsLvl1 = {
            1,2,4,6,9,10,
            14,18,19,
            22,23,24,26,28,29,
            36,37,38,
            41,42,43,44,47,48,50,
            51,60,
            62,63,65,66,67,68,
            72,76,77,80,
            82,84,86,87,88,90,
            94,100
        };

        int[] WallsLvl2 =
        {
           1,2,7,8,9,10,
           13,15,16,20,
           22,23,28,30,
           32,35,37,38,40,
           42,44,47,
           54,57,59,
           61,63,64,66,69,
           71,73,78,79,
           81,85,86,88,
           91,92,93,94,99,100
        };

        int[] WallsLvl3 =
        {

            12,13,14,15,16,17,18,19,
            22,29,
            32,34,35,36,37,39,
            42,44,47,49,
            52,54,57,59,
            62,64,65,67,69,
            72,77,79,
            82,83,84,85,86,87,89,
            99
        };

        int[] WallsLvl4 =
        {
            1,7,8,9,10,
            11,13,14,15,16,17,18,19,20,
            21,
            31,32,33,36,37,39,
            41,42,43,47,49,
            51,52,59,
            61,62,64,66,67,69,
            71,72,74,77,79,
            81,84,85,87,88,89,
            91,92,95
        };

        int[] WallsLvl5 =
        {
            1,2,3,4,5,6,7,8,9,10,
            11,20,
            21,23,24,27,28,30,
            31,33,38,40,
            43,48,50,
            53,58,60,
            61,63,68,70,
            71,73,74,75,76,77,78,80,
            81,90,
            91,92,93,94,95,96,97,98,99,100
        };

        int[] WallsLvl6 =
        {
            1,6,10,
            14,18,
            22,23,24,26,27,28,29,30,
            32,33,34,36,
            43,44,
            53,54,57,58,59,60,
            63,64,69,
            73,74,75,76,77,79,
            82,83,84,87,88,89

        };

        int[] WallsTutorial =
        {
            2,3,4,5,6,7,8,9,10,
            11,12,13,14,15,16,17,18,19,20,
            21,22,23,24,25,26,27,28,29,30,
            31,32,33,34,35,36,37,38,39,40,
            41,50,
            51,60,
            61,62,63,64,65,66,67,68,69,70,
            71,72,73,74,75,76,77,78,79,80,
            81,82,83,84,85,86,87,88,89,90,
            91,92,93,94,95,96,97,98,99,100
        };

        int[] currentWalls;

        //LISTS
        List<int> currentLeaves = new List<int>();

        List<PictureBox> leafPictures = new List<PictureBox>();

        int cellSize = 70;
        int timeElapsed = 0;

        bool isTutorial = false;
        bool isGameWon = false;

        int startLeavesCount = 0;
        int leavesCollected = 0;
        int leavesRemaining = 0;

        int finalTime = 0;

        int currentLevel = 0;

        bool keyUpIsDown = false;
        bool keyDownIsDown = false;
        bool keyRightIsDown = false;
        bool keyLeftIsDown = false;

        /*=================================
         *     SETTING UP ON 'START'
         *================================*/

        public GroundsKeeping()
        {
            InitializeComponent();

            //Levels and Avatar
            picLvl1.Visible = false;
            picLvl2.Visible = false;
            picLvl3.Visible = false;
            picLvl4.Visible = false;
            picLvl5.Visible = false;
            picLvl6.Visible = false;
            picTutorial.Visible = false;
            picAvatar.Visible = false;

            //side walls
            picRightSideWall.Visible = false;
            picTopSideWall.Visible = false;
            picLeftSideWall.Visible = false;
            picBottomSideWall.Visible = false;

            //Titles, buttons and sample images
            picTitle.Visible = true;
            picDisclaimer.Visible = true;

            picTutorialLabel.Visible = true;
            btnLvl1.Visible = true;
            btnLvl2.Visible = true;
            btnLvl3.Visible = true;
            btnLvl4.Visible = true;
            btnLvl5.Visible = true;
            btnLvl6.Visible = true;
            btnTutorial.Visible = true;
            picLvl1Sample.Visible = true;
            picLvl2Sample.Visible = true;
            picLvl3Sample.Visible = true;
            picLvl4Sample.Visible = true;
            picLvl5Sample.Visible = true;
            picLvl6Sample.Visible = true;
            picLeaf.Visible = false;

            picYouWin.Visible = false;
            btnReturn.Visible = false;

            //tutorial labels
            lblTutorial1.Visible = false;
            lblTutorial2.Visible = false;
            lblFinalScore.Visible = false;
            lblTutorial1.Text = "...";
            lblTutorial2.Text = "...";
            lblFinalScore.Text = "...";

            //timer & leaves collected
            label1.Visible = false;
            lblTimeElapsed.Visible = false;
            tmrSeconds.Enabled = false;
            timeElapsed = 0;

            label2.Visible = false;
            lblLeavesCollected.Visible = false;

            picLeafSample.Visible = false;
            picGroundsKeeperSample.Visible = false;
            picRingSample.Visible = false;
            picFrodoSample.Visible = false;
            picAangSample.Visible = false;
            picFireNationSample.Visible = false;
            picMillenniumFalconSample.Visible = false;
            picTieFighterSample.Visible = false;

            lblVersion.Visible = true;

        }

        /*========================================
         *          PLACING THE LEAVES
         *=======================================*/

        private void createCurrentLeaves()
        {
            Random random = new Random();

            currentLeaves.Clear();
            leafPictures.Clear();

            int i = 1;
            while(i <= 100)
            {

                //EXCLUDES 4 CENTRE CELLS (SPAWNING POINT)
                if(i == 55 || i == 56 || i ==45 || i == 46)
                {
                    i++;
                    continue;
                }

                if (!currentWalls.Contains(i))
                {
                    currentLeaves.Add(i);
                    var x = (i % 10) - 1;
                    if (x < 0)
                    {
                        x = 9;
                    }
                    x = (x * cellSize);

                    var y = (i / 10);
                    if (i % 10 == 0)
                    {
                        y--;
                    }
                    y = y * cellSize;

                    //getting leaves to a random poin near the centre of each cell
                    x += (cellSize - picLeaf.Width) / 2;
                    y += (cellSize - picLeaf.Height) / 2;

                    x += random.Next(20) - 10;
                    y += random.Next(20) - 10;


                    var leaf = new PictureBox
                    {
                        Name = "leaf" + i,
                        Size = picLeaf.Size,
                        Location = new Point(x, y),
                        Image = picLeaf.Image
                    };

 
                    this.Controls.Add(leaf);
                    leaf.BringToFront();
                    picAvatar.BringToFront();

                    //stopping the avatar going over the black border lines
                    picRightSideWall.BringToFront();
                    picLeftSideWall.BringToFront();
                    picTopSideWall.BringToFront();
                    picBottomSideWall.BringToFront();

                    leafPictures.Add(leaf);
                    leavesRemaining++;
                }

                i++;

            }
            startLeavesCount = leavesRemaining;
            lblLeavesCollected.Text = "0 / " + startLeavesCount;
        }

      

        /*==========================================================
         *          PREPARING LEVELS ON ANY BUTTON PRESS
         *=========================================================*/

        private void PrepareLevels()
        {
            //prepares level/s for play

            //levels 1, 2, and 3 all have the same 'hedge-leaf' theme.
            if (currentLevel == 1 || currentLevel == 2 || currentLevel == 3)
            {
                picLeaf.Image = picLeafSample.Image;
                picAvatar.Image = picGroundsKeeperSample.Image;

                label2.Font = new Font("NSimSun", 14, FontStyle.Bold);
                label2.Text = "LEAVES COLLECTED";

                MessageBox.Show("Welcome to GROUNDSKEEPING! You are the Grounds Keeper for the Mayor's manor, and" +
               " while he's away he wants you to clean the paths of his hedge maze. He has said to you that the" +
               " quicker you can do this job for him, the more you'll get payed.\n \n" + "Pick up all the leaves" +
               " to complete the maze. Try to beat your personal best scores or beat a friend's!\n \n" + "And remember;" +
               " the Mayor is relying on you! \n \n" + "After you press OK, the timer will start ticking immediately" +
               " (move with arrow keys)");

                isTutorial = false;
            }
            if (currentLevel == 4)
            {
                picLeaf.Image = picRingSample.Image;
                picAvatar.Image = picFrodoSample.Image;

                label2.Font = new Font("NSimSun", 14, FontStyle.Bold);
                label2.Text = "RINGS COLLECTED";

                MessageBox.Show("Welcome to MORDOR... You and a group of Elves, Dwarves, Hobbits, and Men have finally made it to Mordor, where you need to destroy the rings of Sauron. But lo! Your fellow travellers have been captured by the Uruk-Hai and it's up to you to find the rings that they dropped!" +
               "\n \n" + "Pick up all the rings" +
               " to defeat Sauron once and for all. Try to beat your personal best scores or beat a friend's!\n \n" + "And remember;" +
               " the Fellowship - and the rest of Middle Earth are relying on you! \n \n" + "After you press OK, the timer will start ticking immediately" +
               " (move with arrow keys)");

                isTutorial = false;
            }
            if (currentLevel == 5)
            {
                picLeaf.Image = picFireNationSample.Image;
                picAvatar.Image = picAangSample.Image;

                label2.Font = new Font("NSimSun", 12, FontStyle.Bold);
                label2.Text = "SOLDIERS DEFEATED";

                MessageBox.Show("Welcome to Ba-Sing-Se! The town is under seige by the Fire Nation and only the Avatar - master of all four elements can stop them." +
               "\n \n" + "Find all the Fire Nation Soldiers and imprison them" +
               " to save the city of Ba-Sing-Se. Try to beat your personal best scores or beat a friend's!\n \n" + "And remember;" +
               " the civilians need you, Avatar! \n \n" + "After you press OK, the timer will start ticking immediately" +
               " (move with arrow keys)");

                isTutorial = false;
            }
            if (currentLevel == 6)
            {
                picLeaf.Image = picTieFighterSample.Image;
                picAvatar.Image = picMillenniumFalconSample.Image;

                label2.Font = new Font("NSimSun", 14, FontStyle.Bold);
                label2.Text = "SHIPS DESTROYED";

                MessageBox.Show("Solo! You are the last ship left that's big enough to take out Tie Fighters, and the Imperial Fleet have them around every corner! We need you to traverse the asteroids and take out the Tie Fighters." +
               "\n \n" + "Destroy all the Tie Fighters" +
               " to Further the Rebellion's chances of success. Try to beat your personal best scores or beat a friend's!\n \n" + "And remember;" +
               " the Rebellion is counting on you. Youre our last hope. \n \n" + "After you press OK, the timer will start ticking immediately" +
               " (move with arrow keys)");

                isTutorial = false;
            }
            if (currentLevel == 7)
            {
                picLeaf.Image = picLeafSample.Image;
                picAvatar.Image = picGroundsKeeperSample.Image;
                label2.Text = "LEAVES COLLECTED";
            }

            btnLvl1.Visible = false;
            btnLvl2.Visible = false;
            btnLvl3.Visible = false;
            btnLvl4.Visible = false;
            btnLvl5.Visible = false;
            btnLvl6.Visible = false;
            btnTutorial.Visible = false;
            picLvl1Sample.Visible = false;
            picLvl2Sample.Visible = false;
            picLvl3Sample.Visible = false;
            picLvl4Sample.Visible = false;
            picLvl5Sample.Visible = false;
            picLvl6Sample.Visible = false;

            picRightSideWall.Visible = true;
            picTopSideWall.Visible = true;
            picLeftSideWall.Visible = true;
            picBottomSideWall.Visible = true;

            picTitle.Visible = false;
            picDisclaimer.Visible = false;
            picTutorialLabel.Visible = false;

            tmrSeconds.Enabled = true;
            label1.Visible = true;
            lblTimeElapsed.Visible = true;
            label2.Visible = true;
            lblLeavesCollected.Visible = true;

            lblVersion.Visible = false;

            //sets avatar's location to the centre of the maze, and makes it visible
            picAvatar.Left = picLvl1.Left + (picLvl1.Width / 2) - (picAvatar.Width / 2);
            picAvatar.Top = picLvl1.Top + (picLvl1.Height / 2) - (picAvatar.Height / 2);
            picAvatar.Visible = true;
        }
        
        /*==========================================
         *               BUTTONS
         *========================================*/
        //LEVEL 1
        private void btnLvl1_Click(object sender, EventArgs e)
        {
            Lvl1Start();
        }
        private void Lvl1Start()
        {  
            currentLevel = 1;
            PrepareLevels();
            picLvl1.Visible = true;
            currentWalls = WallsLvl1;
            createCurrentLeaves();
        }

        //LEVEL 2
        private void btnLvl2_Click(object sender, EventArgs e)
        {
            Lvl2Start();
        }
        private void Lvl2Start()
        {            
            currentLevel = 2;
            PrepareLevels();
            picLvl2.Visible = true;
            currentWalls = WallsLvl2;
            createCurrentLeaves();
        }

        //LEVEL 3
        private void btnLvl3_Click(object sender, EventArgs e)
        {
            Lvl3Start();
        }
        private void Lvl3Start()
        {
            currentLevel = 3;
            PrepareLevels();
            picLvl3.Visible = true;
            currentWalls = WallsLvl3;
            createCurrentLeaves();
        }

        //LEVEL 4 (MORDOR)
        private void btnLvl4_Click(object sender, EventArgs e)
        {
            Lvl4Start();
        }
        private void Lvl4Start()
        {
            currentLevel = 4;
            PrepareLevels();
            picLvl4.Visible = true;
            currentWalls = WallsLvl4;
            createCurrentLeaves();
        }

        //LEVEL 5 (BAH-SING-SEH)
        private void btnLvl5_Click(object sender, EventArgs e)
        {
            Lvl5Start();
        }
        private void Lvl5Start()
        {
            currentLevel = 5;
            PrepareLevels();
            picLvl5.Visible = true;
            currentWalls = WallsLvl5;
            createCurrentLeaves();
        }

        //LEVEL 6 (STARWARS)
        private void btnLvl6_Click(object sender, EventArgs e)
        {
            Lvl6Start();
        }
        private void Lvl6Start()
        {
            currentLevel = 6;
            PrepareLevels();
            picLvl6.Visible = true;
            currentWalls = WallsLvl6;
            createCurrentLeaves();
        }


        //TUTORIAL
        private void btnTutorial_Click(object sender, EventArgs e)
        {
            currentLevel = 7;
            PrepareLevels();
            picTutorial.Visible = true;
            currentWalls = WallsTutorial;
            createCurrentLeaves();

            isTutorial = true;

            lblTutorial1.Visible = true;
            lblTutorial2.Visible = true;
        }

        /*================================================
         *         TIME ELAPSED & TUTORIAL TEXT
         *===============================================*/
        private void tmrSeconds_Tick(object sender, EventArgs e)
        {
            //displaying the time elapsed
            timeElapsed += 1;
            lblTimeElapsed.Text = timeElapsed + "s";
            
            //timer flashes red if time elapsed is higher than 50 seconds
            if(timeElapsed > 50 && timeElapsed % 2 == 1)
            {
                lblTimeElapsed.ForeColor = System.Drawing.Color.Red;
                label1.ForeColor = System.Drawing.Color.Red;
            }
            if (timeElapsed > 50 && timeElapsed % 2 == 0)
            {
                lblTimeElapsed.ForeColor = System.Drawing.Color.Black;
                label1.ForeColor = System.Drawing.Color.Black;
            }

            //text for tutorial level
            if(isTutorial == true)
            {
                label1.Visible = false;
                lblTimeElapsed.Visible = false;
                label2.Visible = false;
                lblLeavesCollected.Visible = false;

                lblTutorial1.BringToFront();
                lblTutorial2.BringToFront();

                if (timeElapsed == 1)
                {
                    lblTutorial1.Text = "Welcome to GROUNDSKEEPING!";
                    lblTutorial2.Text = ":)";
                }
                if(timeElapsed == 4)
                {
                    lblTutorial1.Text = "Use the arrow keys to move your avatar.";
                    lblTutorial2.Text = "(Grounds Keeper's hat)";
                }
                if(timeElapsed == 8)
                {
                    lblTutorial1.Text = "Your goal is to collect all the leaves";
                    lblTutorial2.Text = "in the shortest time possible.";
                }
                if(timeElapsed == 12)
                {                 
                    lblTutorial2.Font = new Font("Microsoft Sans Seriff", 16);
                    lblTutorial1.Text = "You collect a leaf when you collide with it.";
                    lblTutorial2.Text = "(Note the leaves' and Avatar's square bounding boxes)";
                }
                if (timeElapsed == 15)
                {
                    lblTutorial2.Font = new Font("Microsoft Sans Seriff", 18);
                    lblTutorial1.Text = "Go ahead!";
                    lblTutorial2.Text = "Get them all!";
                }
                if(timeElapsed == 18)
                {
                    lblTutorial1.Text = "Well I suppose you can't quite reach";
                    lblTutorial2.Text = "that one in the corner...";
                }
                if(timeElapsed == 22)
                {
                    lblTutorial1.Text = "But atleast you know how to play now!";
                    lblTutorial2.Text = "USE BUTTON TO RETURN TO LEVEL SELECTION";
                }
                if(timeElapsed >= 24 && leavesRemaining == 1)
                {
                    btnReturn.Visible = true;
                    this.Controls.Remove(leafPictures.ElementAt(0));
                    currentLeaves.Remove(0);
                    startLeavesCount = 0;

                    picAvatar.Visible = false;
                    picTutorial.Visible = false;
                    lblTutorial1.Visible = false;
                    lblTutorial2.Visible = false;

                    picRightSideWall.Visible = false;
                    picLeftSideWall.Visible = false;
                    picTopSideWall.Visible = false;
                    picBottomSideWall.Visible = false;
                };
            }

            if(isGameWon)
            {
                if(timeElapsed <= 1)
                {
                    lblFinalScore.Visible = true;
                    lblFinalScore.Text = "...";
                }
                if(timeElapsed >= 1)
                {
                    if(currentLevel == 1)
                    {
                        lblFinalScore.Text = "YOU COLLECTED ALL THE LEAVES IN: " + finalTime + " SECONDS ON LEVEL 1!";
                    }
                    if (currentLevel == 2)
                    {
                        lblFinalScore.Text = "YOU COLLECTED ALL THE LEAVES IN: " + finalTime + " SECONDS ON LEVEL 2!";
                    }
                    if (currentLevel == 3)
                    {
                        lblFinalScore.Text = "YOU COLLECTED ALL THE LEAVES IN: " + finalTime + " SECONDS ON LEVEL 3!";
                    }
                    if(currentLevel == 4)
                    {
                        lblFinalScore.Text = "YOU COLLECTED ALL THE RINGS IN: " + finalTime + " SECONDS!\nSAURON CAN FINALLY BE DESOLATED!";
                    }
                    if (currentLevel == 5)
                    {
                        lblFinalScore.Text = "YOU DEFEATED ALL THE SOLDIERS IN: " + finalTime + " SECONDS!\nBA-SING-SE CAN LIVE TO SEE ANOTHER DAY!";
                    }
                    if (currentLevel == 6)
                    {
                        lblFinalScore.Text = "YOU DESTROYED ALL THE TIE FIGHTERS IN: " + finalTime + " SECONDS!\nGREAT WORK, SOLO!";
                    }

                    btnReturn.Visible = true;
                }
            }
        }

        /*===============================================
         *             MOVEMENT OF AVATAR
         * =============================================*/

        private void GroundsKeeping_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                keyUpIsDown = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                keyDownIsDown = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                keyRightIsDown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                keyLeftIsDown = false;
            }
        }

        private void GroundsKeeping_KeyDown(object sender, KeyEventArgs e)
        {
            //Controls movement with arrowkeys
            if (e.KeyCode == Keys.Up)
            {
                keyUpIsDown = true;
            }
            if(e.KeyCode == Keys.Down)
            {
                keyDownIsDown = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                keyRightIsDown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                keyLeftIsDown = true;
            }
        }

        private void tmrMovement_Tick(object sender, EventArgs e)
        {
            if (keyUpIsDown)
            {
                MoveUp();
            }
            if (keyDownIsDown)
            {
                MoveDown();
            }
            if (keyRightIsDown)
            {
                MoveRight();
            }
            if (keyLeftIsDown)
            {
                MoveLeft();
            }
        }

        private void MoveUp()
        {
            var newPos = picAvatar.Top - 2;
            var column1 = 0;
            var column2 = 0;
            var wallBottom = 0;

            // CHECKING IF THIS MOVE IS VALID (newPos)
            if(newPos < 0)
            {
                return;
            }

            //finds out the column that the left and right side of the avatar is in
            column1 = (picAvatar.Left / cellSize) + 1;
            column2 = (picAvatar.Right / cellSize) + 1;

            //checking if this move will collide with a wall block above the avatar's top
            foreach(int cell in currentWalls)
            {
                var column = cell % 10;
                if(column == 0)
                {
                    column = 10;
                }
                
                if(column == column1 || column == column2)
                {
                    wallBottom = cellSize * ((cell / 10) + 1);

                    if (column == 10)
                    {
                        wallBottom -= cellSize;
                    }

                    if (newPos < wallBottom && newPos + picAvatar.Height > wallBottom)
                    {
                        return;
                    }
                }
            }

            //IF THIS MOVE IS VALID, EXECUTE
            picAvatar.Top -= 2;

            //CHECK FOR LEAF COLLISION
            int i = 0;

            foreach(int leafCell in currentLeaves)
            {
                i++;

                var column = leafCell % 10;
                if (column == 0)
                {
                    column = 10;
                }

                if(column == column1 || column == column2)
                {
                    var leaf = leafPictures.ElementAt(i - 1);
                    if (leaf.Visible && picAvatar.Bounds.IntersectsWith(leaf.Bounds))
                    {
                        leaf.Visible = false;
                        leavesRemaining--;
                        leavesCollected++;
                        lblLeavesCollected.Text = leavesCollected + " / " + startLeavesCount;

                        //CHECKING IF ALL LEAVES HAVE BEEN COLLECTED
                        if(leavesCollected == startLeavesCount)
                        {
                            gameWon();
                        }
                    }
                }
            }

            
        }

        private void MoveDown()
        {
            var newPos = picAvatar.Bottom + 2;
            var column1 = 0;
            var column2 = 0;
            var wallTop = 0;

            // CHECKING IF THIS MOVE IS VALID (newPos)
            if (newPos > picLvl1.Top + picLvl1.Height)
            {
                return;
            }

            //finds out the column that the left and right side of the avatar is in
            column1 = (picAvatar.Left / cellSize) + 1;
            column2 = (picAvatar.Right / cellSize) + 1;

            //checking if this move will collide with a wall block above the avatar's top
            foreach (int cell in currentWalls)
            {
                var column = (cell % 10);
                if(column == 0)
                {
                    column = 10;
                }

                if (column == column1 || column == column2)
                {
                    wallTop = cellSize * (cell / 10);
                    if (column == 10)
                    {
                        wallTop -= cellSize;
                    }
                   
                    if (newPos > wallTop && newPos - picAvatar.Height < wallTop)
                    {
                        return;
                    }
                }
            }

            //IF THIS MOVE IS VALID, EXECUTE
            picAvatar.Top += 2;

            int i = 0;

            foreach (int leafCell in currentLeaves)
            {
                i++;

                var column = leafCell % 10;
                if (column == 0)
                {
                    column = 10;
                }

                if (column == column1 || column == column2)
                {
                    var leaf = leafPictures.ElementAt(i - 1);
                    if (leaf.Visible && picAvatar.Bounds.IntersectsWith(leaf.Bounds))
                    {
                        leaf.Visible = false;
                        leavesRemaining--;
                        leavesCollected++;
                        lblLeavesCollected.Text = leavesCollected + " / " + startLeavesCount;

                        //CHECKING IF ALL LEAVES HAVE BEEN COLLECTED
                        if (leavesCollected == startLeavesCount)
                        {
                            gameWon();
                        }
                    }
                }
            }
        }

        private void MoveLeft()
        {
            var newPos = picAvatar.Left - 2;
            var row1 = 0;
            var row2 = 0;
            var wallRight = 0;

            // CHECKING IF THIS MOVE IS VALID (newPos)
            if (newPos < 0)
            {
                return;
            }

            //finds out the column that the left and right side of the avatar is in
            row1 = (picAvatar.Top / cellSize) + 1;
            row2 = ((picAvatar.Bottom - 1) / cellSize) + 1;
            //checking if this move will collide with a wall block above the avatar's top
            foreach (int cell in currentWalls)
            {
                var row = (cell / 10) + 1;
                if (cell % 10 == 0)
                {
                    row--;
                }
                if (row == row1 || row == row2)
                {
                    if (cell % 10 == 0)
                    {
                        wallRight = cellSize * 9;
                    }
                    else
                    {
                        wallRight = cellSize * ((cell % 10));
                    }

                    if (newPos < wallRight && newPos + picAvatar.Width > wallRight)
                    {
                        return;
                    }
                }
            }

            //IF THIS MOVE IS VALID, EXECUTE
            picAvatar.Left -= 2;

            int i = 0;

            foreach (int leafCell in currentLeaves)
            {
                i++;

                var row = (leafCell / 10) + 1;
                if (leafCell % 10 == 0)
                {
                    row--;
                }

                if (row == row1 || row == row2)
                {
                    var leaf = leafPictures.ElementAt(i - 1);
                    if (leaf.Visible && picAvatar.Bounds.IntersectsWith(leaf.Bounds))
                    {
                        leaf.Visible = false;
                        leavesRemaining--;
                        leavesCollected++;
                        lblLeavesCollected.Text = leavesCollected + " / " + startLeavesCount;

                        //CHECKING IF ALL LEAVES HAVE BEEN COLLECTED
                        if (leavesCollected == startLeavesCount)
                        {
                            gameWon();
                        }
                    }
                }
            }


        }

        private void MoveRight()
        {
            var newPos = picAvatar.Right + 2;
            var row1 = 0;
            var row2 = 0;
            var wallLeft = 0;

            // CHECKING IF THIS MOVE IS VALID (newPos)
            if (newPos > picLvl1.Width)
            {
                return;
            }

            //finds out the column that the left and right side of the avatar is in
            row1 = (picAvatar.Top / cellSize) + 1;
            row2 = ((picAvatar.Bottom - 1) / cellSize) + 1;

            //checking if this move will collide with a wall block above the avatar's top
            foreach (int cell in currentWalls)
            {
                var row = (cell / 10) + 1;
                if (cell % 10 == 0)
                {
                    row--;
                }
                if (row == row1 || row == row2)
                {
                    if(cell % 10 == 0)
                    {
                        wallLeft = cellSize * 9;
                    }
                    else
                    {
                        wallLeft = cellSize * ((cell % 10) - 1);
                    }
                   
                    if (newPos > wallLeft && newPos - picAvatar.Width < wallLeft)
                    {
                        return;
                    }
                }
            }

            //IF THIS MOVE IS VALID, EXECUTE
            picAvatar.Left += 2;

            int i = 0;

            foreach (int leafCell in currentLeaves)
            {
                i++;

                var row = (leafCell / 10) + 1;
                if (leafCell % 10 == 0)
                {
                    row--;
                }

                if (row == row1 || row == row2)
                {
                    var leaf = leafPictures.ElementAt(i - 1);
                    if (leaf.Visible && picAvatar.Bounds.IntersectsWith(leaf.Bounds))
                    {
                        leaf.Visible = false;
                        leavesRemaining--;
                        leavesCollected++;
                        lblLeavesCollected.Text = leavesCollected + " / " + startLeavesCount;

                        //CHECKING IF ALL LEAVES HAVE BEEN COLLECTED
                        if (leavesCollected == startLeavesCount)
                        {
                            gameWon();
                        }
                    }
                }
            }
        }

        private void gameWon()
        {
            finalTime = timeElapsed;
            timeElapsed = 0;

            picYouWin.Visible = true;

            //Levels and Avatar
            picLvl1.Visible = false;
            picLvl2.Visible = false;
            picLvl3.Visible = false;
            picLvl4.Visible = false;
            picLvl5.Visible = false;
            picLvl6.Visible = false;
            picTutorial.Visible = false;
            picAvatar.Visible = false;

            //side walls
            picRightSideWall.Visible = false;
            picTopSideWall.Visible = false;
            picLeftSideWall.Visible = false;
            picBottomSideWall.Visible = false;

            //timer & leaves collected
            label1.Visible = false;
            lblTimeElapsed.Visible = false;

            label2.Visible = false;
            lblLeavesCollected.Visible = false;

            //see tmr.Tick
            isGameWon = true;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            currentLeaves.Clear();
            leafPictures.Clear();
            startLeavesCount = 0;
            leavesRemaining = 0;

            //Levels and Avatar
            picLvl1.Visible = false;
            picLvl2.Visible = false;
            picLvl3.Visible = false;
            picLvl4.Visible = false;
            picLvl5.Visible = false;
            picLvl6.Visible = false;
            picTutorial.Visible = false;
            picAvatar.Visible = false;

            //side walls
            picRightSideWall.Visible = false;
            picTopSideWall.Visible = false;
            picLeftSideWall.Visible = false;
            picBottomSideWall.Visible = false;

            //Titles, buttons and sample images
            picTitle.Visible = true;
            picDisclaimer.Visible = true;

            picTutorialLabel.Visible = true;
            btnLvl1.Visible = true;
            btnLvl2.Visible = true;
            btnLvl3.Visible = true;
            btnLvl4.Visible = true;
            btnLvl5.Visible = true;
            btnLvl6.Visible = true;
            btnTutorial.Visible = true;
            picLvl1Sample.Visible = true;
            picLvl2Sample.Visible = true;
            picLvl3Sample.Visible = true;
            picLvl4Sample.Visible = true;
            picLvl5Sample.Visible = true;
            picLvl6Sample.Visible = true;
            picLeaf.Visible = false;

            picYouWin.Visible = false;
            btnReturn.Visible = false;

            //tutorial labels
            lblTutorial1.Visible = false;
            lblTutorial2.Visible = false;
            lblFinalScore.Visible = false;
            lblTutorial1.Text = "...";
            lblTutorial2.Text = "...";
            lblFinalScore.Text = "...";

            //timer & leaves collected
            label1.Visible = false;
            lblTimeElapsed.Visible = false;
            tmrSeconds.Enabled = false;
            timeElapsed = 0;

            label2.Visible = false;
            lblLeavesCollected.Visible = false;
            lblVersion.Visible = true;

            isGameWon = false;
            isTutorial = false;
            
            lblTimeElapsed.ForeColor = System.Drawing.Color.Black;
            label1.ForeColor = System.Drawing.Color.Black;

            leavesCollected = 0;
        }
    }
}
