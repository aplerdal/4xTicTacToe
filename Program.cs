using megaTicTacToeSolver;

Console.WriteLine("-- Init --");

var solver = new Solver();
Random rnd = new Random();

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
int iteration = 0;
while (CurrentPosition.Children.Count() > 0 & !(iteration > 30))
{
    CurrentPosition = CurrentPosition.Children[rnd.Next(CurrentPosition.Children.Count)];
    Console.WriteLine($"-- Move: {iteration}, Moved: {((CurrentPosition.Value.move == 0) ? 'Y' : 'X')} --");
    CurrentPosition.Value.PrintBoard();
    solver.PossibleMoves(CurrentPosition,(uint)iteration);
    iteration++;
}
Console.WriteLine("Finished First Path Transversal");
