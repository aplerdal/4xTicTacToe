using megaTicTacToeSolver;
Console.WriteLine("-- Init --");
var board = new Board();
var storage = new StorageBoard(board);
storage.toBoard().PrintBoard();
