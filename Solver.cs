using System;
using System.Collections.ObjectModel;

namespace megaTicTacToeSolver
{
    class Solver{
        static Board start = new Board(new uint[4,4]{{0,1,0,1},{2,2,2,2},{2,2,2,2},{1,0,1,0}});
        TreeNode<Board> root = new TreeNode<Board>(start);
        public void PossibleMoves(TreeNode<Board> board){
            for (int y = 0; y<board.Value.data.GetLength(0); y++){
                for (int x = 0; x<board.Value.data.GetLength(1); x++){
                    if (board.Value.data[x,y] == board.Value.move){
                        if ((x+1)<board.Value.data.GetLength(0) & (board.Value.data[x+1,y]==(uint)Piece.None)) {
                            Board tempBoard = board.Value;
                            tempBoard.data[x,y] = (uint)Piece.None;
                            tempBoard.data[x+1,y] = tempBoard.move;
                            if (tempBoard.move == 0) { tempBoard.move = 1; } else {tempBoard.move = 0;}
                            Console.WriteLine("Found +x Move");
                            tempBoard.PrintBoard();
                            board.AddChild(tempBoard);
                        }
                        if ((x-1)>0 & (board.Value.data[x-1,y]==(uint)Piece.None)) {
                            Board tempBoard = board.Value;
                            tempBoard.data[x,y] = (uint)Piece.None;
                            tempBoard.data[x-1,y] = tempBoard.move;
                            if (tempBoard.move == 0) { tempBoard.move = 1; } else {tempBoard.move = 0;}
                            board.AddChild(tempBoard);
                        }
                        if ((y+1)<board.Value.data.GetLength(1) & (board.Value.data[x,y+1]==(uint)Piece.None)) {
                            Board tempBoard = board.Value;
                            tempBoard.data[x,y] = (uint)Piece.None;
                            tempBoard.data[x,y+1] = tempBoard.move;
                            if (tempBoard.move == 0) { tempBoard.move = 1; } else {tempBoard.move = 0;}
                            board.AddChild(tempBoard);
                        }
                        if ((y-1)>0 & (board.Value.data[x,y-1]==(uint)Piece.None)) {
                            Board tempBoard = board.Value;
                            tempBoard.data[x,y] = (uint)Piece.None;
                            tempBoard.data[x,y+1] = tempBoard.move;
                            if (tempBoard.move == 0) { tempBoard.move = 1; } else {tempBoard.move = 0;}
                            board.AddChild(tempBoard);
                        }
                    }
                }
            }  
        }
        public void CheckWin(){

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

        public TreeNode<T> Parent { get; private set; }

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