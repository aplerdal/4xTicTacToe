using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace megaTicTacToeSolver
{
    class Board
    {
        private string[] output = new string[3] { "X", "O", " "};
        public uint[,] data;
        public uint move;
        public bool tested;
        public uint iteration;
        public Board()
        {
            iteration = 0;
            move = (uint)Move.X;
            data = new uint[4, 4];
            Random random = new Random();
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Array values = Enum.GetValues(typeof(Piece));
                    Piece piece = (Piece)values.GetValue(random.Next(values.Length));
                    data[x, y] = (uint)piece;
                }
            }
        }
        public Board(uint[,] b)
        {
            iteration = 0;
            data = b;
            move = (uint)Move.X;
        }
        public Board(uint[,] b, uint move, uint iteration)
        {
            this.iteration = iteration;
            data = b;
            this.move = move;
        }
        public void PrintBoard()
        {
            Console.WriteLine($" {output[data[0, 0]]} | {output[data[1,0]]} | {output[data[2, 0]]} | {output[data[3, 0]]} ");
            Console.WriteLine("---+---+---+---");
            Console.WriteLine($" {output[data[0, 1]]} | {output[data[1,1]]} | {output[data[2, 1]]} | {output[data[3, 1]]} ");
            Console.WriteLine("---+---+---+---");
            Console.WriteLine($" {output[data[0, 2]]} | {output[data[1,2]]} | {output[data[2, 2]]} | {output[data[3, 2]]} ");
            Console.WriteLine("---+---+---+---");
            Console.WriteLine($" {output[data[0, 3]]} | {output[data[1,3]]} | {output[data[2, 3]]} | {output[data[3, 3]]} ");
        }
    }
    class StorageBoard
    {
        public uint packedBoard;
        
        public StorageBoard(Board board)
        {
            uint temp = 0;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    Console.WriteLine($"piece:{board.data[x, y]}, x,y:{x},{y}");
                    temp |= board.data[x, y] << 30 - (x * 2 + y * 8);
                }
            }
            Console.WriteLine(temp);
            packedBoard = temp;
        }
        public Board toBoard()
        {
            uint bitmask = 0b11000000000000000000000000000000;
            uint[,] data = new uint[4, 4];
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    data[x,y] = (bitmask & packedBoard) >> 30 - (x * 2 + y * 8);

                    bitmask = bitmask >> 2;
                }
            }
            return new Board(data);
        }
    }
    enum Move{
        X = 0,
        O = 1,
    }
    enum Piece
    {
        X    = 0,
        O    = 1,
        None = 2,
    }
}
