using System;
using System.Collections.ObjectModel;

namespace megaTicTacToeSolver
{
    class Solver{
        private static readonly int boardSize = 4;
        public LinkedList<uint> traversedBoards = new LinkedList<uint>();
        public void PossibleMoves(TreeNode<Board> board, uint iterations){
            for (int y = 0; y<board.Value.data.GetLength(0); y++){
                for (int x = 0; x<board.Value.data.GetLength(1); x++){
                    if ((x+1)<board.Value.data.GetLength(0)) {
                        if (board.Value.data[x+1,y]==(uint)Piece.None & (board.Value.data[x,y] == board.Value.move)) {
                            Board tempBoard = new Board((uint[,])board.Value.data.Clone(), board.Value.move, iterations);
                            tempBoard.data[x,y] = (uint)Piece.None;
                            tempBoard.data[x+1,y] = tempBoard.move;
                            if (tempBoard.move == 0) { tempBoard.move = 1; } else { tempBoard.move = 0; }
                            if (CheckWin(tempBoard, x + 1, y)) { tempBoard.won = board.Value.move; }
                            if (!traversedBoards.Contains(tempBoard.toStorageBoard().packedBoard))
                            {
                                //Console.WriteLine($"Found move from {x},{y} to {x+1},{y}");
                                board.AddChild(tempBoard);
                            }
                        }
                    }
                    if ((x-1)>-1) {
                        if (board.Value.data[x-1,y]==(uint)Piece.None & (board.Value.data[x,y] == board.Value.move)){
                            Board tempBoard = new Board((uint[,])board.Value.data.Clone(), board.Value.move, iterations);
                            tempBoard.data[x,y] = (uint)Piece.None;
                            tempBoard.data[x-1,y] = tempBoard.move;
                            if (tempBoard.move == 0) { tempBoard.move = 1; } else { tempBoard.move = 0; }
                            if (CheckWin(tempBoard, x - 1, y)) { tempBoard.won = board.Value.move; }
                            if (!traversedBoards.Contains((new StorageBoard(tempBoard)).packedBoard))
                            {
                                //Console.WriteLine($"Found move from {x},{y} to {x-1},{y}");
                                board.AddChild(tempBoard);
                            }
                        }
                    }
                    if ((y+1)<board.Value.data.GetLength(1)) {
                        if (board.Value.data[x,y+1]==(uint)Piece.None  & (board.Value.data[x,y] == board.Value.move)) {
                            Board tempBoard = new Board((uint[,])board.Value.data.Clone(), board.Value.move, iterations);
                            tempBoard.data[x,y] = (uint)Piece.None;
                            tempBoard.data[x,y+1] = tempBoard.move;
                            if (tempBoard.move == 0) { tempBoard.move = 1; } else { tempBoard.move = 0; }
                            if (CheckWin(tempBoard, x, y + 1)) { tempBoard.won = board.Value.move; }
                            if (!traversedBoards.Contains((new StorageBoard(tempBoard)).packedBoard))
                            {
                                //Console.WriteLine($"Found move from {x},{y} to {x},{y+1}");
                                board.AddChild(tempBoard);
                            }
                        }
                    }
                    if ((y-1)>0) {
                        if (board.Value.data[x,y-1]==(uint)Piece.None  & (board.Value.data[x,y] == board.Value.move)) {
                            Board tempBoard = new Board((uint[,])board.Value.data.Clone(), board.Value.move, iterations);
                            tempBoard.data[x,y] = (uint)Piece.None;
                            tempBoard.data[x,y-1] = tempBoard.move;
                            if (tempBoard.move == 0) { tempBoard.move = 1; } else { tempBoard.move = 0; }
                            if (CheckWin(tempBoard, x, y-1)) { tempBoard.won = board.Value.move; }
                            if (!traversedBoards.Contains((new StorageBoard(tempBoard)).packedBoard))
                            {
                                //Console.WriteLine($"Found move from {x},{y} to {x},{y-1}");
                                board.AddChild(tempBoard);
                            }
                        }
                    }
                }
            }
        }
        public bool CheckWin(Board board, int x, int y)
        {
            int count = 0;
            uint piece = board.data[x,y];
            for (int i = 0; i < 4; i++)
            {
                if (board.data[x,i] == piece)
                {
                    count++;
                    if (count == 3) { return true; }
                }
                else
                {
                    count = 0;
                }
            }
            count = 0;
            for (int i = 0; i < 4; i++)
            {
                if (board.data[i, y] == piece)
                {
                    count++;
                    if (count == 3) { return true; }
                }
                else
                {
                    count = 0;
                }
            }
            //im dumb so I will just check every diagonal (there are only 8)
            if ((board.data[0, 0] == piece & board.data[1, 1] == piece & board.data[2, 2] == piece) |
                (board.data[1, 1] == piece & board.data[2, 2] == piece & board.data[3, 3] == piece) |
                (board.data[1, 0] == piece & board.data[2, 1] == piece & board.data[3, 2] == piece) |
                (board.data[0, 1] == piece & board.data[1, 2] == piece & board.data[2, 3] == piece)
                )
            {
                return true;
            }
            //other ones
            if ((board.data[3, 0] == piece & board.data[2, 1] == piece & board.data[1, 2] == piece) |
                (board.data[0, 3] == piece & board.data[1, 2] == piece & board.data[2, 1] == piece) |
                (board.data[2, 0] == piece & board.data[1, 1] == piece & board.data[0, 2] == piece) |
                (board.data[3, 1] == piece & board.data[2, 2] == piece & board.data[1, 3] == piece)
                )
            {
                return true;
            }
            return false;
        }
    }
    public class TreeNode<T>
    {
        private readonly T _value;
        private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

        public TreeNode(T value)
        {
            _value = value;
        }

        public TreeNode<T> this[int i]
        {
            get { return _children[i]; }
        }

        public TreeNode<T>? Parent { get; private set; }

        public T Value { get { return _value; } }

        public ReadOnlyCollection<TreeNode<T>> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value) {Parent = this};
            _children.Add(node);
            return node;
        }

        public TreeNode<T>[] AddChildren(params T[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        public bool RemoveChild(TreeNode<T> node)
        {
            return _children.Remove(node);
        }

        public void Traverse(Action<T> action)
        {
            action(Value);
            foreach (var child in _children)
                child.Traverse(action);
        }

        public IEnumerable<T> Flatten()
        {
            return new[] {Value}.Concat(_children.SelectMany(x => x.Flatten()));
        }
    }
}