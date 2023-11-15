using System.Diagnostics;
using megaTicTacToeSolver;

Console.WriteLine("-- Init --");

var solver = new Solver();
int searchDepth = 7;

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
uint xwins = 0;
uint owins = 0;
uint boardno = 0;
while (true) { 
    while (CurrentPosition.Children.Count() > 0 & (CurrentPosition.Value.won==3))
    {

        //if (layer > 126){ Thread.Sleep(500); }
        bool openChild = false;
        foreach (var child in CurrentPosition.Children)
        {
            if ((child != child.Parent) & (child.Value.traversed == false) & (!solver.traversedBoards.Contains(child.Value.toStorageBoard().packedBoard)))
            {
                CurrentPosition = child;
                solver.traversedBoards.AddLast(CurrentPosition.Value.toStorageBoard().packedBoard);
                layer++;
                openChild = true;
                break;
            }
        }
        if (!openChild) {CurrentPosition.Value.traversed = true;break;}
        //CurrentPosition = CurrentPosition.Children[rnd.Next(CurrentPosition.Children.Count)];
        Console.WriteLine($"\n--  Move: {layer}, To Move: {((CurrentPosition.Value.move == 0) ? 'X' : 'O')} --");
        CurrentPosition.Value.PrintBoard();
        boardno++;
        Console.WriteLine(boardno);
        //Console.WriteLine(CurrentPosition.Value.toStorageBoard().packedBoard);
        //Console.WriteLine(solver.traversedBoards.Contains(CurrentPosition.Value.toStorageBoard().packedBoard));
        if (CurrentPosition.Value.won != 3 | layer >= searchDepth) { CurrentPosition.Value.traversed = true; break; }
        solver.PossibleMoves(CurrentPosition,layer);
        foreach (var child in CurrentPosition.Children)
        {
            if (child.Value.won != 3)
            {
                CurrentPosition.Value.won = child.Value.won;
                if (child.Value.won == 0){ xwins++; } else { owins++; }
                while (CurrentPosition.Parent != null){
                    CurrentPosition.Value.childwins++;
                    CurrentPosition = CurrentPosition.Parent;
                }
                CurrentPosition = child.Parent;
                // Console.WriteLine($"Winning Position for {((CurrentPosition.Value.move == 0) ? 'X' : 'O')}");
            } else {
            }
        }
    }
    if (CurrentPosition.Parent != null)
    {
        CurrentPosition = CurrentPosition.Parent;
        layer--;
    } else
    {
        break;
    }
}
Console.WriteLine("Finished Transversal");
Console.WriteLine(CurrentPosition.Value.childwins);
Console.WriteLine($"X wins {xwins}/{boardno} positions");
Console.WriteLine($"O wins {owins}/{boardno} positions");