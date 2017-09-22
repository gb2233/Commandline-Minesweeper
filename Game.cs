using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commandline_Minesweeper
{
    class Game
    {
        static Random rnd = new Random();
        const int MINES = 20;
        const int XSIZE = 20;
        const int YSIZE = 10;
        int remainingMines = MINES;

        GameField[,] fields = new GameField[YSIZE, XSIZE];
        bool isOver = false;
        int xPos, yPos;

        public bool GameOver
        {
            get { return isOver; }
            set { isOver = value; }
        }
        public Game()
        {
            int x, y;
            yPos = YSIZE / 2; xPos = XSIZE / 2;
            
            for (y = 0; y < YSIZE; y++)
            {
                for (x = 0; x < XSIZE; x++)
                {
                    fields[y, x] = new GameField();
                }
            }
            
            int mines = MINES;
            while (mines > 0)
            {
                x = rnd.Next(0, XSIZE);
                y = rnd.Next(0, YSIZE);
                if (!fields[y, x].IsMined)
                {
                    fields[y, x].IsMined = true;
                    mines--;
                }
            }
            
            for (y = 0; y < YSIZE; y++)
            {
                for (x = 0; x < XSIZE; x++)
                {
                    fields[y, x].MinesNear = GetMinesNear(y, x);
                }
            }
        }
        
        private int GetMinesNear(int ypos, int xpos)
        {
            int mineNum = 0;
            int xmin = Math.Max(0, xpos - 1);
            int xmax = Math.Min(XSIZE - 1, xpos + 1);
            int ymin = Math.Max(0, ypos - 1);
            int ymax = Math.Min(YSIZE - 1, ypos + 1);
            for (int y = ymin; y <= ymax; y++)
            {
                for (int x = xmin; x <= xmax; x++)
                {
                    if (fields[y, x].IsMined) mineNum++;
                }
            }    
            return mineNum;
        }
        
        public void FlagPos()
        {
            if (fields[yPos, xPos].IsUnmarked && fields[yPos, xPos].IsFlagged)
            {
                fields[yPos, xPos].IsFlagged = false;
                remainingMines++;
            }
            else if (fields[yPos, xPos].IsUnmarked)
            {
                fields[yPos, xPos].IsFlagged = true;
                remainingMines--;
            }
        }
        
        public void Choose()
        {
            if (!fields[yPos, xPos].IsFlagged && fields[yPos, xPos].IsUnmarked)
            {
                RevealNear(yPos, xPos);
                fields[yPos, xPos].IsUnmarked = false;
                if (fields[yPos, xPos].IsMined)
                {
                    isOver = true;
                    remainingMines--;
                    fields[yPos, xPos].Uncover();
                }
            }
        }
        
        private void RevealNear(int y, int x)
        {
            if (y >= 0 && y < YSIZE && x >= 0 && x < XSIZE &&
                !fields[y, x].IsMined && !fields[y, x].IsFlagged && fields[y, x].IsUnmarked)
            {
                fields[y, x].IsUnmarked = false;
                if (fields[y, x].MinesNear == 0)
                {
                    RevealNear(y - 1, x);
                    RevealNear(y - 1, x - 1);
                    RevealNear(y - 1, x + 1);
                    RevealNear(y + 1, x);
                    RevealNear(y + 1, x - 1);
                    RevealNear(y + 1, x + 1);
                    RevealNear(y, x - 1);
                    RevealNear(y, x + 1);
                }
            }
        }
        
        public void Move(int dy, int dx)
        {
            xPos = (XSIZE + (xPos + dx)) % XSIZE; //"wrap around" lépegetés
            yPos = (YSIZE + (yPos + dy)) % YSIZE;
        }
        
        public string VisualizeGameField()
        {
            string stringField = "";
            int remaining = 0;
            for (int y = 0; y < YSIZE; y++)
            {
                for (int x = 0; x < XSIZE; x++)
                {
                    if (!fields[y, x].IsMined && fields[y, x].IsUnmarked)
                       remaining++;
                    string current = fields[y, x].Uncover();
                    if(yPos == y && xPos == x)
                       stringField += "{" + current + "}";
                    else
                        stringField += " " + current + " ";
                }
                stringField += "\r\n";
            }
            stringField = stringField.Insert(0, "Remaining Mines: " + remainingMines + "\n\n\n");
            if (remaining == 0) stringField += "\n Y O U  W O N!";
            stringField += "\r\n";
            return stringField;
        }
        public bool IsGameOver()
        {
            if (isOver) return true;
            for (int y = 0; y < YSIZE; y++)
            {
                for (int x = 0; x < XSIZE; x++)
                {
                    if (fields[y, x].IsUnmarked && !fields[y, x].IsMined)
                        return false;
                }
            }
            return true;
        }
    }
}
