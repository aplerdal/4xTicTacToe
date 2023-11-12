using megaTicTacToeSolver;

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
int layer = 0;
while (true) { 
    while (CurrentPosition.Children.Count() > 0 & (CurrentPosition.Value.won==3))
    {
        bool openChild = false;
        foreach (var child in CurrentPosition.Children)
        {
            if (child.Value.traversed == false)
            {
                solver.traversedBoards.AddLast((new StorageBoard(CurrentPosition.Value)).packedBoard);
                CurrentPosition = child;
                layer++;
                openChild = true;
                break;
            }
        }
        if (!openChild) break;
        //CurrentPosition = CurrentPosition.Children[rnd.Next(CurrentPosition.Children.Count)];
        Console.WriteLine($"\n--  Move: {layer}, To Move: {((CurrentPosition.Value.move == 0) ? 'X' : 'O')} --");
        CurrentPosition.Value.PrintBoard();
        if (CurrentPosition.Value.won != 3 | layer > searchDepth) { CurrentPosition.Value.traversed = true; break; }
        solver.PossibleMoves(CurrentPosition,(uint)layer);
        foreach (var child in CurrentPosition.Children)
        {
            if (child.Value.won != 3)
            {
                CurrentPosition.Value.won = child.Value.won;
                Console.WriteLine($"Winning Position for {((CurrentPosition.Value.move == 0) ? 'X' : 'O')}");
            }
        }
    }
    if (CurrentPosition.Parent != null)
    {
        layer--;
        CurrentPosition = CurrentPosition.Parent;
    } else
    {
        break;
    }
}
Console.WriteLine("Finished Transversal");