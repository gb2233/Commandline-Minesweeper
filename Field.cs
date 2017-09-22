using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commandline_Minesweeper
{
    class GameField
    {
        bool isFlagged;
        bool isMined;
        bool isUnmarked;
        int minesNear;

        public int MinesNear
        {
            get { return minesNear; }
            set { minesNear = value; }
        }

        public bool IsUnmarked
        {
            get { return isUnmarked; }
            set { isUnmarked = value; }
        }
        public bool IsMined
        {
            get { return isMined; }
            set { isMined = value; }
        }
        public bool IsFlagged
        {
            get { return isFlagged; }
            set { isFlagged = value; }
        }

        public GameField()
        {
            isUnmarked = true;
        }
        
        public string Uncover()
        {
            if (!isUnmarked && isMined) return "X";
            if (!isUnmarked && !isMined) return minesNear.ToString();
            if (isFlagged) return "!";
            else return "-";
        }
    }
}
