﻿using megaTicTacToeSolver;

Console.WriteLine("-- Init --");

var solver = new Solver();
Random rnd = new Random();
int searchDepth = 128;

Board start = new Board(new uint[4,4]{{1,2,2,0},{0,2,2,1},{1,2,2,0},{0,2,2,1}});
/*
Board end = new Board((uint[,])start.data.Clone());
end.data[0,0] = 0;

start.PrintBoard();
end.PrintBoard();
*/
TreeNode<Board> root = new TreeNode<Board>(start);
solver.PossibleMoves(root,0);
TreeNode<Board> CurrentPosition = root;
uint layer = 0;
while (true) { 
    while (CurrentPosition.Children.Count() > 0 & (CurrentPosition.Value.won==3))
    {

        if (layer > 126){ Thread.Sleep(500); }
        bool openChild = false;
        foreach (var child in CurrentPosition.Children)
        {
            if ((child.Value != child.Parent.Value) & (child.Value.traversed == false) & (!solver.traversedBoards.Contains(child.Value.toStorageBoard().packedBoard)))
            {
                solver.traversedBoards.AddLast(CurrentPosition.Value.toStorageBoard().packedBoard);
                CurrentPosition = child;
                layer++;
                openChild = true;
                Console.WriteLine("child found");
                child.Value.MiniPrint();
                break;
            }
        }
        Console.WriteLine(openChild);
        if (!openChild) {CurrentPosition.Value.traversed = true;break;}
        //CurrentPosition = CurrentPosition.Children[rnd.Next(CurrentPosition.Children.Count)];
        Console.WriteLine($"\n--  Move: {layer}, To Move: {((CurrentPosition.Value.move == 0) ? 'X' : 'O')} --");
        CurrentPosition.Value.MiniPrint();
        //Console.WriteLine(CurrentPosition.Value.toStorageBoard().packedBoard);
        //Console.WriteLine(solver.traversedBoards.Contains(CurrentPosition.Value.toStorageBoard().packedBoard));
        if (CurrentPosition.Value.won != 3 | layer > searchDepth) { CurrentPosition.Value.traversed = true; break; }
        solver.PossibleMoves(CurrentPosition,layer);
        foreach (var child in CurrentPosition.Children)
        {
            if (child.Value.won != 3)
            {
                CurrentPosition.Value.won = child.Value.won;
                // Console.WriteLine($"Winning Position for {((CurrentPosition.Value.move == 0) ? 'X' : 'O')}");
            }
        }
    }
    if (CurrentPosition.Parent != null)
    {
        Console.WriteLine("Up layer");
        layer--;
        /*if (CurrentPosition.Parent != null) {
            Console.WriteLine("old parent");
            CurrentPosition.Parent.Value.MiniPrint();
        }*/
        CurrentPosition = CurrentPosition.Parent;
        /*if (CurrentPosition.Parent != null) {
            Console.WriteLine("parent");
            CurrentPosition.Parent.Value.MiniPrint();
        }
        if (CurrentPosition.Children.Count > 0) {
            Console.WriteLine("child0");
            CurrentPosition.Children[0].Value.MiniPrint();
        }*/
    } else
    {
        break;
    }
}
Console.WriteLine("Finished Transversal");